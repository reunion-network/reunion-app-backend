using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class UIToggleController : MonoBehaviour {

    [Header("Toggle list - visible for debugging")]
    public List<UIToggle> toggleList;

    [Header("Variables related to Instantiated Toggles")]
    public GameObject togglePrefab;
    public GameObject toggleContainer;

    public UIToggle currentSelectedToggle;

    public ToggleGroup toggleGroup;

    [Header("ID for Debug purposes")]
    public string selected_id;

    [Header("Toggle for Self Relationship")]
    public UIToggle youToggle;

    public void switchTogglesSelection(UIToggle selectedToggle)
    {
        if (currentSelectedToggle == null)
        {
            selectedToggle.toggle.isOn = true;
            currentSelectedToggle = selectedToggle;
            return;
        }
        currentSelectedToggle.toggle.isOn = false;
        currentSelectedToggle = selectedToggle;
    }

    public void ClearToggles()
    {
        foreach (var toggle in toggleList)
        {
            if(toggle != youToggle) 
                Destroy(toggle.gameObject);
        }

        toggleList.Clear();
    }

    public void fillRelationshipToggles()
    {
        ClearToggles();
        Debug.Log("Databridge check"+DataBridge.instance.name);

        if(youToggle != null){
            youToggle.toggleController = this;
            youToggle.id = "";
            youToggle.partner_name = "You";
            youToggle.onToggleStringSelected += updateSelectionData;
            
            if(!toggleList.Contains(youToggle))
                toggleList.Add(youToggle);

            youToggle.toggle.isOn = true;
        }
        
        foreach (var data in DataBridge.instance.all_relationships().Reverse())
        {
            UIToggle toggle = Instantiate(togglePrefab, toggleContainer.transform).GetComponent<UIToggle>();
            toggle.id = data.Key;
            toggle.partner_name = data.Value.partner_name;

            toggle.toggleController = this;

            toggle.onToggleStringSelected += updateSelectionData;

            toggleList.Add(toggle);
        }
    }

    public void fillUserToggles()
    {
        Debug.Log("Filling user toggles");
        ClearToggles();

        foreach (var data in DataBridge.instance.all_users().Reverse())
        {
                if(DataBridge.instance.all_relationships().ContainsKey(data.Key) == false){
                    
                UIToggle toggle = Instantiate(togglePrefab, toggleContainer.transform).GetComponent<UIToggle>();
                toggle.id = data.Key;
                toggle.partner_name = data.Value;

                toggle.toggleController = this;

                toggle.onToggleStringSelected += updateSelectionData;

                toggleList.Add(toggle);
            }
        }
    }

    public void updateSelectionData(string id)
    {
        selected_id = id;

        Debug.Log("selected id: " + selected_id);
    }
}

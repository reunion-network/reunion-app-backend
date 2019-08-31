using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIToggle : MonoBehaviour {

    public enum toggleType
    {
        partner,
        relationship,
        importance
    }

    public toggleType type = new toggleType();

    public Toggle toggle;
    public bool isSelected;

    public string id;
    public string partner_name;
    public string relationship_name;

    public int importance;

    public Text relationshipText;
    public Text partnerText;

    public delegate void ToggleStringSelected(string str);
    public event ToggleStringSelected onToggleStringSelected;

    public delegate void ToggleIntSelected(int value);
    public event ToggleIntSelected onToggleIntSelected;
    public UnityEvent onToggleSelected = new UnityEvent();

    public UIPopUpView popupView;
    public UIToggleController toggleController;

    public ColorBlock selectedColor;
    private ColorBlock idleColor;

    private void Awake()
    {
        toggle = GetComponent<Toggle>();
        idleColor = toggle.colors;
        setupListeners();
        // SetNames();
    }

    private void Start()
    {
        // setupListeners();
        SetNames();
    }

    public void init()
    {
        
    }

    private void setupListeners()
    {
        toggle.onValueChanged.RemoveAllListeners();
        toggle.onValueChanged.AddListener(toggleAction);
    }

    private void toggleAction(bool isOn)
    {
        isSelected = isOn;

        if (isOn)
        {
            toggle.colors = selectedColor;

            if (type == toggleType.partner || type == toggleType.relationship)
                emitToggle_partnerID();
            else if (type == toggleType.importance)
                emitToggle_importance();

            if (toggleController != null)
                toggleController.switchTogglesSelection(this);
            else
                Debug.LogError("No togglecontroller at : " + this.gameObject.name);
            Debug.Log("Toggle selected.");

            onToggleSelected.Invoke();
        }
        else
        {
            toggle.colors = idleColor;
        }

        toggle.interactable = !toggle.isOn;
    }

    private void emitToggle_partnerID()
    {
        if (id.Length > 0)
            onToggleStringSelected(id);
    }

    private void emitToggle_importance()
    {
        if (importance != 0)
            onToggleIntSelected(importance);
    }

    public void SetNames()
    {
        if(relationshipText != null)
            relationshipText.text = relationship_name;
        if(partnerText != null)
            partnerText.text = partner_name;
    }

    public void setReadonly(bool isReadonly){
        toggle.interactable = !isReadonly;
    }
}

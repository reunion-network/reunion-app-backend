using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGridCell : MonoBehaviour {

    public Text relationshipTextUI;
    public Text partnerTextUI;
    public Button button;

    public ColorBlobController_UIColorBlobController colorBlobControllerPrefab;

    public Transform blobContainer;

    public string id;
    public string relationship_name;
    public string partner_name;

    public float relationship_time_percentage = 0f;

    public List<ColorBlobController_UIColorBlobController> colorBlobControllers = new List<ColorBlobController_UIColorBlobController>();

    public virtual void UpdateCell()
    {
        relationshipTextUI.text = relationship_name;
        partnerTextUI.text = partner_name;

        if(colorBlobControllers.Count ==0){
            colorBlobControllerPrefab = Instantiate(colorBlobControllerPrefab,blobContainer);
            colorBlobControllers.Add(colorBlobControllerPrefab);
        }

        colorBlobControllerPrefab.Initialize();

        float size = colorBlobControllerPrefab.sizeByPercentage(relationship_time_percentage*100f);

        colorBlobControllerPrefab.sizeLayers[0] = size;
        colorBlobControllerPrefab.colorLayers[0] = DataBridge.instance.aggregateRelationshipColor(this.id);

        colorBlobControllerPrefab.UpdateLayers();
    }

}

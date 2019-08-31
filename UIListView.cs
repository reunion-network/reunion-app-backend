using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class UIListView : MonoBehaviour {

    public UIListElement listElementPrefab;
    
    public GameObject paired_listeElementPrefab;

    public GameObject container;

    public Dictionary<string, UIListElement> listElements;

    private void Awake()
    {
        listElements = new Dictionary<string, UIListElement>();
    }

    public void createIncomingList(){
        Debug.LogWarning(" REFRESHING UI LIST VIEW");
        clearList();

        foreach (var segment in DataBridge.instance.all_incoming_segments().Reverse())
        {
            if(segment.Value.segmentState != (long)SegmentState.paired){
                GameObject o = Instantiate(listElementPrefab.gameObject, container.transform);
                UIListElement element = o.GetComponent<UIListElement>();
                element.id = segment.Value.segment_id;
                element.isIncoming = true;
                element.updateElement(segment.Value.segment_id);

                listElements.Add(element.id, element);
            }
        }
    }

    public void createPairedList(){
        Debug.LogWarning(" REFRESHING UI LIST VIEW");
        clearList();
        
        foreach (var segment in DataBridge.instance.all_user_segments().Reverse())
        {
            if(segment.Value.segmentState == (long)SegmentState.paired){
                GameObject o = Instantiate(paired_listeElementPrefab.gameObject, container.transform);
                UIListElement element = o.GetComponent<UIListElement>();

                element.id = segment.Value.segment_id;
                element.isIncoming = false;
                element.updateElement(segment.Value.segment_id);

                listElements.Add(element.id, element);
            }
        }
    }

    public virtual void createAllList()
    {
        Debug.LogWarning(" REFRESHING UI LIST VIEW");
        clearList();

        // DRAW ALL INCOMING SEGMENTS
        foreach (var segment in DataBridge.instance.all_incoming_segments().Reverse())
        {
            if(segment.Value.segmentState != (long)SegmentState.paired){
                GameObject o = Instantiate(listElementPrefab.gameObject, container.transform);
                UIListElement element = o.GetComponent<UIListElement>();
                element.id = segment.Value.segment_id;
                element.isIncoming = true;
                element.updateElement(segment.Value.segment_id);

                listElements.Add(element.id, element);
            }
        }

        // DRAW ALL USER SEGMENTS
        foreach (var segment in DataBridge.instance.all_user_segments().Reverse())
        {
            if(segment.Value.segmentState != (long)SegmentState.paired){
                GameObject o;

                o = Instantiate(listElementPrefab.gameObject, container.transform);

                UIListElement element = o.GetComponent<UIListElement>();

                element.id = segment.Key;

                element.isIncoming = false;
                element.updateElement(segment.Value.segment_id);

                listElements.Add(element.id, element);
            }else{
                GameObject o = Instantiate(paired_listeElementPrefab.gameObject, container.transform);
                UIListElement element = o.GetComponent<UIListElement>();

                element.id = segment.Value.segment_id;
                element.isIncoming = false;
                element.updateElement(segment.Value.segment_id);

                listElements.Add(element.id, element);
            }
        }
    }

    public void clearList()
    {
        foreach(var elem in listElements)
        {
            Destroy(elem.Value);
            Destroy(elem.Value.gameObject);
        }
        listElements.Clear();
    }
}

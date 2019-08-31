using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIListElement : MonoBehaviour {

    [Header("UI References (set from Unity - prefab)")]
    public Button button;
    public Text titleText;

    public Text pair_titleText;

    public Text partner_nameText;
    public Image dot;

    [Header("ID of element (matching segment_id)")]
    public string id;

    [Header("Reference to PopUpVCs that contains this element (set automatically)")]
    public EditActivity_UIPopUpViewController editActivityPopUp;
    public IncomingActivity_UIPopUpViewController incomingPopUp;

    public PairedActivity_UIPopUpViewController pairedPopUp;
    public UIPopUpViewController parentPopUpView;

    public bool isIncoming;

    public enum ListElementType
    {
        draft = 1,
        proposed = 2,
        accepted = 4,
        paired = 3,
        rejected = 5,
        incoming = 6
    }
    [Header("Element type / state")]
    public ListElementType type = ListElementType.draft;

    [Header("Dot notification Colors (set from Unity)")]
    public Color draftColor, proposedColor, acceptedColor, pairedColor, rejectedColor, incomingColor;

    public virtual void Start () {
        setupListeners();

        DataBridge.instance.onSegmentChanged += updateElement;
    }

    private void OnDestroy()
    {
        Debug.Log("removed " + name);
        if(DataBridge.instance != null)
            DataBridge.instance.onSegmentChanged -= updateElement;
        button.onClick.RemoveAllListeners();
    }

    public void updateElement(string segment_id)
    {
        if(id == segment_id)
        {
            //Debug.Log("Update request on element: " + id + "/ updated segment id: " + segment_id);
            Segment updatedSegment = DataBridge.instance.GetSegment(id);
            long state = 0;

            ///CHECK IF THIS LISTELEMENT IS POINTING TO AN INCOMING SEGMENT, AND SET UISTATE ACCORDINGLY

            if(isIncoming){
                state = 6;
                Debug.Log("Incoming segment listed"+ updatedSegment.hashtag);
            }else{
                state = updatedSegment.segmentState;
            }
            
            string newTitle = updatedSegment.hashtag;

            string partner_name = "";

            if(isIncoming){
                partner_name = DataBridge.instance.all_users()[updatedSegment.user_id];
            }else{
                if(updatedSegment.pair_user_id != ""){
                    Debug.LogError(updatedSegment.pair_user_id);
                    partner_name = DataBridge.instance.all_users()[updatedSegment.pair_user_id];
                }
                    
                else
                    partner_name = "You";
            }

            string pair_segment_title = "";

            if(updatedSegment.segmentState == (long)SegmentState.paired){
                Debug.LogWarning("Searching for: " + updatedSegment.hashtag + " || " + updatedSegment.segment_id);
                foreach (var item in DataBridge.instance.all_paired_segments()){
                    Debug.LogWarning(item.Key.hashtag + " with " + item.Value.hashtag);
                    Debug.LogWarning(item.Key.segment_id + " with " + item.Value.segment_id);
                }

                Segment paired_segment = new Segment();
                
                if (DataBridge.instance.all_paired_segments().tryGetPairedSegmentFromDicByOwnedSegmentID(updatedSegment.segment_id, out paired_segment))
                {
                    pair_segment_title = paired_segment.hashtag;
                }
                else
                {
                    pair_segment_title = "";
                    Debug.LogError("pair_segment w ID " + updatedSegment.pair_segment_id + "NOT FOUND IN THE App.user.paired_segments. DIC");
                }

                //Segment pair_segment = DataBridge.instance.all_paired_segments()[updatedSegment];
                //Debug.LogWarning(pair_segment.hashtag);
                //pair_segment_title = pair_segment.hashtag;
            }

            if(partner_name == "")
                partner_name = "You";

            setState(state);

            if(type != ListElementType.paired)
                setTitle(newTitle,partner_name);
            else
                setTitle(newTitle,partner_name,pair_segment_title);

            setColorNotification();

            setReadNotification();

            editActivityPopUp = FindObjectOfType<EditActivity_UIPopUpViewController>();
            incomingPopUp = FindObjectOfType<IncomingActivity_UIPopUpViewController>();
            pairedPopUp = FindObjectOfType<PairedActivity_UIPopUpViewController>();
        }
    }

    private void setupListeners() {
        button.onClick.AddListener(SelectSegment);
    }

    private void setTitle(string str, string partner_name = "", string pair_segment_title = "")
    {
        if (titleText != null)
            titleText.text = str;
        else
            Debug.LogError("Title text is null on UIListElement "+ name);

        //set partner name
        partner_nameText.text = partner_name;

        if(type == ListElementType.paired){
            pair_titleText.text = pair_segment_title;
        }
    }

    private void setReadNotification()
    {
        if (type == ListElementType.incoming){
            if(DataBridge.instance.GetSegment(this.id).readState == 0){
                titleText.color = Color.black;
                titleText.fontStyle = FontStyle.Bold;
            }
            else{
                titleText.color = Color.gray;
                titleText.fontStyle = FontStyle.Normal;
            }
        }          
        else{
            titleText.color = Color.gray;
            titleText.fontStyle = FontStyle.Normal;
        }
            
    }

    private void setColorNotification()
    {
        if (type == ListElementType.incoming)
            dot.color = incomingColor;
        if (type == ListElementType.draft)
            dot.color = draftColor;
        if (type == ListElementType.proposed)
            dot.color = proposedColor;
        if (type == ListElementType.accepted)
            dot.color = acceptedColor;
        if (type == ListElementType.paired)
            dot.color = pairedColor;
        if (type == ListElementType.rejected)
            dot.color = rejectedColor;
    }

    private void setState(long state)
    {
        if (state == 6)
            type = ListElementType.incoming;
        else if (state == 1)
            type = ListElementType.draft;
        else if (state == 2)
            type = ListElementType.proposed;
        else if (state == 3)
            type = ListElementType.paired;
        else if (state == 4)
            type = ListElementType.accepted;
        else if (state == 5)
            type = ListElementType.rejected;
    }

    public void SelectSegment()
    {
        Debug.Log("Selecting>>>>>>>");
        // if (DataBridge.instance.CanSelectSegment(id))
        // {
            DataBridge.instance.SelectSegment(id);

            if (type == ListElementType.incoming)
                ShowIncomingPopup();

            if (type == ListElementType.draft || type == ListElementType.proposed || type == ListElementType.rejected || type == ListElementType.accepted)
                ShowEditActivityPopup();

            if(type == ListElementType.paired)
                ShowPairPopup();
        // }
    }

    public void ShowEditActivityPopup()
    {
        Debug.Log("Show popup request");

        if (type == ListElementType.draft || type == ListElementType.proposed || type == ListElementType.rejected || type == ListElementType.paired){
            editActivityPopUp.isReadonly = false;
        }else if(type == ListElementType.accepted){
            editActivityPopUp.isReadonly = true;
        }

        if(parentPopUpView != null){
            parentPopUpView.Hide();
            editActivityPopUp.presentingViewController = parentPopUpView;
        }
        editActivityPopUp.setID(this.id);
        editActivityPopUp.Show();
    }

    public void ShowIncomingPopup()
    {
        Debug.Log("Show incmoing request");

        if (type == ListElementType.incoming)
        {
            incomingPopUp.partner_id = DataBridge.instance.all_incoming_segments()[this.id].user_id;
            incomingPopUp.setID(this.id);
        }
        else
            return;

        incomingPopUp.Show();
    }

    public void ShowPairPopup()
    {
        Debug.Log("Show pair popup request");

        if (type == ListElementType.paired)
        {
            pairedPopUp.setID(this.id);
            if(parentPopUpView != null)
                parentPopUpView.Hide();
        }
        else
        {
            Debug.LogError("Paired popup open request, but ListElementType is not 'Paired'");
            return;
        }

        pairedPopUp.Show();
    }
}

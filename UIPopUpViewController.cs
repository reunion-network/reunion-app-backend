using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class UIPopUpViewController : MonoBehaviour {

    [Header("Public Variables - Needs to be set from Hierarchy")]
    #region Public variables
    public UIScreen parentScreen;
    public Button[] btn_triggerPopup;
    public UIPopUpView popupView;

    [Header("Reference to ViewController that presents this ViewController - stacking -- property only visible for debug purposes!")]
    public UIPopUpViewController presentingViewController = null;
    

    [Header("ID of this controller - visible ONLY for debug purposes")]
    [SerializeField]
    public string ID;
    #endregion

    [Header("UI References")]
    public Button submitButton;
    public Button cancelButton;

    //[Header("Public Events")]
    #region Public delegates & events
    public UnityEvent OnBecomesVisible = new UnityEvent();
    public UnityEvent OnBecomesHidden = new UnityEvent();

    public UnityEvent onPopUpViewClose = new UnityEvent();
    #endregion

    #region Private variables
    private CanvasGroup canvasGroup;

    private bool isOpen = false;
    #endregion

    #region Main Methods
    public virtual void Awake()
    {
        if (popupView == null)
        {
            Debug.LogError("There is no popupview attached to this controller: " + this.name);
            return;
        }

        canvasGroup = GetComponent<CanvasGroup>();

        SetupListeners();
        Hide();
    }

    private void OnDestroy()
    {
        cleanUp();
    }

    private void OnDisable()
    {
        cleanUp();
    }

    #endregion

    #region Helper methods

    private void cleanUp()
    {
        if (btn_triggerPopup != null && btn_triggerPopup.Length > 0)
        {
            foreach (var button in btn_triggerPopup)
                button.onClick.RemoveAllListeners();
        }

        OnBecomesVisible.RemoveAllListeners();
        OnBecomesHidden.RemoveAllListeners();

        setID("");
    }

    public string getID()
    {
        return ID;
    }

    public void setID(string id)
    {
        ID = id;
        popupView.ID = this.ID;
    }

    public virtual void SetupListeners()
    {
        if (btn_triggerPopup != null && btn_triggerPopup.Length > 0)
        {
            foreach(Button button in btn_triggerPopup)
            {
                button.onClick.AddListener(Show);
            }
        }
        else
            Debug.LogWarning("There is no trigger button attached for this popup controller: " + gameObject.name);

        OnBecomesHidden.AddListener(popupViewClosed);

        if(cancelButton != null)
            cancelButton.onClick.AddListener(Hide);
    }

    private void popupViewClosed()
    {
        // emit popupview close event only if there is a parent/presenting viewcontroller
        // if(presentingViewController != null)
        // {
        //     onPopUpViewClose.Invoke();
        //     presentingViewController = null;
        // }
    }

    public virtual void Show()
    {
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;

        if (presentingViewController != null)
        {
            presentingViewController.Hide();
        }
        popupView.Show();
        OnBecomesVisible.Invoke();
        Debug.LogWarning("Showing popup" + name);
    }

    public virtual void Hide()
    {
        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;
        OnBecomesHidden.Invoke();

        if (presentingViewController != null)
        {
            Debug.LogWarning(name+ " dismissed, presenting: "+ presentingViewController.name);
            presentingViewController.Show();
            presentingViewController = null;
        }
        popupView.Hide();
        OnBecomesHidden.Invoke();
        Debug.LogWarning("Hiding popup" + name);
    }

    private void Cancel()
    {
        DataBridge.instance.ClearSelection();
        Hide();
    }

    #endregion

}

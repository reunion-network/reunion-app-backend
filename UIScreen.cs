using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UIScreen : MonoBehaviour {

    #region Public variables
    #endregion

    [Header("Public Events")]
    #region Public delegates & events
    public UnityEvent onHide = new UnityEvent();
    public UnityEvent onShow = new UnityEvent();
    public UnityEvent onBecomesHidden = new UnityEvent();
    public UnityEvent onBecomesVisible = new UnityEvent();
    #endregion

    #region Private variables
    private CanvasGroup canvasGroup;
    #endregion

    #region Main Methods
    public virtual void Awake()
    {
       if(GetComponent<CanvasGroup>() != null)
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }
    }

    #endregion

    #region Helper methods
    public virtual void hideScreen()
    {
        gameObject.SetActive(false);

        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;
        canvasGroup.interactable = false;

        onBecomesHidden.Invoke();

        print("Hidden: " + gameObject.name);
    }

    public virtual void showScreen()
    {
        gameObject.SetActive(true);

        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;
        canvasGroup.interactable = true;

        onBecomesVisible.Invoke();

        print("Show: " + gameObject.name);
    }
    #endregion

}

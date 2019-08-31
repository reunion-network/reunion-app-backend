using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class UIPopUpView : MonoBehaviour {

    [Header("ID of this view - visible ONLY for debug purposes")]
    public string ID;
    private Canvas canvas;

    public virtual void Awake()
    {
        if (GetComponent<Canvas>() != null)
            canvas = GetComponent<Canvas>();
        else
            canvas = transform.parent.GetComponent<Canvas>();
    }

    public virtual void Show()
    {
        if (canvas == null)
            canvas = GetComponent<Canvas>();
        canvas.sortingOrder = CONSTANTS_UI.POPUP_SORTING_ORDER;
    }

    public virtual void Hide()
    {
        if (canvas == null)
            canvas = GetComponent<Canvas>();
        canvas.sortingOrder = CONSTANTS_UI.HIDDEN_SORTING_ORDER;
    }
}

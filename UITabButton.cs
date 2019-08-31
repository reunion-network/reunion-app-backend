using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

[RequireComponent(typeof(Button))]
public class UITabButton : MonoBehaviour{

    #region Public variables
    public ColorBlock selectedColors;
    public Text btn_title;
    public Button button;
    #endregion

    [Header("Public Events")]
    #region Public delegates & events
    public UnityEvent onClicked = new UnityEvent();
    #endregion

    #region Private variables
    private ColorBlock idleColors;
    #endregion

    #region Main Methods
    private void Awake()
    {
        button = GetComponent<Button>();
        idleColors = button.colors;
        changeToDeselected();
    }
    
    #endregion

    #region Helper methods

    public void changeToSelected()
    {
        changeColor(selectedColors);
    }

    public void changeToDeselected()
    {
        changeColor(idleColors);
        btn_title.color = idleColors.normalColor;
    }
    
    private void changeColor(ColorBlock targetColorBlock)
    {
        button.colors = targetColorBlock;
        btn_title.color = targetColorBlock.normalColor;
    }

    #endregion

}

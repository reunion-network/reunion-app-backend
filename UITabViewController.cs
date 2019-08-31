using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UITabViewController : MonoBehaviour {

    [Header("UITabButton references (set from Hierarchy)")]
    #region Public variables
    public UITabButton home_btn;
    public UITabButton create_btn;
    public UITabButton invites_btn;
    public UITabButton relationships_btn;

    [Header("UIScreen references (set from Hierarchy)")]
    public UIScreen home_screen;
    public UIScreen create_screen;
    public UIScreen invites_screen;
    public UIScreen relationships_screen;

    #endregion

    #region Private variables
    private UIScreen currentScreen;
    private Canvas canvas;
    #endregion

    #region Main Methods
    private void Start()
    {
        if(home_btn == null || create_btn == null || invites_btn == null || relationships_btn == null)
        {
            Debug.LogError("Some of the TabButtons are not set in TabViewController! return");
            return;
        }

        if (home_screen == null || create_screen == null || invites_screen == null || relationships_screen == null)
        {
            Debug.LogError("Some of the UIScreens are not set in TabViewController! return");
            return;
        }

        canvas = GetComponent<Canvas>();

        canvas.sortingOrder = CONSTANTS_UI.PERMANENT_SORTING_ORDER;

        turnOnAllScreenObjects_ForInit();

        setupButtonListeners();

        hideAllTabScreens();

        // switchToHomeScreen();
    }

    #endregion

    #region Helper methods
    private void switchScreen(UIScreen targetScreen)
    {
        if(currentScreen != null)
            currentScreen.hideScreen();
        targetScreen.showScreen();
        currentScreen = targetScreen;
    }

    private void setupButtonListeners()
    {
        home_btn.button.onClick.AddListener(switchToHomeScreen);
        create_btn.button.onClick.AddListener(switchToCreateScreen);
        invites_btn.button.onClick.AddListener(switchToInvitesScreen);
        relationships_btn.button.onClick.AddListener(switchToRelationshipScreen);
    }

    private void hideAllTabScreens()
    {
        home_screen.hideScreen();
        create_screen.hideScreen();
        invites_screen.hideScreen();
        relationships_screen.hideScreen();
    }

    private void turnOnAllScreenObjects_ForInit()
    {
        home_screen.gameObject.SetActive(true);
        create_screen.gameObject.SetActive(true);
        invites_screen.gameObject.SetActive(true);
        relationships_screen.gameObject.SetActive(true);
    }

    public void switchToHomeScreen()
    {
        home_btn.changeToSelected();
        create_btn.changeToDeselected();
        invites_btn.changeToDeselected();
        relationships_btn.changeToDeselected();

        switchScreen(home_screen);
    }

    public void switchToCreateScreen()
    {
        create_btn.changeToSelected();
        home_btn.changeToDeselected();
        invites_btn.changeToDeselected();
        relationships_btn.changeToDeselected();

        switchScreen(create_screen);
    }

    public void switchToInvitesScreen()
    {
        invites_btn.changeToSelected();
        create_btn.changeToDeselected();
        home_btn.changeToDeselected();
        relationships_btn.changeToDeselected();

        switchScreen(invites_screen);
    }

    public void switchToRelationshipScreen()
    {
        relationships_btn.changeToSelected();
        invites_btn.changeToDeselected();
        create_btn.changeToDeselected();
        home_btn.changeToDeselected();

        switchScreen(relationships_screen);
    }
    #endregion

}

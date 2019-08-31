using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICardView : MonoBehaviour {

    [Header("UI Button fields for Edit, Save, Delete")]
    public Button editButton;
    public Button saveButton;
    public Button deleteButton;

    private void Awake()
    {
        setupListeners();
    }

    private void setupListeners()
    {
        if(editButton != null)
        {
            editButton.onClick.AddListener(Edit);
            Debug.Log("edit button set up");
        }

        if(saveButton != null)
            saveButton.onClick.AddListener(Save);

        if (deleteButton != null)
            deleteButton.onClick.AddListener(Delete);
    }

    public virtual void Edit()
    {
        Debug.Log("UICardView Edit request");
    }

    public virtual void Save()
    {
        Debug.Log("UICardView Save request");
    }

    public virtual void Delete()
    {
        Debug.Log("UICardView Delete request");
    }

}

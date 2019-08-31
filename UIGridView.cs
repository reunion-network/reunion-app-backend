using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UIGridView : MonoBehaviour {

    public List<UIGridCell> cells;

    public UIGridCell cellPrefab;

    public GameObject container;

    public DataBridge dataBridge;

    public UnityEvent OnBecomesVisible = new UnityEvent();
    public UnityEvent onBecomesHidden = new UnityEvent();

    Canvas canvas;

    private void Awake()
    {
        canvas = GetComponent<Canvas>();
        init();
    }

    private void init()
    {
        cells = new List<UIGridCell>();

        OnBecomesVisible.AddListener(UpdateGrid);
    }

    public virtual void Show()
    {
        canvas.sortingOrder = CONSTANTS_UI.ACTIVE_SORTING_ORDER;
        OnBecomesVisible.Invoke();
    }

    public virtual void Hide()
    {
        canvas.sortingOrder = CONSTANTS_UI.HIDDEN_SORTING_ORDER;
        onBecomesHidden.Invoke();
    }

    public void UpdateGrid()
    {
        clearGrid();

        foreach(var item in DataBridge.instance.all_relationships())
        {
            addToGrid(item.Key, item.Value.relationship_name, item.Value.partner_name);
        }
    }

    private void clearGrid()
    {
        foreach(var cell in cells)
        {
            Destroy(cell.gameObject);
        }

        cells.Clear();
    }

    // needs color and radius
    private void addToGrid(string id, string relationship_name, string partner_name)
    {
        GameObject o = Instantiate(cellPrefab.gameObject, container.transform);
        UIGridCell newCell = o.GetComponent<UIGridCell>();

        float relationship_time_percentage = 1f;

        float relationship_time = DataBridge.instance.time_by_relationship(id);
        float allTime =  DataBridge.instance.all_time();

        relationship_time_percentage = relationship_time/allTime;

        newCell.id = id;
        newCell.relationship_name = relationship_name;
        newCell.partner_name = partner_name;
        newCell.relationship_time_percentage = relationship_time_percentage;

        newCell.UpdateCell();

        cells.Add(newCell);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIColorBlobController : MonoBehaviour {

    [Header("ColorBlob Prefab (set in Unity)")]
    public UIColorBlob colorBlobPrefab;

    [Header("Number of layers this controller has")]
    public int numberOfLayers = 1;

    [Header("List for each layer")]
    public List<UIColorBlob> colorBlobLayers = new List<UIColorBlob>();
    public List<Color> colorLayers = new List<Color>();
    public List<float> sizeLayers = new List<float>();

    public int padding = 0;

    public bool isGridBubble = false;

    private float maxSize;
    public void Initialize()
    {
        setMaxSize();
        for (int i = 0; i < numberOfLayers; i++)
        {
            UIColorBlob colorBlob = Instantiate(colorBlobPrefab, transform).GetComponent<UIColorBlob>();

            colorBlobLayers.Add(colorBlob);
            colorLayers.Add(Color.black);
            sizeLayers.Add(1f);
        }
    }

    public virtual void UpdateLayers()
    {
        int index = 0;
        foreach(var colorBlob in colorBlobLayers)
        {
            UpdateColorBlob(colorBlob, colorLayers[index], sizeLayers[index]);
            index++;
        }
    }

    public float sizeByPercentage(float percentage)
    {
        float size = maxSize * (percentage / 100f);

        return size;
    }

    public virtual void UpdateColorBlob(UIColorBlob colorBlob, Color color, float size)
    {
        colorBlob.UpdateVisual(color, size);
    }

    public void setMaxSize()
    {
        Vector2 size = GetComponent<RectTransform>().sizeDelta;

        if(size.x == 0|| size.y == 0)
        {
            size.x = GetComponent<RectTransform>().rect.width;
            size.y = GetComponent<RectTransform>().rect.height;
        }

        maxSize = (size.x < size.y)? size.x : size.y;
        maxSize -= padding;

        if(maxSize < 5){
            maxSize = Screen.width*0.5f;
        }

        Debug.Log("MAX SIZE FOR BLOB IS " + maxSize);
    }
}

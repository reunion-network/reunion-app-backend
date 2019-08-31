using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIColorBlob : MonoBehaviour {

    [Header("Public UI Elements")]
    public Image graphic;

    public void UpdateVisual(Color newColor, float newSize)
    {
        updateColor(newColor);
        updateSize(newSize);
    }

    private void updateColor(Color newColor)
    {
        newColor.a = 255; //making sure new color is not transparent
        graphic.color = newColor;
    }

    private void updateSize(float newRadius)
    {
        Vector2 newSize = Vector2.one * newRadius;

        graphic.rectTransform.sizeDelta = newSize;

        //Debug.Log("Set ColorBlob size to: " + graphic.rectTransform.rect);
    }
}

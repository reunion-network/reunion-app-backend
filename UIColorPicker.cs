//CREATED BY HARDLYBRIEFDAN

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIColorPicker : MonoBehaviour
{
    public Texture2D colorPalette;
    public RectTransform rectTransform;
    public Image colorPreview;

    public Transform picker;

    public Slider slider;

    public Color currentColor;

    private void Start()
    {
        slider.onValueChanged.AddListener(setColorFromSlider);

        setColorFromSlider(0);
    }

    private void setColorFromSlider(float f)
    {
        if (currentColor != SamplePaletteTexture(picker.position))
        {
            currentColor = SamplePaletteTexture(picker.position);

            colorPreview.color = currentColor;
        }
    }

    private void setSliderFromValue(Color c){
        // should we save slider value ?
    }

    private Color32 SamplePaletteTexture(Vector2 pickPosition)
    {
        float textureSize = colorPalette.width;

        var localPoint = Vector2.zero;
        var rect = rectTransform.rect;

        bool convertedPoint = RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, pickPosition, null,out localPoint);
        var px = Mathf.Clamp(0, (((localPoint.x - rect.x) * colorPalette.width) / rect.width), colorPalette.width);
        var py = Mathf.Clamp(0, (((localPoint.y - rect.y) * colorPalette.height) / rect.height), colorPalette.height);
        var color = colorPalette.GetPixel((int)px, (int)py);

        color.a = 255;
        return color;
    }
}
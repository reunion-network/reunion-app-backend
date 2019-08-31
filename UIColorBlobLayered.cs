using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIColorBlobLayered : MonoBehaviour {

    public List<UIColorBlob> colorBlobLayers;

    public List<Color> colorLayers;

    public List<float> sizeLayers;

    public virtual void Initialize()
    {
        if (colorBlobLayers == null) colorBlobLayers = new List<UIColorBlob>();
        if (colorLayers == null) colorLayers = new List<Color>();
        if (sizeLayers == null) sizeLayers = new List<float>();
    }

    public virtual void UpdateLayers()
    {
        int index = 0;
        foreach(var blob in colorBlobLayers)
        {
            Color c = colorLayers[index];
            float r = sizeLayers[index];
            blob.UpdateVisual(c,r);
        }
    }
}

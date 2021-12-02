using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerManager : MonoBehaviour
{

    public void SetLayer(int layer)
    {
        SetLayerRecursive(this.gameObject, layer);
    }

    private void SetLayerRecursive(GameObject obj, int new_layer)
    {
        if (obj == null)
            return;

        obj.layer = new_layer;

        foreach(Transform child in obj.transform)
        {
            SetLayerRecursive(child.gameObject, new_layer);
        }
    }
}

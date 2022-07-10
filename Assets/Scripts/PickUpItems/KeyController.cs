using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyController : BaseItemController
{
    public Color keyColor = Color.white;

    void Start()
    {
        int children = transform.childCount;
        for (int i = 0; i < children - 1; ++i)
        {
            if (transform.GetChild(i).TryGetComponent(out Renderer matRender))
            {
                matRender.material.color = keyColor;
            }
        }
    }
}

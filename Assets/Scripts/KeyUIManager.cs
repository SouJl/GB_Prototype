using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyUIManager : MonoBehaviour
{

    public static KeyUIManager instance;

    public int KeyCount = 0;
    public Image[] keys;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (keys.Length == 0) return;

        if (KeyCount < 0) KeyCount = 0;

        for (int i =0; i < keys.Length; i++) 
        {
            if (i < KeyCount)
            {
                keys[i].enabled = true;
            }
            else
                keys[i].enabled = false;
        }
    }

    public void AddKey(Color color) 
    {
        keys[KeyCount].color = color;
        KeyCount++;
    }

    public void RemoveKey() => KeyCount--;
}

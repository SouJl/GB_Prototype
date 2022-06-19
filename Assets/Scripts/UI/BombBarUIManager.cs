using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BombBarUIManager : MonoBehaviour
{
    public static BombBarUIManager instance;

    [Header("Set in Inspector")]
    public Image[] bombsImage;

    [Header("Set Dynamically")]
    public int bombCount;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Update()
    {
        if (bombsImage == null) return;

        if (bombCount < 0) bombCount = 0;

        if (bombCount > bombsImage.Length)
        {
            bombCount = bombsImage.Length;
        }

        for (int i = 0; i < bombsImage.Length; i++)
        {
            if (i < bombCount)
            {
                bombsImage[i].enabled = true;
            }
            else
            {
                bombsImage[i].enabled = false;
            }
        }
    }

    public void AddBomb() => bombCount++;

    public void RemoveBomb() => bombCount--;
}

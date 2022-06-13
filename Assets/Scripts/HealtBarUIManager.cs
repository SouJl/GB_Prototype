using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealtBarUIManager : MonoBehaviour
{
    public static HealtBarUIManager instance;

    [Header("Set in Inspector")]
    public int healthCount;
    public int healthLevel;
    public Sprite fullHelthSprite;
    public Sprite emptyHealthSprite;
    public Image[] healthsImage;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }


    void Update()
    {
        if (healthsImage == null) return;

        if (healthCount > healthLevel) 
        {
            healthCount = healthLevel;
        }

        for (int i = 0; i < healthsImage.Length; i++)
        {
            if(i > healthCount - 1) 
            {
                healthsImage[i].sprite = emptyHealthSprite;
            }
            else 
            {
                healthsImage[i].sprite = fullHelthSprite;
            }

            if (i < healthLevel) 
            {
                healthsImage[i].enabled = true;
            }
            else 
            {
                healthsImage[i].enabled = false;
            }
        }
    }

    public void AddHealth() 
    {
        healthLevel++;
        healthCount++;
    }
}

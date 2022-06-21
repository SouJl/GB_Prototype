using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUIManager : MonoBehaviour
{
    public static HealthBarUIManager instance;

    [Header("Set in Inspector")]
    public int healthCount;

    public Color heathFullCollor;
    public Color heathEmptyCollor;
    public Sprite helthFullSprite;
    public Sprite healthEmptySprite;
    public Image[] healthsImage;

    private int maxHealth;
    private int healthLevel;
    
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        healthLevel = healthCount;
        maxHealth = healthsImage.Length;
    }

    void Update()
    {
        if (healthsImage == null) return;

        if (healthCount > healthLevel) 
        {
            healthLevel = healthCount;
        }

        if (healthLevel > maxHealth) healthLevel = maxHealth;

        for (int i = 0; i < healthsImage.Length; i++)
        {
            if(i > healthCount - 1) 
            {
                healthsImage[i].sprite = healthEmptySprite;
                healthsImage[i].color = heathEmptyCollor;
            }
            else 
            {
                healthsImage[i].sprite = helthFullSprite;
                healthsImage[i].color = heathFullCollor;
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
       // healthLevel++;
        healthCount++;
    }

    public void MinusHealth() => healthCount--;
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BaseEnemy : MonoBehaviour
{
    [Header("Set in Inspector: BaseEnemy")]
    public float health = 10f;
    public float speed = 5f;
    public int score = 10;
    public float showDamageDuration = 0.1f;

    [Header("Set Dynamically: BaseEnemy")]
    public Color[] originalColors;
    public Material[] materials;
    public bool showingDamage;
    public float damageDoneTime;

    [NonSerialized]
    public NavMeshAgent enemyAI;

    private void Awake()
    {
        materials = Utils.GetAllMaterials(gameObject);
        originalColors = new Color[materials.Length];
        for (int i = 0; i < materials.Length; i++)
        {
            originalColors[i] = materials[i].color;
        }
        
        enemyAI = GetComponent<NavMeshAgent>();
        enemyAI.speed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        MoveUpdate();

        if (showingDamage && Time.time > damageDoneTime)
        {
            UnShownDamage();
        }
    }

    private void FixedUpdate()
    {
        MoveFixedUpdate();
    }

    public virtual void MoveUpdate(){}

    public virtual void MoveFixedUpdate() { }

    public virtual void OnHitBomb(GameObject collideGo) 
    {
        if (collideGo.tag == "Bomb")
        {
            Main.instance.EnemyDefeat(this);
            Destroy(gameObject);
            Destroy(collideGo);
        }
    }

    public void OnHit(GameObject collideGo)
    {
        if (collideGo.tag == "Bullet")
        {
            ShowDamage();
            var p = collideGo.GetComponent<Bullet>();
            health -= p.damage;
            if (health <= 0)
            {
                Main.instance.EnemyDefeat(this);
                Destroy(gameObject);
            }
            Destroy(collideGo);
        }
    }


    void ShowDamage()
    {
        foreach (var m in materials)
        {
            m.color = Color.white;
        }
        showingDamage = true;
        damageDoneTime = Time.time + showDamageDuration;
    }

    void UnShownDamage()
    {
        for (int i = 0; i < materials.Length; i++)
        {
            materials[i].color = originalColors[i];
        }
        showingDamage = false;
    }
}
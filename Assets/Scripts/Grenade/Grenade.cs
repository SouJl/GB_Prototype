using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    [Header("Set in Inspector")]
    public float detonateTime = 2f;
    public float radius = 5;

    [Header("CircleLine Params")]
    public Color lineColor;
    public int numSegments = 128;
    public LineRenderer circleRender;

    float distToGround = 0f;
    bool isStay = false;
    int boundCount;

    void Start()
    {
        Invoke("Detonate", detonateTime);
        distToGround = gameObject.GetComponent<Collider>().bounds.extents.y;
        boundCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (isStay) return;

        if (IsGround()) 
        {
            Debug.Log(boundCount);
            boundCount++;
        }
        
        if(boundCount > 5) 
        {
            gameObject.GetComponent<Rigidbody>().freezeRotation = true;
            transform.rotation = Quaternion.identity;
            isStay = true;
        }
    }

    void Detonate()
    {
        StartCoroutine(Explode());
    }

    IEnumerator Explode() 
    {
        float minrad = 0.2f;
        while(minrad < radius) 
        {
            DoRenderer(minrad);
            minrad += 0.05f;
            yield return new WaitForSeconds(0.05f);
        }

        var colliders = Physics.OverlapSphere(transform.position, radius);
        foreach (var hit in colliders)
        {
            if (hit.gameObject.CompareTag("Player"))
            {
                var p = hit.gameObject.GetComponent<PlayerController>();
                if (!p.IsForceAdded)
                {
                    p.AddImpact(transform.forward, 30);
                    if (HealthBarUIManager.instance.healthCount > 0)
                    {
                        HealthBarUIManager.instance.MinusHealth();
                    }
                    else
                    {
                        Destroy(hit.gameObject);
                        Main.instance.GameOver(false);
                    }
                }
            }
        }
        Destroy(gameObject);
    }

    private bool IsGround() 
    {
        return Physics.Raycast(transform.position, -Vector3.up, (float)(distToGround + 0.1));
    }

    public void DoRenderer(float radius)
    {
        circleRender.material = new Material(Shader.Find("Sprites/Default"));
        circleRender.startColor = lineColor; circleRender.endColor = lineColor;
        circleRender.startWidth = 0.1f; circleRender.endWidth = 0.1f;
        circleRender.positionCount = numSegments + 1;
        circleRender.useWorldSpace = false;

        float deltaTheta = (float)(2.0 * Mathf.PI) / numSegments;
        float theta = 0f;

        for (int i = 0; i < numSegments + 1; i++)
        {
            float x = radius * Mathf.Cos(theta);
            float z = radius * Mathf.Sin(theta);
            Vector3 pos = new Vector3(x, 0, z);
            circleRender.SetPosition(i, pos);
            theta += deltaTheta;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastPointOfView : MonoBehaviour
{
    public Color color;

    [SerializeField] private LayerMask _mask;
    [SerializeField] private Transform _player;

    private Vector3 playerPos;

    public bool isTargetFind;

    void Start() 
    {
        _player = FindObjectOfType<PlayerController>().transform;
    }

    private void FixedUpdate()
    {
        RaycastHit hit;
        Color drawColor = color;
        var startPos = transform.position;
        var rayCast = Physics.Raycast(startPos, transform.forward, out hit, 1000, _mask);
        if (rayCast)
        {
            if (hit.collider.gameObject.CompareTag("Player"))
            {
                drawColor = Color.green;
                playerPos = hit.point;
                isTargetFind = true;
            }
            else 
            {
                isTargetFind = false;
            }
        }
        else 
        {
            drawColor = color;
        }
        Debug.DrawRay(startPos, transform.forward * 10, drawColor);
    }

    public Vector3 GetTargetPosition() => playerPos;

}

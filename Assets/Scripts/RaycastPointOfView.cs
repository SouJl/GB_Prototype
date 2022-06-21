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
        isTargetFind = false;
        if (Physics.Raycast(startPos, transform.forward, out hit, Mathf.Infinity, _mask))
        {
            if (hit.collider.gameObject.CompareTag("Player"))
            {
                drawColor = Color.green;
                playerPos = hit.point;
                isTargetFind = true;
            }
        }
        Debug.DrawRay(startPos, transform.forward * 20, drawColor);
    }

    public Vector3 GetTargetPosition() => playerPos;

}

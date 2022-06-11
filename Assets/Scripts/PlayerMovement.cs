using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float turnSpeed = 20f;
    public float speed = 1f;

    Vector3 _direction;
    Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        _direction.x = Input.GetAxis("Horizontal");
        _direction.z = Input.GetAxis("Vertical");

        if(EnemySpawner.IsPlayerIn)
            GameManager.instance.PlayerPostion = _rigidbody;
    }

    void FixedUpdate()
    {
        _direction.Normalize();

        if(_direction.magnitude >= 0.1f) 
        {
            Vector3 desiredForward = Vector3.RotateTowards(transform.forward, _direction, turnSpeed * Time.deltaTime, 0f);
            _rigidbody.rotation = Quaternion.LookRotation(desiredForward);

            _rigidbody.position += _direction * speed * Time.deltaTime;
        }      
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Set in Inspector")]
    public float turnSpeed = 20f;
    public float speed = 1f;

    [Header("Set Dynamically")]
    public List<GameObject> keys;

    Vector3 _direction;
    Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        keys = new List<GameObject>();
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

    public void PickUpItem(GameObject item) 
    {
        if(item.tag == "Key") 
        {
            print("Player pickup Key");
            keys.Add(item);
            KeyUIManager.instance.AddKey(item.transform.GetChild(0).GetComponent<Renderer>().material.color);
        }
        if(item.tag == "Health") 
        {
            print("Player pickup Health");
            HealtBarUIManager.instance.AddHealth();
        }
    }
}
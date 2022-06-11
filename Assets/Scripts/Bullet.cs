using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    public float lifeTime = 3f;

    Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        Destroy(gameObject, lifeTime);
    }

    void Start()
    {
        _rigidbody.velocity = transform.up * speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Enemy") 
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }

}

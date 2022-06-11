using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretMissle : MonoBehaviour
{
    public float speed = 10f;
    public float lifeTime = 1f;

    Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        Destroy(gameObject, lifeTime);
    }

    void Start()
    {
        _rigidbody.velocity = transform.forward * speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Destroy(gameObject);
            GameManager.instance.Restart();
        }
    }
}

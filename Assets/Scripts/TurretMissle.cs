using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretMissle : MonoBehaviour
{
    public float speed = 10f;

    Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
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
            Main.instance.Restart();
        }
        if (other.gameObject.tag == "Level") Destroy(gameObject);
    }
}

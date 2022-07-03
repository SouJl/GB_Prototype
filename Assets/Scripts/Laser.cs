using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public float speed = 5f;

    Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        Destroy(gameObject, 1f);
    }

    void Start()
    {
        //_rigidbody.velocity = transform.forward * speed;
    }

    private void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            var p = collision.gameObject.GetComponent<PlayerController>();
            if (!p.IsForceAdded)
            {
                if (HealthBarUIManager.instance.healthCount > 0)
                {
                    HealthBarUIManager.instance.MinusHealth();
                }
                else
                {
                    Destroy(collision.gameObject);
                    Main.instance.GameOver(false);
                }
            }
            Destroy(gameObject);
        }
    }
}

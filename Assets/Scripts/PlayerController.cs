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
    public float offset;

    CharacterController _controller;
    Vector3 _direction;
    float gravity;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
        keys = new List<GameObject>();
    }

    private void Update()
    {
        _direction.x = Input.GetAxisRaw("Horizontal");
        _direction.z = Input.GetAxisRaw("Vertical");
        _direction.Normalize();

        gravity -= 9.81f * Time.deltaTime;
        _controller.Move(new Vector3(_direction.x * speed * Time.deltaTime, gravity, _direction.z * speed * Time.deltaTime));
        if (_controller.isGrounded) gravity = 0;
    }

    void FixedUpdate()
    {

       /* if (_direction.magnitude >= 0.1f) 
        {
            Vector3 desiredRotation = Vector3.RotateTowards(transform.forward, _direction, turnSpeed * Time.deltaTime, 0f);
            transform.rotation = Quaternion.LookRotation(desiredRotation);     
        } */

        Vector2 difference = Camera.main.WorldToScreenPoint(transform.position) - Input.mousePosition;
        difference.Normalize();
        float rotation = Mathf.Atan2(difference.x, difference.y) * Mathf.Rad2Deg - 180f;
        transform.rotation = Quaternion.Euler(0f, rotation, 0f);
    }

    public void PickUpItem(GameObject item) 
    {
        if (Input.GetKeyDown(KeyCode.F)) 
        {
            if (item.tag == "Key")
            {
                keys.Add(item);
                KeyUIManager.instance.AddKey(item.transform.GetChild(0).GetComponent<Renderer>().material.color);
            }
            if (item.tag == "Health")
            {
                HealthBarUIManager.instance.AddHealth();
            }
            Destroy(item);
        }      
    }
}
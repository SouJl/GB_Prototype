using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyController : MonoBehaviour
{
    public Color keyColor = Color.white;
    public GameObject rotateCenter;

    void Start()
    {
        int children = transform.childCount;
        for (int i = 0; i < children - 1; ++i)
        {
            transform.GetChild(i).GetComponent<Renderer>().material.color = keyColor;
        }
    }

    private void Update()
    {
        transform.RotateAround(rotateCenter.transform.position, Vector3.up, 30 * Time.deltaTime);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerController player = (PlayerController)other.GetComponent(typeof(PlayerController));
            player.PickUpItem(gameObject);
        }
    }

}

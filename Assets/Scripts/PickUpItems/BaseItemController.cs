using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType 
{
    none,
    health,
    powerUp,
    key
}

public class BaseItemController : MonoBehaviour
{
    [Header("Set in Inspector: BasePickUpItem")]
    public GameObject rotateCenter;
    public ItemType itemType;

    private void FixedUpdate()
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

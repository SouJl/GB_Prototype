using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    public GameObject rotateCenter;

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

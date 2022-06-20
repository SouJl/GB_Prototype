using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCollision : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bullet" || other.gameObject.tag == "Missle") Destroy(other.gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEnd : MonoBehaviour
{

    private void OnTriggerStay(Collider other)
    {
        if (!Main.instance.IsBossDefeat) return;
        
        if(other.gameObject.tag == "Player") 
        {
            Main.instance.GameOver(true);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Button : MonoBehaviour
{
    public int NumberOfButton = 1;
    public GameObject buttonSwitch;

    private bool _isOn;

    public bool IsOn
    {
        get => _isOn;
        set
        {
            _isOn = value;
        }
    }

    private void Update()
    {

    }

    public void ResetButton()
    {
        buttonSwitch.GetComponent<Renderer>().material.color = Color.white;
        IsOn = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Bullet") 
        {
            if (!IsOn) 
            {
                buttonSwitch.GetComponent<Renderer>().material.color = Color.green;
                IsOn = true;
                RiddleController.instance.buttons.Enqueue(gameObject);
            }
            Destroy(other.gameObject);
        }
    }
}

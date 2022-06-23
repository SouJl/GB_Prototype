using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum DoorState
{
    Default,
    OnEnemy,
    OnKey
}

public class DoorController : MonoBehaviour
{
    public DoorState state;
    public GameObject AnimatorHandler;

    [NonSerialized]
    public Animator doorAnimator;
    new Renderer renderer;

    void Start()
    {
        doorAnimator = AnimatorHandler.GetComponent<Animator>();
        renderer = AnimatorHandler.GetComponent<Renderer>();
        switch (state)
        {
            case DoorState.OnEnemy:
                {
                    renderer.material.color = Color.red;
                    break;
                }
            case DoorState.OnKey:
                {
                    renderer.material.color = Color.cyan;
                    break;
                }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateState(DoorState newState)
    {
        state = newState;
        switch (state)
        {
            case DoorState.Default:
                {
                    renderer.material.color = Color.white;
                    break;
                }
            case DoorState.OnEnemy:
                {
                    renderer.material.color = Color.red;
                    doorAnimator.SetBool("IsOpening", false);
                    break;
                }
            case DoorState.OnKey:
                {
                    renderer.material.color = Color.cyan;
                    break;
                }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player") 
        {
            switch (state) 
            {
                case DoorState.Default: 
                    {
                        doorAnimator.SetBool("IsOpening", true);
                        break;
                    }
                case DoorState.OnKey: 
                    {
                        PlayerController player = (PlayerController)other.GetComponent(typeof(PlayerController));
                        if (player.keys.Any()) 
                        {
                            UpdateState(DoorState.Default);
                            player.keys.Clear();
                            doorAnimator.SetBool("IsOpening", true);
                            KeyUIManager.instance.RemoveKey();
                          
                        }
                        else
                        {
                            Main.instance.ThrowNotification("�������. ����� ����!");
                        }
                            
                        break;
                    }
            }
        }          
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            switch (state)
            {
                case DoorState.Default:
                    {
                        doorAnimator.SetBool("IsOpening", false);
                        break;
                    }
                case DoorState.OnKey: 
                    {
                        Main.instance.ThrowNotification("");
                        break;
                    }
            }           
        }
    }
}

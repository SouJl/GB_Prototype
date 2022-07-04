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
    public Color OnKeyColor;

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
                    renderer.material.color = OnKeyColor;
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
                    SoundManager.instance.Play("Door");
                    doorAnimator.SetBool("IsOpening", false);
                    break;
                }
            case DoorState.OnKey:
                {
                    renderer.material.color = OnKeyColor;
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
                        SoundManager.instance.Play("Door");
                        doorAnimator.SetBool("IsOpening", true);
                        break;
                    }
                case DoorState.OnKey: 
                    {
                        PlayerController player = (PlayerController)other.GetComponent(typeof(PlayerController));
                        if (player.keys.Any()) 
                        {
                            UpdateState(DoorState.Default);
                            player.keys.RemoveAt(player.keys.Count - 1);
                            SoundManager.instance.Play("Door");
                            doorAnimator.SetBool("IsOpening", true);
                            KeyUIManager.instance.RemoveKey();
                          
                        }
                        else
                        {
                            Main.instance.ThrowNotification("Закрыто. Нужен ключ!");
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
                        SoundManager.instance.Play("Door");
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

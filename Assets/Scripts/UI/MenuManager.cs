using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void OnPlay() 
    {
        SoundManager.instance.Play("ButtonClick");
        SceneManager.LoadScene("Level1");
    }

    public void OnExit() 
    {
        SoundManager.instance.Play("ButtonClick");
        Application.Quit();
    }
}

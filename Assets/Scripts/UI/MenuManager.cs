using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void OnPlay() 
    {
        SoundManager.instance.Play("ButtonClick");
        LevelLoader.Instance.LoadScene("Level1", true);
    }

    public void OnOptions()
    {
        SoundManager.instance.Play("ButtonClick");
    }

    public void OnExit() 
    {
        SoundManager.instance.Play("ButtonClick");
        Application.Quit();
    }
}

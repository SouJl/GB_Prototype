using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    [SerializeField]
    private RectTransform _pauseMenu;

    [SerializeField]
    private GameObject _cursor;

    private bool _isEnable;

    private float prevThemeVolume;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!_pauseMenu.gameObject.activeSelf)
            {
                SetPause(true);
            }
        }
    }

    private void SetPause(bool exitState)
    {
        _isEnable = !_isEnable;

     
        if (_isEnable)
        {
            float prvlm = SoundManager.instance.GetVolume("Theme");
            if (prvlm != -1) prevThemeVolume = prvlm;

            SoundManager.instance.SetVolume("Theme", 0.1f);
            Time.timeScale = 0f;
            _pauseMenu.gameObject.SetActive(true);
            if (exitState) 
            {
                Cursor.visible = true;
                _cursor.gameObject.SetActive(false);
            }
        }
        else
        {
            SoundManager.instance.SetVolume("Theme", prevThemeVolume);
            Time.timeScale = 1f;
            _pauseMenu.gameObject.SetActive(false);
            if (exitState) 
            {
                Cursor.visible = false;
                _cursor.gameObject.SetActive(true);
            }
        }
    }

    public void OnResume()
    {
        SetPause(true);
        SoundManager.instance.Play("ButtonClick");
    }

    public void OnRestart() 
    {
        SoundManager.instance.Play("ButtonClick");
        SoundManager.instance.Stop("Theme");
        SetPause(false);
        LevelLoader.Instance.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnMainMenu()
    {
        SoundManager.instance.Play("ButtonClick");
        SoundManager.instance.Stop("Theme");
        SetPause(false);
        LevelLoader.Instance.LoadScene("Menu");
    }

    public void OnGameQuit()
    {
        SoundManager.instance.Play("ButtonClick");
        Application.Quit();
    }
}

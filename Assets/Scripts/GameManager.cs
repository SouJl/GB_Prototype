using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Rigidbody PlayerPostion;

    void Awake()
    {
        if(instance == null) 
        {
            instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameOver(bool isWon)
    {
        if (isWon)
        {
            NotificationUIManager.instance.SetNotification("Поздравляем! Вы дошли до конца уровня");
            Invoke("Exit", 3);
        }
        else
        {
            NotificationUIManager.instance.SetNotification("Вы проиграли!");
            Invoke("Restart", 3);
        }
    }

    public void Restart() 
    {
        SceneManager.LoadScene(0);
    }


    private void Exit()
    {
        Application.Quit();
    }

}

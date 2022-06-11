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

    public void Restart() 
    {
        SceneManager.LoadScene(0);
    }
}

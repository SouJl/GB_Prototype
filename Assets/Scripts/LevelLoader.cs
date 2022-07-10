using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{
    public static LevelLoader Instance;
    
    public Animator transition;

    public GameObject gameIntstruction; 

    public float transitionTime = 2f;

    private bool showInstruction;

    public void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        if(gameIntstruction != null) gameIntstruction.SetActive(false);
    }

    public void LoadScene(string sceneName, bool showinstuction = false)
    {
        showInstruction = showinstuction & gameIntstruction != null;
        StartCoroutine(LoadSceneCoroutine(sceneName));
    }

    IEnumerator LoadSceneCoroutine(string sceneName)
    {
        transition.SetTrigger("Start");  
        if (showInstruction)
        {
            yield return new WaitForSeconds(transitionTime);
            gameIntstruction.SetActive(true);
            while (!Input.GetKeyDown(KeyCode.Space)) 
            {
                yield return null;
            }
            gameIntstruction.GetComponent<Animator>().SetTrigger("End");
            yield return new WaitForSeconds(1);
            gameIntstruction.SetActive(false);
        }
        else
            yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(sceneName);
    }
}

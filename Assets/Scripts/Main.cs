using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;
using System;
using System.Collections.Generic;
using UnityEngine.Audio;

[System.Serializable]
public class SceneOrder
{
    public string CurrentScene;
    public string NextScene;
}

public class Main : MonoBehaviour
{
    public static Main instance;

    [Header("Set in Inspector")]
    public GameObject playerPrefab;
    public Transform playerStartPosition;
    public TextMeshProUGUI score;
    public TextMeshProUGUI notification;
    public TextMeshProUGUI bossName;
    public SliderHealthBarUI enemyHealthBar;

    [Header("Set SceneOrder")]
    public SceneOrder sceneOrder;

    [NonSerialized]
    public Transform playerPosition;


    private int _score = 0;
    private int _enemyCount = 0;
    
    private string _notificationMessage;

    public int EnemyCount
    {
        get => _enemyCount;
        set
        {
            if (value < 0) _enemyCount = 0;
            else _enemyCount = value;
        }
    }

    private bool _isBossDefeat = false;

    public bool IsBossDefeat
    {
        get => _isBossDefeat;
        set
        {
            if (value)
                bossName.text = "";

            _isBossDefeat = value;
        }
    }


    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        if (playerPrefab != null && playerStartPosition != null)
        {
            var go = Instantiate(playerPrefab, playerStartPosition.position, Quaternion.identity);
            playerPosition = go.transform;
        }

        ResetAudiLisner();

        //Cursor.visible = false;
    }

    private void Start()
    {
        bossName.text = "";

        _notificationMessage = "";
        _score = 0;

        if (PlayerPrefs.HasKey("Score"))
        {
            _score = PlayerPrefs.GetInt("Score");
        }
        PlayerPrefs.SetInt("Score", _score);

        UpdateGUI();

        SoundManager.instance.Play("Theme");
    }


    void Update()
    {

    }

    private void ResetAudiLisner()
    {
        bool isActive = gameObject.GetComponent<AudioListener>().enabled;
        gameObject.GetComponent<AudioListener>().enabled = !isActive;
    }

    public void InitSliderHealthBar(int health)
    {
        enemyHealthBar.gameObject.SetActive(true);
        enemyHealthBar.SetMaxHealth(health);
    }

    public void SetValueInHealthBar(int health)
    {
        if (enemyHealthBar.gameObject.activeSelf)
        {
            enemyHealthBar.SetHealth(health);
            if (health <= 0) enemyHealthBar.gameObject.SetActive(false);
        }
    }

    public void EnemyDefeat(BaseEnemy e)
    {
        _score += e.score;
        if (e.gameObject.tag == "Boss") IsBossDefeat = true;
        EnemyCount--;
        UpdateGUI();
    }

    public void ThrowNotification(string message, bool isDisable = false)
    {
        _notificationMessage = message;
        UpdateGUI();
        Invoke("DisabeNotification", 1.5f);
    }

    public void GameOver(bool isWon)
    {
        if (isWon)
        {
            PlayerPrefs.SetInt("Score", _score);
            _notificationMessage = "Поздравляю! Уровень пройден!";
            Invoke("NextLevel", 3);
        }
        else
        {
            ResetAudiLisner();
            _notificationMessage = "Вы проиграли!";
            Invoke("Restart", 3);
        }
        UpdateGUI();
    }

    public void Restart()
    {
        LevelLoader.Instance.LoadScene(sceneOrder.CurrentScene);
    }

    private void UpdateGUI()
    {
        notification.text = _notificationMessage;
        score.text = $"Score: {_score}";
    }

    private void NextLevel()
    {
        LevelLoader.Instance.LoadScene(sceneOrder.NextScene);
    }

    private void DisabeNotification() 
    {
        _notificationMessage = "";
        notification.text = _notificationMessage;
    }
}

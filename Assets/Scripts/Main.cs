using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;
using System;
using System.Collections.Generic;

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
    private string _notificationMessage;
    private float _musicVolume = 0.5f;
    private float _sfxVolume = 0.8f;

    private int _enemyCount = 0;

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

        //Cursor.visible = false;
    }

    private void Start()
    {
        bossName.text = "";

        _notificationMessage = "";
        _score = 0;
        UpdateGUI();
        SetGameSettings();

        SoundManager.instance.Play("Theme");
    }


    void Update()
    {

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

    public void ThrowNotification(string message)
    {
        _notificationMessage = message;
        UpdateGUI();
    }

    public void GameOver(bool isWon)
    {
        if (isWon)
        {
            _notificationMessage = "Поздравляю! Уровень пройден!";
            Invoke("NextLevel", 3);
        }
        else
        {
            _notificationMessage = "Вы проиграли!";
            Invoke("Restart", 3);
        }
        UpdateGUI();
    }

    public void Restart()
    {
        SceneManager.LoadScene(sceneOrder.CurrentScene);
    }

    private void UpdateGUI()
    {
        notification.text = _notificationMessage;
        score.text = $"Score: {_score}";
    }

    private void NextLevel()
    {
        SceneManager.LoadScene(sceneOrder.NextScene);
    }

    private void SetGameSettings()
    {
        if (PlayerPrefs.HasKey("MusicVolume"))
        {
            _musicVolume = PlayerPrefs.GetFloat("MusicVolume");
        }

        if (PlayerPrefs.HasKey("SFXVolume"))
        {
            _sfxVolume = PlayerPrefs.GetFloat("SFXVolume");
        }

        SoundManager.instance.SetVolume("Theme", _musicVolume);

        SoundManager.instance.SetVolume("Bullet", _sfxVolume);
        SoundManager.instance.SetVolume("Door", _sfxVolume);
        SoundManager.instance.SetVolume("EnemyHit", _sfxVolume * 0.8f);
        SoundManager.instance.SetVolume("PlayerHit", _sfxVolume);
    }

}

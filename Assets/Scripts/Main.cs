using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;
using System;

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

    [NonSerialized]
    public Transform playerPosition;
    //[Header("Set Dynamicaly")]

    private int _score = 0;
    private string _notificationMessage;
   

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

        SoundManager.instance.Play("Theme");
    }

    // Update is called once per frame
    void Update()
    {

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
            Invoke("Exit", 3);
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
        SceneManager.LoadScene(0);
    }

    private void UpdateGUI() 
    {
        notification.text = _notificationMessage;
        score.text = $"Score: {_score}";
    }

    private void Exit()
    {
        Application.Quit();
    }

}

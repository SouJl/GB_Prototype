using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

public class Main : MonoBehaviour
{
    public static Main instance;
    
    [Header("Set in Inspector")]
    public TextMeshProUGUI score;
    public TextMeshProUGUI notification;

    private int _score = 0;
    private string _notificationMessage;


    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        _notificationMessage = "";
        _score = 0;
        UpdateGUI();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void EnemyDefeat(Enemy e) 
    {
        _score += e.score;
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
            _notificationMessage = "Поздравляем! Вы дошли до конца уровня";
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

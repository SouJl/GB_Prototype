using UnityEngine;
using TMPro;

public class NotificationUIManager : MonoBehaviour
{
    public static NotificationUIManager instance;

    public TextMeshProUGUI notifaction;

    void Awake()
    {
        if (instance == null)
            instance = this;
    }

    private void Start()
    {
        notifaction.text = "";
    }


    public void SetNotification(string text) 
    {
        notifaction.text = text;
    }

    public void ResetNotifaction() => notifaction.text = "";
}

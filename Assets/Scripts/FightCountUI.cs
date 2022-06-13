using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FightCountUI : MonoBehaviour
{
    public static FightCountUI instance;

    public TextMeshProUGUI counter;

    private int maxCount = 0;

    private void Awake()
    {
        instance = this;
        counter.text = "";
    }


    public void StartCount(int time) 
    {
        maxCount = time;
        StartCoroutine(Counter());
    }

    IEnumerator Counter() 
    {
        while(maxCount != 0) 
        {
            counter.text = maxCount.ToString();
            yield return new WaitForSeconds(1);
            maxCount--;
        }
        counter.text = "";
    }
}

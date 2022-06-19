using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum AnswerState 
{
    start,
    next1,
    end
}
public class RiddleController : MonoBehaviour
{
    public static RiddleController instance;

    public GameObject award;
    public Transform awardPosition;

    [NonSerialized]
    public Queue<GameObject> buttons;

    private List<GameObject> goBuffer;
    private bool _isRiddleResolve;
    private AnswerState _answerState;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        _isRiddleResolve = false;
        _answerState = AnswerState.start;
        buttons = new Queue<GameObject>();
        goBuffer = new List<GameObject>();
    }

    void Update()
    {
        if (_isRiddleResolve) return;

        if (buttons.Count != 3) return;

        goBuffer.AddRange(buttons.ToList());

        while (buttons.Any()) 
        {
            var button = buttons.Peek();
            var bObject = button.GetComponent<Button>();
            buttons.Dequeue();
            if (bObject.IsOn)
            {
                AnswerPattern(bObject.NumberOfButton);
            }
        }
    }

    private void AnswerPattern(int numOfButton) 
    {
        switch (_answerState) 
        {
            case AnswerState.start: 
                {
                    if (numOfButton == 3) 
                    {
                        _answerState = AnswerState.next1;
                    }
                    else 
                    {
                        RiddleResult(false);
                    }
                    break;
                }
            case AnswerState.next1:
                {
                    if (numOfButton == 1) 
                    {
                        _answerState = AnswerState.end;
                    }
                    else 
                    {
                        _answerState = AnswerState.start;
                        RiddleResult(false);
                    }
                    break;
                }
            case AnswerState.end:
                {
                    if (numOfButton == 2) 
                    {
                        RiddleResult(true);
                    }
                    break;
                }
        }
    }

    private void RiddleResult(bool result) 
    {
        if (result == true) 
        {
            var go = Instantiate(award, awardPosition.position, award.transform.rotation);
            go.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);        
            _isRiddleResolve = true;
        }
        else 
        {
            goBuffer.ForEach(b => b.GetComponent<Button>().ResetButton());
        }
        buttons.Clear();
        goBuffer.Clear();
        
    }
}

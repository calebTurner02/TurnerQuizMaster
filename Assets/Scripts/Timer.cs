using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{

    [SerializeField] float fltTimeToCompleteQuestion = 30f;
    [SerializeField] float fltTimeToShowAnswer = 10f;  

    public bool bolLoadNextQuestion;
    public float fltFillFraction;

    bool bolIsAnsweringQuestion;
    float fltTimervalue;     
    void Update()
    {
        updateTimer();
    }

    public void cancelTimer()
    {
        
    } 


    void updateTimer()
    {
        fltTimervalue -= Time.deltaTime;

        if(bolIsAnsweringQuestion)
        {
            if(fltTimervalue > 0)
            {
                fltFillFraction = fltTimervalue / fltTimeToCompleteQuestion;
            }
            else
            {
                bolIsAnsweringQuestion = false;
                fltTimervalue = fltTimeToShowAnswer;
            }
        }
        else
        {
                  if(fltTimervalue > 0)
            {
                fltFillFraction = fltTimervalue / fltTimeToShowAnswer;
            }
            else
            {
                bolIsAnsweringQuestion = true;
                fltTimervalue = fltTimeToCompleteQuestion;
                bolLoadNextQuestion = true;
            }
        }
    }
}

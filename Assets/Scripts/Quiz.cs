using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Quiz : MonoBehaviour
{

    [Header("Questions")]
   [SerializeField] TextMeshProUGUI questionText;
    [SerializeField] QuestionSO question;


    [Header("Answers")]
    [SerializeField] GameObject[] answerButtons;
    int intCorrectAnswerIndex;
    bool bolHasAnswerdEarly;


    [Header("Button colors")]
   [SerializeField] Sprite defaultAnswerSprite;
   [SerializeField] Sprite correctAnswerSprite;


   [Header("Timer")]
   [SerializeField] Image timerImage;
   Timer timer;
    void Start()
    {
        timer = FindObjectOfType<Timer>();
        getNextQuestion();
        //DisplayQuestion();   
    } 

    void Update() 
    {
        timerImage.fillAmount = timer.fltFillFraction;

        if(timer.bolLoadNextQuestion)
        {
            bolHasAnswerdEarly = false;
            getNextQuestion();
            timer.bolLoadNextQuestion = false;
        }
        else if(!bolHasAnswerdEarly && !timer.bolIsAnsweringQuestion)
        {
            displayAnswer(-1);
            setButtonState(false);
        }
    }
    public void onAnswerSelected(int index)
    {    
        bolHasAnswerdEarly = true;
        displayAnswer(index);
        setButtonState(false);
        timer.cancelTimer();
    }

    void displayAnswer(int index)
    {
             Image buttonImage;
        if(index == question.intGetCorrectAnswerIndex())
        {
            questionText.text = "correct!";
            buttonImage = answerButtons[index].GetComponent<Image>();
            buttonImage.sprite = correctAnswerSprite;
        }
        else 
        {

            intCorrectAnswerIndex = question.intGetCorrectAnswerIndex();
            string correctAnswer = question.strGetAnswer(intCorrectAnswerIndex);
            questionText.text = "Sorry, the correct answer was; \n " + correctAnswer;
            buttonImage = answerButtons[intCorrectAnswerIndex].GetComponent<Image>();
            buttonImage.sprite = correctAnswerSprite;
        }
    }

    void getNextQuestion()
    {
        setButtonState(true);
        setDefaultButtonStates();
        DisplayQuestion();
    }

    void DisplayQuestion()
    {

     questionText.text = question.strgetQuestion();

        for(int intI = 0; intI < answerButtons.Length; intI++)
        {
            TextMeshProUGUI buttonText = answerButtons[intI].GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = question.strGetAnswer(intI);
        } 


    }
    void setButtonState(bool state)
    {
        for(int intI = 0; intI < answerButtons.Length; intI++)
        {
            Button button = answerButtons[intI].GetComponent<Button>();
            button.interactable = state;
        }
    }
    void setDefaultButtonStates()
    {
        for(int intI = 0; intI < answerButtons.Length; intI++)
        {
            Image buttonImage = answerButtons[intI].GetComponent<Image>();
            buttonImage.sprite = defaultAnswerSprite;
        }
    }
}

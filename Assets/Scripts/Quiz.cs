using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Quiz : MonoBehaviour
{
   [SerializeField] TextMeshProUGUI questionText;
    [SerializeField] QuestionSO question;
    [SerializeField] GameObject[] answerButtons;
    int intCorrectAnswerIndex;
   [SerializeField] Sprite defaultAnswerSprite;
   [SerializeField] Sprite correctAnswerSprite;
    void Start()
    {
        getNextQuestion();
      //DisplayQuestion();   
    } 
    public void onAnswerSelected(int index)

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
        setButtonState(false);
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

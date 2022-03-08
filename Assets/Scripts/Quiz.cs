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
        questionText.text = question.strgetQuestion();

        for(int intI = 0; intI < answerButtons.Length; intI++)
        {
            TextMeshProUGUI buttonText = answerButtons[intI].GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = question.strGetAnswer(intI);
        }     
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
    }
}

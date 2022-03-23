using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Quiz : MonoBehaviour
{

    [Header("Questions")]
   [SerializeField] TextMeshProUGUI questionText;

    [SerializeField] List<QuestionSO> questions = new List<QuestionSO>();
    QuestionSO currentQuestion;


    [Header("Answers")]
    [SerializeField] GameObject[] answerButtons;
    int intCorrectAnswerIndex;
    bool bolHasAnswerdEarly = true;


    [Header("Button colors")]
   [SerializeField] Sprite defaultAnswerSprite;
   [SerializeField] Sprite correctAnswerSprite;


   [Header("Timer")]
   [SerializeField] Image timerImage;
   Timer timer;

    [Header("Score")]
    [SerializeField] TextMeshProUGUI scoreText;
    ScoreKeeper scoreKeeper;

    [Header("Progressbar")]
    [SerializeField] Slider progressBar;

    public bool bolIsComplete;


    void Awake()
    {
        timer = FindObjectOfType<Timer>(); 
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        progressBar.maxValue = questions.Count;
        progressBar.value = 0; 

    } 

    void Update() 
    {
        timerImage.fillAmount = timer.fltFillFraction;

        if(timer.bolLoadNextQuestion)
        {

            if(progressBar.value == progressBar.maxValue)
            {
                bolIsComplete = true;
            }

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
        scoreText.text = "Score: " + scoreKeeper.intCalculateScore() + "%";

      

    }

    void displayAnswer(int index)
    {
             Image buttonImage;
        if(index == currentQuestion.intGetCorrectAnswerIndex())
        {
            questionText.text = "correct!";
            buttonImage = answerButtons[index].GetComponent<Image>();
            buttonImage.sprite = correctAnswerSprite;
            scoreKeeper.IncrementCorrectAnswers();
        }
        else 
        {

            intCorrectAnswerIndex = currentQuestion.intGetCorrectAnswerIndex();
            string correctAnswer = currentQuestion.strGetAnswer(intCorrectAnswerIndex);
            questionText.text = "Sorry, the correct answer was; \n " + correctAnswer;
            buttonImage = answerButtons[intCorrectAnswerIndex].GetComponent<Image>();
            buttonImage.sprite = correctAnswerSprite;
        }
    }

    void getNextQuestion()
    {

        if(questions.Count > 0)
        {
            setButtonState(true);
            setDefaultButtonStates();
            GetRandomQuestion();
            DisplayQuestion();
            progressBar.value++;
            scoreKeeper.incrementQuestionsSeen();
        }
    }

    void GetRandomQuestion()
    {
        int intIndex = Random.Range(0, questions.Count);
        currentQuestion = questions[intIndex];

        if(questions.Contains(currentQuestion))
        {
            questions.Remove(currentQuestion);
        }
        
    }

    void DisplayQuestion()
    {

     questionText.text = currentQuestion.strgetQuestion();

        for(int intI = 0; intI < answerButtons.Length; intI++)
        {
            TextMeshProUGUI buttonText = answerButtons[intI].GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = currentQuestion.strGetAnswer(intI);
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

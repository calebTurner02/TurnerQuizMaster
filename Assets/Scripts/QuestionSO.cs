using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Quiz Question", fileName = "New Question")]
public class QuestionSO : ScriptableObject
{
    [TextArea(2, 6)]
   [SerializeField] string strQuestion = "Enter quiz question here";
   [SerializeField] string[] strAnswer = new string[4];
   [SerializeField] int intCorrectAnswerIndex;

   public string strgetQuestion()
   {
       return strQuestion;
   }

   public string strGetAnswer(int index)
   {
       return strAnswer[index];
   }
   public int intGetCorrectAnswerIndex()
   {
       return intCorrectAnswerIndex;
   }
}

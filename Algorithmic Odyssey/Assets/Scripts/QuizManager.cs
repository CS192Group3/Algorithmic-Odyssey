// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.UI;

// public class QuizManager : MonoBehaviour
// {
//     public List<QuestionandAnswer>QnA;
//     public GameObject[] options;
//     public int currentQuestion;

//     public Text QuestionTxt;
//     private void Start()
//     {
//         generateQuestion();
//     }
//     public void correct()
//     {
//        //generate next
//        QnA.RemoveAt(currentQuestion);
//        generateQuestion();
//     }

//     void setAnswers()
//     {
//         for(int i = 0; i < options.Length; i++)
//         {
//             options[i].GetComponent<AnswerScript>().isCorrect = false;
//             options[i].transform.GetChild(0).GetComponent<UnityEngine.UI.Text>().text= QnA[currentQuestion].Answer[i];

//             if(QnA[currentQuestion].CorrectAnswer == i+1)
//             {
//                 options[i].GetComponent<AnswerScript>().isCorrect = true;
//             }
//         }
//     }
//     void generateQuestion()
//     {
//         currentQuestion = Random.Range(0, QnA.Count);
//         QuestionTxt.text = QnA[currentQuestion].Question;
//         setAnswers();

//         QnA.RemoveAt(currentQuestion);
//     }
   
// }

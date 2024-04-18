using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuizManager : MonoBehaviour
{
    
    [SerializeField] private QuizUI quizUI;
    [SerializeField]// private QuizDataScriptable quizData;
    // to edit in inspector
    private List<Question> questions;
    private Question selectedQuestion;
    // Start is called before the first frame update
    private int scoreCOunt = 0;
    void Start()
    {
        scoreCOunt = 0;
        SelectQuestion();
        
    }
    // create question randomly
    void SelectQuestion()
    {
        if (questions.Count == 0)
        {
            SceneManager.LoadScene("ReviewScene");
            return;
        }
        int val = Random.Range(0, questions.Count);
        selectedQuestion = questions[val];

        quizUI.SetQuestion(selectedQuestion);
        //questions.Remove(selectedQuestion);
    }
    // check ans
    public bool Answer(string answered)
    {
        bool correctAns = false;
        if (answered == selectedQuestion.correctAns)
        {
            correctAns = true;
            scoreCOunt +=1;
            quizUI.UpdateScore(scoreCOunt);
        }
        else
        {
            // wrong ans
        }
        // generate new after 0.4 sec
        questions.Remove(selectedQuestion);
        Invoke("SelectQuestion", 0.4f);
        return correctAns;
    }

}
[System.Serializable]
public class Question
{
    public string questionInfo;
    public QuestionType questionType;
    public Sprite qustionImg;
    public AudioClip qustionClip;
    public UnityEngine.Video.VideoClip qustionVideo;
    public List<string> options;
    public string correctAns;
}
[System.Serializable]
public enum QuestionType
{
    TEXT,
    IMAGE,
    VIDEO
}


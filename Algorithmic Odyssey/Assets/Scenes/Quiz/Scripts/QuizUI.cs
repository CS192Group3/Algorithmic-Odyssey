using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuizUI : MonoBehaviour
{
    [SerializeField]private QuizManager quizManager;
    [SerializeField]private Text questionText, scoreText;
    [SerializeField]private Image questionImage;
    [SerializeField]private UnityEngine.Video.VideoPlayer questionVideo;
    [SerializeField]private List<Button> options;
    [SerializeField]public Color correctCol, wrongCol, normalCol;

    private Question question;
    private bool answered;

    public Text ScoreText { get { return scoreText;} }
    public void UpdateScore(int newScore){
        scoreText.text = "Score: " + newScore.ToString();
    }
   
    void Awake()
    {
        for (int i = 0; i < options.Count; i++)
        {
            Button localBtn = options[i];
            localBtn.onClick.AddListener(() => OnClick(localBtn));
        }
    }
    // populate question
    public void SetQuestion(Question question)
    {
        this.question = question;
        switch (question.questionType)
        {
            case QuestionType.TEXT:
                questionImage.transform.parent.gameObject.SetActive(false);
                break;
            case QuestionType.IMAGE:
                ImageHolder();
                questionImage.transform.gameObject.SetActive(true);

                questionImage.sprite = question.qustionImg;
                
                break;
            case QuestionType.VIDEO:
                ImageHolder();
                questionVideo.transform.gameObject.SetActive(true);

                questionVideo.clip = question.qustionVideo;
                questionVideo.Play();
                break;

        }
        questionText.text = question.questionInfo;
        List<string> answerList = ShuffleList.ShuffleListItems<string>(question.options);

        for (int i = 0; i < options.Count; i++)
        {
           options[i].GetComponentInChildren<Text>().text = answerList[i];
           options[i].name = answerList[i];
           // normal color each time
           ColorUtility.TryParseHtmlString("#384255", out normalCol);
           options[i].image.color = normalCol;
        }
        answered = false;

    }
    void ImageHolder()
    {
        questionImage.transform.parent.gameObject.SetActive(true);
        //deac other components
        questionImage.transform.gameObject.SetActive(false);
        questionVideo.transform.gameObject.SetActive(false);
    }

    private void OnClick(Button btn)
    {
        if (!answered)
        {
            answered = true;
            bool val = quizManager.Answer(btn.name);
            if (val)
            {
                // correct ans
                ColorUtility.TryParseHtmlString("#00FF00", out correctCol);
                btn.image.color = correctCol;
            }
            else
            {
                // wrong ans
                ColorUtility.TryParseHtmlString("#FF0000", out wrongCol);
                btn.image.color = wrongCol;
            }

            
        }
    }




}

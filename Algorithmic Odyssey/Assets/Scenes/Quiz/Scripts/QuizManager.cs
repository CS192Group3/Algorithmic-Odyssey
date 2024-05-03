using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;
public class QuizManager : MonoBehaviour
{
    
    [SerializeField] private QuizUI quizUI;
    [SerializeField]// private QuizDataScriptable quizData;
    // to edit in inspector
    private List<Question> questions;
    private Question selectedQuestion;
    // Start is called before the first frame update
    private int scoreCOunt = 0;
    //private PlayerPrefsCoinManager coinsManager;
    // private InventoryManager inventoryManager;
    // private Item rewardItem;
    // [SerializeField] private List<Item> rewardItems;

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
            // Check if score has reached the threshold
            if (scoreCOunt > 5)  
            {
                string[] resources = { "iron", "stone", "treelog" };
                int[] quantities = { 14, 10, 8 };

                // Shuffle the resources and quantities arrays
                System.Random rng = new System.Random();
                resources = resources.OrderBy(x => rng.Next()).ToArray();
                quantities = quantities.OrderBy(x => rng.Next()).ToArray();
    
                for (int i = 0; i < resources.Length; i++)
                {
                    switch (resources[i])
                    {
                        case "iron":
                            CoinsManager.iron += quantities[i];
                            PlayerPrefs.SetInt("Iron", CoinsManager.iron);
                            PlayerPrefs.Save();
                            CoinsManager.UpdateIron();
                            break;
                        case "stone":
                            CoinsManager.stone += quantities[i];
                            PlayerPrefs.SetInt("Stone", CoinsManager.stone);
                            PlayerPrefs.Save();
                            CoinsManager.UpdateStone();
                            break;
                        case "treelog":
                            CoinsManager.treelog += quantities[i];
                            PlayerPrefs.SetInt("treeLog", CoinsManager.treelog);
                            PlayerPrefs.Save();
                            CoinsManager.UpdateTreeLog();
                            break;
                    }
                }
                
            }
            SceneManager.LoadScene("ReviewScene");
            return;
        }
        int val = Random.Range(0, questions.Count);
        selectedQuestion = questions[val];

        quizUI.SetQuestion(selectedQuestion);
        //questions.Remove(selectedQuestion);
    }
    
    string SelectRandomResource()
    {
        // Create a list of resources
        List<string> resources = new List<string> { "log", "iron", "stone" };

        // Create a new random number generator
        System.Random random = new System.Random();

        // Get a random index into the list
        int index = random.Next(resources.Count);

        // Select the resource at the random index
        string selectedResource = resources[index];

        // Return the selected resource
        return selectedResource;
    }
    // check ans
    public bool Answer(string answered)
    {
        bool correctAns = false;
        if (answered == selectedQuestion.correctAns)
        {
            correctAns = true;
            scoreCOunt +=5;
            quizUI.UpdateScore(scoreCOunt);
        }
        else
        {
            // wrong ans
        }
        questions.Remove(selectedQuestion);
        Invoke("SelectQuestion", 2f);
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


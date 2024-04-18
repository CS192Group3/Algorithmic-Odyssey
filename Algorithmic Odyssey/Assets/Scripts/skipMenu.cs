using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Windows;

public class SkipMenu : MonoBehaviour
{

    public GameObject skipMenu;
    public static int[] readArray = new int[10];
    public string targetScene;

    void Start()
    {
        skipMenu.SetActive(false);
    }
    public void GoToScene(string sceneName)
    {

        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);

    }

    public void GoToTopic(string sceneName)
    {
        targetScene = sceneName;
        string numberPart = sceneName.Substring(5);
        int topicNum = int.Parse(numberPart);
        if (readArray[topicNum] == 1)
        {
            skipMenu.SetActive(true);
        } else
        {
            readArray[topicNum] = 1;
            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
        }
        
    }

    public void Continue()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(targetScene);
        skipMenu.SetActive(false);
    }
    public void backSkip()
    {
        skipMenu.SetActive(false);
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;

public class SkipMenu : MonoBehaviour
{

    public GameObject skipMenu;
    public static bool readTopic1 = false;
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
        if (readTopic1 == true)
        {
            skipMenu.SetActive(true);
        } else
        {
            if (sceneName == "Topic1")
            {
                readTopic1 = true;
            }
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

using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void GoToScene(string sceneName)
    {
        SceneManagerScript.LoadScene(sceneName);
    }

    public void NewGame()
    {
        PlayerPrefs.SetInt("Saved", 0);
        PlayerPrefs.SetInt("TimeToLoad", 0);
        PlayerPrefs.Save();
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

    public void GoToReview()
    {
        SceneManagerScript.SetInitialScene("MainMenu");
        SceneManagerScript.LoadScene("ReviewScene");
    }

    public void ReviewBack()
    {
        SceneManagerScript.LoadPreviousScene();
    }

    public void BackInitial()
    {
        SceneManagerScript.LoadInitialScene();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour
{
    public static string previousScene;
    public static string returnScene;
    public static string initialScene;

    public static void LoadScene(string sceneName)
    {
        previousScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(sceneName);
    }

    public static void SetReturnScene(string sceneName)
    {
        returnScene = sceneName;
    }

    public static void SetInitialScene(string sceneName)
    {
        initialScene = sceneName;
    }

    public static void LoadPreviousScene()
    {
        if (!string.IsNullOrEmpty(returnScene))
        {
            SceneManager.LoadScene(returnScene);
            returnScene = null;
        }
        else if (!string.IsNullOrEmpty(previousScene))
        {
            SceneManager.LoadScene(previousScene);
        }
    }

    public static void LoadInitialScene()
    {
        if (!string.IsNullOrEmpty(initialScene))
        {
            SceneManager.LoadScene(initialScene);
            initialScene = null;
        }
        else if (!string.IsNullOrEmpty(previousScene))
        {
            SceneManager.LoadScene(previousScene);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLoader : MonoBehaviour
{
    public void LoadNextScene()
    {
        int currentSceneIndex = 
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;

        UnityEngine.SceneManagement.SceneManager.LoadScene(currentSceneIndex + 1);
    }



    public void LoadStartScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{

    public static LevelManager instance;

    private void Start()
    {
        instance = this;
    }

    public void LoadLevel(int scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void LoadRandomLevel()
    {
        int RandLevel =  Random.Range(2, 4);
        SceneManager.LoadScene(RandLevel);
    }


    public void QuitGame()
    {
        Application.Quit();
    }


}

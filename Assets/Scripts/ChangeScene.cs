using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour {

    [SerializeField]
    string SceneName = "";

    // Update is called once per frame
    void Update() {
        ChangesScene();


    }

    public void GameOverChangeScene()
    {
                SceneManager.LoadScene("game");
    }

    public void GameOverQuit()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
    }



    public void ChangesScene()
    {
        if (Input.GetMouseButtonDown(0))
            if (!string.IsNullOrEmpty(SceneName))
                SceneManager.LoadScene(SceneName);

        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();

    }

}

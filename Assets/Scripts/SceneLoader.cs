using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject loadingCanvas;

    public void StartGame()
    {
        // SceneManager.LoadSceneAsync
        mainMenu.SetActive(false);
        loadingCanvas.SetActive(true);
        SceneManager.LoadSceneAsync("Scenes/SampleScene", LoadSceneMode.Additive).completed += (AsyncOperation action) =>
        {
            if (action.isDone)
            {
                loadingCanvas.SetActive(false);
                PostStartFloor();
            }
        };
    }

    public void PostStartFloor()
    {
        Debug.Log("Preparing floor");
        // TODO spawn enemies etc
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.sceneCount > 1)
        {
            mainMenu.SetActive(false);
            PostStartFloor();
        }
        else
        {
            mainMenu.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}

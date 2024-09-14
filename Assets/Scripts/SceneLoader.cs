using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject loadingScreen;

    public GameObject player;

    public void StartGame()
    {
        mainMenu.SetActive(false);
        ShowLoadingScreen();
        SceneManager.LoadSceneAsync("Developers/yurispeondivision/FloorSample", LoadSceneMode.Additive).completed += (AsyncOperation action) =>
        {
            if (action.isDone)
            {
                StartCoroutine(SetupFloor());
            }
        };
    }

    IEnumerator SetupFloor()
    {
        yield return new WaitForSeconds(0);
        var spawn = FindObjectOfType<PlayerSpawnPoint>();
        player.transform.position = spawn.transform.position;
        ShowLoadingScreen(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ShowLoadingScreen(bool show = true)
    {
        loadingScreen.SetActive(show);
    }

    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.sceneCount > 1)
        {
            mainMenu.SetActive(false);
            ShowLoadingScreen();
            StartCoroutine(SetupFloor());
        }
        else
        {
            mainMenu.SetActive(true);
        }
    }
}

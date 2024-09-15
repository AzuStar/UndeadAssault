using System.Collections;
using System.Collections.Generic;
using UndeadAssault;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public int floorLevel = 0;
    public string[] floorPool;
    public string currentScene;

    public GameObject mainMenu;
    public GameObject loadingScreen;

    public GameObject player;

    public void StartGame()
    {
        mainMenu.SetActive(false);
        ShowLoadingScreen();
        StartFloor("Scenes/FloorSample");
    }

    public void StartFloor(string path)
    {
        player.SetActive(false);
        SceneManager.LoadSceneAsync(path, LoadSceneMode.Additive).completed += (
            AsyncOperation action
        ) =>
        {
            if (action.isDone)
            {
                StartCoroutine(SetupFloor());
            }
        };
    }

    public void StartNextFloor()
    {
        player.SetActive(false);
        foreach (var room in FindObjectsOfType<RoomTrigger>())
        {
            room.Cleanup();
        }
        for (var i = 0; i < SceneManager.sceneCount; i++)
        {
            if (SceneManager.GetSceneAt(i).name != "GameMasterScene")
            {
                SceneManager.UnloadSceneAsync(SceneManager.GetSceneAt(i).buildIndex).completed += (
                    AsyncOperation action
                ) =>
                {
                    if (action.isDone)
                    {
                        StartFloor("Scenes/FloorSample");
                    }
                };
            }
        }
    }

    IEnumerator SetupFloor()
    {
        yield return new WaitForSeconds(0);
        var spawn = FindObjectOfType<PlayerSpawnPoint>();
        if (spawn != null && spawn.enabled)
        {
            player.transform.position = spawn.transform.position;
        }
        // ShowLoadingScreen(false);
        player.SetActive(true);
        SceneTransition.instance.FadeIn();
        FloorScreen.instance.FadeOut();
        SimpleAudioManager.Manager.instance.SetIntensity(2);
        // SimpleAudioManager.Manager.instance.
        // SimpleAudioManager.Manager.instance.PlaySong(new SimpleAudioManager.Manager.PlaySongOptions()
        // {
        //     song = 0,
        //     intensity = 2,
        // });
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
        player.SetActive(false);
        if (SceneManager.sceneCount > 1)
        {
            mainMenu.SetActive(false);
            // ShowLoadingScreen();
            StartCoroutine(SetupFloor());
        }
        else
        {
            mainMenu.SetActive(true);
        }
    }
}

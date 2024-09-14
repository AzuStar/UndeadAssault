using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject loadingCanvas;

    public GameObject player;

    public void StartGame()
    {
        mainMenu.SetActive(false);
        loadingCanvas.SetActive(true);
        SceneManager.LoadSceneAsync("Developers/yurispeondivision/FloorSample", LoadSceneMode.Additive).completed += (AsyncOperation action) =>
        {
            if (action.isDone)
            {
                StartCoroutine(SetupFloor());
            }
        };
        player.GetComponent<NavMeshAgent>().enabled = false;
    }

    IEnumerator SetupFloor()
    {
        yield return new WaitForSeconds(0);
        var spawn = FindObjectOfType<PlayerSpawn>();
        player.transform.position = spawn.transform.position;
        player.GetComponent<NavMeshAgent>().enabled = true;
        loadingCanvas.SetActive(false);
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
            loadingCanvas.SetActive(true);
            StartCoroutine(SetupFloor());
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

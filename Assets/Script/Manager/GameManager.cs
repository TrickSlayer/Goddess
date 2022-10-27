using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using static UnityEditor.Experimental.GraphView.GraphView;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public PlayerManager playerManager;
    public ObjectPooler pooler;

    private List<string> SceneHasPool = new List<string>();
    [HideInInspector] public string preScene;
    [HideInInspector] public string currentScene;
    public GameObject dialogBoxUI;
    [HideInInspector] public TimerManager timer;
    public GameObject gameStatus;

    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }

        DontDestroyOnLoad(this.gameObject);
            
    }

    private void Start()
    {
        gameStatus.SetActive(false);
        timer = GetComponent<TimerManager>();

        Restart();
    }

    public void Restart()
    {
        timer.LoadTimer();
        playerManager.LoadPlayer();
        if (playerManager.newGame)
        {
            dialogBoxUI.SetActive(true);
        }
        else
        {
            dialogBoxUI.SetActive(false);
        }
        CameraManager.instance.AddContraintCamera();

        currentScene = SceneManager.GetActiveScene().name;
        preScene = currentScene;
        SceneHasPool.Add(currentScene);
        pooler = ObjectPooler.instance;
    }

    private void Update()
    {
        if (timer.bossDie && !gameStatus.activeInHierarchy)
        {
            Time.timeScale = 0f;
            GameStatusUI ui = gameStatus.GetComponent<GameStatusUI>();
            ui.SetTime(timer.timePlay);
            gameStatus.SetActive(true);
        }
    }

    private void OnLevelWasLoaded(int level)
    {
        currentScene = SceneManager.GetActiveScene().name;
        AddListScene(currentScene);

        try
        {
            if (GameObject.FindGameObjectWithTag("Mark") == null)
            {
                pooler.SpawnPool();
            }
        }

        catch (Exception e)
        {
            Debug.LogWarning(e);
        }

        GameObject[] startPoints = GameObject.FindGameObjectsWithTag("StarPos");
        GameObject startPoint = startPoints.FirstOrDefault(x => x.name.Equals(preScene));
        preScene = currentScene;
        if (startPoint != null)
        {
            playerManager.player.transform.position = startPoint.transform.position;
        }
        CameraManager.instance.AddContraintCamera();
    }

    private void OnApplicationQuit()
    {
        timer.SaveTimmer();
        playerManager.SavePlayer();
    }

    private void AddListScene(string current)
    {

        if (SceneHasPool.Where(x => x.Equals(current)).ToList().Count == 0)
        {
            SceneHasPool.Add(current);
        }
    }

}

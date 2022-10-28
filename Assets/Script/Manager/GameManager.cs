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

    [HideInInspector] public string preScene;
    [HideInInspector] public string currentScene;
    public GameObject dialogBoxUI;
    [HideInInspector] public TimerManager timer;
    public GameObject gameStatus;

    private bool startGame = true;

    private void Awake()
    {
        if (instance != null && instance != this)
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

        startGame = false;
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

        if (!startGame)
        {
           pooler.SpawnPool();
        }

        if (playerManager.loadPlayer)
        {
            playerManager.SetPosition(playerManager.startPosition);
            playerManager.loadPlayer = false;
        }
        else
        {
            GameObject[] startPoints = GameObject.FindGameObjectsWithTag("StarPos");
            GameObject startPoint = startPoints.FirstOrDefault(x => x.name.Equals(preScene));
            preScene = currentScene;
            if (startPoint != null)
            {
                playerManager.SetPosition(startPoint.transform.position);
            }
        }
        CameraManager.instance.AddContraintCamera();
    }

    private void OnApplicationQuit()
    {
        timer.SaveTimmer();
        playerManager.SavePlayer();
    }

}

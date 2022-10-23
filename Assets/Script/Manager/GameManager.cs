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

    public List<string> SceneHasPool = new List<string>();
    public string currentScene;
    private bool newScene = false;

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
        playerManager.LoadPlayer();

        currentScene = SceneManager.GetActiveScene().name;
        SceneHasPool.Add(currentScene);
    }

    private void OnLevelWasLoaded(int level)
    {
        currentScene = SceneManager.GetActiveScene().name;
        AddListScene(currentScene);
        if (newScene)
        {
            pooler.SpawnPool();
        }
    }

    private void OnApplicationQuit()
    {
        playerManager.SavePlayer();
    }

    private void AddListScene(string current)
    {

        if (SceneHasPool.Where(x => x.Equals(current)).ToList().Count == 0)
        {
            SceneHasPool.Add(current);
            newScene = true;
        }
        else
        {
            newScene = false;
        }
        playerManager.player.transform.position = GameObject.FindWithTag("StarPos").transform.position;
    }


}

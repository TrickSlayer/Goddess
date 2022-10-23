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
        CameraManager.instance.AddContraintCamera();

        currentScene = SceneManager.GetActiveScene().name;
        preScene = currentScene;
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
    }


}

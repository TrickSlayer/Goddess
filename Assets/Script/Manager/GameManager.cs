using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public PlayerManager playerManager;
    public ObjectPooler pooler;

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

        playerManager = GetComponent<PlayerManager>();
    }

    private void Start()
    {
        playerManager.LoadPlayer();
    }

    private void OnApplicationQuit()
    {
        playerManager.SavePlayer();
    }

}

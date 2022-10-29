using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Revival_UI : MonoBehaviour
{
    public static Revival_UI instance;
    [HideInInspector] GameObject revivalPoint;

    PlayerManager playerManager;

    private void Awake()
    {
        gameObject.SetActive(false);
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    private void Start()
    {
        playerManager = PlayerManager.instance;
    }

    public void Revival()
    {
        StartCoroutine(LoadScreenRevival());
    }

    public IEnumerator LoadScreenRevival()
    {

        var asyncLoadLevel = SceneManager.LoadSceneAsync("Beach", LoadSceneMode.Single);

        while (!asyncLoadLevel.isDone)
        {
            Debug.Log("Loading the Scene");
            yield return null;
        }

        playerManager.statsP.wasDie = false;
        gameObject.SetActive(false);

        PlayerStats stats = playerManager.statsP;
        stats.RecoverHealth(stats.Health.Value);
        stats.RecoverMana(stats.Mana.Value);

        playerManager.movementP.isHurt(false);
        playerManager.player.tag = "Player";

        revivalPoint = GameObject.FindGameObjectWithTag("Revival");
        playerManager.SetPosition(revivalPoint.transform.position);

    }
}

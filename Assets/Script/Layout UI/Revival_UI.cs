using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

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
        revivalPoint = GameObject.FindGameObjectWithTag("Revival");
        playerManager = PlayerManager.instance;
    }

    public void Revival()
    {
        playerManager.statsP.wasDie = false;
        gameObject.SetActive(false);

        PlayerStats stats = playerManager.statsP;
        stats.RecoverHealth(stats.Health.Value);
        stats.RecoverMana(stats.Mana.Value);

        playerManager.movementP.isHurt(false);
        playerManager.player.tag = "Player";

        playerManager.SetPosition(revivalPoint.transform.position);
    }
}

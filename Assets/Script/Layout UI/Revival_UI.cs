using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Revival_UI : MonoBehaviour
{
    public static Revival_UI instance;
    [HideInInspector] GameObject revivalPoint;

    private void Awake()
    {
        gameObject.SetActive(false);
        instance = this;
        revivalPoint = GameObject.FindGameObjectWithTag("Revival");
    }

    public void Revival()
    {
        PlayerManager.instance.statsP.wasDie = false;
        gameObject.SetActive(false);
        PlayerManager.instance.statsP.RecoverHealth(PlayerManager.instance.statsP.Health.Value);
        PlayerManager.instance.statsP.RecoverMana(PlayerManager.instance.statsP.Mana.Value);
        PlayerManager.instance.player.transform.position = revivalPoint.transform.position;
        Time.timeScale = 1f;
    }
}

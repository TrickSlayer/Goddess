using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Master : MonoBehaviour
{
    [HideInInspector] public bool inRange = false;

    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            inRange = true;
            PlayerManager manager = PlayerManager.instance;
            manager.statsP.RecoverHealth(manager.statsP.Health.Value);
            manager.statsP.RecoverMana(manager.statsP.Mana.Value);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            inRange = false;
        }
    }
}

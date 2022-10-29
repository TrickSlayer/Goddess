using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    [HideInInspector] public PlayerInventory inventoryP;
    [HideInInspector] public PlayerStats statsP;
    [HideInInspector] public PlayerMovement movementP;
    [HideInInspector] public GameObject player;
    public bool newGame = false;
    public Vector3 startPosition = new Vector3(0, 0, -1);
    public bool loadPlayer = true;

    // Start is called before the first frame update

    public static PlayerManager instance;

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

        player = gameObject;
        inventoryP = player.GetComponent<PlayerInventory>();
        statsP = player.GetComponent<PlayerStats>();
        movementP = player.GetComponent<PlayerMovement>();
    }

    public void SavePlayer()
    {
        SaveSystem.SavePlayer(statsP, inventoryP, movementP);
    }

    public void LoadPlayer()
    {
        PlayerData data =  SaveSystem.LoadPlayer();

        if(data == null)
        {
            statsP.SetStartHealth();
            statsP.SetStartMana();
            statsP.SetHealth(statsP.Health.Value);
            statsP.SetMana(statsP.Mana.Value);
            statsP.currentExperience = 0;

            ObjectPooler.instance.SpawnPool();

            newGame = true;
            loadPlayer = false;

            StartCoroutine(LoadLevel.LoadScreen(1));

            return;
        }

        statsP.Health = data.Health;
        statsP.Mana = data.Mana;
        statsP.Attack = data.Attack;
        statsP.Defense = data.Defense;
        statsP.Dodge = data.Dodge;
        statsP.CritDamage = data.CritDamage;
        statsP.CritRate = data.CritRate;
        statsP.SetStartHealth();
        statsP.SetStartMana();
        statsP.SetHealth(data.currentHealth);
        statsP.SetMana(data.currentMana);

        statsP.currentExperience = data.currentExperience;
        statsP.Level = data.Level;
        statsP.Experience = data.Experience;
        statsP.Score = data.Score;

        inventoryP.inventory.slots = data.getSlots();
        inventoryP.inventory.needFresh = true;

        if (data.map != "StartScene")
            StartCoroutine(LoadLevel.LoadScreen(data.map));
        else StartCoroutine(LoadLevel.LoadScreen("Beach"));

        startPosition.x = data.position[0];
        startPosition.y = data.position[1];
        startPosition.z = 0;

        if (statsP.currentHealth <= 0)
        {
            statsP.Die();
        }

    }

    public void SetPosition(Vector3 position)
    {
        player.transform.position = position;
    }

}

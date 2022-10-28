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
    public PlayerBeginData data;

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

    public void ResetStats()
    {
        statsP.Level = data.Level;
        statsP.Score = data.Score;
        statsP.Experience = data.Experience;
        statsP.Health = data.Health;
        statsP.Mana = data.Mana;
        statsP.Defense = data.Defense;
        statsP.Attack = data.Attack;
        statsP.CritRate = data.CritRate;
        statsP.CritDamage = data.CritDamage;
        statsP.Dodge = data.Dodge;
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
            ResetStats();
            statsP.SetStartHealth();
            statsP.SetStartMana();
            statsP.SetHealth(statsP.Health.Value);
            statsP.SetMana(statsP.Mana.Value);
            statsP.currentExperience = 0;

            ObjectPooler.instance.SpawnPool();

            newGame = true;

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

        StartCoroutine(LoadLevel.LoadScreen(data.map));

        Vector3 position;
        position.x = data.position[0];
        position.y = data.position[1];
        position.z = data.position[2];

        SetPosition(position);

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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    [HideInInspector] public PlayerInventory inventoryP;
    [HideInInspector] public PlayerStats statsP;
    [HideInInspector] public PlayerMovement movementP;
    [HideInInspector] public GameObject player;

    private GameObject[] players;

    // Start is called before the first frame update

    public static PlayerManager instance;

    private void Awake()
    {
        player = gameObject;
        inventoryP = player.GetComponent<PlayerInventory>();
        statsP = player.GetComponent<PlayerStats>();
        movementP = player.GetComponent<PlayerMovement>();

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
            statsP.SetHealth(statsP.Health.Value);
            statsP.SetMana(statsP.Mana.Value);
            return;
        }

        statsP.Health = data.Health;
        statsP.Mana = data.Mana;
        statsP.Attack = data.Attack;
        statsP.Defense = data.Defense;
        statsP.Dodge = data.Dodge;
        statsP.CritDamage = data.CritDamage;
        statsP.CritRate = data.CritRate;
        statsP.SetHealth(data.currentHealth);
        statsP.SetMana(data.currentMana);

        statsP.currentExperience = data.currentExperience;
        statsP.Level = data.Level;
        statsP.Experience = data.Experience;
        statsP.Score = data.Score;

        inventoryP.inventory.slots = data.getSlots();

        SceneManager.LoadScene(data.map);

        Vector3 position;
        position.x = data.position[0];
        position.y = data.position[1];
        position.z = data.position[2];

        player.transform.position = position;

    }

}

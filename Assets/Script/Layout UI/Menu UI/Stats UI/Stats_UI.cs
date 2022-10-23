using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Stats_UI : MonoBehaviour
{
    public List<Stat_UI> statList = new List<Stat_UI>();
    public List<GameObject> buttons = new List<GameObject> ();
    private int preScore = 0;
    public TextMeshProUGUI ScoreText;
    public TextMeshProUGUI LevelText;
    public InformationBar experienceBar;
    [HideInInspector] public bool needFresh = true;
    [HideInInspector] PlayerStats playerStats;
    public static Stats_UI instance;

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
    }

    // Start is called before the first frame update
    void Start()
    {
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
        FreshLevel();
    }

    public void FreshLevel()
    {
        experienceBar.SetMaxValue(playerStats.Experience.Value);
        experienceBar.SetValue(playerStats.currentExperience);
        LevelText.text = "Level: " + playerStats.Level;
        needFresh = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerStats.Score != preScore)
        {
            needFresh = true;
        }

        if (needFresh)
        {
            needFresh = false;
            statList[0].setAttribute("Max Health: " + playerStats.Health.Value);
            statList[1].setAttribute("Max Mana: " + playerStats.Mana.Value);
            statList[2].setAttribute("Defense: " + playerStats.Defense.Value);
            statList[3].setAttribute("Attack: " + playerStats.Attack.Value);
            statList[4].setAttribute("Crit Rate: " + playerStats.CritRate.Value + "%");
            statList[5].setAttribute("Crit Damage: " +  playerStats.CritDamage.Value + "%");
            statList[6].setAttribute("Dodge: " + playerStats.Dodge.Value + "%");

            ScoreText.text = "Score: " + playerStats.Score;

            if(playerStats.Score == 0)
            {
                foreach (var item in buttons)
                {
                    item.SetActive(false);
                }
            } else
            {
                foreach (var item in buttons)
                {
                    item.SetActive(true);
                }
            }

            preScore = playerStats.Score;
        }
    }

    public void PowerUp(int position)
    {

        switch (position)
        {
            case 0:
                PowerUpComon();
                playerStats.Health.AddModifier(new Goddess.PlayerStat.Stat(100, Goddess.PlayerStat.StatType.Flat));
                playerStats.HealthBar.SetMaxValue(playerStats.Health.Value);
                break;
            case 1:
                PowerUpComon();
                playerStats.Mana.AddModifier(new Goddess.PlayerStat.Stat(100, Goddess.PlayerStat.StatType.Flat));
                playerStats.ManaBar.SetMaxValue(playerStats.Mana.Value);
                break;
            case 2:
                PowerUpComon();
                playerStats.Defense.AddModifier(new Goddess.PlayerStat.Stat(10, Goddess.PlayerStat.StatType.Flat));
                break;
            case 3:
                PowerUpComon();
                playerStats.Attack.AddModifier(new Goddess.PlayerStat.Stat(20, Goddess.PlayerStat.StatType.Flat));
                break;
            case 4:
                PowerUpComon();
                playerStats.CritRate.AddModifier(new Goddess.PlayerStat.Stat(1, Goddess.PlayerStat.StatType.Flat));
                break;
            case 5:
                PowerUpComon();
                playerStats.CritDamage.AddModifier(new Goddess.PlayerStat.Stat(5, Goddess.PlayerStat.StatType.Flat));
                break;
            case 6:
                PowerUpComon();
                playerStats.Dodge.AddModifier(new Goddess.PlayerStat.Stat(1, Goddess.PlayerStat.StatType.Flat));
                break;
        }
    }

    private void PowerUpComon()
    {
        needFresh = true;
        playerStats.Score--;
    }
}

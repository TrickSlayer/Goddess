using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Stats_UI : MonoBehaviour
{
    public List<Stat_UI> statList = new List<Stat_UI>();
    public List<GameObject> buttons = new List<GameObject> ();
    public int Score = 0;
    private int preScore = 0;
    public TextMeshProUGUI ScoreText;
    [HideInInspector] public bool needFresh = true;
    [HideInInspector] PlayerStats playerStats;
    // Start is called before the first frame update
    void Start()
    {
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Score != preScore)
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
            statList[5].setAttribute("Crit Damage: " + playerStats.CritDamage.Value + "%");
            statList[6].setAttribute("Dodge: " + playerStats.Dodge.Value + "%");

            ScoreText.text = "Score: " + Score;

            if(Score == 0)
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

            preScore = Score;
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
        Score--;
    }
}

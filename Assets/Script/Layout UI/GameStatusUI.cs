using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStatusUI : MonoBehaviour
{
    int hours = 0;
    int minutes = 0;
    int seconds = 0;
    public TextMeshProUGUI message;

    public void SetTime(float time)
    {
        float timeRaw = time;
        hours = (int)(timeRaw / (60 * 60));
        timeRaw = (int)(timeRaw % (60 * 60));
        minutes = (int)(timeRaw / 60);
        seconds = (int)(timeRaw % 60);

        string content = $"Time completed: {hours}:{minutes}:{seconds}";
        message.text = content;
    }

    public void ResetGame()
    {
        Time.timeScale = 1f;
        SaveSystem.DeletePlayerSave();
        SaveSystem.DeleteTimerSave();
        GameManager.instance.Restart();

        StartCoroutine(Revival_UI.instance.LoadScreenRevival());

        gameObject.SetActive(false);
    }
}

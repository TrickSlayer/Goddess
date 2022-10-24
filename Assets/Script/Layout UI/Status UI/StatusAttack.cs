using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StatusAttack : MonoBehaviour
{

    public TextMeshProUGUI status;

    public static StatusAttack instance;
    public float timeShow = 5f;
    public float countDown = 0;

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

    public void ShowMess(string mess, Color color)
    {
        StartCoroutine(MessAsync(mess, color));
    }

    public IEnumerator MessAsync(string mess, Color color)
    {
        status.text = mess;
        status.color = color;

        countDown = timeShow;

        while (countDown > 0)
        {
            countDown -= Time.deltaTime;
            yield return null;
        }

        status.text = "";
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LoadLevel : MonoBehaviour
{
    public string sLevelToLoad;

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if(collision.gameObject.tag == "Player")
        {
            GameObject collisionGameObject = collision.gameObject;
            StartCoroutine(LoadScreen(sLevelToLoad));
        }
            
    }

    public static IEnumerator LoadScreen(string name)
    {
        var asyncLoadLevel = SceneManager.LoadSceneAsync(name, LoadSceneMode.Single);
        
        while (!asyncLoadLevel.isDone)
        {
            Debug.Log("Loading the Scene");
            yield return null;
        }

    }

    public static IEnumerator LoadScreen(int index)
    {
        var asyncLoadLevel = SceneManager.LoadSceneAsync(index, LoadSceneMode.Single);

        while (!asyncLoadLevel.isDone)
        {
            Debug.Log("Loading the Scene");
            yield return null;
        }

    }
}

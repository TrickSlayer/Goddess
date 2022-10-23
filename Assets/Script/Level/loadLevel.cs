using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LoadLevel : MonoBehaviour
{
    public int iLevelToLoad;
    public string sLevelToLoad;

    public bool useIntegerToLoadLevel = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {

        Debug.Log(collision.gameObject.tag);

        if(collision.gameObject.tag == "Player")
        {
            GameObject collisionGameObject = collision.gameObject;
            LoadScene();
        }
            
    }
    void LoadScene()
    {
        if (useIntegerToLoadLevel)
        {
            SceneManager.LoadScene(iLevelToLoad);
        }
        else
        {
            SceneManager.LoadScene(sLevelToLoad);
        }
    }
}

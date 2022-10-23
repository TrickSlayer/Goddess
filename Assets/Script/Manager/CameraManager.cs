using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class CameraManager : MonoBehaviour
{
    public static CameraManager instance;

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

    }

    public void AddContraintCamera()
    {
        GameObject follower = gameObject.transform.GetChild(1).gameObject;
        GameObject background = GameObject.FindGameObjectWithTag("Background");
        

        CinemachineConfiner cinemachine = follower.GetComponent<CinemachineConfiner>();
        cinemachine.m_BoundingShape2D = background.GetComponent<Collider2D>();
    }

}

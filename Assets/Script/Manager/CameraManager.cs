using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager instance;
    public GameObject Camera;
    private Camera cam;
    private BoxCollider2D camBox;
    private float sizeX, sizeY, ratio;

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

    private void Start()
    {
        cam = Camera.GetComponent<Camera>();
        camBox = Camera.GetComponent<BoxCollider2D>();
        sizeY = cam.orthographicSize * 2;
        ratio = (float)Screen.width / (float)Screen.height;
        sizeX = sizeY * ratio;
        camBox.size = new Vector2(sizeX, sizeY);
    }

    private void Update()
    {
    }

    public void AddContraintCamera()
    {
        GameObject follower = gameObject.transform.GetChild(1).gameObject;
        GameObject background = GameObject.FindGameObjectWithTag("Background");
        

        CinemachineConfiner cinemachine = follower.GetComponent<CinemachineConfiner>();
        cinemachine.m_BoundingShape2D = background.GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
    }

}

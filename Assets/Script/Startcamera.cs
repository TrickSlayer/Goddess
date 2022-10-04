using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Startcamera : MonoBehaviour
{
    // Start is called before the first frame update
    private Transform Player;
    private float minX = -13.9f, maxX = 5.3f;
    void Start()
    {
        Player = GameObject.FindWithTag("Player").transform;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Player != null)
        {
            Vector3 position = transform.position;
            position.x = Player.position.x; 
            if (position.x < minX) position.x = minX;
            if(position.x > maxX) position.x = maxX;
            transform.position = position;
        }
    }
}

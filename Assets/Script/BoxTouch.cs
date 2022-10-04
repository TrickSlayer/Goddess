using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxTouch : MonoBehaviour
{
    // Start is called before the first frame update
    public float donaycuakhoi = 0.5f;
    public float tocdonay = 4f;
    public bool Duocnay = true;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Player")
        {
            print("da va cham");
        }
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("hitdetect");
    }
}

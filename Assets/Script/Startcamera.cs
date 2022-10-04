using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Startcamera : MonoBehaviour
{
    // Start is called before the first frame update
    private Transform Player;
    private float minX, maxX, minY, maxY;

    public float LimitCamera;

    public GameObject BackGround;
    void Start()
    {
        Player = GameObject.FindWithTag("Player").transform;

        var BgPoint1 = BackGround.transform.TransformPoint(-1, -1, 0);
        var BgPoint2 = BackGround.transform.TransformPoint(1, 1, 0);

        var Point1 = transform.TransformPoint(0, 0, 0);
        var Point2 = transform.TransformPoint(1, 1, 0);
        var camWidth = Point2.x - Point1.x;
        var camHeight = Point2.y - Point1.y;

        minX = BgPoint1.x + camWidth;
        maxX = BgPoint2.x - camWidth;
        minY = BgPoint1.y + camHeight;
        maxY = BgPoint2.y - camHeight;

    }

    // Update is called once per frame
    void Update()
    {
        if (Player != null)
        {
            Vector3 position = transform.position;
            position.x = Player.position.x;
            position.y = Player.position.y;

            if (position.x < minX) position.x = minX;
            if (position.x > maxX) position.x = maxX;
            if (position.y < minY) position.y = minY;
            if (position.y > maxY) position.y = maxY;
            transform.position = position;

        }
    }
}

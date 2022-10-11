using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class EmptyObject : MonoBehaviour
{
    Vector3 target;
    // Start is called before the first frame update

    void Start()
    {
        target = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) { 

            target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            target.z = transform.position.z;

            transform.position = Vector3.MoveTowards(transform.position, target, 100);

            /* EmptyObject empObj = empty.GetComponent<EmptyObject>();
             empObj.changePosition(position);*/
        }
        Debug.Log(gameObject.transform.position);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Destroy(gameObject);
    }
}

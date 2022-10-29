using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour
{
    private float time = 0.5f;
    private float countdown = 0;

    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (countdown <= 0 && gameObject.activeInHierarchy)
        {
            countdown = time;
        }
    }

    private void FixedUpdate()
    {
        if (countdown > 0)
        {
            countdown -= Time.fixedDeltaTime;
            if(countdown <= 0)
            {
                gameObject.SetActive(false);
            }
        }
    }
}

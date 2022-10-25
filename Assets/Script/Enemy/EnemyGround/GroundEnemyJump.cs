using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundEnemyJump : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            GameObject parent = gameObject.transform.parent.gameObject;
            Rigidbody2D rg = parent.GetComponent<Rigidbody2D>();
            rg.AddForce(new Vector3(0, 5, 0));
        }
    }
}

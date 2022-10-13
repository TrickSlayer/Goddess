using UnityEngine;

[RequireComponent(typeof(Item))]
public class Collectable : MonoBehaviour
{
    [HideInInspector] bool clone = false;

    private bool onGround = false;
    Rigidbody2D Rigidbody2D;
    Item Item;

    private void Awake()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        Item = GetComponent<Item>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer != LayerMask.NameToLayer("Ground"))
        {
            if (clone) return;

            PlayerStats player = collision.gameObject.GetComponent<PlayerStats>();

            if (player)
            {
                clone = true;

                if (player.Health.Value != player.currentHealth && Item.data.recoverHealth != 0)
                {
                    player.OnEquipmentChanged(Item, null);
                } 
                else
                if (player.Mana.Value != player.currentMana && Item.data.recoverMana != 0)
                {
                    player.OnEquipmentChanged(Item, null);
                }
                else
                {
                    PlayerInventory playerInv = collision.gameObject.GetComponent<PlayerInventory>();

                    if (Item != null)
                    {
                        playerInv.inventory.Add(Item);
                    }

                }

                Destroy(this.gameObject);
            }
        }
        else
        {
            onGround = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer != LayerMask.NameToLayer("Ground"))
        {
            onGround = false;
        }
    }

    private void Update()
    {
        if (onGround)
        {
            Rigidbody2D.velocity = Vector2.zero;
            Rigidbody2D.gravityScale = 0;
        }

        if (!onGround)
        {
            Rigidbody2D.gravityScale = 1;
        }
    }

}

using UnityEngine;

[RequireComponent(typeof(Item))]
[RequireComponent(typeof(Selectable))]
public class Collectable : MonoBehaviour
{
    [HideInInspector] bool clone = false;

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

                if (player.Health.Value > player.currentHealth && Item.data.recoverHealth != 0)
                {
                    player.OnEquipmentChanged(Item, null);
                } 
                else
                if (player.Mana.Value > player.currentMana && Item.data.recoverMana != 0)
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

                this.gameObject.SetActive(false);
                clone = true;
            }
        }
        else
        {
            Rigidbody2D.velocity = Vector2.zero;
            Rigidbody2D.gravityScale = 0;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            Rigidbody2D.gravityScale = 1;
        }
    }

}

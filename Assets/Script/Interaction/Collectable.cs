using UnityEngine;

[RequireComponent(typeof(Item))]
public class Collectable : MonoBehaviour
{
    [HideInInspector] bool clone = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer != LayerMask.NameToLayer("Ground"))
        {
            if (clone) return;

            PlayerStats player = collision.gameObject.GetComponent<PlayerStats>();

            if (player)
            {
                clone = true;

                if (player.Health.Value != player.currentHealth)
                {
                    player.RecoverHealth(20);
                }
                else
                {
                    PlayerInventory playerInv = collision.gameObject.GetComponent<PlayerInventory>();
                    Item item = GetComponent<Item>();

                    if (item != null)
                    {
                        playerInv.inventory.Add(item);
                    }

                }

                Destroy(this.gameObject);
            }
        }

    }

}

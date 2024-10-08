using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    // chest will open with two loot options for the player
    // the player chooses one of the two and adds it to spell slots

    [SerializeField]
    private SpriteRenderer _sprite;

    [SerializeField]
    private Collider2D _proximitySensor;

    private bool playerInRange;

    public void Start()
    {
        playerInRange = false;
        ClosedChest();
    }

    public void ClosedChest()
    {

        
    }

    public void OpeningChest()
    {

    }

    public void OpenedChest()
    {

    }

    public void LootedChest()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.gameObject != null)
        {
            if (coll.gameObject.GetComponent<PlayerInputHandler>())
            {
                playerInRange = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.gameObject != null)
        {
            if (coll.gameObject.GetComponent<PlayerInputHandler>())
            {
                playerInRange = false;
            }
        }
    }
}

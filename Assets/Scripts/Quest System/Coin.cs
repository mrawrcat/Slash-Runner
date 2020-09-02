using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour, ICollectable
{
    public string itemID { get; set; }


    // Start is called before the first frame update
    void Start()
    {
        itemID = "Coin";
    }

    public void Collect()
    {
        CollectionEvents.CollectableCollected(this);
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            Collect();
        }
    }
}

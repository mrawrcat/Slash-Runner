using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnCollectablesBackOn : MonoBehaviour
{
    public GameObject[] collectables;
    // Start is called before the first frame update
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "CollectableOnDetect")
        {
            for (int i = 0; i< collectables.Length; i++)
            {
                collectables[i].SetActive(true);
            }
            //print("touched turn collectables on detect trigger");
        }
    }
}

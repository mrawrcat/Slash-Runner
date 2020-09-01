using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnCollectablesBackOn : MonoBehaviour
{
    public GameObject[] Objects;
    // Start is called before the first frame update
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "TurnObjectsOn")
        {
            for (int i = 0; i< Objects.Length; i++)
            {
                Objects[i].SetActive(true);
            }
            //print("touched turn collectables on detect trigger");
        }
    }
}

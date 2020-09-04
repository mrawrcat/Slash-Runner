using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformTrnslate : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, new Vector2(0, transform.position.y), 5f * Time.deltaTime);
    }
}

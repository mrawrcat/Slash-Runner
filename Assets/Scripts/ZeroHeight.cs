﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZeroHeight : MonoBehaviour
{
    public Transform targetobj;
    public float offsetY;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector2(transform.position.x, targetobj.position.y + offsetY);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathzoneCamStick : MonoBehaviour
{
    [SerializeField] private Camera cam;

    public Vector2 booga;
    // Start is called before the first frame update
    void Start()
    {
        booga = new Vector2(0, 0.5f);
        cam = Camera.main;
        transform.position = cam.ViewportToWorldPoint(booga);
    }
}

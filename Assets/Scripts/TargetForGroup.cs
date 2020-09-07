using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetForGroup : MonoBehaviour
{
    private Rigidbody2D rb2d;
    public float beginX, endX;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Walk();
    }

    public void Walk()//this script governs the player's ability to walk back to original position when pushed by terrain
    {
        if (transform.position.x > endX)
        {
            rb2d.velocity = new Vector2(-2, rb2d.velocity.y);
        }
        else if (transform.position.x < beginX)
        {
            rb2d.velocity = new Vector2(2, rb2d.velocity.y);
        }
        else
        {
            rb2d.velocity = new Vector2(0, rb2d.velocity.y);
        }
    }
}

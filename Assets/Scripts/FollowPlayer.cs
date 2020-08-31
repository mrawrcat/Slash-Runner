using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform focus;
    public float offset;
    void Update()
    {
        focus.position = new Vector2(focus.position.x, transform.position.y + offset);
    }
}

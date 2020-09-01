using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnim : MonoBehaviour
{
    private Animator anim;
    private PlayerCollisions player_collisions;
    private PlayerJump player_jump;
    private PlayerAttack player_attack;
    private Rigidbody2D rb2d;
    private float velY;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        player_collisions = GetComponent<PlayerCollisions>();
        rb2d = GetComponent<Rigidbody2D>();
        player_attack = GetComponent<PlayerAttack>();
        player_jump = GetComponent<PlayerJump>();
    }

    // Update is called once per frame
    void Update()
    {
        SetAnims();
        velY = rb2d.velocity.y;

    }

    public void SetAnims()//sets animation variables for walk and jump; attack animation is set in PlayerAttack script
    {
        anim.SetFloat("Vertical Velocity", velY);
        anim.SetBool("isGrounded", player_collisions.isGrounded);
        anim.SetBool("holding_atk", player_attack.holding_atk);
        anim.SetFloat("holding_atk_timer", player_attack.holding_atk_timer);
    }
}

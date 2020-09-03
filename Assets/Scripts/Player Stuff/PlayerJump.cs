using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    [Header("Jump Stuff")]
    public float jumpforce;
    public float ground_time;
    public float ground_counter;
    public bool Jump1;
    public bool Jump2;
    public ParticleSystem jump_particle;
    public bool holding_jump;

    private bool groundTouch;
    private Rigidbody2D rb2d;
    private PlayerCollisions player_collisions;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        player_collisions = GetComponent<PlayerCollisions>();
    }

    // Update is called once per frame
    void Update()
    {
        JumpCheck();
        KeyboardInputJump();
    }

    public void Jump()
    {
        SoundManager.sound_manager.jump_sfx.Play();
        rb2d.velocity = Vector2.zero;
        rb2d.AddForce(Vector2.up * jumpforce, ForceMode2D.Impulse);
    }

    public void JumpCheck()
    {
        if (player_collisions.isGrounded)
        {
            if (!groundTouch)
            {
                Play_Land_Particle();
                groundTouch = true;
            }
            Jump1 = true;
            Jump2 = true;
            ground_counter = ground_time;
        }
        else
        {
            if(ground_counter < 0)
            {
                Jump1 = false;
            }
            ground_counter -= Time.deltaTime;
            groundTouch = false;
        }  
    }

    public void Play_Land_Particle()
    {
        jump_particle.Play();
    }

    public void KeyboardInputJump()
    {
        if (Input.GetKey(KeyCode.Z))
        {
            if (!GameManager.manager.movingReset && !GameManager.manager.dead)
            {
                holding_jump = true;
            }
        }
        if (Input.GetKeyUp(KeyCode.Z))
        {
            if (!GameManager.manager.movingReset && !GameManager.manager.dead)
            {
                if (ground_counter > 0 && Jump1)
                {
                    Jump();
                    Jump1 = false;
                }
                else if (!player_collisions.isGrounded && !Jump1 && Jump2)
                {
                    Jump();
                    Jump2 = false;
                }
            }
            holding_jump = false;
        }
    }


}

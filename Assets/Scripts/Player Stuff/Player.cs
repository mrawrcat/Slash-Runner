using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    
    [Header("Touch stuff")]
    public Vector2 startPos;
    public Vector2 direction;
    public bool directionChosen;
    public bool holding_jump;
    public bool holding_atk;
    public float holding_atk_timer;
    
    [Header("Colliders")]
    public BoxCollider2D normal_collider;
    public GameObject revive_collider;
    public GameObject revive_ground;

    [Header("Collision Detection")]
    public Vector2 bottomOffset;
    public Vector2 boxSize;
    public Vector2 inWallOffset;
    public Vector2 inWallBoxsize;
    

    [Header("Status")]
    public bool isGrounded;
    public bool isBashing;
    public bool isUnderSlideWall;
    public bool insideWall;

    [Header("Better Jumping")]
    public float gravity;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;

    [Header("Layers")]
    public LayerMask whatIsGround;
    public LayerMask SlideWall;
    public LayerMask inWall;
    public LayerMask whatIsEnemy;

    [Header("Ability Stuff")]
    public float Atk_Radius;
    public Transform Atk_Pos;

    [Header("Jump Stuff")]
    public float jumpforce;
    public float ground_time;
    public float ground_counter;
    public ParticleSystem jump_particle;
    public bool Jump1;
    public bool Jump2;
    private bool groundTouch;

    public Transform startpos;

    public ObjectPoolNS bullet_pool;
    private bool isFrozen;
    private CamShake shake;
    private float velY;
    private Rigidbody2D rb2d;
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        shake = FindObjectOfType<CamShake>();
        normal_collider = GetComponent<BoxCollider2D>();
        revive_collider.SetActive(false);
        revive_ground.SetActive(false);
        gravity = rb2d.gravityScale;
        isFrozen = false;
    }

    // Update is called once per frame
    void Update()
    {
        velY = rb2d.velocity.y;
        Walk();
        SetAnims();
        IfDead();
        CollisionDetection();
        JumpCheck();
        Standalone_touch();
        KeyboardInput();

        

    }

    private void FixedUpdate()
    {
        BetterJumping();
    }

    public void CollisionDetection()
    {
        insideWall = Physics2D.OverlapBox((Vector2)transform.position + inWallOffset, inWallBoxsize, 0, inWall);
        if (!GameManager.manager.dead && !GameManager.manager.movingReset)
        {
            isGrounded = Physics2D.OverlapBox((Vector2)transform.position + bottomOffset, boxSize, 0, whatIsGround);
            


            if (insideWall)
            {
                normal_collider.isTrigger = true;
                revive_collider.SetActive(true);
                revive_ground.SetActive(true);
            }
            else
            {
                normal_collider.isTrigger = false;
                revive_collider.SetActive(false);
                revive_ground.SetActive(false);
            }

            //if invincible
            if (GameManager.manager.invincible > 0)
            {
                normal_collider.isTrigger = true;
                revive_collider.SetActive(true);
                revive_ground.SetActive(true);
            }
            else 
            {
                if (!insideWall)
                {
                    normal_collider.isTrigger = false;
                    revive_collider.SetActive(false);
                    revive_ground.SetActive(false);
                }
            }
        }

    }
    public void Walk()
    {
        if (transform.position.x > -5)
        {
            rb2d.velocity = new Vector2(-2, rb2d.velocity.y);
        }
        else if (transform.position.x < -5.5)
        {
            rb2d.velocity = new Vector2(2, rb2d.velocity.y);
        }
        else
        {
            rb2d.velocity = new Vector2(0, rb2d.velocity.y);
        }
    }
    public void SetAnims()
    {
        anim.SetFloat("Vertical Velocity", velY);
        anim.SetBool("isGrounded", isGrounded);
        //anim.SetBool("insideWall", insideWall);
        anim.SetBool("holding_atk", holding_atk);
        anim.SetFloat("holding_atk_timer", holding_atk_timer);
    }
    public void Play_Land_Particle()
    {
        jump_particle.Play();
    }
    
    
    
    public void Jump()
    {
        
        //GameManager.manager.jump_sfx.Play();
        rb2d.velocity = Vector2.zero;
        rb2d.AddForce(Vector2.up * jumpforce, ForceMode2D.Impulse);
    }

    public void JumpCheck()
    {
        if (isGrounded)
        {
            if(!groundTouch)
            {
                Play_Land_Particle();
                groundTouch = true;
            }
            Jump1 = true;
            Jump2 = true;
            //anim.ResetTrigger("Attack");
            ground_counter = ground_time;
        }
        else
        {
            Jump1 = false;
            ground_counter -= Time.deltaTime;
            groundTouch = false;
        }

        //jump
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (ground_counter > 0 && !isUnderSlideWall && Jump1)
            {
                Jump();
                Jump1 = false;
            }
            else if(!isGrounded && !Jump1 && Jump2)
            {
                Jump();
                Jump2 = false;
            }
        }
        else if (Input.GetKey(KeyCode.W))
        {
            if (!isGrounded && ground_counter <= 0)
            {
                rb2d.gravityScale = gravity / 6;
            }
        }


    }
    private void IfDead()
    {
        if (GameManager.manager.dead)
        {
            /*
            if (!GameManager.manager.hit_sfx.isPlaying && !GameManager.manager.play_hit_sfx)
            {
                GameManager.manager.hit_sfx.Play();
                GameManager.manager.play_hit_sfx = true;
            }
            
            if(GameManager.manager.gameplay_soundtrack.isPlaying)
            {
                GameManager.manager.gameplay_soundtrack.Stop();
                
            }

            if (transform.position.y <= -10)
            {
                
                if (!GameManager.manager.death_sfx.isPlaying && !GameManager.manager.play_death_sfx)
                {
                    GameManager.manager.death_sfx.Play();
                    GameManager.manager.play_death_sfx = true;
                }
                
                transform.position = new Vector2(-5, -10);
                rb2d.velocity = Vector2.zero;
                GameManager.manager.isUnder = true;
                //rb2d.gravityScale = 0;
            }
            */
        }
        if (GameManager.manager.movingReset)
        {
            /*
            GameManager.manager.play_hit_sfx = false;
            GameManager.manager.play_death_sfx = false;
            if (GameManager.manager.death_sfx.isPlaying)
            {
                GameManager.manager.death_sfx.Stop();
            }

            if (!GameManager.manager.gameplay_soundtrack.isPlaying)
            {
                GameManager.manager.gameplay_soundtrack.Play();
                
            }
            */
            if (rb2d.velocity.y < 0)
            {
                rb2d.velocity = Vector2.zero;
            }

            GameManager.manager.isUnder = false;
            transform.position = Vector2.MoveTowards(transform.position, startpos.position, 5f * Time.deltaTime);

            if (transform.position == startpos.position)
            {
                GameManager.manager.movingReset = false;
                GameManager.manager.invincible = 1f;
                //rb2d.gravityScale = 3;
            }
        }

        if (GameManager.manager.dead || GameManager.manager.movingReset)
        {

            normal_collider.isTrigger = true;

        }
        else
        {
            if (insideWall)
            {
                normal_collider.isTrigger = true;
                revive_collider.SetActive(true);
            }
            else
            {
                normal_collider.isTrigger = false;
                revive_collider.SetActive(false);
            }

            

        }

    }
    public void KeyboardInput()
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
                if (ground_counter > 0 && !isUnderSlideWall && Jump1)
                {
                    Jump();
                    Jump1 = false;
                }
                else if (!isGrounded && !Jump1 && Jump2)
                {
                    Jump();
                    Jump2 = false;
                }
            }
            holding_jump = false;
        }


        if (Input.GetKey(KeyCode.X))
        {
            if (!GameManager.manager.movingReset && !GameManager.manager.dead)
            {
                holding_atk_timer += Time.deltaTime;
                holding_atk = true;
            }
        }
        if (Input.GetKeyUp(KeyCode.X))
        {
            if (!GameManager.manager.movingReset && !GameManager.manager.dead)
            {
                if (holding_atk_timer > 1f)
                {
                    Attack();
                    Shoot_Bullet();
                    Debug.Log("holding power atk");
                }
                else
                {
                    Attack();
                    Debug.Log("regular attack");
                }

                holding_atk_timer = 0;
            }
            holding_atk = false;
        }

    }
    public void Standalone_touch()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            switch (touch.phase)
            {
                // Record initial touch position.
                case TouchPhase.Began:
                    startPos = touch.position;


                    directionChosen = false;

                    holding_jump = false;
                    Debug.Log("touch began");
                    break;

                case TouchPhase.Stationary:

                    if (startPos.x <= Screen.width / 2)
                    {
                        if (!GameManager.manager.movingReset && !GameManager.manager.dead)
                        {
                            holding_jump = true;
                        }
                    }
                    else if (startPos.x > Screen.width / 2)
                    {
                        if (!EventSystem.current.IsPointerOverGameObject(Input.touches[0].fingerId))
                        {
                            if (!GameManager.manager.movingReset && !GameManager.manager.dead)
                            {
                                holding_atk_timer += Time.deltaTime;
                                holding_atk = true;
                            }
                        }

                    }
                    directionChosen = false;


                    Debug.Log("touch stay");
                    break;

                // Determine direction by comparing the current touch position with the initial one.
                case TouchPhase.Moved:
                    direction = touch.position - startPos;

                    break;

                // Report that a direction has been chosen when the finger is lifted.
                case TouchPhase.Ended:
                    if (startPos.x <= Screen.width / 2)
                    {
                        if (!EventSystem.current.IsPointerOverGameObject(Input.touches[0].fingerId))
                        {
                            if (!GameManager.manager.movingReset && !GameManager.manager.dead)
                            {
                                if (ground_counter > 0 && !isUnderSlideWall && Jump1)
                                {
                                    Jump();
                                    Jump1 = false;
                                }
                                else if (!isGrounded && !Jump1 && Jump2)
                                {
                                    Jump();
                                    Jump2 = false;
                                }
                            }

                        }
                    }
                    else if (startPos.x > Screen.width / 2)
                    {
                        if (!EventSystem.current.IsPointerOverGameObject(Input.touches[0].fingerId))
                        {
                            if (!GameManager.manager.movingReset && !GameManager.manager.dead)
                            {
                                if(holding_atk_timer > 1f)
                                {
                                    Attack();
                                    Shoot_Bullet();
                                    Debug.Log("holding power atk");
                                }
                                else
                                {
                                    Attack();
                                    Debug.Log("regular attack");
                                }

                                holding_atk_timer = 0;
                            }
                        }
                    }
                    holding_jump = false;
                    holding_atk = false;
                    directionChosen = true;
                    Debug.Log("touch ended");

                    break;
            }



        }
    }
    
    public void Shoot_Bullet()
    {
        bullet_pool.SpawnProjectile(Atk_Pos);

    }
    public void Attack()
    {
        anim.SetTrigger("Attack");
        Collider2D[] hit_Enemies = Physics2D.OverlapCircleAll(Atk_Pos.position, Atk_Radius, whatIsEnemy);
        foreach (Collider2D enemy in hit_Enemies)
        {
            Debug.Log("we hit: " + enemy.name);
            shake.Shake();
            if (enemy.GetComponent<Enemy>() != null)
            {

                //GameManager.manager.smash_sfx.Play();
                

                if (!isFrozen)
                {
                    StartCoroutine(do_freeze_public(.1f));
                }
                Debug.Log("hit enemy");
                enemy.GetComponent<Enemy>().TakeDmg();
            }
            if (enemy.GetComponent<Barrel>() != null)
            {

                //GameManager.manager.smash_sfx.Play();
                
                if (!isFrozen)
                {
                    StartCoroutine(do_freeze_public(.1f));
                }
                enemy.GetComponent<Barrel>().Take_Dmg();
            }
            if(enemy.GetComponent<EnemyProjectile>() != null)
            {

                //GameManager.manager.smash_sfx.Play();
                if (!isFrozen)
                {
                    StartCoroutine(do_freeze_public(.1f));
                }
                Debug.Log("attacked enemy projectile");
                enemy.GetComponent<EnemyProjectile>().Atk_Projectile();
            }
            if(enemy.GetComponent<IEnemy>() != null)
            {
                enemy.GetComponent<IEnemy>().Die();
            }


        }
    }

    public void BetterJumping()
    {
        if (rb2d.velocity.y < 0)
        {
            if (holding_jump)
            {
                rb2d.gravityScale = gravity / 6;
            }
            else
            {
                rb2d.gravityScale = fallMultiplier * gravity;
            }
        }
        else if (GameManager.manager.movingReset)
        {
            rb2d.gravityScale = 0;
        }
        else
        {
            rb2d.gravityScale = gravity;
        }
    }

    public IEnumerator do_freeze_public(float dur)
    {
        isFrozen = true;
        var original_timescale = Time.timeScale;
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(dur);
        Time.timeScale = original_timescale;
        isFrozen = false;
    }
    public void InstantMove()
    {
        transform.position = startpos.position;
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube((Vector2)transform.position + bottomOffset, boxSize);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube((Vector2)transform.position + inWallOffset, inWallBoxsize);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere((Vector2)Atk_Pos.position, Atk_Radius);
    }
}

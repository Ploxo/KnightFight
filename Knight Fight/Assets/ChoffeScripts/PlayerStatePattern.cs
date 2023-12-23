using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerStatePattern : MonoBehaviour
{
    public GameObject spawnEffect;
    public GameObject particleDashEffect;
    public Transform spawnEffectPosition;
    public Transform DashEffectPosition;
    public ParticleSystem[] onHitVfx = null;

    public Transform crowdParent;
    public PlayerIState currentState;
    [HideInInspector] public GameManager gameManager;
    [HideInInspector] public CameraStatePattern cameraScript;
    [HideInInspector] public CommentatorStatePattern commentatorScript;
    
    public GameObject cameraObject;

    [HideInInspector] public PlayerBasicState basicState;
    [HideInInspector] public PlayerIdleState idleState;
    [HideInInspector] public PlayerDashState dashState;
    [HideInInspector] public PlayerThrowState throwState;
    [HideInInspector] public PlayerAttackState attackState;
    [HideInInspector] public PlayerDeadState deadState;
    [HideInInspector] public PlayerWinState winState;
    public GameObject rightHandGameobject = null;
    public GameObject leftHandGameobject = null;
    public GameObject projectileSpawnPos = null;


    public  float globalCD = 0.5f;
    public float dashCD = 0.2f;
    public float attackCD = 0.5f;
    [HideInInspector]  public  float internalGCDTimer;
    [HideInInspector] public float internalDashTimer;
    [HideInInspector] public float internalAttackTimer;

    public float movementSpeedMultiplier = 35.0f;

    public float dashDuration = 0.1f;
    public float dashSpeed = 500.0f;
    [HideInInspector] public float attackAnimDuration;
    public float fistAnimDuration = 1f;
    public float throwAnimDuration = 0.5f;
    private float movementInputForDashDirThreshhold = 0.15f; 
    public float internalDashRayDist = 1.3f;
    public bool canDash = true;
    [HideInInspector] public bool weaponDestroyed = false;
    public GameObject weapon;
    
    //Fists
    public GameObject leftFist;
    public float fistDamage = 8f;

    
    //tags
    public string weaponTag = "Weapon";
    public string weaponProjectileTag = "WeaponProjectile";
    public string throwableTag = "Throwable";
    public string projectileTag = "Projectile";
    public string environmentTag = "Environment";
    public string playerTag = "Player";
    public string deadPlayerTag = "DeadPlayer";
    public string fistTag = "Fist";

    //values
    [HideInInspector] public Vector2 moveDir;
    Vector2 moveLastDir;
    Vector3 move;
    Vector3 lastMove;
    public float maxHealth = 100f;
    public float health;
    [HideInInspector] public PlayerRagdollHandler ragdollHandler;
    [HideInInspector] public Collider col;
    [HideInInspector] public Rigidbody rb;
    [HideInInspector] public AudioPlayer audioPlayer;
    [HideInInspector] public Animator animator;



    public int UnequippedLayer = 12;
    public int EquippedLayer = 13;
    [SerializeField] private int playerIndex;
    public GameObject spawnPosition;

    private void Awake()
    {
        basicState = new PlayerBasicState(this);
        idleState = new PlayerIdleState(this);
        dashState = new PlayerDashState(this);
        throwState = new PlayerThrowState(this);
        deadState = new PlayerDeadState(this);
        attackState = new PlayerAttackState(this);
        winState = new PlayerWinState(this);

        col = GetComponent<Collider>();
        rb = GetComponent<Rigidbody>();
        gameManager = GameObject.FindObjectOfType<GameManager>();
        cameraScript = cameraObject.GetComponent<CameraStatePattern>();
        commentatorScript = cameraObject.GetComponent<CommentatorStatePattern>();
        audioPlayer = GetComponent<AudioPlayer>();
        animator = GetComponent<Animator>();
        ragdollHandler = GetComponent<PlayerRagdollHandler>();
    }

    public void OnEnable()
    {
        GameObject spawnParticle = Instantiate(spawnEffect, spawnEffectPosition.position, spawnEffectPosition.rotation);
        Destroy(spawnParticle, 3);
        transform.position = spawnPosition.transform.position;
        health = maxHealth;
        tag = playerTag;
        DisableRagdoll();
        currentState = idleState;
        currentState.OnStateEnter();
        internalGCDTimer = globalCD;
        internalDashTimer = dashCD;
        weapon = null;
        Physics.IgnoreLayerCollision(gameObject.layer, UnequippedLayer, false);
        foreach (ParticleSystem particle in onHitVfx)
        {
            particle.Stop();
        }


    }

    public void OnDisable()
    {
        
        GameObject dieParticle = Instantiate(spawnEffect, spawnEffectPosition.position, spawnEffectPosition.rotation);
        Destroy(dieParticle, 3);
        transform.position = spawnPosition.transform.position;
    }

    private void FixedUpdate()
    {
        Ray environmentRay = new Ray(transform.position, lastMove);
        RaycastHit environmentRayHit;

        if (Physics.Raycast(environmentRay, out environmentRayHit, internalDashRayDist))
        {
            if (environmentRayHit.transform.tag == environmentTag)
            {
                canDash = false;
            }
        }
        else
        {
            canDash = true;
        }
    }

    private void Update()
    {
        currentState.UpdateState();
        if (internalGCDTimer < globalCD)
        {
            internalGCDTimer += Time.deltaTime;
        }
        if (internalDashTimer < dashCD)
        {
            internalDashTimer += Time.deltaTime;
        }
        if (internalAttackTimer < attackCD)
        {
            internalAttackTimer += Time.deltaTime;
        }
        if (weaponDestroyed == true)
        {
            RemoveWep();
            weaponDestroyed = false;
        }

    }
    public int GetPlayerIndex()
    {
        return playerIndex;
    }


    private void OnCollisionEnter(Collision collision)
    {
        if(currentState != deadState)
        {
            if (collision.gameObject.tag == weaponProjectileTag)
            {
                OnHit(collision.gameObject.GetComponent<WeaponBaseClass>().thrownDamage);
            }
            if (collision.gameObject.tag == throwableTag)
            {
                OnHit(collision.gameObject.GetComponent<WeaponBaseClass>().damage);
            }
            if (collision.gameObject.tag == projectileTag)
            {
                OnHit(collision.gameObject.GetComponent<ProjectileBase>().damage);
            }
            if(collision.gameObject.tag == weaponTag)
            {
                if (collision.gameObject.layer == UnequippedLayer)
                {
                    if(weapon == null)
                    {
                        PickupItem(collision.gameObject);
                    }
                    
                }
                else if (collision.gameObject.layer == EquippedLayer)
                {

                    OnHit(collision.gameObject.GetComponent<WeaponBaseClass>().damage);
                } 
            }
        }
        if (currentState == dashState)
        {
           StateChanger(idleState);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == fistTag)
        {
            OnHit(fistDamage);
        }
    }

    //Attack                                                       
    public void Attack()
    {
        if(weapon != null)
        {
            weapon.GetComponent<WeaponBaseClass>().Attack();
            internalAttackTimer = 0f;
        }
        else
        {
            audioPlayer.PlayerPunching();
            leftFist.SetActive(true);
        }
    }

    public bool ValidStateChange(PlayerIState newState)
    {
        if (internalGCDTimer >= globalCD)
        {
            if (newState == dashState)
            {
                if (internalDashTimer >= dashCD)
                {
                    return true;
                }
                else
                {
                    Debug.Log("Dash on cooldown");
                    return false;
                }
            }
            if(newState == throwState)
            {
                if(weapon != null)
                {
                    audioPlayer.PlayerThrowing();
                    Debug.Log("throw wep");
                    return true;
                }
                else
                {
                    Debug.Log("nothing to throw");
                    return false;
                }
            }
            if (newState == attackState)
            {
                if (internalAttackTimer >= attackCD)
                {
                    return true;
                }
                else
                {
                    Debug.Log("Attack on cooldown");
                    return false;
                }
            }
            else
            {
                Debug.Log("invalid state");
                return false;
            }
        }
        else
        {
            Debug.Log("Global cooldown!");
            return false;
        }
    }

    public void StateChanger(PlayerIState newState)
    {
        
        if(newState == deadState)
        {
            currentState.ExitState();
            currentState = newState;
            currentState.OnStateEnter();
        }

        else if(newState == idleState || newState == basicState || newState == winState)
        {
            currentState.ExitState();
            currentState = newState;
            currentState.OnStateEnter();
        }
        else if(currentState == idleState || currentState == basicState)
        {
            if (ValidStateChange(newState))
            {
                currentState.ExitState();
                currentState = newState;
                currentState.OnStateEnter();
            }
        }
    }

    public void RunOrIdleDecider()
    {
        if(moveDir == Vector2.zero)
        {
            StateChanger(idleState);
        }
        else
        {
            StateChanger(basicState);
        }
    }

    public void WeaponTypeIdentifier()
    {
        switch (weapon.GetComponent<WeaponBaseClass>().thisWepType)
        {
            case WeaponBaseClass.Weapontype.oneHSword:
                animator.SetBool("1hSword", true);
                animator.SetBool("2hSword", false);
                animator.SetBool("Spellbook", false);
                animator.SetBool("Throwable", false);
                break;
            case WeaponBaseClass.Weapontype.twoHSword:
                animator.SetBool("1hSword", false);
                animator.SetBool("2hSword", true);
                animator.SetBool("Spellbook", false);
                animator.SetBool("Throwable", false);
                break;
            case WeaponBaseClass.Weapontype.spellbook:
                animator.SetBool("1hSword", false);
                animator.SetBool("2hSword", false);
                animator.SetBool("Spellbook", true);
                animator.SetBool("Throwable", false);
                break;
            case WeaponBaseClass.Weapontype.throwable:
                animator.SetBool("1hSword", false);
                animator.SetBool("2hSword", false);
                animator.SetBool("Spellbook", false);
                animator.SetBool("Throwable", true);
                break;
        }
    }

    public void ChangeDirection()
    {

        if (Hypotenuse(moveDir.x, moveDir.y) >= movementInputForDashDirThreshhold)
        {
            move = Vector3.Normalize(new Vector3(moveDir.x, 0.0f, moveDir.y) * Time.deltaTime * movementSpeedMultiplier);
            moveLastDir = moveDir;
        }
        else
        {
            moveDir = Vector2.zero;
        }

        lastMove = Vector3.Normalize(new Vector3(moveLastDir.x, 0.0f, moveLastDir.y) * Time.deltaTime * movementSpeedMultiplier);
        transform.forward = lastMove;
    }

    public void Movement()
    {
        rb.velocity = move * movementSpeedMultiplier;
    }

    public void Dash()
    {
        GameObject DashParticle = Instantiate(particleDashEffect, DashEffectPosition.position, DashEffectPosition.rotation);
        Destroy(DashParticle, 3);
        transform.Translate(lastMove * dashSpeed * Time.deltaTime, Space.World);
    }

    public void ThrowItem()
    {
        weapon.GetComponent<WeaponBaseClass>().ThrowWep();
        RemoveWep();
    }

    public void RemoveWep()
    {
        weapon = null;
        animator.SetBool("1hSword", false);
        animator.SetBool("2hSword", false);
        animator.SetBool("Spellbook", false);
        animator.SetBool("Throwable", false);
        Physics.IgnoreLayerCollision(gameObject.layer, UnequippedLayer, false);
    }

    public void PickupItem(GameObject weaponObject)
    {
        weapon = weaponObject;
        Physics.IgnoreCollision(col, weapon.GetComponent<Collider>(), true);
        Physics.IgnoreLayerCollision(gameObject.layer, UnequippedLayer, true);
        attackAnimDuration = weapon.GetComponent<WeaponBaseClass>().animationDuration;
        weapon.gameObject.layer = EquippedLayer; //läggs här för att inte ske före on collision
        WeaponTypeIdentifier();

        weapon.GetComponent<WeaponBaseClass>().SetParentPlayer(this.gameObject);
        weapon.GetComponent<WeaponBaseClass>().ChangeState(weapon.GetComponent<WeaponBaseClass>().equippedState);
    }

    public void OnHit(float damage)
    {
        commentatorScript.boredCooldownTimer = 0.0f;
        audioPlayer.PlayerHurting();
        gameManager.DecrementCombinedHealth(damage);
        currentState.TakeDamage(damage);
        foreach (ParticleSystem particle in onHitVfx)
        {
            particle.Play();
        }
        Debug.Log("Hello!!?");
        //foreach (ParticleSystem particle in onHitVfx)
        //{
        //    particle.Stop();
        //}
    }

    public void EnableRagdoll()
    {
        ragdollHandler.SetRagdollActive();
        animator.enabled = false;
    }

    public void DisableRagdoll()
    {
        ragdollHandler.SetRagdollInactive();
        animator.enabled = true;
    }

    public void Die()
    {
        StateChanger(deadState);
    }

    private float Hypotenuse(float sideA, float sideB)
    {
        return Mathf.Sqrt(sideA * sideA + sideB * sideB);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class ProjectileBase : MonoBehaviour
{
    public GameObject spawnEffect;

    public ProjectileIState currentState;
    public float damage;
    public float despawnTimerMin = 15f;
    public float despawnTimerMax = 30f;
    //public PlayerStatePattern parentObject = null;
    public GameObject parentObject = null;
    public GameObject player = null;
    public GameObject spellBook;
    public enum ProjectileType { SpellBookProjectile, TreeProjectile };
    public ProjectileType projectileType;
    [HideInInspector] public GameObject Player;
    [HideInInspector] public Transform projectileTransform;
    [HideInInspector] public int i = 1;
    [HideInInspector] public ProjectileFlyingState flyingState;
    [HideInInspector] public ProjectileGroundedState groundedState;
    public Rigidbody rb;
    public AudioProjectile audioPlayer;

    public float ProjectileSpeed = 15f;
    public string playerTag = "Player";

    public void OnDestroy()
    {
        Debug.Log("puff");
        GameObject spawnParticle = Instantiate(spawnEffect, transform.position, Quaternion.identity);
        Destroy(spawnParticle, 3);
    }

    public abstract void LaunchPos(GameObject parent);
    public abstract void StateChanger(ProjectileIState newState);

    public abstract void OnCollisionStay(Collision collision);
    public abstract void OnCollisionEnter(Collision collision);


}

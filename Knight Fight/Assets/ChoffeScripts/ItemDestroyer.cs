using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDestroyer : MonoBehaviour
{
    public WeaponSpawnManager weaponSpawnManager;


    private void OnCollisionEnter(Collision collision)
    {
        if (weaponSpawnManager.activeWeaponsList.Contains(collision.gameObject))
        {
            weaponSpawnManager.activeWeaponsList.Remove(collision.gameObject);
            GameObject.Destroy(collision.gameObject);
        }
    }
}

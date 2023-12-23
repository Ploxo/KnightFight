using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface WeaponIState 
{
    void OnStateEnter();
    void UpdateState();
    void ChangePhysics();
    void CollisionEnter(Collision col);
    void CollisionStay(Collision col);
}

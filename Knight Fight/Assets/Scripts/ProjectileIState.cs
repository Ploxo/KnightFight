using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ProjectileIState
{
    void OnStateEnter();
    //void ChangePhysics();
    void UpdateState();
    void ChangeState(ProjectileIState newState);
    void CollisionStay(Collision col);

    void CollisionEnter(Collision col);

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface PlayerIState
{
    void OnStateEnter();
    void UpdateState();
    void ExitState();
    void TakeDamage(float damage);
}

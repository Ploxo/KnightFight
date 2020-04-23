﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface GameIState
{
    void OnStateEnter();
    void UpdateState();
}

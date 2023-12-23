using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScoreTracker : MonoBehaviour
{
    public int score;

    private void Start()
    {
        score = 0;
    }

    public void IncrementScore()
    {
        score += 1;
    }

    public void ClearScore()
    {
        score = 0;
    }
}

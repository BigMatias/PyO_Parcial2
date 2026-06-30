using System;
using System.Collections.Generic;
using UnityEngine;

public class GameStateChecker : MonoBehaviour
{
    [SerializeField] private GameObject[] players;
    [SerializeField] private GameObject[] enemies;

    public event Action OnVictory;
    public event Action OnDefeat;

    public void CheckGameState()
    {
        if (AnyPlayerDead())
        {
            OnDefeat?.Invoke();
            Debug.Log("Lost");
            return;
        }

        if (AllEnemiesDead())
        {
            Debug.Log("Won");
            OnVictory?.Invoke();
        }
    }

    private bool AnyPlayerDead()
    {
        foreach (GameObject p in players)
        {
            Character c = p.GetComponent<Character>();
            if (!c.IsAlive) return true;
        }
        return false;
    }

    private bool AllEnemiesDead()
    {
        foreach (GameObject e in enemies)
        {
            Character c = e.GetComponent<Character>();
            if (c.IsAlive) return false;
        }
        return true;
    }
}
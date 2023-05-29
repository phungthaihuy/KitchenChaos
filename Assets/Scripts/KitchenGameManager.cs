using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class KitchenGameManager : MonoBehaviour
{
    public static KitchenGameManager Instance { get; private set; }
    public event EventHandler OnStateChanged; 

    private enum State
    {
        waitingToStart,
        gamePlaying,
        gameOver,
    }
    private State state;
    private float waitingToStartTimer = 3f;
    private float gamePlayingTimerMax = 180f;
    private float gamePlayingTimer;

    private void Awake()
    {
        Instance = this;
        state = State.waitingToStart;
    }

    public bool IsGamePlaying()
    {
        return state == State.gamePlaying;
    }

    public bool IsWaitingToStart()
    {
        return state == State.waitingToStart;
    }

    public bool IsGameOver()
    {
        return state == State.gameOver;
    }

    public float GetWaitingToStartTimer()
    {
        return waitingToStartTimer;
    }

    public float GetGamePlayingTimer()
    {
        return 1 - (gamePlayingTimerMax / gamePlayingTimer);
    }

    private void Update()
    {
        switch (state)
        {
            case State.waitingToStart:
                waitingToStartTimer -= Time.deltaTime;
                if (waitingToStartTimer < 0f)
                {
                    state = State.gamePlaying;
                    gamePlayingTimer = gamePlayingTimerMax;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.gamePlaying:
                gamePlayingTimerMax -= Time.deltaTime;
                
                if (gamePlayingTimerMax < 0f)
                {
                    state = State.gameOver;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.gameOver:
                break;
        }
    }
}

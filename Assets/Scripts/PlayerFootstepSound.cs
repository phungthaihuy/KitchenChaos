using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFootstepSound : MonoBehaviour
{
    public static event EventHandler OnFootStep;

    private Playerr playerr;
    private float footstepTimer;
    private float footstepTimerMax = .1f;

    private void Awake()
    {
        playerr = GetComponent<Playerr>();
    }

    private void Update()
    {
        footstepTimer -= Time.deltaTime;
        if (footstepTimer < 0)
        {
            footstepTimer = footstepTimerMax;
            if (playerr.IsWalking())
            {
                OnFootStep?.Invoke(this, EventArgs.Empty);
            }
            
        }
    }
}

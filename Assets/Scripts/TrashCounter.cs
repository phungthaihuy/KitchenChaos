using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TrashCounter : BaseCounter
{
    public static event EventHandler OnTrash;

    public override void Interact(Playerr playerr)
    {
        if (playerr.HasKitchenObject())
        {
            OnTrash?.Invoke(this, EventArgs.Empty);
            playerr.GetKitchenObject().DestroyKitchenObject();
        }
        else
        {
            Debug.Log("didn't have any kitchen object");
        }
    }
}

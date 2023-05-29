using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BaseCounter : MonoBehaviour, IKitchenObjectParent
{

    [SerializeField] private Transform kitchenObjectHoldPoint;

    public static event EventHandler OnDropSth;
        
    private KitchenObject kitchenObject;

    public virtual void Interact(Playerr playerr) { }

    public virtual void InteractAlternate(Playerr playerr) { }
    public Transform GetKitchenObjectFollower()
    {
        return kitchenObjectHoldPoint;
    }

    public void ClearKitchenObject() 
    {
        kitchenObject = null;
    }

    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;

        if (this.kitchenObject != null)
        {
            OnDropSth?.Invoke(this, EventArgs.Empty);
        }
    }
    public KitchenObject GetKitchenObject()
    {
        return kitchenObject;
    }

    public bool HasKitchenObject()
    {
        return kitchenObject != null;
    }
}

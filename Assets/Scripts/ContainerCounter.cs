using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounter : BaseCounter
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    public event EventHandler OnPlayerGrabbed;
    public override void Interact(Playerr playerr)
    {
        if (!HasKitchenObject())
        {
            if (!playerr.HasKitchenObject())
            {
                Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab, GetKitchenObjectFollower());
                kitchenObjectTransform.localPosition = Vector3.zero;
                kitchenObjectTransform.GetComponent<KitchenObject>().SetKitchenObjectParent(playerr);

                OnPlayerGrabbed?.Invoke(this, EventArgs.Empty);
            }
            //kitchenObject = kitchenObjectTransform.GetComponent<KitchenObject>(); //dinh nghia kitchenObject, setParent cho kitchenObject, chi dc spawn tren Counter 1 lan.
            //kitchenObject.SetKitchenObjectParent(this);
        }
       
    }
}

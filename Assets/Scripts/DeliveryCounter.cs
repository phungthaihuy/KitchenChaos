using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryCounter : BaseCounter
{
    public static DeliveryCounter Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public override void Interact(Playerr playerr)
    {
        if (playerr.HasKitchenObject())
        {
            if (playerr.GetKitchenObject().TryGetPlate(out PlatesKitchenObject platesKitchenObject))
            {
                DeliveryManager.Instance.DeliverRecipe(platesKitchenObject);

                playerr.GetKitchenObject().DestroyKitchenObject();
            }
        }
    }
}

using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class PlatesCounter : BaseCounter
{
    [SerializeField] KitchenObjectSO platesKitchenObjectSO;

    public event EventHandler OnPlatesVisual;
    public event EventHandler OnRemovePlate;
    
    private float platesSpawnedTime;
    private float platesSpawnedTimeMax = 4f;
    private int platesCountAmount;
    private int platesCountAmountMax = 4;


    private void Update()
    {
        platesSpawnedTime += Time.deltaTime;
        
            if (platesSpawnedTime > platesSpawnedTimeMax)
            {
                platesSpawnedTime = 0;

                if (platesCountAmount < platesCountAmountMax)
                {
                    platesCountAmount++;

                    OnPlatesVisual?.Invoke(this, EventArgs.Empty);
                }
                
            }
        
        
    }

    public override void Interact(Playerr playerr)
    {
        if (!playerr.HasKitchenObject())
        {
            if (platesCountAmount > 0)
            {
                platesCountAmount--;

                Transform kitchenObjectTransform = Instantiate(platesKitchenObjectSO.prefab, GetKitchenObjectFollower());
                kitchenObjectTransform.localPosition = Vector3.zero;
                kitchenObjectTransform.GetComponent<KitchenObject>().SetKitchenObjectParent(playerr);

                OnRemovePlate?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}

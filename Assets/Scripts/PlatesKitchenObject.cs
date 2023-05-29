using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlatesKitchenObject : KitchenObject
{
    [SerializeField] private List<KitchenObjectSO> validKitchenObjectSOList;

    public event EventHandler<OnIngtedientAddEventArgs> OnIngtedientAdd;
    public class OnIngtedientAddEventArgs : EventArgs
    {
        public KitchenObjectSO kitchenObjectSO;
    }

    private List<KitchenObjectSO> kitchenObjectsSOList;

    private void Awake()
    {
        kitchenObjectsSOList = new List<KitchenObjectSO>();
    }

    public bool TryAddIngredient(KitchenObjectSO kitchenObjectSO)
    {
        
        if (!validKitchenObjectSOList.Contains(kitchenObjectSO)) //kiem tra kiechenObject co trung voi validKitchenObject hay khong
        {
            return false;
        }

        if (kitchenObjectsSOList.Contains(kitchenObjectSO)) // khong lay 1 kitchenObject 2 lan
        {
            return false;
            
        }
        else
        {
            kitchenObjectsSOList.Add(kitchenObjectSO);
            OnIngtedientAdd?.Invoke(this, new OnIngtedientAddEventArgs
            {
                kitchenObjectSO = kitchenObjectSO
            });
            return true;
            
        }
    }

    public List<KitchenObjectSO> GetkitchenObjectsSOList()
    {
        return kitchenObjectsSOList;
    }
}

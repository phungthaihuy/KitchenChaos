using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlateCompleteVisual : MonoBehaviour
{
    [Serializable]
    public struct KitchenObject_GameObject
    {
        public KitchenObjectSO kitchenObjectSO;
        public GameObject gameObject;
    }
    [SerializeField] private PlatesKitchenObject platesKitchenObject;
    [SerializeField] private List<KitchenObject_GameObject> listKitchenObject_GameObjects;
    private void Start()
    {
        platesKitchenObject.OnIngtedientAdd += PlatesKitchenObject_OnIngtedientAdd;
        foreach (KitchenObject_GameObject kitchenObject_GameObject in listKitchenObject_GameObjects)
        {
            kitchenObject_GameObject.gameObject.SetActive(false);
        }
    }

    private void PlatesKitchenObject_OnIngtedientAdd(object sender, PlatesKitchenObject.OnIngtedientAddEventArgs e)
    {
        foreach (KitchenObject_GameObject kitchenObject_GameObject in listKitchenObject_GameObjects)
        {
            if (kitchenObject_GameObject.kitchenObjectSO == e.kitchenObjectSO)
            {
                kitchenObject_GameObject.gameObject.SetActive(true);
            }
        }
    }
}

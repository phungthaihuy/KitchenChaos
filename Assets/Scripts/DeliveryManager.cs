using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManager : MonoBehaviour
{
    public static DeliveryManager Instance { get; private set; }

    public event EventHandler OnrecipeSpawned;
    public event EventHandler OnRecipeCompleted;
    public event EventHandler OnDeliverySuccess;
    public event EventHandler OnDeliveryFailed;

    [SerializeField] private MenuListSO menuListSO;

    
    private List<MainDishSO> waitingMainDishSOList;
    private float spawnMainDishTimer;
    private float spawnMainDishTimerMax = 4f;
    private int  waitingMainDishMax = 4;
    private int recipeDelivered;



    private void Awake()
    {
        Instance = this;

        waitingMainDishSOList = new List<MainDishSO>();
    }

    private void Update()
    {
        spawnMainDishTimer -= Time.deltaTime;
        if (spawnMainDishTimer <= 0f)
        {
            spawnMainDishTimer = spawnMainDishTimerMax;

            if (waitingMainDishSOList.Count < waitingMainDishMax)
            {
                MainDishSO waitingMainDishSO = menuListSO.mainDishSOList[UnityEngine.Random.Range(0, menuListSO.mainDishSOList.Count)]; //spawn a main dish
                waitingMainDishSOList.Add(waitingMainDishSO);

                OnrecipeSpawned?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public void DeliverRecipe(PlatesKitchenObject platesKitchenObject)
    {
        for (int i = 0; i < waitingMainDishSOList.Count; i++)
        {
            MainDishSO waitingMainDishSO = waitingMainDishSOList[i];
            if (waitingMainDishSO.kitchenObjectsSOList.Count == platesKitchenObject.GetkitchenObjectsSOList().Count)
            {
                // has the same number
                bool plateKitchenObjectMatchesMainDish = true;
                foreach (KitchenObjectSO mainDishKitchenObjectSO in waitingMainDishSO.kitchenObjectsSOList)
                {
                    bool ingredientFound = false;
                    foreach (KitchenObjectSO plateKitchenObjectSO in platesKitchenObject.GetkitchenObjectsSOList())
                    {
                        if (plateKitchenObjectSO == mainDishKitchenObjectSO)
                        {
                            ingredientFound = true;
                            break;
                        }
                    }
                    if (!ingredientFound)
                    {
                        //ingredient khong tim thay trong dia~
                        plateKitchenObjectMatchesMainDish = false;
                    }
                }
                if (plateKitchenObjectMatchesMainDish)
                {
                    // player delivery correct kitchenObject on maindish
                    waitingMainDishSOList.RemoveAt(i);
                    OnDeliverySuccess?.Invoke(this, EventArgs.Empty);
                    OnRecipeCompleted?.Invoke(this, EventArgs.Empty);
                    recipeDelivered++;
                    return;
                }

            }
        }

        OnDeliveryFailed?.Invoke(this, EventArgs.Empty);
    }

    public List<MainDishSO> GetWaitingMainDishSO()
    {
        return waitingMainDishSOList;
    }

    public int GetRecipeDelivered()
    {
        return recipeDelivered;
    }
}

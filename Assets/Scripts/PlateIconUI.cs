using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateIconUI : MonoBehaviour
{
    [SerializeField] private PlatesKitchenObject platesKitchenObject;
    [SerializeField] private Transform iconTemplate;

    private void Awake()
    {
        iconTemplate.gameObject.SetActive(false);
    }
    private void Start()
    {
        platesKitchenObject.OnIngtedientAdd += PlatesKitchenObject_OnIngtedientAdd;
    }

    private void PlatesKitchenObject_OnIngtedientAdd(object sender, PlatesKitchenObject.OnIngtedientAddEventArgs e)
    {
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        foreach (Transform child in this.transform)
        {
            if (child.transform == iconTemplate)
            {
                continue;
            }
            Destroy(child.gameObject);
        }
        foreach (KitchenObjectSO kitchenObjectSO in platesKitchenObject.GetkitchenObjectsSOList())
        {
            Transform iconTransform = Instantiate(iconTemplate, this.transform);
            iconTransform.gameObject.SetActive(true);
            iconTransform.GetComponent<PlateIconSingleUI>().SetKitchenObjectSO(kitchenObjectSO);
            
        }
    }
}

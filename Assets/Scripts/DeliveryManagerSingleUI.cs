using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DeliveryManagerSingleUI : MonoBehaviour
{
    public static DeliveryManagerSingleUI Instance { get; private set; }
    [SerializeField] private TextMeshProUGUI recipeNameText;
    [SerializeField] private Transform iconContainer;
    [SerializeField] private Transform iconTemplate;

    private void Awake()
    {
        Instance = this;

        iconTemplate.gameObject.SetActive(false);
    }

    public void SetMainDishSO (MainDishSO mainDishSO)
    {
        recipeNameText.text = mainDishSO.mainDishName; 

        foreach (Transform child in iconContainer)
        {
            if (child == iconTemplate) continue;
            Destroy(child.gameObject);
        }

        foreach (KitchenObjectSO kitchenObjectSO in mainDishSO.kitchenObjectsSOList)
        {
            Transform iconTransform = Instantiate(iconTemplate, iconContainer);
            iconTransform.gameObject.SetActive(true);
            iconTransform.GetComponent<Image>().sprite = kitchenObjectSO.sprite;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DeliveryResultsUI : MonoBehaviour
{
    [SerializeField] private Image backgroundImage;
    [SerializeField] private Image iconImage;
    [SerializeField] private TextMeshProUGUI messengerText;
    [SerializeField] private Color successColor;
    [SerializeField] private Color failedColor;
    [SerializeField] private Sprite successSprite;
    [SerializeField] private Sprite failedSprite;

    private float showTimerMax = 1f;

    private void Start()
    {
        DeliveryManager.Instance.OnDeliverySuccess += Instance_OnDeliverySuccess;
        DeliveryManager.Instance.OnDeliveryFailed += Instance_OnDeliveryFailed;

        gameObject.SetActive(false);
    }

    private void Update()
    {
        if (backgroundImage.color == failedColor && iconImage.sprite == failedSprite && messengerText.text == "DELIVERY\nFAILED")
        {
            showTimerMax -= Time.deltaTime;
            if (showTimerMax <= 0f)
            {
                gameObject.SetActive(false);
            }
        }

        if (backgroundImage.color == successColor && iconImage.sprite == successSprite && messengerText.text == "DELIVERY\nSUCCESS")
        {
            showTimerMax -= Time.deltaTime;
            if (showTimerMax <= 0f)
            {
                gameObject.SetActive(false);
            }
        }
    }

    private void Instance_OnDeliveryFailed(object sender, System.EventArgs e)
    {
        showTimerMax = 1f;
        gameObject.SetActive(true);
        backgroundImage.color = failedColor;
        iconImage.sprite = failedSprite;
        messengerText.text = "DELIVERY\nFAILED";
        
    }

    private void Instance_OnDeliverySuccess(object sender, System.EventArgs e)
    {
        showTimerMax = 1f;
        gameObject.SetActive(true);
        backgroundImage.color = successColor;
        iconImage.sprite = successSprite;
        messengerText.text = "DELIVERY\nSUCCESS";
        
    }

    
}

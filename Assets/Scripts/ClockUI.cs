using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClockUI : MonoBehaviour
{
    [SerializeField] private Image clockFilled;

    private void Update()
    {
        clockFilled.fillAmount = KitchenGameManager.Instance.GetGamePlayingTimer(); 
    }
}

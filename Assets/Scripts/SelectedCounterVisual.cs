using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour
{
    [SerializeField] BaseCounter baseCounter;
    [SerializeField] GameObject[] visualGameobjectArray;
    private void Start()
    {
        Playerr.Instance.OnSelectedCounterChanged += Player_OnSelectedCounterChanged;
    }

    private void Player_OnSelectedCounterChanged(object sender, Playerr.OnSelectedCounterChangedEventArgs e)
    {
        if (e.selectedCounter == baseCounter)
        {
            Show();
        }
        else if (e.selectedCounter != baseCounter)
        {
            Hide();
        } 
    }
    
    private void Show()
    {
        foreach (GameObject visualGameObject in visualGameobjectArray)
        {

            visualGameObject.SetActive(true);
        }
    }
    private void Hide()
    {
        foreach (GameObject visualGameObject in visualGameobjectArray)
        {
            visualGameObject.SetActive(false);
        }
        
    }
}

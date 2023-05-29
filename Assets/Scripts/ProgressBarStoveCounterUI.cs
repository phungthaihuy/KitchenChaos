using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarStoveCounterUI : MonoBehaviour
{
    [SerializeField] StoveCounter stoveCounter;
    [SerializeField] Image barImage;

    private void Start()
    {
        stoveCounter.OnProgressBarChanged += StoveCounter_OnProgressBarChanged;

        barImage.fillAmount = 0f;
        Hide();
    }

    private void StoveCounter_OnProgressBarChanged(object sender, StoveCounter.OnProgressBarChangedEventArgs e)
    {
        barImage.fillAmount = e.progressBarNomalized;
        if (barImage.fillAmount == 0)
        {
            Hide();
        }
        else
        {
            Show();
        }
        
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }
}

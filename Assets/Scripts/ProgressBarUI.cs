using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
    [SerializeField] CuttingCounter cuttingCounter;
    [SerializeField] Image barImage;

    private void Start()
    {
        cuttingCounter.OnProgressBarChanged += CuttingCounter_OnProgressBarChanged;

        barImage.fillAmount = 0f;
        Hide();
    }

    private void CuttingCounter_OnProgressBarChanged(object sender, CuttingCounter.OnProgressBarChangedEventArgs e)
    {
        barImage.fillAmount = e.progressBarNomalized;

        if (barImage.fillAmount == 0 || barImage.fillAmount == 1)
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

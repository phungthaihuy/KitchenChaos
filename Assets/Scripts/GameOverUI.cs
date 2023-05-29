using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI gameoverText;
    [SerializeField] private Button quitButton;

    private void Awake()
    {
        quitButton.onClick.AddListener(QuitGame);
    }

    private void Start()
    {
        KitchenGameManager.Instance.OnStateChanged += Instance_OnStateChanged;

        Hide();
    }

    private void QuitGame()
    {
        Application.Quit();
    }

    private void Instance_OnStateChanged(object sender, System.EventArgs e)
    {
        if (KitchenGameManager.Instance.IsGameOver())
        {
            Show();
            gameoverText.text = DeliveryManager.Instance.GetRecipeDelivered().ToString();
        }
        else
        {
            Hide();
        }
    }

    private void Update()
    {
        
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }
    private void Hide()
    {
        gameObject.SetActive(false);
    }
}

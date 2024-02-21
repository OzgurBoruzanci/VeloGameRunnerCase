using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private GameObject gamePanel;
    [SerializeField] private TextMeshProUGUI cointext;
    [SerializeField] private TextMeshProUGUI endCoinText;
    [SerializeField] private SaveManager saveManager;
    private int coin;
    private int endCoin;
    private void OnEnable()
    {
        EventManager.EarnCoin += EarnCoin;
    }
    private void OnDisable()
    {
        EventManager.EarnCoin -= EarnCoin;
    }
    private void EarnCoin()
    {
        coin++;
        UpdateCoinText();
    }
    private void UpdateCoinText()
    {
        cointext.text = "Coin : " + coin;
    }
    public void Play()
    {
        coin = 0;
        UpdateCoinText();
        panel.SetActive(false);
        gamePanel.SetActive(true);
        GameManager.Instance.PlayGame();
    }
    public void GameOver()
    {
        if (saveManager != null)
        {
            saveManager.CoinValue = coin;
            saveManager.SaveData();
        }
        panel.SetActive(true);
        endCoin = saveManager.LoadData();
        endCoinText.text = "Coin : " + endCoin;
        gamePanel.SetActive(false);
    }
}

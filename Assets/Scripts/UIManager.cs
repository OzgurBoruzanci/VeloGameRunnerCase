using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    public void Play()
    {
        panel.SetActive(false);
        GameManager.Instance.PlayGame();
    }
    public void GameOver()
    { 
        panel.SetActive(true); 
    }
}

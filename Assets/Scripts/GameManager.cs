using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public UnityEvent GameEnd;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameManager>();
                if (_instance == null)
                {
                    GameObject obj = new GameObject("GameManager");
                    _instance = obj.AddComponent<GameManager>();
                }
            }
            return _instance;
        }
    }
    private GameManager() { }
    public void PlayGame()
    {
        EventManager.PlayGame();
    }
    public void GameOver()
    {
        GameEnd.Invoke();
    }
    private void OnDestroy()
    {
        _instance = null;
    }
}

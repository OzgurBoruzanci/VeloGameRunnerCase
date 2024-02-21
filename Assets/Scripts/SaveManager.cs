using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    private string filePath;
    public int CoinValue { get; set; }

    private void Start()
    {
        filePath = Path.Combine(Application.persistentDataPath, "coinData.json");
    }
    public int LoadData()
    {
        if (File.Exists(filePath))
        {
            string jsonData = File.ReadAllText(filePath);
            CoinData loadedData = JsonUtility.FromJson<CoinData>(jsonData);
            CoinValue = loadedData.coinValue;
            return CoinValue;
        }
        else
        {
            CoinValue = 0;
            return CoinValue;
        }
    }
    public void SaveData()
    {
        CoinData data = new CoinData();
        data.coinValue = CoinValue;
        string jsonData = JsonUtility.ToJson(data);
        File.WriteAllText(filePath, jsonData);
    }

    [System.Serializable]
    private class CoinData
    {
        public int coinValue;
    }
}

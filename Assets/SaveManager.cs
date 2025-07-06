using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveManager : MonoBehaviour
{
    private string savePath => Application.persistentDataPath + "/save.json";

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            SaveGame();
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            LoadGame();
        }
    }

    public void SaveGame()
    {
        SaveData data = new SaveData
        {
            playerName = "Aimer",
            score = 100,
            items = new List<string> { "a", "b", "c" }
        };

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(savePath, json);

        Debug.Log("存檔成功"+savePath); 
    }
    public void LoadGame()
    {
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            Debug.Log("載入成功玩家" + data.playerName + "，分數:" + data.score);
        }
        else
        {
            Debug.LogWarning("沒有找到存檔");
        }

    }
}

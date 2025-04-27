using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveSceneUnlocks 
{
    public static void SaveScene(GameDatas gameData)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = GetPath();

        // Tạo một FileStream để lưu dữ liệu
        using (FileStream fs = new FileStream(path, FileMode.Create))
        {
            formatter.Serialize(fs, gameData);
        }

        
    }

    public static GameDatas Load()
    {
        string path = GetPath();

        // Nếu file không tồn tại, tạo dữ liệu mới và lưu
        if (!File.Exists(path))
        {
            
            GameDatas emptyGameData = new GameDatas();
            SaveScene(emptyGameData);
            return emptyGameData;
        }

        // Đọc dữ liệu từ file
        BinaryFormatter formatter = new BinaryFormatter();
        using (FileStream fs = new FileStream(path, FileMode.Open))
        {
            GameDatas data = formatter.Deserialize(fs) as GameDatas;
            Debug.Log("Dữ liệu đã được tải từ: " + path);
            return data;
        }
    }

    // Lấy đường dẫn lưu file
    private static string GetPath()
    {
        return Application.persistentDataPath + "/NCKH.qnd";
    }
}
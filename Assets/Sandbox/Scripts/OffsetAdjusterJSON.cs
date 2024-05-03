using System.IO;
using UnityEngine;

public partial class OffsetAdjuster : MonoBehaviour
{
    private string SavePath => $"{Application.persistentDataPath}/blasterOffsets.json";

    public void SaveOffsets()
    {
        string json = JsonUtility.ToJson(currentOffsets);
        File.WriteAllText(SavePath, json);
    }

    public void LoadOffsets()
    {
        if (File.Exists(SavePath))
        {
            string json = File.ReadAllText(SavePath);
            currentOffsets = JsonUtility.FromJson<BlasterOffsets>(json);
        }
    }
}


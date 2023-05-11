using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;

public class LoadData : EntryLeaf
{
    public static LoadData Instance { get; private set; }

    private string _filePath;
    private bool _gameLoaded = false;

    protected override void AwakeComponent()
    {
        if (Instance == null)
            Instance = this;

        if (!_gameLoaded)
        {
            _filePath = Application.persistentDataPath + "/save.savedata";
            LoadGame();
        } 
    }

    public void LoadGame()
    {
        if (!File.Exists(_filePath))
            return;

        using (var file = File.Open(_filePath, FileMode.Open))
        {
            if (file.Length.Equals(0))
                return;

            BinaryFormatter bf = new BinaryFormatter();
            Save save = (Save)bf.Deserialize(file);

            new WalletModel(save.Coins);
            _gameLoaded = true;
        }

        Debug.Log("Data loaded");
    }

    public void SaveGame()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream fs = new FileStream(_filePath, FileMode.Create);

        Save save;
        if (WalletModel.Instance != null)
        {
            save = new Save(WalletModel.Instance.coins);
            bf.Serialize(fs, save);
        }

        fs.Close();
        Debug.Log("Data saved");
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }
}

[System.Serializable]
public class Save{

    public int Coins { get; private set; }

    public Save(int coins)
    {
        Coins = coins;
    }
}

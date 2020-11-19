using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

[Author(mainAuthor = "Philipp Forstner")]
public static class SaveLoadGame
{
    static string playerPath = Application.persistentDataPath + "/player.data";
    static string levelPath = Application.persistentDataPath + "/levelprogress.data";
    public static void SaveGameData(PlayerStatistics playerData, PlayerAttack playerAttack, LevelManager levelData)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        FileStream stream = new FileStream(playerPath, FileMode.Create);
        PlayerSaveData data = new PlayerSaveData(playerData, playerAttack);
        formatter.Serialize(stream, data);

        stream = new FileStream(levelPath, FileMode.Create);
        LevelSaveData gameProgress = new LevelSaveData(levelData);
        formatter.Serialize(stream, gameProgress);
        stream.Close();
    }

    public static PlayerSaveData LoadPlayer()
    {
        if (File.Exists(playerPath))
        {
            BinaryFormatter formatter = new BinaryFormatter();

            FileStream stream = new FileStream(playerPath, FileMode.Open);
            PlayerSaveData data = (PlayerSaveData)formatter.Deserialize(stream);
            stream.Close();

            return data;
        }
        else
        {
            return null;
        }
    }

    public static LevelSaveData LoadGameProgress()
    {
        if (File.Exists(levelPath))
        {
            BinaryFormatter formatter = new BinaryFormatter();

            FileStream stream = new FileStream(levelPath, FileMode.Open);
            LevelSaveData data = formatter.Deserialize(stream) as LevelSaveData;
            stream.Close();

            return data;
        }
        else
        {
            return null;
        }
    }
}

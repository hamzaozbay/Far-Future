using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem {


    public static void Save(int previousScene, int nextScene, int playerHealth, int spaceshipHealth) {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/save.xd";
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData(previousScene, nextScene, playerHealth, spaceshipHealth);

        formatter.Serialize(stream, data);
        stream.Close();
    }


    public static PlayerData Load() {
        string path = Application.persistentDataPath + "/save.xd";

        if (File.Exists(path)) {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();
            
            return data;
        } 
        else {
            Debug.LogError("Save file not found" + path);
            return null;
        }
    }


}

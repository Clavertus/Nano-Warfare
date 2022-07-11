using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class DataHandler : MonoBehaviour
{
    static public DataHandler instance;
    string path;

    void Awake()
    {
        path = Application.persistentDataPath + "/save.data";

        if(instance == null)
        {
            instance = this;
        } else
        {
            Destroy(gameObject);
        }
    }

    public void SaveData(PlayerData data)
    {
        FileStream stream = new FileStream(path, FileMode.Create);

        BinaryFormatter converter = new BinaryFormatter();
        converter.Serialize(stream, data);

        stream.Close();

        Debug.Log("Data saved to " + path);
    }

    public PlayerData LoadData()
    {
        if (File.Exists(path))
        {
            FileStream stream = new FileStream(path, FileMode.Open);

            BinaryFormatter converter = new BinaryFormatter();
            PlayerData data = converter.Deserialize(stream) as PlayerData;

            Debug.Log("Data loaded from " + path);


            stream.Close();
            return data;

        } else
        {
            Debug.Log("Save data does not exist at " + path);
            return null;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveLoad : MonoBehaviour
{
    public static void Save()
    {
        //StateMachineManager.Instance.data.Save();
        BinaryFormatter bf = new BinaryFormatter();
        List<System.Object> objects = new List<System.Object>();
        FileStream file = File.Create(Application.persistentDataPath + "/localSave.portaler");

        objects.Add(GameState.player);

        bf.Serialize(file, objects);
        file.Close();
    }

    public static void Load()
    {
        //StateMachineManager.Instance.data.Load();
        if (!File.Exists(Application.persistentDataPath + "/localSave.portaler"))
            return;

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath + "/localSave.portaler", FileMode.Open);
        object serializedObject = bf.Deserialize(file);
        List<System.Object> objects = serializedObject as List<System.Object>;

        GameState.player = (Player)objects[0];

        file.Close();
    }
}

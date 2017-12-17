using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveLoad
{
    static ScriptableData data = StateMachineManager.Instance.data;
    static string path = Application.persistentDataPath + "/localSave.portaler";
    public static void Save()
    {

        BinaryFormatter bf = new BinaryFormatter();
        List<System.Object> objects = new List<System.Object>();
        FileStream file = File.Create(path);

        objects.Add(GameState.player);
        objects.Add(data.SaveLoad(Scriptable.weapons, Cmd.Get, objects));
        objects.Add(data.SaveLoad(Scriptable.levels, Cmd.Get, objects));
        objects.Add(SoundManager.Instance.Sound);

        bf.Serialize(file, objects);
        file.Close();
    }

    public static void Load()
    {
        if (!File.Exists(path))
            return;

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(path, FileMode.Open);
        object serializedObject = bf.Deserialize(file);
        List<System.Object> objects = serializedObject as List<System.Object>;

        GameState.player = (Player)objects[0];
        data.SaveLoad(Scriptable.weapons, Cmd.Set, (List<System.Object>)objects[1]);
        data.SaveLoad(Scriptable.levels, Cmd.Set, (List<System.Object>)objects[2]);
        SoundManager.Instance.Sound = (SoundOptions)objects[3];
        file.Close();
    }
}

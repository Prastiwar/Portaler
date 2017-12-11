using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using System.Diagnostics;

public class SaveLoad : MonoBehaviour
{
    public static void Save()
    {
        Stopwatch sw = new Stopwatch();
        sw.Start();

        //StateMachineManager.Instance.data.Save();
        BinaryFormatter bf = new BinaryFormatter();
        List<System.Object> objects = new List<System.Object>();
        FileStream file = File.Create(Application.persistentDataPath + "/localSave.portaler");

        objects.Add(GameState.player);
        objects.Add(StateMachineManager.Instance.data.Get(Scriptable.weapons));
        objects.Add(StateMachineManager.Instance.data.Get(Scriptable.levels));

        bf.Serialize(file, objects);
        file.Close();

        sw.Stop();
        UnityEngine.Debug.Log(sw.ElapsedMilliseconds);
    }

    public static void Load()
    {
        Stopwatch sw = new Stopwatch();
        sw.Start();

        //StateMachineManager.Instance.data.Load();
        if (!File.Exists(Application.persistentDataPath + "/localSave.portaler"))
            return;

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath + "/localSave.portaler", FileMode.Open);
        object serializedObject = bf.Deserialize(file);
        List<System.Object> objects = serializedObject as List<System.Object>;

        GameState.player = (Player)objects[0];
        StateMachineManager.Instance.data.Set(Scriptable.weapons, (List<System.Object>)objects[1]);
        StateMachineManager.Instance.data.Set(Scriptable.levels, (List<System.Object>)objects[2]);

        file.Close();

        sw.Stop();
        UnityEngine.Debug.Log(sw.ElapsedMilliseconds);
    }
}

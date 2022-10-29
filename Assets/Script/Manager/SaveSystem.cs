using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;
using UnityEngine.SceneManagement;

public static class SaveSystem
{

    public static void SavePlayer(PlayerStats stats, PlayerInventory inventory, PlayerMovement movement)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.fun";
        FileStream stream = new FileStream(path, FileMode.Create);

        String map = SceneManager.GetActiveScene().name;
        PlayerData data = new PlayerData(stats, inventory, movement, map);
        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static PlayerData LoadPlayer()
    {
        try
        {
            string path = Application.persistentDataPath + "/player.fun";
            FileStream stream = new FileStream(path, FileMode.Open);
            if (File.Exists(path) && stream.Length > 0)
            {
                BinaryFormatter formatter = new BinaryFormatter();
                PlayerData data = formatter.Deserialize(stream) as PlayerData;
                stream.Close();
                return data;
            }
        } catch (Exception e)
        {
            Debug.Log(e);
            Debug.Log("New Game");
        }
        return null;
    }

    public static void DeletePlayerSave()
    {
        File.Delete(Application.persistentDataPath + "/player.fun");
    }

    public static void DeleteTimerSave()
    {
        File.Delete(Application.persistentDataPath + "/timer.fun");
    }

    [Serializable]
    public class TimeKeeper
    {
        public float time;
        public bool bossDie;

        public TimeKeeper(float time, bool boosDie)
        {
            this.time = time;
            this.bossDie = boosDie;
        }
    }

    public static void SaveTimer(float time, bool bossDie)
    {
        TimeKeeper timeKeeper = new TimeKeeper(time, bossDie);
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/timer.fun";
        FileStream stream = new FileStream(path, FileMode.Create);
        formatter.Serialize(stream, timeKeeper);
        stream.Close();
    }

    public static  (float time ,bool bossDie) LoadTimer()
    {
        try
        {
            string path = Application.persistentDataPath + "/timer.fun";
            FileStream stream = new FileStream(path, FileMode.Open);
            if (File.Exists(path) && stream.Length > 0)
            {
                BinaryFormatter formatter = new BinaryFormatter();
                TimeKeeper data = formatter.Deserialize(stream) as TimeKeeper;
                stream.Close();
                return (data.time, data.bossDie);
            }
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }
        return (0, false);
    }
}

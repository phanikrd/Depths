using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager instance { get; private set; }
    //what we want to save like int, boolean, strings, chars, and arrays.//money
    public int currentCar;
    public int money;
    public bool[] carsUnlocked = new bool[11] {true, false, false, false, false, false, false, false, false, false, false};
    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(gameObject);
        else
            instance = this; 
        DontDestroyOnLoad(gameObject);
        Load();
    }
    public void Load()
    {
        if(File.Exists(Application.persistentDataPath + "/playerInfo.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
            PlayerData_Storage data = (PlayerData_Storage)bf.Deserialize(file);
            currentCar = data.currentCar;
            money = data.money;
            carsUnlocked=data.carsUnlocked;

            if(data.carsUnlocked == null)
            {
                carsUnlocked=new bool[11] { true, false, false, false, false, false, false, false, false, false, false};
            }

            file.Close();

        }
    }

    public void Save()
    {
        BinaryFormatter bf= new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat");
        PlayerData_Storage data = new PlayerData_Storage();

        data.currentCar = currentCar;//save the car details i.e. name.
        data.money= money;
        data.carsUnlocked= carsUnlocked;
        //you can add more data to save here.

        bf.Serialize(file, data);
        file.Close();
    }

}

[Serializable]
class PlayerData_Storage
{
    public int currentCar;
    public int money;
    public bool[] carsUnlocked;
}
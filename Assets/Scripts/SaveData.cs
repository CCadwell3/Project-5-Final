using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;


[System.Serializable]

public class SaveData : MonoBehaviour
{
    //what do we want to save
    public string charName;
    public int charLevel;
    public float experience;
    public float gold;
    public List<GameObject> Inventory;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Save(string filename)
    {
        //filestream requires system.io
        using (FileStream stream = new FileStream(string.Format("{0}/{1}.save", Application.persistentDataPath, filename), FileMode.Create))//create file 
        {
            //IFormatter uses system.runtime.serialization  binaryformatter needs System.Runtime.Serialization.Formatters.Binary
            IFormatter formatter = new BinaryFormatter();//creat binary formatter
            formatter.Serialize(stream, this);//format savefile as binary
        }

    }

    public static SaveData Load(string filename)
    {
        using (FileStream stream = new FileStream(string.Format("{0}/{1}.save", Application.persistentDataPath, filename), FileMode.Open, FileAccess.Read))
        {
            IFormatter formatter = new BinaryFormatter();
            return formatter.Deserialize(stream) as SaveData;
        }
    }

    /*Format for creating saves
     * 
     * 
    SaveData mySave = new SaveData();
    mySave.playerName = "Bob";
    mySave.currentLevel = 10;
    mySave.experience = 1000f;
    mySave.Save("Save 1");

    SaveData loadedSave = SaveData.Load("Save 1");
    Debug.Log(loadedSave.playerName);
    *
    *
    */




}

using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System;

public class DataManager : MonoBehaviour
{
    public List<Quest> currentQuestDataList;
    public List<SubQuest> currenSubQuestDataList;
    public ClearStage currentQuestCount;
    public SaveFileDate saveFileDate;
    public Option currentOptionData;

    private FileInfo fileInfo;

    private int currentSlot;

    public TextAsset DefaultSaveTime;
    public TextAsset DefaultQuestData;
    public TextAsset DefaultSubQuestData;
    public TextAsset DefaultClearStage;

    public void Start()
    {
        string fileName = "DefaultSaveTime";
        string path = Application.persistentDataPath + fileName + ".Json";

        fileInfo = new FileInfo(path);
        if (!fileInfo.Exists)
        {
            saveFileDate = JsonConvert.DeserializeObject<SaveFileDate>(DefaultSaveTime.ToString());
        }

        string timeJson = File.ReadAllText(path);
        saveFileDate = JsonConvert.DeserializeObject<SaveFileDate>(timeJson);
    }

    public void LoadQuestData(int slotNum)
    {
        string fileName = "Quest" + slotNum;
        string path = Application.persistentDataPath + fileName + ".Json";

        fileInfo = new FileInfo(path);
       
        if (!fileInfo.Exists)
        {
            currentQuestDataList = JsonConvert.DeserializeObject<List<Quest>>(DefaultQuestData.ToString());

            currenSubQuestDataList = JsonConvert.DeserializeObject<List<SubQuest>>(DefaultSubQuestData.ToString());
            currentQuestCount = JsonConvert.DeserializeObject<ClearStage>(DefaultClearStage.ToString());
        }
        else
        {
            string json = File.ReadAllText(path);
            currentQuestDataList = JsonConvert.DeserializeObject<List<Quest>>(json);

            fileName = "SubQuest" + slotNum;
            path = Application.persistentDataPath + fileName + ".Json";
            json = File.ReadAllText(path);
            currenSubQuestDataList = JsonConvert.DeserializeObject<List<SubQuest>>(json);

            fileName = "ClearStage" + slotNum;
            path = Application.persistentDataPath + fileName + ".Json";
            json = File.ReadAllText(path);
            currentQuestCount = JsonConvert.DeserializeObject<ClearStage>(json);
        }      
        currentSlot = slotNum;
    }

    public void SaveQuestData()
    {
        string fileName = "Quest" + currentSlot;
        string path = Application.persistentDataPath + fileName + ".Json";
        var setJson = JsonConvert.SerializeObject(currentQuestDataList);
        File.WriteAllText(path, setJson);

        fileName = "SubQuest" + currentSlot;
        path = Application.persistentDataPath + fileName + ".Json";
        setJson = JsonConvert.SerializeObject(currenSubQuestDataList);
        File.WriteAllText(path, setJson);

        fileName = "ClearStage" + currentSlot;
        path = Application.persistentDataPath + fileName + ".Json";
        setJson = JsonConvert.SerializeObject(currentQuestCount);
        File.WriteAllText(path, setJson);

        if (currentSlot == 1)
        {
            saveFileDate.Stage1Time = DateTime.Now.ToString(("MM-dd HH:mm:ss tt"));

            fileName = "DefaultSaveTime";
            path = Application.persistentDataPath + fileName + ".Json";
            setJson = JsonConvert.SerializeObject(saveFileDate);
            File.WriteAllText(path, setJson);
        }
    }

    public void DeleteQuestData(int slotNum)
    {
        string fileName = "Quest" + slotNum;
        string path = Application.persistentDataPath + fileName + ".Json";
        File.Delete(path);

        fileName = "SubQuest" + slotNum;
        path = Application.persistentDataPath + fileName + ".Json";
        File.Delete(path);

        fileName = "ClearStage" + slotNum;
        path = Application.persistentDataPath + fileName + ".Json";
        File.Delete(path);

        fileName = "DefaultSaveTime";
        path = Application.persistentDataPath + fileName + ".Json";
        var setJson = JsonConvert.SerializeObject(saveFileDate);
        File.WriteAllText(path, setJson);
    }
}

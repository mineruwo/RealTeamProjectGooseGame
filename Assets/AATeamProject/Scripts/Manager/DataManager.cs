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
        string path = Application.persistentDataPath + "/" + "AATeamProject" + "/"  +
            "Json" + "/" + fileName + ".Json";

        fileInfo = new FileInfo(path);
        if (!fileInfo.Exists)
        {
            saveFileDate = JsonConvert.DeserializeObject<SaveFileDate>(DefaultSaveTime.ToString());
        }

        string timeJson = File.ReadAllText(path);
        saveFileDate = JsonConvert.DeserializeObject<SaveFileDate>(timeJson);

        Debug.Log($"[DataManager] 받아온 시간{saveFileDate.Stage1Time}");
    }

    public void LoadQuestData(int slotNum)
    {
        string fileName = "Quest" + slotNum;
        string path = Application.persistentDataPath + "/" + "AATeamProject" + "/"  + 
            "Json" + "/" + fileName + ".Json";


        fileInfo = new FileInfo(path);
       
        if (!fileInfo.Exists)
        {
            Debug.Log($"[DataManager] 세이브 데이터 없음");

            currentQuestDataList = JsonConvert.DeserializeObject<List<Quest>>(DefaultQuestData.ToString());
            Debug.Log($"[DataManager] 불러온 파일명 :  {DefaultQuestData.name}");

            currenSubQuestDataList = JsonConvert.DeserializeObject<List<SubQuest>>(DefaultSubQuestData.ToString());
            currentQuestCount = JsonConvert.DeserializeObject<ClearStage>(DefaultClearStage.ToString());


            //fileName = "DefaultQuestData";
            //path = Application.persistentDataPath + "/" + "AATeamProject" + "/"  +
            //"Json" + "/" + fileName + ".Json";
            //string json = File.ReadAllText(path);
            //currentQuestDataList = JsonConvert.DeserializeObject<List<Quest>>(json);
            //Debug.Log($"[DataManager] 불러온 파일명 :  {fileName}");

            //fileName = "DefaultSubQuestData";
            //path = Application.persistentDataPath + "/" + "AATeamProject" + "/"  +
            //"Json" + "/" + fileName + ".Json";
            //json = File.ReadAllText(path);
            //currenSubQuestDataList = JsonConvert.DeserializeObject<List<SubQuest>>(json);

            //fileName = "DefaultClearStage";
            //path = Application.persistentDataPath + "/" + "AATeamProject" + "/"  +
            //"Json" + "/" + fileName + ".Json";
            //json = File.ReadAllText(path);
            //currentQuestCount = JsonConvert.DeserializeObject<ClearStage>(json);
        }
        else
        {
            string json = File.ReadAllText(path);
            currentQuestDataList = JsonConvert.DeserializeObject<List<Quest>>(json);
            Debug.Log($"[DataManager] 불러온 파일명 :  {fileName}");

            fileName = "SubQuest" + slotNum;
            path = Application.persistentDataPath + "/" + "AATeamProject" + "/"  +
            "Json" + "/" + fileName + ".Json";
            json = File.ReadAllText(path);
            currenSubQuestDataList = JsonConvert.DeserializeObject<List<SubQuest>>(json);

            fileName = "ClearStage" + slotNum;
            path = Application.persistentDataPath + "/" + "AATeamProject" + "/"  +
            "Json" + "/" + fileName + ".Json";
            json = File.ReadAllText(path);
            currentQuestCount = JsonConvert.DeserializeObject<ClearStage>(json);
            Debug.Log($"[DataManager] 불러온거 체크 :  {currentQuestCount.stage1}");
        }

        

        currentSlot = slotNum;
    }

    public void SaveQuestData()
    {
        string fileName = "Quest" + currentSlot;
        string path = Application.persistentDataPath + "/" + "AATeamProject" + "/"  +
            "Json" + "/" + fileName + ".Json";
        var setJson = JsonConvert.SerializeObject(currentQuestDataList);
        File.WriteAllText(path, setJson);

        Debug.Log($"[DataManager] 저장한 파일명 : {fileName}");

        fileName = "SubQuest" + currentSlot;
        path = Application.persistentDataPath + "/" + "AATeamProject" + "/"  +
        "Json" + "/" + fileName + ".Json";
        setJson = JsonConvert.SerializeObject(currenSubQuestDataList);
        File.WriteAllText(path, setJson);

        fileName = "ClearStage" + currentSlot;
        path = Application.persistentDataPath + "/" + "AATeamProject" + "/"  +
        "Json" + "/" + fileName + ".Json";
        setJson = JsonConvert.SerializeObject(currentQuestCount);
        File.WriteAllText(path, setJson);

        if (currentSlot == 1)
        {
            saveFileDate.Stage1Time = DateTime.Now.ToString(("MM-dd HH:mm:ss tt"));
            Debug.Log($"[DataManager] 저장한 시간 : {saveFileDate.Stage1Time}");


            fileName = "DefaultSaveTime";
            path = Application.persistentDataPath + "/" + "AATeamProject" + "/"  +
                "Json" + "/" + fileName + ".Json";
            setJson = JsonConvert.SerializeObject(saveFileDate);
            File.WriteAllText(path, setJson);
        }
    }

    public void DeleteQuestData(int slotNum)
    {
        string fileName = "Quest" + slotNum;
        string path = Application.persistentDataPath + "/" + "AATeamProject" + "/"  +
           "Json" + "/" + fileName + ".Json";
        File.Delete(path);

        Debug.Log($"[DataManager] 지운 파일명 : {fileName}");

        fileName = "SubQuest" + slotNum;
        path = Application.persistentDataPath + "/" + "AATeamProject" + "/"  +
            "Json" + "/" + fileName + ".Json";
        File.Delete(path);

        fileName = "ClearStage" + slotNum;
        path = Application.persistentDataPath + "/" + "AATeamProject" + "/"  +
        "Json" + "/" + fileName + ".Json";
        File.Delete(path);

        fileName = "DefaultSaveTime";
        path = Application.persistentDataPath + "/" + "AATeamProject" + "/"  +
            "Json" + "/" + fileName + ".Json";
        var setJson = JsonConvert.SerializeObject(saveFileDate);
        File.WriteAllText(path, setJson);
    }
}

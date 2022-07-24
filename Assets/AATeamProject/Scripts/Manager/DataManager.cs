using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

public class DataManager : MonoBehaviour
{
    public List<Quest> currentQuestDataList;
    public List<SubQuest> currenSubQuestDataList;
    public ClearStage currentQuestCount;
    public Option currentOptionData;

    private FileInfo fileInfo;

    private int currentSlot;

    public void LoadQuestData(int slotNum)
    {
        string fileName = "Quest" + slotNum;
        string path = Application.dataPath + "/" + "AATeamProject" + "/"  + 
            "Json" + "/" + fileName + ".Json";

        fileInfo = new FileInfo(path);

        if (!fileInfo.Exists)
        {
            Debug.Log($"[DataManager] 세이브 데이터 없음");

            fileName = "DefaultQuestData";
            path = Application.dataPath + "/" + "AATeamProject" + "/"  +
            "Json" + "/" + fileName + ".Json";
            string json = File.ReadAllText(path);
            currentQuestDataList = JsonConvert.DeserializeObject<List<Quest>>(json);

            fileName = "DefaultSubQuestData";
            path = Application.dataPath + "/" + "AATeamProject" + "/"  +
            "Json" + "/" + fileName + ".Json";
            json = File.ReadAllText(path);
            currenSubQuestDataList = JsonConvert.DeserializeObject<List<SubQuest>>(json);

            fileName = "DefaultClearStage";
            path = Application.dataPath + "/" + "AATeamProject" + "/"  +
            "Json" + "/" + fileName + ".Json";
            json = File.ReadAllText(path);
            currentQuestCount = JsonConvert.DeserializeObject<ClearStage>(json);
        }
        else
        {
            string json = File.ReadAllText(path);
            currentQuestDataList = JsonConvert.DeserializeObject<List<Quest>>(json);

            fileName = "SubQuest" + slotNum;
            path = Application.dataPath + "/" + "AATeamProject" + "/"  +
            "Json" + "/" + fileName + ".Json";
            json = File.ReadAllText(path);
            currenSubQuestDataList = JsonConvert.DeserializeObject<List<SubQuest>>(json);

            fileName = "ClearStage" + slotNum;
            path = Application.dataPath + "/" + "AATeamProject" + "/"  +
            "Json" + "/" + fileName + ".Json";
            json = File.ReadAllText(path);
            currentQuestCount = JsonConvert.DeserializeObject<ClearStage>(json);
            Debug.Log($"[DataManager] 불러온거 체크 :  {currentQuestCount.stage1}");
        }

        Debug.Log($"[DataManager] 불러온 파일명 :  {fileName}");
        currentSlot = slotNum;
    }

    public void SaveQuestData()
    {
        string fileName = "Quest" + currentSlot;
        string path = Application.dataPath + "/" + "AATeamProject" + "/"  +
            "Json" + "/" + fileName + ".Json";
        var setJson = JsonConvert.SerializeObject(currentQuestDataList);
        File.WriteAllText(path, setJson);

        fileName = "SubQuest" + currentSlot;
        path = Application.dataPath + "/" + "AATeamProject" + "/"  +
        "Json" + "/" + fileName + ".Json";
        setJson = JsonConvert.SerializeObject(currenSubQuestDataList);
        File.WriteAllText(path, setJson);

        fileName = "ClearStage" + currentSlot;
        path = Application.dataPath + "/" + "AATeamProject" + "/"  +
        "Json" + "/" + fileName + ".Json";
        setJson = JsonConvert.SerializeObject(currentQuestCount);
        File.WriteAllText(path, setJson);

        Debug.Log($"[DataManager] 저장한 파일명 : {fileName}");
    }

    public void DeleteQuestData(int slotNum)
    {
        string fileName = "Quest" + slotNum;
        string path = Application.dataPath + "/" + "AATeamProject" + "/"  +
           "Json" + "/" + fileName + ".Json";
        File.Delete(path);

        fileName = "SubQuest" + slotNum;
        path = Application.dataPath + "/" + "AATeamProject" + "/"  +
            "Json" + "/" + fileName + ".Json";
        File.Delete(path);

        fileName = "ClearStage" + slotNum;
        path = Application.dataPath + "/" + "AATeamProject" + "/"  +
        "Json" + "/" + fileName + ".Json";
        File.Delete(path);
    }
}

[System.Serializable]
public class Quest
{
    public int id { get; set; }
    public int stage { get; set; }
    public string questName { get; set; }
    public bool isClear { get; set; }
    public bool isMainQuest { get; set; }
    public int subQuestCount { get; set; }
}

[System.Serializable]
public class SubQuest
{
    public int surQuestId { get; set; }
    public int id { get; set; }
    public int stage { get; set; }
    public string questName { get; set; }
    public bool isClear { get; set; }
}

[System.Serializable]
public class SaveFileDate
{
    public string Stage1Time { get; set; }
    public string Stage2Time { get; set; }
    public string Stage3Time { get; set; }
}

[System.Serializable]
public class ClearStage
{
    public int tutorial { get; set; }
    public int stage1 { get; set; }
    public int stage2 { get; set; }
}

[System.Serializable]
public class Option
{
    public int musicVolume;

    // false : 거위만 true : 거위 + NPC
    public bool isNpcFocus;
}

[System.Serializable]
public class StartPositon
{
    public Transition position1;
    public Transition position2;
}
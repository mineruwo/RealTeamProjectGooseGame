[System.Serializable]
public class Quest
{
    public int id { get; set; }
    public int stage { get; set; }
    public string questName { get; set; }
    public bool isClear { get; set; }
    public bool isMainQuest { get; set; }
}

[System.Serializable]
public class ClearStage
{
    public bool stage1 = false;
    public bool stage2 = false;
}

[System.Serializable]
public class Option
{
    public string language;

    public int musicVolume;
    public int sfxVolume;

    // false : 거위만 true : 거위 + NPC
    public bool isNpcFocus;
}
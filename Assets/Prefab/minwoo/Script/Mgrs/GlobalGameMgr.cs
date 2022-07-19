using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalGameMgr : MonoBehaviour 
{
    public static GlobalGameMgr instance;

    public static GlobalGameMgr Instance
    {
        get
        {
            if (!instance)
            {
                instance = FindObjectOfType(typeof(GlobalGameMgr)) as GlobalGameMgr;

                if (instance == null)
                    Debug.Log("Instance is Null");
            }
            return instance;
        }
    }

    public AudioMgr audioMgr;

    public QuestMgr questMgr;

    public UiMgr uiMgr;

    public OptionMgr optionMgr;

    public DataMgr dataMgr;

    public CameraSystem cameraSystem;

    public SceneMgr sceneMgr;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
      
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

}

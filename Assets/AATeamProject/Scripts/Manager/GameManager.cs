using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
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

    public AudioManager audioMgr;

    public QuestManager questMgr;

    public UiManager uiMgr;

    public OptionManager optionMgr;

    public DataManager dataMgr;

    public ObjectManager objectMgr;

    public ObjectPoolManager objpoolMgr;

    // public CameraSystem cameraSystem;

    public GlobalSceneManager sceneMgr;
}

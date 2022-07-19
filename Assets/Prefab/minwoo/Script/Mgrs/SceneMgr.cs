using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMgr : MonoBehaviour
{
    private float time;

    void Start()
    {
        if (SceneManager.sceneCount == 1)
        {
            StartCoroutine(LoadAsynSceneCoroutine());
        }
    }

    IEnumerator LoadAsynSceneCoroutine()
    {

        AsyncOperation operation = SceneManager.LoadSceneAsync(2);

        operation.allowSceneActivation = false;

        while (!operation.isDone)
        {

            time = +Time.time;

            if (time > 3)
            {
                operation.allowSceneActivation = true;
            }

            yield return null;
        }

        GlobalGameMgr.instance.cameraSystem.SetActive();
    }
}

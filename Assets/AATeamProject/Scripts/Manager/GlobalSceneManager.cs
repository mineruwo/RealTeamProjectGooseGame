using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GlobalSceneManager : MonoBehaviour
{
    private float time;

    void Start()
    {
        //if (SceneManager.sceneCount == 1)
        if(SceneManager.GetActiveScene().buildIndex == 0)
        {
            Debug.Log($"[GlobalSceneManager] ÇöÀç ·Îµù¾À");
            StartCoroutine(LoadAsynSceneCoroutine());
        }
    }

    IEnumerator LoadAsynSceneCoroutine()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(1);

        operation.allowSceneActivation = false;

        while (!operation.isDone)
        {

            time = + Time.time;

            if (time > 2)
            {
                operation.allowSceneActivation = true;
            }

            yield return null;
        }
        //GlobalGameMgr.instance.cameraSystem.SetActive();
    }

    public void LoadGameScene()
    {
        SceneManager.LoadScene(2);
    }

}

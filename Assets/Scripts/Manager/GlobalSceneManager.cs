using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GlobalSceneManager : MonoBehaviour
{
    private float time;

    public void Awake()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            NextScene();
        }
    }

    IEnumerator LoadAsynSceneCoroutine(int curscene)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(curscene + 1);

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

    }

    public void LoadGameScene()
    {
        NextScene();
    }

    public void NextScene()
    {
        var curScene = SceneManager.GetActiveScene().buildIndex;

        StartCoroutine(LoadAsynSceneCoroutine(curScene));
    }
    public void NextScene(int idx)
    {

        StartCoroutine(LoadAsynSceneCoroutine(idx - 1));
    }
}

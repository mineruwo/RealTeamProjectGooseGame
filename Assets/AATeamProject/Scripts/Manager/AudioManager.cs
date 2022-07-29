using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private Dictionary<string, AudioClip> audioClips = new Dictionary<string, AudioClip>();
    public List<AudioClip> asyncCilps = new List<AudioClip>();
    private Dictionary<int, AudioClip> BGMClips = new Dictionary<int, AudioClip>();
    public AudioSource SoundSource;
    public string SFXFilePath = "Audio/SFX/";
    public string BGMFilePath = "Audio/BGM/Bruyeres/";
    private int BGMCurrentSequence = 1;
    public bool isAsync = false;
    [SerializeField]
    private int idx = 0;


    public void BGMPlay()
    {
        string fileName = "Bruyeres_";


        switch (isAsync)
        {
            case true:

                if (asyncCilps.Count == 0)
                {
                    StartCoroutine(LoadBGMAsync($"{BGMFilePath}{fileName}"));
                }


                SoundSource.PlayOneShot(asyncCilps[idx]);

                idx++;

                
                if (idx == asyncCilps.Count)
                {

                    UnloadClips();

                    StartCoroutine(LoadBGMAsync($"{BGMFilePath}{fileName}"));

                }

                break;


            case false:
                if (!BGMClips.ContainsKey(BGMCurrentSequence))
                {
                    //  www = new($"{Application.persistentDataPath}{BGMFilePath}{fileName}{index}.wav");

                    var audio = Resources.Load($"{BGMFilePath}{fileName}{indexConverter(BGMCurrentSequence)}") as AudioClip;

                    BGMClips.Add(BGMCurrentSequence, audio);

                }

                var clip = BGMClips.GetValueOrDefault(BGMCurrentSequence);

                SoundSource.PlayOneShot(clip);

                break;
        }





        if (BGMCurrentSequence > 154)
        {
            BGMCurrentSequence = 0;
        }

    }

    public void BGMStop(string key)
    {

    }

    public AudioClip SFXPlay(string key)
    {
        if (!audioClips.ContainsKey(key))
        {
            // www = new($"{Application.persistentDataPath}{SFXFilePath}{key}.wav");

            //var audio = www.GetAudioClip();

            var audio = Resources.Load($"{SFXFilePath}{key}") as AudioClip;

            audioClips.Add(key, audio);

        }

        var clip = audioClips.GetValueOrDefault(key);

        SoundSource.PlayOneShot(clip);

        return clip;
    }

    private void Update()
    {
        var set = Camera.main.transform.GetComponent<AudioSource>();

        if (set != null)
        {
            SoundSource = set;
        }

    }


    private IEnumerator LoadBGMAsync(string path)
    {

        int count = 0;

        while (count < 10)
        {

            ResourceRequest resourceRequest = Resources.LoadAsync<AudioClip>($"{path}{indexConverter(BGMCurrentSequence)}");

            count++;

            AudioClip audioClip = resourceRequest.asset as AudioClip;

            asyncCilps.Add(audioClip);

            BGMCurrentSequence++;

            yield return resourceRequest;


        }



    }


    private string indexConverter(int idx)
    {
        string index;

        if (idx < 10)
        {
            index = string.Format("{0:D2}", idx);
        }
        else
        {
            index = idx.ToString();
        }

        return index;
    }


    private void UnloadClips()
    {
        for (int i = 0; i < asyncCilps.Count; i++)
        {
            asyncCilps[i].UnloadAudioData();
        }

        asyncCilps.Clear();
        idx = 0;
    }


}

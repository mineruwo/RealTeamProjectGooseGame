using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioMgr : MonoBehaviour
{
    public Dictionary<string,AudioClip> audioClips;
    public AudioSource soundSource;
    public string filePath = "Assets/Resources/Audio/SFX/";
    [System.Obsolete]
    WWW www;

    public void BGMPlay()
    {
        soundSource.Play();
    }

    public void BGMStop()
    {
        soundSource.Stop();
    }

    [System.Obsolete]
    public AudioClip SFXPlay(string key)
    {
        if (!audioClips.ContainsKey(key))
        {
            www = new(Application.persistentDataPath + "Assets/Resources/Audio/SFX/"
                + key + ".wav");

            var audio = www.GetAudioClip();

            audioClips.Add(key, audio);

        }

        var clip = audioClips.GetValueOrDefault(key);

        return clip;
    }

}

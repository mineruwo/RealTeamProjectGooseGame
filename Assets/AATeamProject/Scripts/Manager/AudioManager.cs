using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private Dictionary<string, AudioClip> audioClips = new Dictionary<string, AudioClip>();
    private Dictionary<int, AudioClip> BGMClips = new Dictionary<int, AudioClip>();
    public AudioSource SoundSource;
    public string SFXFilePath = "Assets/Resources/Audio/SFX/";
    public string BGMFilePath = "Assets/Resources/Audio/BGM/Bruyers/";
    private int BGMCurrentSequence = 1;
    private string index;
  

    public void BGMPlay()
    {
        string fileName = "Bruyeres_";

        if (BGMCurrentSequence < 10)
        {
            index = string.Format("{0:D1}", BGMCurrentSequence);
        }
        else
        {
            index = BGMCurrentSequence.ToString();
        }


        if (!BGMClips.ContainsKey(BGMCurrentSequence))
        {
          //  www = new($"{Application.persistentDataPath}{BGMFilePath}{fileName}{index}.wav");

            var audio = Resources.Load("Audio/SFX/" + fileName+index) as AudioClip;

            BGMClips.Add(BGMCurrentSequence, audio);

        }
        var clip = BGMClips.GetValueOrDefault(BGMCurrentSequence);


        SoundSource.PlayOneShot(clip);

        BGMCurrentSequence++;

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

            var audio = Resources.Load("Audio/SFX/" + key) as AudioClip;

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
}

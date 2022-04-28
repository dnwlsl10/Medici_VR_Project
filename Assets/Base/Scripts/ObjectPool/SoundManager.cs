using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    List<GameObject> soundObjects;
    List<AudioClip> audioClips;
    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        soundObjects = new List<GameObject>();
        audioClips = new List<AudioClip>();
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    public void PlaySound(Vector3 position, string soundName)
    {
        GameObject temp = GetSoundObjectNotPlaying(transform.position, soundName);
        temp.GetComponent<AudioSource>().Play();
    }
    public void PlaySound(Vector3 position, string path, string soundName)
    {
        GameObject temp = GetSoundObjectNotPlaying(transform.position, path, soundName);
        temp.GetComponent<AudioSource>().Play();
    }

    public GameObject CreateEmptyObject()
    {
        return new GameObject();
    }
    public GameObject CreateEmptyObject(Vector3 position)
    {
        GameObject createdEmptyGameObject = new GameObject();
        createdEmptyGameObject.transform.position = position;
        return createdEmptyGameObject;
    }
    public GameObject CreateEmptyObject(Vector3 position, string name)
    {
        GameObject createdEmptyGameObject = new GameObject(name);
        createdEmptyGameObject.transform.position = position;
        return createdEmptyGameObject;
    }
    private void AddSoundSourceAtObject(GameObject obj)
    {
        if (obj.GetComponent<AudioSource>() != null)
        {
            return;
        }
        obj.AddComponent<AudioSource>();
    }
    private void AddSoundObjectAtList(GameObject obj)
    {
        if (soundObjects.Contains(obj))
        {
            return;
        }
        soundObjects.Add(obj);
    }
    private GameObject GetSoundObjectNotPlaying(string soundName)
    {
        for (int i = 0; i < soundObjects.Count; i++)
        {
            AudioSource audioSouce = soundObjects[i].GetComponent<AudioSource>();

            if (!audioSouce.isPlaying && audioSouce.clip.name.Contains(soundName))
            {

                return soundObjects[i];
            }
        }
        GameObject emptyObj = CreateEmptyObject();
        AddSoundObjectAtList(emptyObj);
        return emptyObj;
    }
    private GameObject GetSoundObjectNotPlaying(Vector3 position)
    {
        for (int i = 0; i < soundObjects.Count; i++)
        {
            if (!soundObjects[i].GetComponent<AudioSource>().isPlaying)
            {
                return soundObjects[i];
            }
        }
        GameObject emptyObj = CreateEmptyObject(position);
        AddSoundObjectAtList(emptyObj);
        return emptyObj;
    }
    private GameObject GetSoundObjectNotPlaying(Vector3 position, string soundClipName)
    {
        for (int i = 0; i < soundObjects.Count; i++)
        {
            if (!soundObjects[i].GetComponent<AudioSource>().isPlaying && soundObjects[i].GetComponent<AudioSource>().clip.name.Contains(soundClipName))
            {
                return soundObjects[i];
            }
        }
        GameObject emptyObj = CreateEmptyObject(position);
        AddSoundSourceAtObject(emptyObj);
        emptyObj.GetComponent<AudioSource>().clip = GetAudioclip(soundClipName);
        AddSoundObjectAtList(emptyObj);
        return emptyObj;
    }

    private GameObject GetSoundObjectNotPlaying(Vector3 position, string path, string soundClipName)
    {
        for (int i = 0; i < soundObjects.Count; i++)
        {
            if (!soundObjects[i].GetComponent<AudioSource>().isPlaying)
            {
                soundObjects[i].GetComponent<AudioSource>().clip.name.Contains(soundClipName);
                return soundObjects[i];
            }
        }
        GameObject emptyObj = CreateEmptyObject(position);
        AddSoundSourceAtObject(emptyObj);
        emptyObj.GetComponent<AudioSource>().clip = GetAudioclip(path, soundClipName);
        AddSoundObjectAtList(emptyObj);
        return emptyObj;
    }

    private AudioClip GetAudioclip(string soundClipName)
    {
        for (int i = 0; i < audioClips.Count; i++)
        {
            if (audioClips[i].name.Contains(soundClipName))
            {
                return audioClips[i];
            }
        }
        AudioClip clip = FindAudioClipFromResource(soundClipName);
        if (clip == null)
        {
            return null;
        }
        audioClips.Add(clip);
        return clip;
    }

    private AudioClip GetAudioclip(string path, string audioClipName)
    {
        for (int i = 0; i < audioClips.Count; i++)
        {
            if (audioClips[i].name.Contains(audioClipName))
            {
                return audioClips[i];
            }
        }
        AudioClip clip = FindAudioClipFromResource(path, audioClipName);
        if (clip == null)
        {
            return null;
        }
        audioClips.Add(clip);
        return clip;
    }

    private bool SetSoundClipAtSoundSource(GameObject obj, AudioClip audioClip)
    {
        if (obj.GetComponent<AudioSource>() == null)
        {
            return false;
        }
        obj.GetComponent<AudioSource>().clip = audioClip;
        return true;
    }
    private AudioClip FindAudioClipFromResource(string audioClipName)
    {
        return Resources.Load<AudioClip>(audioClipName);
    }
    private AudioClip FindAudioClipFromResource(string path, string audioClipName)
    {
        return Resources.Load<AudioClip>(path + audioClipName);
    }

}
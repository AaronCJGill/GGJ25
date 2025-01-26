using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    int index = 0;
    [SerializeField]
    AudioSource _as;
    [SerializeField]
    List<AudioClip> clipList = new List<AudioClip>();
    public static MusicManager instance;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        Debug.Log("First Song index: "+index);
        index = Random.Range(0, clipList.Count);
        _as.clip = clipList[index];
        _as.Play();
    }
    public void playNextSong()
    {
        index++;
        if (index > clipList.Count)
        {
            index = 0;
            Debug.Log("Resetting list");
        }


        _as.clip = clipList[index];
        _as.Play();
    }

    private void Update()
    {
        if (!_as.isPlaying)
        {
            playNextSong();
        }
    }

}

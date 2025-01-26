using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedBottle : MonoBehaviour
{
    public AudioSource _as;

    public List<AudioClip> PopSoundClips = new List<AudioClip>();
    public List<AudioClip> BottleChangeClips = new List<AudioClip>();
    public List<AudioClip> FizzSoundClips = new List<AudioClip>();
    public List<AudioClip> BottleBreakSound = new List<AudioClip>();


    public void playClip()
    {
        //play sound
        Debug.Log("Need to play glass sound");
        //_as.clip = clips[ Random.Range(0, clips.Count)];
        // _as.Play();
    }
    public void bottleHasBeenReset()
    {
        BottleBehavior.instance.BottleIsReset();
    }

    public void CorkShot()
    {
        //should also play sound
        BottleBehavior.instance.shootCork();
    }

    public void finalizeCorkShot()
    {
        BottleBehavior.instance.finalizeCorkShot();
    }

    public void playBottleChange()
    {

    }

    public void playFizzSound()
    {

    }
}

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
    public List<AudioClip> DrinkingSoundClips = new List<AudioClip>();

    public static AnimatedBottle instance;
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        instance = this;
    }

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
        _as.clip = PopSoundClips[Random.Range(0, PopSoundClips.Count)];
        _as.pitch = Random.Range(0.7f,1.3f);
        _as.Play();
        BottleBehavior.instance.shootCork();
    }

    public void finalizeCorkShot()
    {
        //allow player to shoot
        BottleBehavior.instance.finalizeCorkShot();
    }

    public void playBottleChange()
    {
        //when the bottle is coming up from the bottom
        _as.clip = BottleChangeClips[Random.Range(0, BottleChangeClips.Count)];
        _as.pitch = Random.Range(0.7f, 1.3f);
        _as.Play();
    }

    public void playFizzSound()
    {
        //when the player shoots
        _as.clip = FizzSoundClips[Random.Range(0, FizzSoundClips.Count)];
        _as.pitch = Random.Range(0.7f, 1.3f);
        _as.Play();
    }

    public void playBottleBreak()
    {
        //end of moveleft routine
        _as.clip = BottleBreakSound[Random.Range(0, BottleBreakSound.Count)];
        _as.pitch = Random.Range(0.7f, 1.3f);
        _as.Play();
    }

    public void playDrinkingSound()
    {
        //used in CorkShot animation
        _as.clip = DrinkingSoundClips[Random.Range(0, DrinkingSoundClips.Count)];
        _as.pitch = Random.Range(0.7f, 1.3f);
        _as.Play();
    }
}

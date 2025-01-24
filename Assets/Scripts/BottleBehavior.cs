using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BottleBehavior : MonoBehaviour
{
    [Header("Bottle Mechanics")]
    float powerAmnt = 0;
    [SerializeField]
    float powerIncrement = 0.25f;
    [SerializeField]
    float powerMax = 3.5f;
    [SerializeField]
    float decreaseTimerMax = 3f;
    float decreaseTimer = 0;
    [SerializeField]
    float decreaseDecrement = 0.5f;
    int points = 0;

    

    [Header("In World Objects")]
    [SerializeField]
    Transform close, mid, far;
    [SerializeField]
    TMP_Text timeText;
    [SerializeField]
    TMP_Text pointsText;
    //moves the image down or moves the slider down in response
    [SerializeField]
    Slider fizzSlider;
    [SerializeField]
    GameObject fizzbubbles;

    public static BottleBehavior instance;
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        instance = this;
    }
    private void shootBehavior()
    {
        if (powerAmnt == 3)
        {

        }
    }
    private void shakeBehavior()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            decreaseTimer = 0;
            powerAmnt += powerIncrement;
        }
        if (decreaseTimer >= decreaseTimerMax)
        {
            //goes down at a consistent rate
            powerAmnt -= decreaseDecrement * Time.deltaTime;
        }
    }

    private void reloadBehavior()
    {
        
    }

    private void updateGraphics()
    {
        timeText.text = Time.time.ToString();
        pointsText.text = points.ToString();

    }



    


}

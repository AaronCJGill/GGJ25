using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class BottleBehavior : MonoBehaviour
{
    [Header("Bottle Mechanics")]
    float powerAmnt = 0;
    [SerializeField]
    float powerIncrement = 0.25f;
    [SerializeField]
    float powerMax = 3.5f;
    float decreaseTimerMax = 3f;
    [SerializeField]
    [Tooltip("How much time before the bottle fizz starts to go back down again")]
    float decreaseResetTime = 2;
    [SerializeField]
    [Tooltip("How much fizz is lost when the bottle is powering down")]//high values in this gives us a harder game, but the challenge can be maintained
    float decreaseDecrement = 0.5f;
    int points = 0;
    public float levelTimerMax = 60;
    public float currentLevelTimer = 0;


    [Header("In World Objects")]
    [SerializeField]
    TMP_Text timeText;
    [SerializeField]
    Transform close, mid, far;
    [SerializeField]
    TMP_Text pointsText;
    //moves the image down or moves the slider down in response
    [SerializeField]
    Slider fizzSlider;
    [SerializeField]
    GameObject fizzbubbles;
    [SerializeField]
    GameObject corkObject;
    [SerializeField]
    TMP_Text powertext;

    float shotsLeft = 6;
    List<Transform> liquidPositions = new List<Transform>();//positions of the liquid in the bottle
    bool isEmpty { get { return shotsLeft <= 0; } }
    bool isReloading = false;
    float reloadTimer = 1f; 

    public static BottleBehavior instance;
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        instance = this;
            
        liquidImagey = liquidImage.GetComponent<RectTransform>().anchoredPosition.y;
    }

    private int ammoCounter = 6; //amount of shots you have
    public Image liquidImage;

    private int changeY = 8;
    float liquidImagey;

    public TextMeshProUGUI reloadTXT;
    private void shootBehavior()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // shoot
        {
            if (ammoCounter > 0)
            {
                if (ammoCounter == 1)
                    reloadTXT.text = "RELOAD with SPACE";
                if (powerAmnt >= 3)
                {
                    Instantiate(fizzbubbles, far);
                    liquidImage.GetComponent<RectTransform>().anchoredPosition = 
                        new Vector2(liquidImage.GetComponent<RectTransform>().anchoredPosition.x,
                            liquidImage.GetComponent<RectTransform>().anchoredPosition.y - changeY);
                    powerAmnt = 0;
                    ammoCounter--;
                }
                else if (powerAmnt >= 2)
                {
                    Instantiate(fizzbubbles, mid);
                    liquidImage.GetComponent<RectTransform>().anchoredPosition = 
                        new Vector2(liquidImage.GetComponent<RectTransform>().anchoredPosition.x,
                            liquidImage.GetComponent<RectTransform>().anchoredPosition.y - changeY);
                    powerAmnt = 0;
                    ammoCounter--;

                }
                else if (powerAmnt >= 1)
                {
                    Instantiate(fizzbubbles, close);
                    liquidImage.GetComponent<RectTransform>().anchoredPosition = 
                        new Vector2(liquidImage.GetComponent<RectTransform>().anchoredPosition.x,
                            liquidImage.GetComponent<RectTransform>().anchoredPosition.y - changeY);
                    powerAmnt = 0;
                    ammoCounter--;
                }
                else
                {
                    //fizzles down some sad precum of champagne
                }
                //powerAmnt = 0;
                
            }
            else // if reloading
            {
                ammoCounter = 6;
                liquidImage.GetComponent<RectTransform>().anchoredPosition = 
                    new Vector2(liquidImage.GetComponent<RectTransform>().anchoredPosition.x,
                        liquidImagey);
                reloadTXT.text = "";
                
                //increase shader
            }
        }
    }

	private float timer = 0;

    private void Update()
    {
        shootBehavior();
        shakeBehavior();
        reloadBehavior();
        updateGraphics();
		
		timer += Time.deltaTime;
		if (timer >= 60) // 61 (+1 of timer)
        {
            //change scene, level is over
            Debug.Log("Change Scene");
            SceneManager.LoadScene("EndScene");
            
            //maybe retain the score here
        }
    }

    private void shakeBehavior()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && ammoCounter > 0)
        {
            powerAmnt += powerIncrement;
            decreaseTimerMax = Time.time + decreaseResetTime;
        }
        //Debug.Log("decreaseTimer " + decreaseTimer + " -- decreaseTimerMax " + decreaseTimerMax);
        if (Time.time >= decreaseTimerMax)
        {
            //goes down at a consistent rate
            powerAmnt -= decreaseDecrement * Time.deltaTime;
        }
        //decreaseTimer += Time.deltaTime;
        powerAmnt = Mathf.Clamp(powerAmnt, 0, powerMax);
    }

    private void reloadBehavior()
    {
        //do animation

        //

    }

    private void updateGraphics()
    {
        //change timer text
        //timeText.text = String.Format("{0:0.##}",Time.time)+ " / " + levelTimerMax+".00";
        timeText.text = String.Format("{0:0.##}",60-timer);
        pointsText.text = "points: " + points;
        fizzSlider.value = powerAmnt;
        //powertext.text = powerAmnt+"";
        powertext.text = String.Format("{0:0.##}",powerAmnt);
    }

    public void addPoints(int pointsToAdd)
    {
        points += pointsToAdd;
        Debug.Log("Piints added " + pointsToAdd + "  -- " + points);
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ContextSceneManager : MonoBehaviour
{

    public Image img1;
    public Image img2;
    public Image textBoxIMG;
    public GameObject mom;
    public TMP_Text textBox;

    private int counter = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            if (counter == 0)
            {
                img1.enabled = false;
            }else if (counter == 1)
            {
                img2.enabled = false;
                
            }else if (counter == 2)
            {
                textBoxIMG.enabled = true;
                textBox.text = "Oh...it's you";
            }else if (counter == 3)
            {
                textBox.text = "Please try and behave this year";
            }else if (counter == 4)
            {
                textBox.text = "It's Bobbi's special day today and Sally's mom has been sober for a year now";
            }else if (counter == 5)
            {
                textBox.text = "So please don't press <b>LEFT SHIFT</b> to shake the champagne bottle";
            }else if (counter == 6)
            {
                textBox.text = "Especially don't press <b>SPACE</b> to shoot the kids with champagne";
            }else if (counter == 7)
            {
                textBox.text = "Anyways, what was your name again?";
            }else if (counter == 8)
            {
                mom.SetActive(false);
            }
            counter++;
        }
    }
        
    
}

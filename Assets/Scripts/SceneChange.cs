using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
public class SceneChange : MonoBehaviour
{
    public string sceneName;
    public TMP_InputField inputfield;
   
    public void buttonClick()
    {
        SceneManager.LoadScene(sceneName);
    }

    public void buttonClickName()
    {
        ScoreManager.instance.initializeName(inputfield.text);
        
        SceneManager.LoadScene(sceneName);

    }


///// ALT CTRL
    public GameObject button;
    public Sprite newSprite;

    void Update()
    {
        
        if (Input.GetKey(KeyCode.Space))
        {
            ChangeSprite(newSprite);
            
            //SceneManager.LoadScene(sceneName);
            
        }
    }

    public void ChangeSprite(Sprite sprite) {
        button.GetComponent<Image>().sprite = newSprite;
        Invoke("buttonClick", 0.5f);
    }
}

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
}

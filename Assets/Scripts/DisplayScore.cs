using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
public class DisplayScore : MonoBehaviour
{

    public TMP_Text textScore;


    private void Update()
    {
        setScoreUI();
        if (Input.GetKeyDown(KeyCode.C))
        {
            orderScores("Void", -200);
        }
    }

    public void setScoreUI()
    {
        textScore.text = getScores();
    }
    private string getScores()
    {
        if (PlayerPrefs.HasKey("Initialized"))
        {
            string s = "";
            for (int i = 0; i < ScoreManager.amountOfScoresToSave; i++)
            {
                int positionInData = i;
                string scoreNameKey = "HSName" + positionInData;
                string scoreNumKey = "HSScore" + positionInData;
                //if the score is 1 then dont add it
                s += (i + 1) + ")" + PlayerPrefs.GetString(scoreNameKey) + " - " + PlayerPrefs.GetInt(scoreNumKey) + "\n";
            }
            return s;
        }
        else
        {
            //display the basic empty thing
            string s = "";
            for (int i = 0; i < ScoreManager.amountOfScoresToSave; i++)
            {
                int positionInData = i;
                string scoreNameKey = "HSName" + positionInData;
                string scoreNumKey = "HSScore" + positionInData;
                //if the score is 1 then dont add it
                s += (i+1) + ")" + PlayerPrefs.GetString(scoreNameKey) + " - " + PlayerPrefs.GetInt(scoreNumKey) + "\n";
            }
            return s;
        }

    }

    public void orderScores(string scoreName, int scoreToAdd)
    {

        int positionsToMove = 500;
        bool replacing = false;
        for (int i = 0; i < ScoreManager.amountOfScoresToSave; i++)
        {
            string scoreNumKey = "HSScore" + i;
            if (PlayerPrefs.GetInt(scoreNumKey) == -1)
            {
                replacing = false;
                positionsToMove = i;
                break;
            }
            if (PlayerPrefs.GetInt(scoreNumKey) < scoreToAdd)
            {
                positionsToMove = i;
                replacing = true; 
                break;
            }
        }
        if (!replacing)
        {
            PlayerPrefs.SetInt("HSScore" + positionsToMove, scoreToAdd);
            PlayerPrefs.SetString("HSName" + positionsToMove, scoreName);
        }
        else if (replacing && positionsToMove < ScoreManager.amountOfScoresToSave)
        {
            //move backwards from these
            for (int i = ScoreManager.amountOfScoresToSave; i < positionsToMove; i--)
            {
                int newPos = i + 1;
                string originalScoreNumKey = "HSScore" + i;
                string originalScoreNameKey = "HSName" + i;
                string newScoreNumKey = "HSScore" + newPos;//first run should be at HSScore11, which is normally not accessed
                string newScoreNameKey = "HSName" + newPos;

                int previousScore = PlayerPrefs.GetInt(originalScoreNumKey);

                PlayerPrefs.SetInt(newScoreNumKey, previousScore);
                PlayerPrefs.SetString(newScoreNameKey, newScoreNameKey);
            }


            PlayerPrefs.SetInt("HSScore" + positionsToMove, scoreToAdd);
            PlayerPrefs.SetString("HSName" + positionsToMove, scoreName);
        }

    }

}

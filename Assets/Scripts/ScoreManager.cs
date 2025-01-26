using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public const int amountOfScoresToSave = 10;

    public int lastSavedScore = -1;
    public string playerName;
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void initializeName(string playerName)
    {
        PlayerPrefs.SetString("PlayerName", playerName);
    }

    private void initializeScores()
    {
        if (!PlayerPrefs.HasKey("Initialized"))
        {
            //initialize the scores if they have not been
            for (int i = 0; i < amountOfScoresToSave; i++)
            {
                int positionInData = i;

                string scoreNameKey = "HSName"+ positionInData;//these are the keys to finding the scores

                string scoreNumKey = "HSScore" + positionInData;

                PlayerPrefs.SetInt(scoreNumKey, -1);
                PlayerPrefs.SetString(scoreNameKey, "NotSet");
                Debug.Log("playerpref saved: "+ scoreNameKey + " " + PlayerPrefs.GetInt(scoreNumKey));
            }
            PlayerPrefs.SetInt("Initialized", 1);
        }
    }


    public void setScore(int lastSavedScore)
    {
        //loop through and find the first score that has not been set, replace that score with this score.
        //otherwise find whichever score has the lowest and move that down. 
        orderScores(playerName, lastSavedScore);
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

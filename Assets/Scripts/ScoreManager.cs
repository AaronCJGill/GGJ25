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

    public void initializeName(string PlayerName)
    {
        if (string.IsNullOrEmpty(PlayerName))
        {
            PlayerName = "Player";
            Debug.Log("Player name is empty and has been reset");
        }

        playerName = PlayerName;
        PlayerPrefs.SetString("PlayerName", PlayerName);
        Debug.Log("Name initialized to : " + PlayerPrefs.GetString("PlayerName"));   
    }

    public void checkPlayerName()
    {
        if (string.IsNullOrEmpty(playerName))
            playerName = "Player";
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
        Debug.Log("settingScore");
        checkPlayerName();
        orderScores(playerName, lastSavedScore);
    }

    public void orderScores(string scoreName, int scoreToAdd)
    {

        Debug.Log("Ordering scores in scoremanager");
        int positionsToMove = 500;
        bool replacing = false;


        if (PlayerPrefs.HasKey("HSScore"+ (amountOfScoresToSave-1)))
        {
            Debug.Log("Player prefs already has all slots filled");
            if (scoreToAdd < PlayerPrefs.GetInt("HSScore" + (amountOfScoresToSave - 1)))
            {
                Debug.Log("smaller than everything");
                return;
            }
        }


        //check which we are smaller than
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
            Debug.Log("adding score");

        }
        else if (replacing && positionsToMove < ScoreManager.amountOfScoresToSave)
        {
            Debug.Log("replacing a score at position " + positionsToMove);
            //move backwards from these
            for (int i = amountOfScoresToSave; i > positionsToMove; i--)
            {
                int moveToPosition = i;//if its 10, then itll be 11
                string originalScoreNumKey = "HSScore" + i;
                string originalScoreNameKey = "HSName" + i;
                string newScoreNumKey = "HSScore" + moveToPosition;//first run should be at HSScore11, which is normally not accessed
                string newScoreNameKey = "HSName" + moveToPosition;


                if (PlayerPrefs.HasKey(originalScoreNameKey))
                {
                    //if this position isnt empty
                    //we move it down by one

                    //get current name and score as well
                    string currentName = PlayerPrefs.GetString(originalScoreNameKey);
                    int currentScore = PlayerPrefs.GetInt(originalScoreNumKey);

                    //set the string at the new position to our current name
                    PlayerPrefs.SetString(newScoreNameKey, currentName);
                    PlayerPrefs.SetInt(newScoreNumKey, currentScore);
                }

            }


            PlayerPrefs.SetInt("HSScore" + positionsToMove, scoreToAdd);
            PlayerPrefs.SetString("HSName" + positionsToMove, scoreName);
        }

    }



}

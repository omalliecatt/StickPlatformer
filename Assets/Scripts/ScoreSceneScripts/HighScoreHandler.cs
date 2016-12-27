using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HighScoreHandler : MonoBehaviour
{
    public Text[] highScoreText = new Text[10];
    public Text[] initalText = new Text[10];
    public InputField initialBox;
    public Button menuButton;
    public Button scoreBackButton;
    private bool initalEnt = false;
    private bool onTheList = false;
    private bool noHScore = false;
    private int savedSpot;

    void Start()
    {
        if (PlayerPrefs.GetInt("newScore") > 0)
        {
            int newScore = PlayerPrefs.GetInt("newScore");
            CheckNewScores(newScore);
            if (!onTheList)
                noHScore = true;
        }
        else
        {
            noHScore = true;
        }
    }

    void Update()
    {
        if ((onTheList && initalEnt) || noHScore)
        {
            DisplayScores();
            PlayerPrefs.SetInt("newScore", -1);
        }

        if (Input.GetKeyDown(KeyCode.Return) && initialBox.text.ToString().Length == 3 && !initalEnt)
        {
            string initals = initialBox.text.ToString();
            PlayerPrefs.SetString("hscoreS" + savedSpot, initals);
            initialBox.transform.Translate(0, 400, 0);
            menuButton.transform.Translate(0, -800, 0);
            scoreBackButton.transform.Translate(0, -800, 0);
            initalEnt = true;
        }
    }

    void CheckNewScores(int newScore)
    {
        int i;
        int tempScore = 0;
        int anotherTempScore = 0;
        string tempString = "";
        string anotherTempString = "";
        for (i = 0; i < 10; i++)
        {
            if (PlayerPrefs.GetInt("hscore" + i) < newScore)
            {
                if (!onTheList)
                {
                    initialBox.transform.Translate(0, -400, 0);
                    menuButton.transform.Translate(0, 800, 0);
                    scoreBackButton.transform.Translate(0, 800, 0);
                    savedSpot = i;
                    onTheList = true;
                }
                tempScore = PlayerPrefs.GetInt("hscore" + i);
                tempString = PlayerPrefs.GetString("hscoreS" + i);
                PlayerPrefs.SetInt("hscore" + i, newScore);
                newScore = -1;
                break;
            }
        }
        
        while (i < 10)
        {
            i++;
            anotherTempScore = PlayerPrefs.GetInt("hscore" + i);
            anotherTempString = PlayerPrefs.GetString("hscoreS" + i);
            PlayerPrefs.SetInt("hscore" + i, tempScore);
            PlayerPrefs.SetString("hscoreS" + i, tempString);
            tempScore = anotherTempScore;
            tempString = anotherTempString;
        }
    }

    void DisplayScores()
    {
        for (int i = 0; i < 10; i++)
        {
            highScoreText[i].text = PlayerPrefs.GetInt("hscore" + i).ToString();
            if (PlayerPrefs.GetInt("hscore" + i) > 0)
                initalText[i].text = PlayerPrefs.GetString("hscoreS" + i);
            else
                initalText[i].text = "---";
        }
    }
}

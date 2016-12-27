using UnityEngine;
using System.Collections;

public class ResetButton : MonoBehaviour
{
    public void ResetScores()
    {
        for (int i = 0; i < 10; i++)
        {
            PlayerPrefs.DeleteAll();
        }
    }
}

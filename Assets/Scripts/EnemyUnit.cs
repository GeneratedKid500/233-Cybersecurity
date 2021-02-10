using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUnit : MonoBehaviour
{
    public string enemyName;
    public int hp;

    public string[] questions;

    public string[] allAnswers;

    public string[][] answerArrays;

    // Start is called before the first frame update
    void Awake()
    {
        #region Error Prevention
        if ((allAnswers.Length) % 3 != 0)
        {
            Debug.LogError("Some questions do not have 3 answers");
            Application.Quit();
            return;
        }

        if ((questions.Length) * 3 != allAnswers.Length)
        {
            Debug.LogError("Number of questions is not relative to the amount of answers. 3 per Question");
            Application.Quit();
            return;
        }
        #endregion

        answerArrays = new string[questions.Length][];

        int allAnswerCounter = 0; 
        for (int i=0; i < questions.Length; i++)
        {
            answerArrays[i] = new string[3] { allAnswers[allAnswerCounter], allAnswers[allAnswerCounter + 1], allAnswers[allAnswerCounter + 2] } ;
            allAnswerCounter += 3;
        }

        #region Debug
        int r = 0;
        for (int d=0; d < answerArrays.Length; d++)
        {
            Debug.Log("Question " + d);
            Debug.Log(answerArrays[d][r]);
            Debug.Log(answerArrays[d][r+1]);
            Debug.Log(answerArrays[d][r+2]);
            Debug.Log("--------------------");
        }
        #endregion
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    //editor vars
    public GameObject enemyPrefab;

    public Transform enemyLocation;

    //script references
    UpdateUI updateUI;
    EnemyUnit enemyUnit;

    //system vars
    int questionNumber = 1;
    int battleScore, correctQuestions;
    List<int> incorrectQuestions;
    public bool incorrectLoop = false;

    void Awake()
    {
        updateUI = GameObject.FindGameObjectWithTag("MainCanvas").GetComponent<UpdateUI>();

        GameObject enemyGo = Instantiate(enemyPrefab, enemyLocation);
        enemyUnit = enemyGo.GetComponent<EnemyUnit>();
    }

    void Start()
    {
        incorrectQuestions = new List<int>();
    }

    /// <summary>
    /// calculates the players score and cycles through arrays in the incorrect loop
    /// </summary>
    /// <param name="result"></param>
    void UpdateScore(bool result)
    {
        // if the player answered correctly
        if (result)
        {
            //things that happen in both loops
            correctQuestions++;
            battleScore = correctQuestions * 200;
            updateUI.DisplayResult("That answer was... correct!");
            enemyUnit.TakeDamage(1);

            //things that happen in incorrect loop
            if (incorrectLoop)
            {
                enemyUnit.incorrectlyAnsweredQs.RemoveAt(1);
            }
        }
        //if the player answered incorrectly
        else
        {
            //things that happen in both loops 
            updateUI.DisplayResult("That answer was... incorrect");
            
            //things that happen in incorrect loop
            if (incorrectLoop)
            {
                // removes from list and adds to another as the system uses the second slot for calculations
                // but the incorrect questions still need to be stored somewhere
                this.incorrectQuestions.Add(enemyUnit.incorrectlyAnsweredQs[1]);
                enemyUnit.incorrectlyAnsweredQs.RemoveAt(1);
            }

            //things that happen in first loop
            else
            {
                enemyUnit.incorrectlyAnsweredQs.Add(questionNumber);
            }
        }

        UpdateQuestion();
    }

    /// <summary>
    /// calculates the parameters to update the question on display
    /// </summary>
    void UpdateQuestion()
    {
        // checks to see if the questions are over and if there are any incorrect questions
        // if true, enables the "incorrect loop"
        if (enemyUnit.incorrectlyAnsweredQs.Count > 1 && 
            questionNumber == enemyUnit.questions.Length)
        {
            incorrectLoop = true;
        }

        // checks if it should calculate the question on display
        // firstly checks if the question isn't at the end of the array of questions yet
        // or checks if the variable "incorrect loop" is true
        if (questionNumber < enemyUnit.questions.Length || incorrectLoop)
        {
            //if its not the "incorrect loop", simply adds 1 to question counter
            //also ensures chance2 is disabled
            if (!incorrectLoop)
            {
                updateUI.DisplayChance2();
                questionNumber++;
            }
            else
            {
                //ensures chance 2 is enabled
                updateUI.DisplayChance2(true);
                // if the incorrectly answered lists length is equal to 1 (it has a palceholder value to start)
                // then it will end the battle
                if (enemyUnit.incorrectlyAnsweredQs.Count == 1)
                {
                    EndBattle();
                }
                // otherwise, it sets the question number to be the second number in the list of this array
                else
                {
                    questionNumber = enemyUnit.incorrectlyAnsweredQs[1];
                }
            }

            //passes it onto the updateUI class with question number in toe
            updateUI.DisplayQuestion(questionNumber);
        }
        else
        {
            EndBattle();
        }
    }

    /// <summary>
    /// sets up parameters to call the interval screen
    /// </summary>
    void EndBattle()
    {
        int aoIncorrectQuestions = this.incorrectQuestions.Count;

        updateUI.DisplayResult("Quiz is over");

        Debug.LogWarning("End Application - Cause: Questions Ended!");
        return;
    }

    /// <summary>
    /// updates the enemy on display
    /// // currently unused
    /// </summary>
    public void UpdateEnemy()
    {
        if (updateUI.CheckForNewEnemy())
        {
            enemyUnit = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyUnit>();
        }
    }

    /// <summary>
    /// called when Button1 is pressed
    /// </summary>
    public void OnButton1()
    {
        Debug.Log("Button1 - " + updateUI.option1.text + " - pressed");

        UpdateScore(enemyUnit.checkCorrectAnswer(questionNumber, updateUI.option1.text));
    }

    /// <summary>
    /// called when Button2 is pressed
    /// </summary>
    public void OnButton2()
    {
        Debug.Log("Button1 - " + updateUI.option2.text + " - pressed");

        UpdateScore(enemyUnit.checkCorrectAnswer(questionNumber, updateUI.option2.text));
    }

    /// <summary>
    /// called when Button3 is pressed
    /// </summary>
    public void OnButton3()
    {
        Debug.Log("Button1 - " + updateUI.option3.text + " - pressed");

        UpdateScore(enemyUnit.checkCorrectAnswer(questionNumber, updateUI.option3.text));
    }
}

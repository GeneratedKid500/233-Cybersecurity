using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateUI : MonoBehaviour
{
    public Text enemyName;
    public Text enemyAttack;
    public Text option1;
    public Text option2;
    public Text option3;

    GameObject[] hpCovers;
    EnemyUnit _enemy;

    // Start is called before the first frame update
    void Start()
    {
        hpCovers = GameObject.FindGameObjectsWithTag("EnemyHpCovers");
        _enemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyUnit>();

        //updating enemy name
        enemyName.text = _enemy.enemyName;

        //Updating Health Bar
        for (int i = 0; i < hpCovers.Length; i++)
        {
            //Debug.Log(hpCovers[i].name[10] - '0');
            if ((hpCovers[i].name[10] - '0') <= _enemy.hp-1) //converts number in the name of the hpCover to an int then compares it to the hp value of the enemy
                hpCovers[i].SetActive(false);
        }

        DisplayQuestion(1);
    }
    
    public void DisplayQuestion(int questionNo) //actual question number must be passed in as -1 is added to ensure it is in the enemy's questions[]
    {
        questionNo--;
        try { enemyAttack.text = _enemy.questions[questionNo]; }
        catch { Debug.LogError("INVALID QUESTION INPUTTED");  return; }

        option1.text = _enemy.answerArrays[questionNo][0];
        option2.text = _enemy.answerArrays[questionNo][1];
        option3.text = _enemy.answerArrays[questionNo][2];
    }

}

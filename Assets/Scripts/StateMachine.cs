using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BattleStates { START, ENEMYTURN, PLAYERTURN, WIN, LOSE }

public class StateMachine : MonoBehaviour
{
    public BattleStates states;

    // Start is called before the first frame update
    void Start()
    {
        states = BattleStates.START;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

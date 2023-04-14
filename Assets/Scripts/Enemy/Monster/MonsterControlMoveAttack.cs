using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterControlMoveAttack : MonoBehaviour
{
    public MonsterMoving monsterMoving;
    public MonsterControl monsterControl;

    private bool moving;

    private void Start()
    {
        monsterMoving = GetComponent<MonsterMoving>();
        monsterControl = GetComponent<MonsterControl>();
    }

    private void Update()
    {
        if (!monsterControl.PlayerInsight() && moving == true)
        {           
           
        } else
        {
            
        }
    }

    private void Check()
    {

    }
}

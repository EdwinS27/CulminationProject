using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Targetable : MonoBehaviour {
    public enum EnemyType {MINION , ENEMY, MONSTER, CHARACTER};
    public EnemyType enemyType;
    public EnemyType GetEnemyType()    {   return this.enemyType;  }
}

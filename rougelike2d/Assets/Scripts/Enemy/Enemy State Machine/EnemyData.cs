using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newEnemyEntityData", menuName = "EntityData/Enemy/EnemyData")]

public class EnemyData : ScriptableObject
{
    public float hurtTime;
    public Material whiteMat;
}

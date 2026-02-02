using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEventState : MonoBehaviour
{
    public PlayerEntity entity;
    public void Attack()
    {
        entity.attackComponent.Attack();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "AVV/SO_Enemy", order = 1)]
public class SO_Enemy : ScriptableObject
{
    public int health;
    public int damage;
}

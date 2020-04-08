using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Character : MonoBehaviour
{
    [SerializeField] protected float _health = 1;
    [SerializeField] protected float _damage = 1;

    public float health
    {
        get => _health;
        set => _health = value;
    }

    public float damage
    {
        get => _damage;
    }


}

abstract public class NPC : Character
{

}

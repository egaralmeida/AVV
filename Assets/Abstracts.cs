using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: Remove?
abstract public class Character : MonoBehaviour
{
    public SO_Character myData;

    [SerializeField] private float _health = 1;
    public float health { get => _health; set => _health = value; }

    protected virtual void kill()
    {
        Destroy(this.gameObject);
    }
}

abstract public class Enemy : Character
{
    public Transform target { get => _target; set => _target = value; }
    private Transform _target;
}

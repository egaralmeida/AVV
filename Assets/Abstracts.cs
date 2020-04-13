using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Character : MonoBehaviour
{
    [SerializeField] private float _health = 1;
    public SO_Character myData;

    public float health
    {
        get => _health;
        set => _health = value;
    }

    protected virtual void kill()
    {
        Destroy(this.gameObject);
    }
}

abstract public class Enemy : Character
{

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Enemy")]
public class ScriptableDataEnemy : ScriptableObject
{
    public int health;
    public int Damage;

    public float cooldown;
    public float fireRate;

    public string SoundHit;
    public string SoundDeath;

    public string SoundAttack;
}

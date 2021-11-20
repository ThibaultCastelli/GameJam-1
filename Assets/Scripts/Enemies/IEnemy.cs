using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemy
{
    int Damage { get; }

    void Spawn();
    void Move();
    void TakeDamage(int amount);
}

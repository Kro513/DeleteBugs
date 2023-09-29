using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PesticideController : MonoBehaviour
{
    private float _timeSinceLastAttack = float.MaxValue;
    protected bool IsAttacking { get; set; }
    protected Pesticide Stats { get; private set; }

    protected virtual void Awack()
    {
        Stats = GetComponent<Pesticide>();
    }
    protected virtual void Update()
    {

    }
    private void HandleAttackDelay()
    {
        if(Stats.Pesti)
    }
}

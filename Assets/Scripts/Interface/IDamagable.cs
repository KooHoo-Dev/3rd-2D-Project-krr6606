
using UnityEngine;

public interface IDamagable 
{
    public abstract void TakeDamage(float damage, ElementalType damageType, Transform attacker);
}

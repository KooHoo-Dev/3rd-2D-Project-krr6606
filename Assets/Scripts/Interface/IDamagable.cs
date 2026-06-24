
using UnityEngine;

public interface IDamagable 
{
    public abstract void TakeDamage(float damage, DamageType damageType, Transform attacker);
}

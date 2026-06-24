using UnityEngine;
using TMPro;
public class Hero : BaseUnit
{

    public TMP_Text healthText;
    public HeroAttack AttackState { get; private set; }
    public HeroMove MoveState { get; private set; }
    public HeroDie DieState { get; private set; }
    public HeroIdle IdleState { get; private set; }
    public HeroHitBack hitBackState { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        AttackState = new HeroAttack(stateMachine, "IsAttack", this);
        MoveState = new HeroMove(stateMachine, "IsMove", this);
        DieState = new HeroDie(stateMachine, "IsDie", this);
        IdleState = new HeroIdle(stateMachine, "IsIdle", this);
        hitBackState = new HeroHitBack(stateMachine, "IsHitBack", this);
    }
    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(IdleState);
        healthText.text = $"{CurrentHealth}/{useData.DefaultHealth}";
    }
    public override void TakeDamage(float damage, ElementalType damageType, Transform attacker)
    {
        CurrentHealth -= damage;
        CurrentHealth = Mathf.Clamp(CurrentHealth, 0, useData.DefaultHealth);
        healthText.text = $"{CurrentHealth}/{useData.DefaultHealth}";
        if (CurrentHealth <= 0)
        {
            stateMachine.ChangeState(hitBackState);
            return;
        }

    }
}

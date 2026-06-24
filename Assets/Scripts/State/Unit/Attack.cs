using UnityEngine;

public class Attack : Unit_State
{
    public Attack(StateMachin stateMachine, string animBoolName, Unit unit) : base(stateMachine, animBoolName, unit)
    {
    }
    public override void Enter()
    {
        base.Enter();
        stateTimer = entity.useData.BackDelay;
        entity.isAttacking = true;
    }
    public override void Update()
    {
        base.Update();
        if (entity.isAttacking == false)
        {
            stateTimer -= Time.deltaTime;
        }

        if (stateTimer <= 0 && entity.isAttacking == false)
        {
            stateMachine.ChangeState(entity.IdleState);
        }
    }
    public override void Exit()
    {
        base.Exit();
        // Additional logic for exiting the Attack state
    }
}


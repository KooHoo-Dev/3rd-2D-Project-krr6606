using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.EventSystems.EventTrigger;
public class HeroAttack : Hero_State
{
    public HeroAttack(StateMachin stateMachine, string animBoolName, Hero hero) : base(stateMachine, animBoolName, hero)
    {
    }
    public override void Enter()
    {
        base.Enter();
        stateTimer = hero.useData.BackDelay;
        hero.isAttacking = true;
    }
    public override void Update()
    {
        base.Update();
        if ((Keyboard.current.aKey.isPressed
            || Keyboard.current.leftArrowKey.isPressed
            || Keyboard.current.dKey.isPressed
            || Keyboard.current.rightArrowKey.isPressed )&& stateTimer <= 0)
        {
            stateMachine.ChangeState(hero.MoveState);
        }
        if (hero.isAttacking == false)
        {
            stateTimer -= Time.deltaTime;
        }

        if (stateTimer <= 0 && hero.isAttacking == false)
        {
            stateMachine.ChangeState(hero.IdleState);
        }
    }
}

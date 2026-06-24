using UnityEngine.InputSystem;
public class HeroIdle : Hero_State
{
    public HeroIdle(StateMachin stateMachine, string animBoolName, Hero hero) : base(stateMachine, animBoolName, hero)
    {
    }
    public override void Update()
    {
        base.Update();
        if (Keyboard.current.aKey.wasPressedThisFrame
            || Keyboard.current.leftArrowKey.wasPressedThisFrame
            || Keyboard.current.dKey.wasPressedThisFrame
            || Keyboard.current.rightArrowKey.wasPressedThisFrame)
        {
            stateMachine.ChangeState(hero.MoveState);
        }
        else if (Keyboard.current.zKey.wasPressedThisFrame || Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            stateMachine.ChangeState(hero.AttackState);
        }
    }
}

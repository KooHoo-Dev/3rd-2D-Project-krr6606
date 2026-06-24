using UnityEngine;
using UnityEngine.InputSystem;
public class HeroMove : Hero_State
{
    int dir;
    public HeroMove(StateMachin stateMachine, string animBoolName, Hero hero) : base(stateMachine, animBoolName, hero)
    {
    }

    public override void Update()
    {
        base.Update();
        if(Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed)
        {
            hero.transform.Translate(Vector2.left * Time.deltaTime * hero.useData.speed);
        }
        else if(Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed)
        {
            hero.transform.Translate(Vector2.right * Time.deltaTime * hero.useData.speed);
        }
        else
        {
            stateMachine.ChangeState(hero.IdleState);
        }
    }
}

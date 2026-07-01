using UnityEngine;
using UnityEngine.InputSystem;
public class HeroMove : Hero_State
{
    public HeroMove(StateMachin stateMachine, string animBoolName, Hero hero) : base(stateMachine, animBoolName, hero)
    {
    }

    public override void Update()
    {
        base.Update();
        if(Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed)
        {
            Debug.Log("Left Key Pressed");
            hero.transform.Translate(Vector2.left * Time.deltaTime * hero.useData.speed * hero.multiplier);
        }
        else if(Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed)
        {
            hero.transform.Translate(Vector2.right * Time.deltaTime * hero.useData.speed * hero.multiplier);
        }
        else if(Keyboard.current.zKey.wasPressedThisFrame)
        {
            stateMachine.ChangeState(hero.AttackState);
        }
        else
        {
            stateMachine.ChangeState(hero.IdleState);
        }
        Vector3 bottomEdge = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, Camera.main.nearClipPlane));
        Vector3 topEdge = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, Camera.main.nearClipPlane));
        float clampedX = Mathf.Clamp(hero.transform.position.x, bottomEdge.x, topEdge.x);
        hero.transform.position = new Vector3(clampedX, hero.transform.position.y, hero.transform.position.z);
    }
}

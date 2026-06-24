using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class Hero_State : EntityState
{
    protected Hero hero;
    public Hero_State(StateMachin stateMachine, string animBoolName, Hero hero) : base(stateMachine, animBoolName)
    {
        this.hero = hero;
        this.animator = hero.animator;

    }
    public override void Update()
    {
        base.Update();

    }

}

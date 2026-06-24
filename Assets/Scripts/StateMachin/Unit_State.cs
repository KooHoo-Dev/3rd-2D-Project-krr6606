using UnityEngine;

public class Unit_State : EntityState
{
    protected Unit entity;
    public Unit_State(StateMachin stateMachine, string animBoolName, Unit unit) : base(stateMachine, animBoolName)
    {
        this.entity = unit;
        this.animator = entity.animator;
    }
}

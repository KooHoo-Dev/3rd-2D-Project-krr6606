using UnityEngine;

public class Walk : Unit_State
{
    private float dir = 1f;
    public Walk(StateMachin stateMachine, string animBoolName, Unit unit) : base(stateMachine, animBoolName, unit)
    {
    }
    public override void Enter()
    {
        base.Enter();
        dir = entity.useData.isCat ? -1f : 1f;
    }
    public override void Update()
    {
        base.Update();

        RaycastHit2D hit = Physics2D.Raycast(entity.transform.position, Vector3.right * dir , entity.useData.Range * entity.multiplier, entity.TargetLayer);
        if (hit.collider != null)
        {
            stateMachine.ChangeState(entity.AttackState);

        }
        entity.transform.Translate(Vector3.right * dir * entity.useData.speed * 0.05f * Time.deltaTime);
    }
    
}
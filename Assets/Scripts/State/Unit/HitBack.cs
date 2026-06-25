using UnityEngine;
using DG.Tweening;
public class HitBack : Unit_State
{

    int dir;
    public HitBack(StateMachin stateMachine, string animBoolName, Unit unit) : base(stateMachine, animBoolName, unit)
    {
    }
    public override void Enter()
    {
        base.Enter();
        dir = entity.useData.isCat ? 1 : -1;
        entity.IsInvincibility = true;


        entity.transform.rotation = Quaternion.Euler(0, 0, -dir * 20);
        entity.transform.DOJump(entity.transform.position + Vector3.right * dir * 1.2f, 0.6f, 1, 0.6F).SetEase(Ease.Linear).OnComplete(() =>
        {
            entity.transform.DOJump(entity.transform.position + Vector3.right * dir * 0.5f, 0.2f, 1, 0.25f).SetEase(Ease.Linear).OnComplete(() =>
            {
                if(entity.CurrentHealth <= 0)
                {
                    stateMachine.ChangeState(entity.DieState);
                    return;
                }
                stateMachine.ChangeState(entity.WalkState);
            });

        });
    }
    public override void Exit()
    {
        base.Exit();
        Debug.Log("Exit HitBack State : " + entity.name);
        entity.transform.rotation = Quaternion.identity;
        entity.IsInvincibility = false;
    }

}

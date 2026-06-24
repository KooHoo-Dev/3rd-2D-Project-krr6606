
using UnityEngine;
using DG.Tweening;

public class HeroHitBack : Hero_State
{
    int dir;
    public HeroHitBack(StateMachin stateMachine, string animBoolName, Hero hero) : base(stateMachine, animBoolName, hero)
    {
    }
    public override void Enter()
    {
        base.Enter();
        dir = hero.useData.isCat ? 1 : -1;
        hero.IsInvincibility = true;


        hero.transform.rotation = Quaternion.Euler(0, 0, -dir * 20);
        hero.transform.DOJump(hero.transform.position + Vector3.right * dir * 1.2f, 0.6f, 1, 0.6F).SetEase(Ease.Linear).OnComplete(() =>
        {
            hero.transform.DOJump(hero.transform.position + Vector3.right * dir * 0.5f, 0.2f, 1, 0.25f).SetEase(Ease.Linear).OnComplete(() =>
            {
                if (hero.CurrentHealth <= 0)
                {
                    stateMachine.ChangeState(hero.DieState);
                    return;
                }
                stateMachine.ChangeState(hero.MoveState);
            });

        });
    }
    public override void Exit()
    {
        base.Exit();
        hero.transform.rotation = Quaternion.identity;
        hero.IsInvincibility = false;
    }
}


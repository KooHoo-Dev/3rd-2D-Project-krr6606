using DG.Tweening;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class HeroDie : Hero_State
{
    public HeroDie(StateMachin stateMachine, string animBoolName, Hero hero) : base(stateMachine, animBoolName, hero)
    {
    }
    public override void Enter()
    {
        base.Enter();
        hero.collider2d.enabled = false;
        Sequence sequence = DOTween.Sequence();
        sequence.Append(hero.transform.DOShakePosition(5, new Vector3(0.2f, 0, 0f), 2, 90f, false, false).SetEase(Ease.Linear));
        sequence.Join(hero.transform.DOMoveY(hero.transform.position.y + 25f, 5).SetEase(Ease.Linear));
        sequence.OnComplete(() =>
        {
            GameObject.Destroy(hero.gameObject);
        });
    }
}

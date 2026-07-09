using UnityEngine;
using TMPro;
public class Hero : BaseUnit
{

    public TMP_Text healthText;
    public HeroAttack AttackState { get; private set; }
    public HeroMove MoveState { get; private set; }
    public HeroDie DieState { get; private set; }
    public HeroIdle IdleState { get; private set; }
    public HeroHitBack hitBackState { get; private set; }

    private int MaxHealth;
    private float Speed;
    private int Damage;

    protected override void Awake()
    {
        base.Awake();
        AttackState = new HeroAttack(stateMachine, "IsAttack", this);
        MoveState = new HeroMove(stateMachine, "IsMove", this);
        DieState = new HeroDie(stateMachine, "IsDie", this);
        IdleState = new HeroIdle(stateMachine, "IsIdle", this);
        hitBackState = new HeroHitBack(stateMachine, "IsHitBack", this);
    }
    protected override void Start()
    {
        base.Start();
        MaxHealth = (int)useData.DefaultHealth * GameDataManager.Instance.userGameDataSO.levelDataList[(int)GameLevelDataName.HeroHealthLevel].level;
        Speed = useData.speed + useData.speed * (GameDataManager.Instance.userGameDataSO.levelDataList[(int)GameLevelDataName.HeroSpeedLevel].level - 1) * 0.1f;
        Damage = (int)(useData.DefaultDamage + useData.DefaultDamage * (GameDataManager.Instance.userGameDataSO.levelDataList[(int)GameLevelDataName.HeroDamageLevel].level - 1) * 0.5f);
        stateMachine.Initialize(IdleState);
        healthText.text = $"{CurrentHealth}/{MaxHealth}";
    }

    public override void TakeDamage(float damage, ElementalType damageType, Transform attacker)
    {
        int RandomSound = Random.Range(0, 2);
        switch (RandomSound)
        {
            case 0:
                StringSoundManager.Instance.PlaySFX("Snd-공격 성공-1");
                break;
            case 1:
                StringSoundManager.Instance.PlaySFX("Snd-공격 성공-2");
                break;
        }
        damage = Mathf.RoundToInt(damage);
        CurrentHealth -= damage;
        CurrentHealth = Mathf.Clamp(CurrentHealth, 0, MaxHealth);
        healthText.text = $"{CurrentHealth}/{MaxHealth}";
        if (CurrentHealth <= 0)
        {
            stateMachine.ChangeState(hitBackState);
            return;
        }

    }
    public void HitBack()
    {
        stateMachine.ChangeState(hitBackState);
    }
    public void Kill()
    {
        stateMachine.ChangeState(DieState);
    }
    protected override void Attack()
    {

        float dir = useData.isCat ? -1f : 1f;
        if (useData.IsOneTarget)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position + Vector3.right * dir * (useData.AttackRangeStart * multiplier), Vector2.right * dir, useData.AttackRangeEnd * multiplier, TargetLayer);
            if (hit.collider == null) return;
            IDamagable damagable = hit.collider.GetComponent<IDamagable>();
            if (damagable == null) return;
            Debug.Log("공격 대상: " + hit.collider.name + ", 현재 타겟레이어: " + LayerMask.LayerToName(hit.collider.gameObject.layer));
            damagable.TakeDamage(Damage, ElementalType.normal, transform);
        }
        else
        {
            RaycastHit2D[] hitList = Physics2D.RaycastAll(transform.position + Vector3.right * dir * (useData.AttackRangeStart * multiplier), Vector2.right * dir, useData.AttackRangeEnd * multiplier, TargetLayer);
            if (hitList.Length == 0) return;
            foreach (RaycastHit2D hit in hitList)
            {
                IDamagable damagable = hit.collider.GetComponent<IDamagable>();
                Debug.Log("공격 대상: " + hit.collider.name + ", 현재 타겟레이어: " + LayerMask.LayerToName(hit.collider.gameObject.layer));
                if (damagable == null) continue;
                damagable.TakeDamage(Damage, ElementalType.normal, transform);
            }
        }
        animatorHit.gameObject.SetActive(true);
    }
}

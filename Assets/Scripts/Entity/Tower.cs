using UnityEngine;
using TMPro;
public class Tower : MonoBehaviour, IDamagable
{
    Animator animator;
    TMP_Text healthText;
    public TowerSO towerData;
     public int MaxHealth;
    public const float HealthMultiplier = 0.2f;
    public int CurrentHealth { get; private set; }

    private void Start()
    {
        MaxHealth = towerData.MaxHealth;
        MaxHealth += Mathf.RoundToInt(MaxHealth * (towerData.Level - 1) * HealthMultiplier);
        CurrentHealth = MaxHealth;
        animator = GetComponent<Animator>();
        healthText = GetComponentInChildren<TMP_Text>();
        UpdateHealthText();
    }
    public void TakeDamage(float damage, ElementalType damageType, Transform attacker)
    {
        StringSoundManager.Instance.PlaySFX("Snd -타워 공격 성공");
        int damageInt = Mathf.RoundToInt(damage);
        CurrentHealth -= damageInt;
        animator.SetTrigger("IsHurt");
        CurrentHealth = Mathf.Max(CurrentHealth, 0);
        if(!StageManager.Instance.isStageEnd && CurrentHealth <= 0)
        {
            StageManager.Instance.GameWin();
            StageManager.Instance.isStageEnd = true;
        }
        UpdateHealthText();
    }
    public void UpdateHealthText()
    {
        healthText.text = $"{CurrentHealth} / {MaxHealth}";
    }
}

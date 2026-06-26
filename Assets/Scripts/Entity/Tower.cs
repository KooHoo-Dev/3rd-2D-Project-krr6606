using UnityEngine;
using TMPro;
public class Tower : MonoBehaviour, IDamagable
{
    Animator animator;
    TMP_Text healthText;
    public TowerSO towerData;
     private int MaxHealth;
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
        Debug.Log($"타워가 {damage}만큼 피해를 입었습니다. 현재 체력: {CurrentHealth}");
        int damageInt = Mathf.RoundToInt(damage);
        CurrentHealth -= damageInt;
        animator.SetTrigger("IsHurt");
        CurrentHealth = Mathf.Max(CurrentHealth, 0);

        UpdateHealthText();
    }
    public void UpdateHealthText()
    {
        healthText.text = $"{CurrentHealth} / {MaxHealth}";
    }
}

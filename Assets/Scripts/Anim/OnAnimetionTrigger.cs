using UnityEngine;

public class OnAnimetionTrigger : MonoBehaviour
{
    public BaseUnit entity;

    private void Awake()
    {
        entity = GetComponent<BaseUnit>();
    }
    public void OnAttackTrigger()
    { 
        entity.OnHit();
        entity.OnEndAttack();
    }

    public void OnEndAnimationDust()
    {
        gameObject.SetActive(false);
    }
}

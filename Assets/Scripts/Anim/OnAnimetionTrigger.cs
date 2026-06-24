using UnityEngine;
[RequireComponent(typeof(BaseUnit))]
public class OnAnimetionTrigger : MonoBehaviour
{
    public BaseUnit entity;

    private void Awake()
    {
        entity = GetComponent<BaseUnit>();
    }
    public void OnAttackTrigger()
    { 
        entity.OnAttack();
        entity.OnEndAttack();
    }
    public void OnEndAnimationDust()
    {
        gameObject.SetActive(false);
    }
}

using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class HeroSkill01 : MonoBehaviour
{
    public Image skillIconMask;

    public Animator IconAnimator;
    public GameObject OKIcon;


    public Transform skillEffectPoint;
    public SpriteRenderer BeamEffectSR;
    public GameObject line;
    public GameObject boomEffectPrefab;

    GameDataManager gameDataManager;

    public float DefaultSkillCooldown = 60f;
    public float skillCooldown;
    public float currentSkillCooldown = 0f;

    public int DefaultSkillDamage = 500;

    public float DefaultSkillRange = 10f;

    private bool isSkillReady = false;
    public float Multiplier = 0.005f;
    private float buffer = 0f;
    void Start()
    {
        gameDataManager = GameDataManager.Instance;
        skillCooldown = Mathf.Max(DefaultSkillCooldown - DefaultSkillCooldown * (gameDataManager.userGameDataSO.skillCoolDownLevel - 1) * 0.08f, 15f);
        currentSkillCooldown = skillCooldown;
        skillIconMask.fillAmount = 1;
        BeamEffectSR.gameObject.SetActive(false);
        line.SetActive(false);
    }
    public void Update()
    {
        if (StageManager.Instance.IsStageEnd()) return;
        if (currentSkillCooldown > 0)
        {
            currentSkillCooldown -= Time.deltaTime;
            buffer += Time.deltaTime;
            if (buffer >= skillCooldown / 12)
            {
                buffer -= skillCooldown / 12;
                skillIconMask.fillAmount = currentSkillCooldown / DefaultSkillCooldown;
            }

        }
        else if (currentSkillCooldown <= 0)
        {
            line.gameObject.SetActive(true);
            OKIcon.SetActive(true);
            line.transform.position = new Vector3(skillEffectPoint.position.x + gameDataManager.userGameDataSO.skillRangeLevel * DefaultSkillRange * Multiplier, line.transform.position.y, line.transform.position.z);
            currentSkillCooldown = 0;
            IconAnimator.SetBool("IsCanFire", true);
            isSkillReady = true;
        }
    }

    public void Fire()
    {
        if (isSkillReady == false) return;
        currentSkillCooldown = skillCooldown;
        skillIconMask.fillAmount = 1;
        IconAnimator.SetBool("IsCanFire", false);
        OKIcon.SetActive(false);
        StartCoroutine(BeamAnimation());
    }
    private IEnumerator BeamAnimation()
    {
        BeamEffectSR.gameObject.SetActive(true);
        Vector2 BeamEndPoint = new Vector2(skillEffectPoint.position.x, line.transform.position.y);
        while (true)
        {

            BeamEndPoint.x = Mathf.Lerp(BeamEndPoint.x, skillEffectPoint.position.x + gameDataManager.userGameDataSO.skillRangeLevel * DefaultSkillRange * Multiplier, Time.deltaTime * 10);
            Vector2 direction = new Vector2(skillEffectPoint.position.x - BeamEndPoint.x, skillEffectPoint.position.y - BeamEndPoint.y).normalized;
            BeamEffectSR.gameObject.transform.rotation = Quaternion.LookRotation(direction * -1);
            BeamEffectSR.size = new Vector2(BeamEffectSR.size.x, Mathf.Abs(skillEffectPoint.position.x - BeamEndPoint.x));

            Debug.Log($"X거리: {BeamEndPoint.x}, 최대 범위.x: {BeamEndPoint.x}, 스킬 효과 지점 위치.x: {skillEffectPoint.position.x}");
            if (BeamEndPoint.x >= skillEffectPoint.position.x)
            {
                Debug.Log("Boom");
                break;
            }
            yield return null;
        }
        BeamEffectSR.gameObject.SetActive(false);
        line.gameObject.SetActive(false);
        isSkillReady = false;
    }
}

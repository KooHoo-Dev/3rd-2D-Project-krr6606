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
    private ComponentPool BoomEffectPool;
    GameDataManager gameDataManager;

    public float DefaultSkillCooldown = 60f;
    public float skillCooldown;
    public float currentSkillCooldown = 0f;

    public int DefaultSkillDamage = 120;

    public float DefaultSkillRange = 10f;
    public LayerMask targetLayer;
    private bool isSkillReady = false;
    public float Multiplier = 0.005f;
    private float buffer = 0f;

    void Start()
    {
        gameDataManager = GameDataManager.Instance;
        BoomEffectPool = new ComponentPool(boomEffectPrefab,transform);
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
        if (isSkillReady == false)
        {
            StringSoundManager.Instance.PlaySFX("Snd-사용 불가");
            return;
        }
        StringSoundManager.Instance.PlaySFX("Snd-머니 사용");
        currentSkillCooldown = skillCooldown;
        skillIconMask.fillAmount = 1;
        IconAnimator.SetBool("IsCanFire", false);
        OKIcon.SetActive(false);
        StartCoroutine(BeamAnimation());
    }
    public int GetSkillDamage()
    {
        return DefaultSkillDamage + Mathf.RoundToInt(DefaultSkillDamage * (gameDataManager.userGameDataSO.skillDamageLevel - 1) * 0.08f);
    }
    private IEnumerator BeamAnimation()
    {
        BeamEffectSR.gameObject.SetActive(true);
        BeamEffectSR.transform.SetParent(null);

        float startX = skillEffectPoint.position.x;
        float startY = skillEffectPoint.position.y;  // 포신 높이

        // ▼ 땅의 Y좌표 (line 오브젝트의 Y를 땅으로 사용)
        float groundY = line.transform.position.y;

        float maxRange = startX + gameDataManager.userGameDataSO.skillRangeLevel
                         * DefaultSkillRange * Multiplier;

        float currentEndX = startX;
        float sweepSpeed = 10f;
        float dir = maxRange > startX ? 1f : -1f;
        sweepSpeed *= dir;

        BeamEffectSR.transform.position = skillEffectPoint.position;
        StringSoundManager.Instance.PlaySFX("Snd-레이저");
        while (true)
        {
            currentEndX += sweepSpeed * Time.deltaTime;

            bool reachedEnd = dir > 0f
                ? currentEndX >= maxRange
                : currentEndX <= maxRange;

            currentEndX = dir > 0f
                ? Mathf.Min(currentEndX, maxRange)
                : Mathf.Max(currentEndX, maxRange);

            // ▼ 포신(높은 곳) → 땅(낮은 곳)으로 방향 계산
            Vector2 beamStart = new Vector2(startX, startY);   // 포신 위치
            Vector2 beamEnd = new Vector2(currentEndX, groundY); // 끝점은 항상 땅

            Vector2 beamDir = beamEnd - beamStart;

            float angle = Mathf.Atan2(beamDir.y, beamDir.x) * Mathf.Rad2Deg;
            BeamEffectSR.transform.rotation = Quaternion.Euler(0, 0, angle - 90f); // 스프라이트 피벗이 Bottom Center이므로 -90도 회전

            float beamLength = beamDir.magnitude;
            BeamEffectSR.transform.localScale = new Vector3(
                BeamEffectSR.transform.localScale.x,
                beamLength,
                1f
            );

            if (reachedEnd)
            {
                Debug.Log("Boom");
                break;
            }

            yield return null;
        }

        BeamEffectSR.gameObject.SetActive(false);
        line.gameObject.SetActive(false);
        isSkillReady = false;
        StringSoundManager.Instance.StopSFX();
        StartCoroutine(BoomEffectCo(new Vector3(startX, groundY, 0f), new Vector3(maxRange, groundY, 0f)));
    }
    private IEnumerator BoomEffectCo(Vector3 StartPos, Vector3 EndPos)
    {

        GameObject temp =  BoomEffectPool.Get();
        Bounds box = temp.GetComponent<Collider2D>().bounds;
        int InitCount = Mathf.CeilToInt(Mathf.Abs(StartPos.x - EndPos.x) / box.size.x);
        temp.SetActive(false);
        for (int i = 0; i < InitCount; i++)
        {
            Vector3 spawnPos = new Vector3(StartPos.x + (EndPos.x - StartPos.x) * (i / (float)InitCount), StartPos.y, StartPos.z);
            GameObject boomEffect = BoomEffectPool.Get();
            boomEffect.transform.position = spawnPos;
            StringSoundManager.Instance.PlaySFX("Snd-레이저 폭발");
            yield return new WaitForSeconds(0.1f);

        }
    }
}

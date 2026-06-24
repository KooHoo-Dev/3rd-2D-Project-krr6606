using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Swipe_Slot : MonoBehaviour
{
    public Scrollbar scrollbar;

    float[] pos;
    float distance;
    bool isDragging = false;
    int targetIndex = 0;

    void Start()
    {
        pos = new float[transform.childCount];
        distance = 1f / (pos.Length - 1f);

        for (int i = 0; i < pos.Length; i++)
        {
            pos[i] = distance * i;
        }
    }

    private void Update()
    {
        if (Mouse.current.leftButton.isPressed)
        {
            isDragging = true;
        }
        else
        {
            // 드래그를 막 놓은 순간 → 가장 가까운 인덱스 1회 계산
            if (isDragging)
            {
                isDragging = false;
                targetIndex = GetNearestIndex(scrollbar.value);
            }

            // 매 프레임 target 위치로 Lerp
            scrollbar.value = Mathf.Lerp(scrollbar.value, pos[targetIndex], 0.1f);
        }

        // 스케일은 targetIndex 기준으로만 처리
        for (int i = 0; i < pos.Length; i++)
        {
            Vector2 targetScale = (i == targetIndex)
                ? new Vector2(1f, 1f)
                : new Vector2(0.8f, 0.8f);

            transform.GetChild(i).localScale =
                Vector2.Lerp(transform.GetChild(i).localScale, targetScale, 0.1f);
        }
    }

    // 현재 value에서 가장 가까운 pos 인덱스 반환
    int GetNearestIndex(float value)
    {
        int nearest = 0;
        float minDist = float.MaxValue;

        for (int i = 0; i < pos.Length; i++)
        {
            float dist = Mathf.Abs(value - pos[i]);
            if (dist < minDist)
            {
                minDist = dist;
                nearest = i;
            }
        }
        return nearest;
    }
}
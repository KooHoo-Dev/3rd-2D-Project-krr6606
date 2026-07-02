using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Swipe_Slot : MonoBehaviour
{
    public Scrollbar scrollbar;
    public SlotData[] slotDataArray;
    public int currentIndex = 0;
    float[] pos;
    float distance;
    bool isDragging = false;
    public int targetIndex{ get; private set; }

    List<SlotData> slotDatas;

    public GameObject slotPrefab;
    public InvenType invenType;

    GameDataManager gameDataManager;
    void Start()
    {

        gameDataManager = GameDataManager.Instance;
        slotDatas = new List<SlotData>();
        Init();
        // 슬롯 데이터 배열 초기화
        slotDataArray = new SlotData[transform.childCount];
        InvenManager.Instance.Register(this);
        Debug.Log("Child Count: " + transform.childCount);
        for (int i = 0; i < transform.childCount; i++)
        {
            slotDataArray[i] = transform.GetChild(i).GetComponent<SlotData>();
        }
        pos = new float[transform.childCount];
        distance = 1f / (pos.Length - 1f);

        for (int i = 0; i < pos.Length; i++)
        {
            pos[i] = distance * i;
        }
    }

    private void Update()
    {
        if(invenType != InvenType.PickSlots) return; 
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
            Debug.Log($"스크롤 위치: {scrollbar.value}, 목표 인덱스: {targetIndex}, pos 갯수: {pos.Length}");
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
        currentIndex = targetIndex;
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
    public CatName GetCatName(int index)
    {
        if (index >= 0 && index < slotDatas.Count)
        {
            return slotDatas[index].catName;
        }
        return CatName.None;
    }
    public void SetCatName(int index, CatName catName)
    {
        if (index >= 0 && index < slotDatas.Count)
        {
            slotDatas[index].catName = catName;
        }
    }
    private void Init()
    {
        switch (invenType)
        {
            case InvenType.None:
 
                break;
            case InvenType.PickSlots:
                int _count = gameDataManager.userGameDataSO.UnLockList.Count;
                for (int i = 0; i < _count; i++)
                {
                    if (isInList(gameDataManager.userGameDataSO.UnLockList[i]) || gameDataManager.userGameDataSO.UnLockList[i] == CatName.None)
                    {
                        continue;
                    }
                    GameObject slotObj = Instantiate(slotPrefab, transform);
                    SlotData slotData = slotObj.GetComponent<SlotData>();
                    CatName catName = gameDataManager.userGameDataSO.UnLockList[i];
                    slotData.SetNormalSlotData(catName);
                    slotDatas.Add(slotData);
                }
                break;
            case InvenType.ResultInven:
                slotDatas = new List<SlotData>(GetComponentsInChildren<SlotData>());
                for (int i = 0; i < gameDataManager.userGameDataSO.CatSlotArray.Length; i++)
                {
                    CatName catName = gameDataManager.userGameDataSO.CatSlotArray[i];
                    if (catName != CatName.None)
                    {
                        slotDatas[i].SetResultSlotData(catName);
                    Debug.Log($"ResultInven Slot {i}: {slotDatas[i].catName}");
                    }
                }
                break;
            default:
                break;
        }

    }
    public bool isInList(CatName catName)
    {
        for (int i = 0; i < slotDatas.Count; i++)
        {
            if (slotDatas[i].catName == catName)
            {
                return true;
            }
        }
        return false;
    }

    public bool IsIn(SlotData slot)
    {
        if (slot == null) return false;

        for (int i = 0; i < slotDatas.Count; i++)
        {
            if (slotDatas[i] == slot)
            {
                return true;
            }
        }


        return false;
    }
}
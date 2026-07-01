using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UnityEngine.AdaptivePerformance.Provider.AdaptivePerformanceSubsystemDescriptor;
using static UnityEngine.UIElements.UxmlAttributeDescription;
namespace Study_Inventory
{


    public class InventoryInput : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        [field: SerializeField] public SlotData cursorSlot { get; private set; }
        private GraphicRaycaster raycaster;
        private SlotData startSlot;    //드래그를 시작한 위치의 슬롯
        private SlotData endSlot;      //드래그가 종료된 위치의 슬롯

        private void Awake()
        {
            raycaster = GetComponent<GraphicRaycaster>();
            Debug.Log($"raycaster 널 여부: {raycaster == null}");
        }
        private SlotData GetSlotOrNull(PointerEventData eventData)
        {
            List<RaycastResult> raycastResults = new List<RaycastResult>();
            //canvas에 붙어있는 GraphicRaycaster를 이용해서
            //마우스 위치에 있는 UI 요소들을 raycast하고
            //그 결과를 raycastResults에 저장한다.
            raycaster.Raycast(eventData, raycastResults);

            foreach (RaycastResult raycastResult in raycastResults)
            {
                if (raycastResult.gameObject.TryGetComponent<SlotData>(out SlotData slot))
                {
                    if (slot == cursorSlot) continue; //cursorSlot은 제외한다.
                    return slot;
                }
            }
            return null;
        }
        public void OnBeginDrag(PointerEventData eventData)
        {

            startSlot = GetSlotOrNull(eventData);
            Debug.Log($"슬롯 여부: {startSlot == null}, 슬롯 이름: {startSlot?.catName}, 슬롯 고양이 이름: {startSlot?.catName}");
            if (startSlot == null) return;  //검색된 슬롯이 없거나
            if (startSlot.catName == CatName.None) return;   //검색된 슬롯이 빈 슬롯인 경우
            if(InvenManager.Instance.GetInventoryOrNull(startSlot).invenType == InvenType.PickSlots)
            {
                cursorSlot.gameObject.SetActive(true);
                cursorSlot.SetNormalSlotData(startSlot.catName);
            }
            else if(InvenManager.Instance.GetInventoryOrNull(startSlot).invenType == InvenType.ResultInven)
            {
                cursorSlot.gameObject.SetActive(true);
                cursorSlot.SetResultSlotData(startSlot.catName);
                startSlot.SetResultSlotData(CatName.None);
                GameDataManager.Instance.userGameDataSO.CatSlotArray[startSlot.slotIndex] = CatName.None;
            }
            Debug.Log($"드래그 시작: {startSlot.catName}, 인벤 타입 : {InvenManager.Instance.GetInventoryOrNull(startSlot).invenType}");
        }

        public void OnDrag(PointerEventData eventData)
        {

            if (startSlot == null || cursorSlot.gameObject.activeSelf == false || cursorSlot.catName == CatName.None) return; //드래그 시작되지 않았음
            cursorSlot.SetPosition(eventData.position); //CursorSlot의 위치를 마우스 위치로 설정한다.
            Debug.Log($"드래그 중: {cursorSlot.catName}");
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            // 3. 드래그가 종료되면 상황에 따라 알맞은 처리를 한다.
            //  3.1 종료된 위치의 Slot이 빈 Slot인 경우 : Cursor의 아이템을 Slot에 Set한다.
            //  3.2 종료된 위치의 Slot이 아이템이 존재하는 Slot인 경우 : Cursor의 아이템과 Slot의 아이템을 서로 교환한다.
            //  3.3 종료된 위치가 Slot이 아닌 경우 : Cursor의 아이템을 원래 Slot에 Set한다.

            if (startSlot == null) return;
            if(startSlot.catName == CatName.None) return;
            endSlot = GetSlotOrNull(eventData);
            
            if (endSlot == null)
            {
                
                if(InvenManager.Instance.GetInventoryOrNull(startSlot).invenType == InvenType.ResultInven)
                {
                    startSlot.SetResultSlotData(cursorSlot.catName);
                    GameDataManager.Instance.userGameDataSO.CatSlotArray[startSlot.slotIndex] = cursorSlot.catName;
                }
                ResetCursor();
                return; //종료된 위치가 Slot이 아닌 경우
            }
            if (endSlot.catName == CatName.None 
                && InvenManager.Instance.GetInventoryOrNull(endSlot) != null 
                && InvenManager.Instance.GetInventoryOrNull(endSlot).invenType == InvenType.ResultInven)
            {
                
                endSlot.SetResultSlotData(cursorSlot.catName);
                GameDataManager.Instance.userGameDataSO.CatSlotArray[endSlot.slotIndex] = cursorSlot.catName;
            }
            if(endSlot.catName != CatName.None 
                && InvenManager.Instance.GetInventoryOrNull(endSlot) != null 
                && InvenManager.Instance.GetInventoryOrNull(endSlot).invenType == InvenType.ResultInven)
            {
                CatName tempCatName = endSlot.catName;
                endSlot.SetResultSlotData(cursorSlot.catName);
                GameDataManager.Instance.userGameDataSO.CatSlotArray[endSlot.slotIndex] = cursorSlot.catName;
                startSlot.SetResultSlotData(tempCatName);
                GameDataManager.Instance.userGameDataSO.CatSlotArray[startSlot.slotIndex] = tempCatName;
            }
            ResetCursor();
            Debug.Log($"드래그 종료: {endSlot.catName}");
        }
        private void ResetCursor()
        {
            cursorSlot.gameObject.SetActive(false);
            cursorSlot.SetNormalSlotData(CatName.None);
            startSlot = null;
        }

    }
}
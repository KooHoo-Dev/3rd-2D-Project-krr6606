using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class VolumeSettingPanel : MonoBehaviour,IPointerDownHandler
{
    private GraphicRaycaster raycaster;
    private void Awake()
    {
        raycaster = GetComponentInParent<GraphicRaycaster>();
        Debug.Log($"raycaster널 여부: {raycaster == null}");
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        List<RaycastResult> raycastResults = new List<RaycastResult>();
        raycaster.Raycast(eventData, raycastResults);
        if (raycastResults.Count > 0 && raycastResults[0].gameObject == gameObject)
        {

            gameObject.SetActive(false);
        }
        Debug.Log($"패널 객체: {gameObject.name}");
        Debug.Log($"감지 위치: {eventData.position}");
    }


}

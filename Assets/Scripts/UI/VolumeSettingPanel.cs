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
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        List<RaycastResult> raycastResults = new List<RaycastResult>();
        raycaster.Raycast(eventData, raycastResults);
        if (raycastResults.Count > 0 && raycastResults[0].gameObject == gameObject)
        {

            gameObject.SetActive(false);
        }

    }


}

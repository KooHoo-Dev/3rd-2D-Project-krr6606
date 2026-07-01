using UnityEngine;
using System.Collections.Generic;
public class InvenManager : MonoBehaviour
{
    public static InvenManager Instance { get; private set; }
    private List<Swipe_Slot> catDragInventories = new List<Swipe_Slot>();
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }
    public void Register(Swipe_Slot inventory)
    {
        if (catDragInventories.Contains(inventory))
        {
            Debug.LogWarning($"이미 등록된 인벤토리입니다. {inventory}");
            return;
        }
        catDragInventories.Add(inventory);
    }
    public void Unregister(Swipe_Slot inventory)
    {
        catDragInventories.Remove(inventory);
    }
    public Swipe_Slot GetInventoryOrNull(SlotData targetSlot)
    {
        for (int i = 0; i < catDragInventories.Count; i++)
        {
            if (catDragInventories[i].IsIn(targetSlot))
            {
                return catDragInventories[i];
            }
        }
        return null;
    }
}


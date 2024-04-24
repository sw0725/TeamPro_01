using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{
    [Tooltip("�ش� ������ Ÿ�� �Ҵ�")]
    public List<ItemType> slotType = new List<ItemType>();
    [Tooltip("������ ������ ����")]
    [HideInInspector] public ItemData itemData = null;

    public delegate void ItemChanged();
    /// <summary>
    /// ������ ui ����
    /// </summary>
    public event ItemChanged OnItemChanged;

    private ItemSlot itemslot;

    public bool IsEmpty => itemData == null;

    public bool AddItem(ItemData item)
    {
        if (item != null && slotType.Contains(item.itemType))
        {
            itemData = item;
            OnItemChanged?.Invoke();
            return true;
        }
        return false;
    }

    public void RemoveItem()
    {
        itemData = null;
        OnItemChanged?.Invoke();
    }

    public void EquipItem(ItemData item)
    {
        if (item != null && slotType.Contains(item.itemType))
        {
            itemData = item;
            OnItemChanged?.Invoke();
        }
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class Equip : Inventory
{
    //[SerializeField] public Slot[] slots;
    //private QuickSlotUI quickSlotUI;

    //private void Awake()
    //{
    //    slots = GetComponentsInChildren<Slot>();
    //    quickSlotUI = FindObjectOfType<QuickSlotUI>(true);
    //}

    ///// <summary>
    ///// ���Կ� ������ ���� �߰�.
    ///// </summary>
    ///// <param name="item"></param>
    ///// <param name="index"></param>
    //public void AddItemToSlot(ItemData item, uint index)
    //{
    //    if (slots[index].IsEmpty)
    //    {
    //        slots[index].AddItem(item);
    //        quickSlotUI.QuickSlotImageChange(index);
    //    }
    //}

    ///// <summary>
    ///// ���Կ� �̹� �������� ������ ��� �� �����۰� ���� ������ ����.
    ///// </summary>
    ///// <param name="from"></param>
    ///// <param name="to"></param>
    //public void SwitchItemToSlot(Slot from, Slot to)
    //{
    //    if (!from.IsEmpty && to.IsEmpty)
    //    {
    //        ItemData temp = from.itemData;
    //        from.RemoveItem();
    //        to.EquipItem(temp);
    //    }
    //}

    public ItemSlot[] slots;
    private SlotType slotsType;
    const int Default_Inventory_Size = 8;
    //Player owner;

    public Equip(Player owner, uint size = Default_Inventory_Size)
        : base (owner, size)
    {

    }

    /// <summary>
    /// ������ �������� Ÿ�԰� ���� Ÿ���� �� �� ����� ���� ��/���� ����
    /// </summary>
    /// <param name="data">������ �������� ������</param>
    /// <returns></returns>
    public bool CheckItemSlotType(ItemData data)
    {
        bool isEquip = false;

        if (data != null && slotsType.slotType.Contains(data.itemType))
        {
            isEquip = true;
        }

        return isEquip;
    }

    //public void EquipItem(ItemCode code, ItemData data)
    //{
    //    if (data != null && slotsType.slotType.Contains(data.itemType))
    //    {
    //        base.AddItem(code);
    //    }
    //}
}
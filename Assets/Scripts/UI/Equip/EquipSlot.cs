using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipSlot : ItemSlot
{
    [Tooltip("�ش� ������ Ÿ�� �Ҵ�")]
    public List<ItemType> slotType = new List<ItemType>();

    public EquipSlot(uint index) : base(index)
    {

    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEditor.SceneManagement;
using UnityEngine;

//public class Equip : Inventory
public class Equip : MonoBehaviour
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

    private const int Default_Inventory_Size = 8;
    public EquipSlot[] slots;
    public EquipSlot this[uint index] => slots[index];
    private int SlotCount => slots.Length;
    private Player owner;
    private DragSlot dragSlot;
    private uint dragSlotIndex = 999999999;
    public DragSlot DragSlot => dragSlot;
    ItemDataManager itemDataManager;
    public Player Owner => owner;
    private Equip_UI equipUI;
    // private SlotType slotsType;

    //public Equip(Player owner, uint size = Default_Inventory_Size)
    //    : base (owner, size)
    //{

    //}

    public Equip(Player owner, GameObject slotsParent, uint size = Default_Inventory_Size)
    {
        slots = new EquipSlot[size];

        for (uint i = 0; i < slots.Length; i++)
        {
            slots[i] = new EquipSlot(i);
            slots[i].slotType = slotsParent.GetComponentsInChildren<EquipSlot_UI>()[i].slotType;
        }

        dragSlot = new DragSlot(dragSlotIndex);
        itemDataManager = GameManager.Instance.ItemData;
        equipUI = GameManager.Instance.EquipUI;
        this.owner = owner;
    }

    private void Awake()
    {
        //slots[0].AssignSlotItem(equipUI.data01);
        //MoveItem(slots[1], slots[0]);
        //slots[0].AssignSlotItem(equipUI.data02);
        //slots[0].AssignSlotItem(equipUI.data03);
    }

    /// <summary>
    /// ����â�� Ư�� �������� �����ϴ� �Լ�.
    /// </summary>
    /// <param name="code">���� �� ������ �ڵ�</param>
    /// <returns>�����ϸ� true ���� �����ϸ� false ����</returns>
    public bool AddItem(ItemCode code)
    {
        for (int i = 0; i < SlotCount; i++)
        {
            if (AddItem(code, (uint)i))
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// Ư�� ����â ���Կ� Ư�� �������� 1�� �����ϴ� �Լ�
    /// </summary>
    /// <param name="code">���� �� �������� �ڵ�</param>
    /// <param name="slotIndex">�������� ���� �� ������ �ε���</param>
    /// <returns>�����ϸ� true ���� �����ϸ� false ����</returns>
    public bool AddItem(ItemCode code, uint slotIndex)
    {
        bool result = false;

        if (IsValidIndex(slotIndex))
        {
            ItemData data = itemDataManager[code];
            EquipSlot slot = slots[slotIndex];

            if (slot.slotType.Contains(data.itemType))
            {
                if (slot.slotType.Contains(ItemType.Gun))
                {
                    Pistol pistol = gameObject.AddComponent<Pistol>();
                    //Rifle rifle = gameObject.AddComponent<Rifle>();
                    //Shotgun shotgun = gameObject.AddComponent<Shotgun>();
                    //Sniper sniper = gameObject.AddComponent<Sniper>();
                    var slotItemComponents = slot.ItemData.itemPrefab.GetComponents<Component>();

                    foreach (var gunCom in slotItemComponents)
                    {
                        if (gunCom.GetType() == pistol.GetType())
                        {
                            //slotIndex = 2;
                            //slot = slots[slotIndex];
                            if (slotIndex != 2)
                            {
                                result = false;

                                return result;
                            }
                        }
                        else
                        {

                        }
                    }
                }

                //if (slot.slotType.Contains(data.itemType) && slot.IsEmpty)
                if (slot.IsEmpty)
                {
                    //if (slot.slotType.Contains(ItemType.Gun))
                    //{
                    //    Pistol pistol = gameObject.AddComponent<Pistol>();
                    //    //Rifle rifle = gameObject.AddComponent<Rifle>();
                    //    //Shotgun shotgun = gameObject.AddComponent<Shotgun>();
                    //    //Sniper sniper = gameObject.AddComponent<Sniper>();
                    //    var slotItemComponents = slot.ItemData.itemPrefab.GetComponents<Component>();

                    //    foreach (var gunCom in slotItemComponents)
                    //    {
                    //        if (gunCom.GetType() == pistol.GetType())
                    //        {
                    //            slotIndex = 2;
                    //            slot = slots[slotIndex];
                    //        }
                    //        else
                    //        {

                    //        }
                    //    }
                    //}

                    slot.AssignSlotItem(data);
                    result = true;
                }   
            }
        else
        {
            if (slot.ItemData == data)
            {
                // result = slot.SetSlotCount(out _);  ������ ���� �ʿ� ����.
            }
            else
            {
                // �ٸ� ������ ������
            }
        }
        }
        else
        {
            // �߸��� ����
        }

        return result;
    }

    public void MoveItem(ItemSlot from, ItemSlot to)
    {
        // from ������ to������ ���� �ٸ� ��ġ�̰� ��� valid�� �����̾�� �Ѵ�.
        if ((from != to) && IsValidIndex(from) && IsValidIndex(to))
        {
            bool fromIsTemp = (from == dragSlot);
            ItemSlot fromSlot = fromIsTemp ? DragSlot : from;   // from���� ��������

            if (!fromSlot.IsEmpty)
            {
                // from�� �������� �ִ�.
                ItemSlot toSlot = null;

                if (to == dragSlot)
                {
                    // to�� drag�����̸�
                    toSlot = DragSlot;      // ���� �����ϰ�
                    DragSlot.SetFromIndex(fromSlot.Index);  // �巡�� ���� ���� ����(fromIndex)
                }
                else
                {
                    toSlot = to;        // drag������ �ƴϸ� ���Ը� ����
                }

                if (fromSlot.ItemData == toSlot.ItemData)
                {
                    //// ���� ������ ������ => to�� ä�� �� �ִµ����� ä���. to�� �Ѿ ��ŭ from�� ���ҽ�Ų��.
                    //toSlot.SetSlotCount(out uint overCount, fromSlot.ItemCount);    // from�� ���� ������ŭ to �߰�
                    //fromSlot.DecreaseSlotItem(fromSlot.ItemCount - overCount);      // from���� to�� �Ѿ ������ŭ�� ����

                    // ���� �������� �߰� ���Ѵ�.
                }
                else
                {
                    if (fromIsTemp)
                    {
                        ItemSlot returnSlot = slots[DragSlot.FromIndex];

                        if (returnSlot.IsEmpty)
                        {
                            returnSlot.AssignSlotItem(toSlot.ItemData, toSlot.ItemCount);
                            toSlot.AssignSlotItem(DragSlot.ItemData, DragSlot.ItemCount);
                            DragSlot.ClearSlot();
                        }
                        else
                        {
                            if (returnSlot.ItemData == toSlot.ItemData)
                            {
                                MoveItem(toSlot, returnSlot);
                                returnSlot.SetSlotCount(out uint overCount, toSlot.ItemCount);
                                toSlot.DecreaseSlotItem(toSlot.ItemCount - overCount);
                            }

                            SwapSlot(dragSlot, toSlot);
                        }
                    }
                    else
                    {
                        SwapSlot(fromSlot, toSlot);
                    }
                }
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="slotA"></param>
    /// <param name="slotB"></param>
    public void SwapSlot(ItemSlot slotA, ItemSlot slotB)
    {
        ItemData dragData = slotA.ItemData;
        uint dragCount = slotA.ItemCount;

        slotA.AssignSlotItem(slotB.ItemData, slotB.ItemCount);
        slotB.AssignSlotItem(dragData, dragCount);
    }

    private bool IsValidIndex(uint index)
    {
        return (index < SlotCount) || (index == dragSlotIndex);
    }

    private bool IsValidIndex(ItemSlot slot)
    {
        return (slot != null) || (slot == dragSlot);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="slotIndex"></param>
    /// <param name="count"></param>
    public void RemoveItem(uint slotIndex, uint count = 1)
    {
        if (IsValidIndex(slotIndex))
        {
            EquipSlot slot = slots[slotIndex];
            slot.DecreaseSlotItem(count);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public void ClearEquip()
    {
        foreach (var slot in slots)
        {
            slot.ClearSlot();
        }
    }

    /// <summary>
    /// �÷��̾ ����ϰų� �߰��� ������ �� �κ��丮�� ���� �Լ�
    /// </summary>
    public void GameOver()
    {
        ClearEquip();
    }
}
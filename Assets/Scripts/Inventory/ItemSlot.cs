using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using static UnityEditor.Timeline.TimelinePlaybackControls;

public class ItemSlot
{
    uint slotIndex;

    public uint Index => slotIndex;


    ItemData itemData = null;   // 인벤토리에 들어가게 될 아이템 정보

    public ItemData ItemData
    {
        get => itemData;
        set
        {
            if (itemData != value)
            {
                itemData = value;
                onSlotItemChange?.Invoke();
            }
        }
    }

    public Action onSlotItemChange;

    public bool IsEmpty => itemData == null;

    uint itemCount = 0;

    public uint ItemCount
    {
        get => itemCount;
        set
        {
            if(itemCount != value)
            {
                itemCount = value;
                onSlotItemChange?.Invoke();
            }
        }
    }

    public ItemSlot(uint index)
    {
        slotIndex = index;
        itemData = null;
        itemCount = 0;
    }


    /// <summary>
    /// 인벤토리에 아이템을 추가해주는 함수
    /// </summary>
    /// <param name="item">획득한 아이템</param>
    /// <param name="count">획득한 아이템의 수량</param>
    public void AssignSlotItem(ItemData item, uint count = 1)
    {
        if(item != null)
        {
            ItemData = item;
            ItemCount = count;
           // Debug.Log($"인벤토리 {slotIndex}번 슬롯에 [{ItemData.itemName}]아이템이 [{ItemCount}]개 설정");


        }
        else
        {
            ClearSlot();
        }
    }

    public virtual void ClearSlot()
    {
        ItemData = null;
        ItemCount = 0;

        //Debug.Log($"인벤토리 [{slotIndex}]번 슬롯을 비웁니다.");
    }

    /// <summary>
    /// 인벤토리에서 아이템의 수량을 변경하는 함수
    /// </summary>
    /// <param name="count">획득한 갯수</param>
    public bool SetSlotCount(out uint oveCount, uint count = 1)
    {
        bool result;
        uint newCount = ItemCount + count;
        int over = (int)newCount - (int)ItemData.maxItemCount;

        if (over > 0)
        {
            ItemCount = ItemData.maxItemCount;
            oveCount = (uint)over;
            result = false;

        }
        else
        {
            ItemCount = newCount;
            oveCount = 0;
            result = true;
        }
        return result;
    }

    /// <summary>
    /// 이 슬롯의 아이템 개수를 감소시키는 함수
    /// </summary>
    /// <param Name="decreaseCount">감소시킬 개수</param>
    public void DecreaseSlotItem(uint count = 1)
    {
        int newCount = (int)ItemCount - (int)count;
        if (newCount > 0)
        {
            // 아직 아이템이 남아있음
            ItemCount = (uint)newCount;
        }
        else
        {
            ClearSlot();
        }
    }
}

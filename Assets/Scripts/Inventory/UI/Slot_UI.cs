using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Slot_UI : Slot_UI_Base, IPointerClickHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public Action<uint> onDragBegin;
    public Action<RectTransform, RectTransform, uint> onDragEnd;

    public Action<uint> onRightClick;

    public Action<RectTransform, uint> OnClick;

    public Action<ItemSlot> onValuePlus;
    public Action<ItemSlot> onValueMinus;


    protected override void Awake()
    {
        base.Awake();   
    }

    public void SlotSwap(ItemSlot slotA, ItemSlot slotB)
    {
        ItemData dragData = slotA.ItemData;
        uint dragCount = slotA.ItemCount;

        slotA.AssignSlotItem(slotB.ItemData, slotB.ItemCount);
        slotB.AssignSlotItem(dragData, dragCount);

        onValuePlus?.Invoke(slotA);
        onValueMinus?.Invoke(slotB);
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        RectTransform rect = eventData.pointerCurrentRaycast.gameObject.GetComponent<RectTransform>();
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            onRightClick?.Invoke(Index);
            Debug.Log("우클릭");
        }
        else
        {
            OnClick?.Invoke(rect, Index);
            Debug.Log("좌클릭");
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        onDragBegin?.Invoke(Index);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        GameObject obj = eventData.pointerCurrentRaycast.gameObject;   // UI 대상 레이캐스트
        if (obj != null)
        {
            
            //Debug.Log(eventData.pointerCurrentRaycast.gameObject);
            Slot_UI endSlot = obj.GetComponent<Slot_UI>();
            if (endSlot != null)
            {
                RectTransform rect = obj.transform.GetComponent<RectTransform>();
                RectTransform rectParent = obj.transform.parent.parent.parent.parent.GetComponent<RectTransform>();
                onDragEnd?.Invoke(rectParent, rect, endSlot.Index);
                // 당연히 빈 슬롯인데 없는게 정상이지
            }
            else
            {
                // onDragEnd?.Invoke(rectParent, rect, Index);
            }
        }
        else
        {

        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        
    }
}

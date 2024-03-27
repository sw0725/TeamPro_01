using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Slot_UI : Slot_UI_Base, IPointerClickHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public Action<uint> onDragBegin;
    public Action<uint> onDragEnd;

    public Action<uint> onRightClick;

    public Action<uint> OnClick;

    protected override void Awake()
    {
        base.Awake();   
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            onRightClick?.Invoke(Index);
            Debug.Log("우클릭");
        }
        else
        {
            OnClick?.Invoke(Index);
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
            Debug.Log(eventData.pointerCurrentRaycast.gameObject);
            Slot_UI endSlot = obj.GetComponent<Slot_UI>();
            if (endSlot != null)
            {
                onDragEnd?.Invoke(endSlot.Index);
            }
            else
            {
                onDragEnd?.Invoke(Index);
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

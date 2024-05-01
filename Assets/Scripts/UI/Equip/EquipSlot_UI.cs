using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EquipSlot_UI : Slot_UI_Base, IPointerClickHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [Tooltip("�ش� ������ Ÿ�� �Ҵ�")]
    public List<ItemType> slotType = new List<ItemType>();

    public Action<ItemSlot> onDragBegin;
    public Action<ItemSlot, RectTransform> onDragEnd;


    public Action<ItemSlot, RectTransform> OnClick;

    public Action<uint> onRightClick;

    protected override void Awake()
    {
        //Transform child = transform.GetChild(0);
        //itemImage = child.GetComponent<Image>();
        base.Awake();
    }

    /// <summary>
    /// ����UI�� ȭ�� ����
    /// </summary>
    private void Refresh()
    {
        if (ItemSlot.IsEmpty)
        {
            // ��� ���� ��
            itemImage.color = Color.clear;   // ������ �����ϰ�
            itemImage.sprite = null;         // ��������Ʈ ����
            //itemText.text = string.Empty;  // ���ڵ� ����
        }
        else
        {
            // �������� ���������
            //if (!IsEquipment)
            //{
            //    // �������� ���������� �ƴϸ�
            //    itemImage.sprite = ItemSlot.ItemData.itemImage;      // ��������Ʈ �̹��� ����
            //    itemImage.color = Color.white;                       // �̹��� ���̰� �����
            //    //itemText.text = ItemSlot.ItemCount.ToString();    // ������ ���� ����
            //    //itemText.alpha = 1;

            //}
            //else
            //{
            //    itemImage.sprite = ItemSlot.ItemData.itemImage;      // ��������Ʈ �̹��� ����
            //    itemImage.color = Color.white;                       // �̹��� ���̰� �����
            //    //itemText.text = ItemSlot.ItemCount.ToString();    // ������ ���� ����
            //    //itemText.alpha = 0;
            //}

            itemImage.sprite = ItemSlot.ItemData.itemImage;      // ��������Ʈ �̹��� ����
            itemImage.color = Color.white;                       // �̹��� ���̰� �����
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            onRightClick?.Invoke(Index);
        }
        else
        {
            RectTransform rect = transform.parent.parent.parent.parent.GetComponent<RectTransform>();
            OnClick?.Invoke(ItemSlot, rect);
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        onDragBegin?.Invoke(ItemSlot);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        GameObject obj = eventData.pointerCurrentRaycast.gameObject;   // UI ��� ����ĳ��Ʈ
        if (obj != null)
        {
            Slot_UI endSlot = obj.GetComponent<Slot_UI>();
            if (endSlot != null)
            {
                RectTransform rect = obj.GetComponent<RectTransform>();
                onDragEnd?.Invoke(endSlot.ItemSlot, rect);
            }
            else
            {
                RectTransform rect = transform.parent.parent.parent.parent.GetComponent<RectTransform>();
                onDragEnd?.Invoke(ItemSlot, rect);
            }
        }
        else
        {
            RectTransform rect = transform.parent.parent.parent.parent.GetComponent<RectTransform>();
            onDragEnd?.Invoke(ItemSlot, rect);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {

    }
}

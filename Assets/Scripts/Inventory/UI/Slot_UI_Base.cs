using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Slot_UI_Base : MonoBehaviour
{
    ItemSlot itemSlot;
    public ItemSlot ItemSlot => itemSlot;

    public Image itemImage; // 아이템의 이미지

    TextMeshProUGUI itemText;

    public uint Index => itemSlot.Index;

    protected virtual void Awake()
    {
        Transform child = transform.GetChild(0);
        itemImage = child.GetComponent<Image>();
        itemText = GetComponentInChildren<TextMeshProUGUI>();
    }


    /// <summary>
    /// 슬롯을 초기화 하는 함수(=ItemSlot과 SlotUI를 연결)
    /// </summary>
    /// <param name="slot"></param>
    public virtual void InitializeSlot(ItemSlot slot)
    {
        itemSlot = slot;
        itemSlot.onSlotItemChange = Refresh;   // 슬롯의 아이템에 변경이 있을 때 Refresh함수 실행(이전에 연결된 함수는 모두 무시)
        Refresh();      // 첫 화면 갱신
    }

    /// <summary>
    /// 슬롯UI의 화면 갱신
    /// </summary>
    private void Refresh()
    {
        if (ItemSlot.IsEmpty)
        {
            // 비어 있을 때
            itemImage.color = Color.clear;   // 아이콘 투명하게
            itemImage.sprite = null;         // 스프라이트 비우기
            itemText.text = string.Empty;  // 글자도 제거
        }
        else
        {
            // 아이템이 들어있으면
            itemImage.sprite = ItemSlot.ItemData.itemImage;      // 스프라이트 이미지 설정
            itemImage.color = Color.white;                       // 이미지 보이게 만들기
            itemText.text = ItemSlot.ItemCount.ToString();    // 아이템 개수 쓰기
        }
    }


}

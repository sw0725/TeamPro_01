using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Inventory_UI : MonoBehaviour
{
    Inventory inventory;

    Slot_UI[] slotsUI;

    DragSlotUI dragSlotUI;

    SelectMenuUI select;

    DropSlotUI dropSlot;

    WeightPanel_UI weightPanel;

    MoneyPanel_UI moneyPanel;

    RectTransform invenRect;

    // 무게는 이후에 플레이어로 옮긴 이후 무게에 따라서 이동속도 변환 기믹 구현하기
    int weight = 0;
    public int Weight
    {
        get => weight;
        set
        {
            if (weight != value)
            {
                weight = value;
                onWeightChange?.Invoke(weight);
            }
        }
    }

    int money = 0;
    public int Money
    {
        get => money;
        set
        {
            if (money != value)
            {
                money = value;
                onMoneyChange?.Invoke(money);
            }
        }
    }


    /// <summary>
    /// 인벤토리의 무게에 변화가 있으면 실행되는 델리게이트
    /// </summary>
    public Action<int> onWeightChange;

    //인벤토리의 돈에 변화가 있으면 실행되는 델리게이트
    public Action<int> onMoneyChange;


    private void Awake()
    {
        Transform child = transform.GetChild(0);
        slotsUI = child.GetComponentsInChildren<Slot_UI>();

        child = transform.GetChild(1);
        select = child.GetComponent<SelectMenuUI>();

        child = transform.GetChild(2);
        dragSlotUI = child.GetComponent<DragSlotUI>();

        child = transform.GetChild(3);
        dropSlot = child.GetComponent<DropSlotUI>();

        child = transform.GetChild(4);
        weightPanel = child.GetComponent<WeightPanel_UI>();

        child = transform.GetChild(5);
        moneyPanel = child.GetComponent<MoneyPanel_UI>();

        invenRect = transform.GetComponent<RectTransform>();
    }

    public void InitializeInventory(Inventory playerInventory)
    {
        inventory = playerInventory;

        for (uint i = 0; i < slotsUI.Length; i++)
        {
            slotsUI[i].InitializeSlot(inventory[i]);
            slotsUI[i].onDragBegin += OnItemMoveBegin;
            slotsUI[i].onDragEnd += OnItemMoveEnd;
            slotsUI[i].onRightClick += OnRightClick;
            slotsUI[i].OnClick += OnClick;
            slotsUI[i].onValuePlus += OnValuePlus;
        }

        dragSlotUI.InitializeSlot(inventory.DragSlot);  // 임시 슬롯 초기화

        select.onItemDrop += OnItemDrop;
        select.onItemSort += (by) =>
        {
            inventory.MergeItems();
            OnItemSort(by);
        };
        select.Close();

        dropSlot.onOkClick += OnDropOk;
        dropSlot.Close();

        onWeightChange += weightPanel.Refresh;
        weightPanel.Refresh(Weight);

        onMoneyChange += moneyPanel.Refresh;
        moneyPanel.Refresh(Money);

    }

    private void OnValuePlus(ItemSlot slot)
    {
        inventory.PlusValue(slot);
    }

    /// <summary>
    /// 아이템 드래그 시작하면 실행되는 함수
    /// </summary>
    /// <param name="index">시작한 슬롯의 index</param>
    private void OnItemMoveBegin(uint index)
    {
        inventory.MoveItem(index, dragSlotUI.Index);
        dragSlotUI.Open();
    }

    /// <summary>
    /// 아이템 드래그가 끝이나면 실행되는 함수
    /// </summary>
    /// <param name="index">끝난 슬롯의 index</param>
    private void OnItemMoveEnd(RectTransform rectParent, RectTransform rect , uint index)
    {
        if(rectParent == invenRect)
        {
            inventory.MoveItem(dragSlotUI.Index, index);

            if (dragSlotUI.ItemSlot.IsEmpty)
            {
                dragSlotUI.Close();
            }
        }
        else
        {
            Slot_UI slot = rect.GetComponent<Slot_UI>();

            inventory.MinusValue(dragSlotUI.ItemSlot);
            slot.SlotSwap(slot.ItemSlot, dragSlotUI.ItemSlot);
        }

    }

    /// <summary>
    /// 슬롯을 클릭하면 실행되는 함수
    /// </summary>
    /// <param name="index"></param>
    private void OnClick(RectTransform rect, uint index)
    {
        if(!dragSlotUI.ItemSlot.IsEmpty)
        {
            OnItemMoveEnd(invenRect, rect, index);
        }
    }

    /// <summary>
    /// 슬롯을 우클릭 시 실행되는 함수
    /// </summary>
    /// <param name="index">우클릭한 슬롯의 index</param>
    private void OnRightClick(uint index)
    {
        // 버리기, 상세보기 등 UI따로 띄우기
        Slot_UI target = slotsUI[index];
        select.Open(target.ItemSlot);
    }

    /// <summary>
    /// 인벤토리를 정렬하는 함수
    /// </summary>
    /// <param name="type">정렬되는 순서</param>
    private void OnItemSort(ItemType type)
    {
        inventory.SlotSorting(type, true);
        select.Close();
    }

    /// <summary>
    /// 아이템을 버리는 함수
    /// </summary>
    /// <param name="index"></param>
    private void OnItemDrop(uint index)
    {
        Slot_UI target = slotsUI[index];
        select.Close();
        dropSlot.Open(target.ItemSlot);
    }

    /// <summary>
    /// 버리기 창에서 확인 버튼을 누르면 실행되는 함수
    /// </summary>
    /// <param name="index">아이템을 버릴 슬롯의 index</param>
    /// <param name="count">아이템 버릴 개수</param>
    private void OnDropOk(uint index, uint count)
    {
        inventory.RemoveItem(index, count);
        dropSlot.Close();
    }
}

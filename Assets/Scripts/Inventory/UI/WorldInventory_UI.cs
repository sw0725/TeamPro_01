using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class WorldInventory_UI : MonoBehaviour
{

    WorldInventory worldInven;

    Slot_UI[] worldSlotUI;


    DragSlotUI worldDragSlotUI;

    SelectMenuUI worldSelect;

    DropSlotUI worldDropSlot;

    RectTransform worldRect;

    Scrollbar scrollbar;


    private void Awake()
    {
        Transform child = transform.GetChild(0);
        worldSlotUI = child.GetComponentsInChildren<Slot_UI>();

        child = transform.GetChild(1);
        worldDragSlotUI = child.GetComponent<DragSlotUI>();

        child = transform.GetChild(2);
        worldSelect = child.GetComponent<SelectMenuUI>();

        child = transform.GetChild(3);
        worldDropSlot = child.GetComponent<DropSlotUI>();

        worldRect = transform.gameObject.GetComponent<RectTransform>();   

        scrollbar = GetComponentInChildren<Scrollbar>();
    }

    private void Start()
    {
        scrollbar.value = 1;
    }

    public void InitializeWorldInventory(WorldInventory playerInventory)
    {
        worldInven = playerInventory;

        for (uint i = 0; i < worldSlotUI.Length; i++)
        {
            worldSlotUI[i].InitializeSlot(worldInven[i]);
            worldSlotUI[i].onDragBegin += OnItemMoveBegin;
            worldSlotUI[i].onDragEnd += OnItemMoveEnd;
            worldSlotUI[i].onRightClick += OnRightClick;
            worldSlotUI[i].OnClick += OnClick;

        }

        worldDragSlotUI.InitializeSlot(worldInven.DragSlot);  // 임시 슬롯 초기화

        worldSelect.onItemDrop += OnItemDrop;
        worldSelect.onItemSort += (by) =>
        {
            worldInven.MergeItems();
            OnItemSort(by);
        };
        worldSelect.Close();

        worldDropSlot.onOkClick += OnDropOk;
        worldDropSlot.Close();


    }

    /// <summary>
    /// 아이템 드래그 시작하면 실행되는 함수
    /// </summary>
    /// <param name="index">시작한 슬롯의 index</param>
    private void OnItemMoveBegin(uint index)
    {
        worldInven.MoveItem(index, worldDragSlotUI.Index);
        worldDragSlotUI.Open();
    }

    /// <summary>
    /// 아이템 드래그가 끝이나면 실행되는 함수
    /// </summary>
    /// <param name="index">끝난 슬롯의 index</param>
    private void OnItemMoveEnd(RectTransform rectParent, RectTransform rect, uint index)
    {
        if (rectParent == worldRect)
        {
            worldInven.MoveItem(worldDragSlotUI.Index, index);

            if (worldDragSlotUI.ItemSlot.IsEmpty)
            {
                worldDragSlotUI.Close();
            }
        }
        else
        {
            Slot_UI slot = rect.GetComponent<Slot_UI>();
            if(slot != null)
            {
                slot.SlotSwap(slot.ItemSlot, worldDragSlotUI.ItemSlot);
                //worldDragSlotUI.ClearSlot();
            }
        }

    }

    /// <summary>
    /// 슬롯을 클릭하면 실행되는 함수
    /// </summary>
    /// <param name="index"></param>
    private void OnClick(RectTransform rect, uint index)
    {
        if (!worldDragSlotUI.ItemSlot.IsEmpty)
        {
            OnItemMoveEnd(worldRect, rect, index);
        }
    }

    /// <summary>
    /// 슬롯을 우클릭 시 실행되는 함수
    /// </summary>
    /// <param name="index">우클릭한 슬롯의 index</param>
    private void OnRightClick(uint index)
    {
        // 버리기, 상세보기 등 UI따로 띄우기
        Slot_UI target = worldSlotUI[index];
        worldSelect.Open(target.ItemSlot);
    }

    /// <summary>
    /// 인벤토리를 정렬하는 함수
    /// </summary>
    /// <param name="type">정렬되는 순서</param>
    private void OnItemSort(ItemType type)
    {
        worldInven.SlotSorting(type, true);
        worldSelect.Close();
    }

    /// <summary>
    /// 아이템을 버리는 함수
    /// </summary>
    /// <param name="index"></param>
    private void OnItemDrop(uint index)
    {
        Slot_UI target = worldSlotUI[index];
        worldSelect.Close();
        worldDropSlot.Open(target.ItemSlot);
    }

    /// <summary>
    /// 버리기 창에서 확인 버튼을 누르면 실행되는 함수
    /// </summary>
    /// <param name="index">아이템을 버릴 슬롯의 index</param>
    /// <param name="count">아이템 버릴 개수</param>
    private void OnDropOk(uint index, uint count)
    {
        worldInven.RemoveItem(index, count);
        worldDropSlot.Close();
    }
}
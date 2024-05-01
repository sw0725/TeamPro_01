using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Equip_UI : MonoBehaviour
{
    Equip equip;

    PlayerInput inputActions;

    public Equip Equip => equip;

    [SerializeField] EquipSlot_UI[] equipSlot_UI;

    [SerializeField] DropSlotUI dropSlot;

    [SerializeField] InventoryManager invenManager;

    [SerializeField] RectTransform invenTransform;

    [SerializeField] CanvasGroup canvas;

    public ItemData data01;
    public ItemData data02;
    public ItemData data03;
    public ItemData data04;
    public ItemData data05;

    //Button sortButton;

    Player Owner => equip.Owner;


    ///// <summary>
    ///// �������� ����ߴٰ� �˸��� ��������Ʈ(ItemSlot : ����� �������� ���Կ� ���� ����)
    ///// </summary>
    //public Action<ItemSlot> onEquipped;


    private void Awake()
    {
        inputActions = new PlayerInput();

        Transform child = transform.GetChild(0);
        equipSlot_UI = child.GetComponentsInChildren<EquipSlot_UI>();

        child = transform.GetChild(1);
        dropSlot = child.GetComponent< DropSlotUI>();

        //child = transform.GetChild(2);
        // weightPanel = child.GetComponent<WeightPanel_UI>();

        //child = transform.GetChild(3);
        //sortButton = child.GetComponent<Button>();
        //sortButton.onClick.AddListener(() =>
        //{
        //    // OnItemSort(ItemType.Buff);
        //});

        invenManager = GetComponentInParent<InventoryManager>();

        invenTransform = GetComponent<RectTransform>();

        canvas = GetComponent<CanvasGroup>();
    }

    private void OnEnable()
    {
        // ����â Ű
    }



    void OnDisable()
    {
        // ����â Ű
    }

    public void InitializeInventory(Equip playerEquip)
    {
        equip = playerEquip;

        for (uint i = 0; i < equipSlot_UI.Length; i++)
        {
            equipSlot_UI[i].InitializeSlot(equip[i]);
            equipSlot_UI[i].onDragBegin += OnItemMoveBegin;
            equipSlot_UI[i].onDragEnd += OnItemMoveEnd;
            // equipSlot_UI[i].onRightClick += OnRightClick;
            equipSlot_UI[i].OnClick += OnClick;
        }
        invenManager.DragSlot.InitializeSlot(equip.DragSlot);  // �ӽ� ���� �ʱ�ȭ

        // dropSlot.onDropOk += OnDropOk;
        dropSlot.Close();

        // Close();
    }

    //private void Start()
    //{
    //    GameManager.Instance.WeaponBase.onReload += equip.Reload;
    //}

    ////public void PlusValue(ItemSlot slot)
    ////{
    ////    //Money += (int)slot.ItemData.Price;
    ////    //Owner.Weight += slot.ItemData.weight;
    ////}

    ///// <summary>
    ///// ������ ���� ���� �����κ��丮 �����ϰ� ����ȭ������ ������ �Լ�
    ///// </summary>
    ////public void InventoryResult()
    ////{
    ////    //int tenThousand = Money / 10000;
    ////    //int Thousand = (Money % 10000) / 1000;
    ////    //int hundred = (Money % 1000) / 100;

    ////    //Debug.Log($"10000�� {tenThousand}�� 1000�� {Thousand}�� 100�� {hundred}��");

    ////    GameManager game = GameManager.Instance;

    ////    //game.WorldInventory_UI.Money += Money;
    ////    equip.ClearInventory();
    ////    //Money = 0;
    ////    //Owner.Weight = 0;

    ////    // ���Ŀ� ����ȭ������ ������
    ////}



    /// <summary>
    /// ������ �巡�� �����ϸ� ����Ǵ� �Լ�
    /// </summary>
    /// <param name="index">������ ������ index</param>
    private void OnItemMoveBegin(ItemSlot slot)
    {
        invenManager.DragSlot.InitializeSlot(equip.DragSlot);  // �ӽ� ���� �ʱ�ȭ
        equip.MoveItem(slot, invenManager.DragSlot.ItemSlot);
        invenManager.DragSlot.Open();
    }



    /// <summary>
    /// ������ �巡�װ� ���̳��� ����Ǵ� �Լ�
    /// </summary>
    /// <param name="index">���� ������ index</param>
    private void OnItemMoveEnd(ItemSlot slot, RectTransform rect)
    {
        equip.MoveItem(invenManager.DragSlot.ItemSlot, slot);

        Inventory_UI inven;
        inven = FindObjectOfType<Inventory_UI>();

        if (inven != null)
        {
            //inven.MinusValue(slot, (int)slot.ItemCount);
            //inven.PlusValue(slot);
        }

        if (invenManager.DragSlot.ItemSlot.IsEmpty)
        {
            invenManager.DragSlot.Close();
        }

        // ���콺�� ���� �� ��ġ�� ���â�̶�� 
        // slot.EquipItem();                    ����ϰ�
        // ��� ���â�� �����ϰ�(�κ��丮�� �ִ� ���� �״�� �ΰ�)
        // onEquipped?.Invoke(slot);            ������ ���� ���� �˷��ֱ�
    }

    /// <summary>
    /// ������ Ŭ���ϸ� ����Ǵ� �Լ�
    /// </summary>
    /// <param name="index"></param>
    private void OnClick(ItemSlot slot, RectTransform rect)
    {
        if (!invenManager.DragSlot.ItemSlot.IsEmpty)
        {
            OnItemMoveEnd(slot, rect);
        }
    }

    ///// <summary>
    ///// ������ ��Ŭ�� �� ����Ǵ� �Լ�
    ///// </summary>
    ///// <param name="index">��Ŭ���� ������ index</param>
    //private void OnRightClick(uint index)
    //{
    //    // ������, �󼼺��� �� UI���� ����
    //    Slot_UI target = slotsUI[index];
    //    dropSlot.Open(target.ItemSlot);
    //}

    ///// <summary>
    ///// ������ â���� Ȯ�� ��ư�� ������ ����Ǵ� �Լ�
    ///// </summary>
    ///// <param name="index">�������� ���� ������ index</param>
    ///// <param name="count">������ ���� ����</param>
    ////private void OnDropOk(uint index, uint count)
    ////{
    ////    inventory.RemoveItem(index, count);
    ////    dropSlot.Close();
    ////}

    public void open()
    {
        canvas.alpha = 1;
        canvas.interactable = true;
        canvas.blocksRaycasts = true;
    }

    public void Close()
    {
        canvas.alpha = 0;
        canvas.interactable = false;
        canvas.blocksRaycasts = false;
    }

    private void InventoryOnOff(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        if (canvas.interactable)
        {
            Close();
        }
        else
        {
            open();
        }
    }
}

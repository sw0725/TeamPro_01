using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WorldInventory_UI : MonoBehaviour
{
    uint minPageCount = 1;
    uint maxPageCount = 4;

    uint pageCount = 1;


    public uint PageCount
    {
        get => pageCount;
        set
        {
            if(pageCount != value)
            {
                pageCount = Math.Clamp(value, minPageCount, maxPageCount);
                inputField.text = PageCount.ToString();
            }

        }
    }

    WorldInventory worldInven;


    Slot_UI[] slotsUI_Page1;
    Slot_UI[] slotsUI_Page2;
    Slot_UI[] slotsUI_Page3;
    Slot_UI[] slotsUI_Page4;


    DragSlotUI dragSlotUI;

    SelectMenuUI select;

    DropSlotUI dropSlot;

    TMP_InputField inputField;

    private void Awake()
    {
        Transform child = transform.GetChild(0);
        slotsUI_Page1 = child.GetComponentsInChildren<Slot_UI>();

        child = transform.GetChild(1);
        slotsUI_Page1 = child.GetComponentsInChildren<Slot_UI>();

        child = transform.GetChild(2);
        slotsUI_Page1 = child.GetComponentsInChildren<Slot_UI>();

        child = transform.GetChild(3);
        slotsUI_Page1 = child.GetComponentsInChildren<Slot_UI>();

        child = transform.GetChild(4);
        dragSlotUI = child.GetComponent<DragSlotUI>();

        child = transform.GetChild(5);
        select = child.GetComponent<SelectMenuUI>();

        child = transform.GetChild(6);
        dropSlot = child.GetComponent<DropSlotUI>();

        child = transform.GetChild(7);
        Button pagePlus = child.GetComponent<Button>();
        pagePlus.onClick.AddListener(() =>
        {

        });

        child = transform.GetChild(8);
        Button pageMinus = child.GetComponent<Button>();
        pageMinus.onClick.AddListener(() =>
        {

        });

        child = transform.GetChild(9);
        inputField = child.GetComponent<TMP_InputField>();

    }
}

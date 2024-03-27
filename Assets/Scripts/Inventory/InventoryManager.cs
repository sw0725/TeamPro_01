using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// 인벤토리 및 장비창 열고 닫기용 클래스
public class InventoryManager : MonoBehaviour
{

    PlayerInput inputActions;
    GameObject inventory_Base;

    bool isInventoryActive = false;

    private void Awake()
    {
        inputActions = new PlayerInput();
        inventory_Base = transform.GetChild(0).gameObject;
    }

    private void OnEnable()
    {
        inputActions.UI.Enable();
        inputActions.UI.Inventory.performed += OnInventory;
    }

    private void OnDisable()
    {
        inputActions.UI.Inventory.performed -= OnInventory;
        inputActions.UI.Disable();
    }

    private void OnInventory(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        if (!isInventoryActive)
        {
            inventory_Base.SetActive(true);
            isInventoryActive = true;
        }

        else
        {
            inventory_Base.SetActive(false);
            isInventoryActive = false;
        }
    }

    bool MousePointInRect()
    {
        Vector2 screenPos = Mouse.current.position.ReadValue();
        Vector2 diff = screenPos - (Vector2)transform.position; // 이 UI의 피봇에서 마우스 포인터가 얼마나 떨어져 있는지 계산

        RectTransform rectTransform = (RectTransform)transform;
        return rectTransform.rect.Contains(diff);
    }
}

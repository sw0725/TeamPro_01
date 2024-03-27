using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.FilePathAttribute;

public class Player : MonoBehaviour
{
    /// <summary>
    /// �÷��̾� �̵��ӵ�
    /// </summary>
    public float moveSpeed = 3f;

    /// �÷��̾� ������
    /// </summary>
    public float jumpPower = 5f;

    /// <summary>
    /// �÷��̾� ü��
    /// </summary>
    float hp = 100;
    public float maxHp = 100;
    float Hp
    {
        get => hp;
        set
        {
            if (hp != value)
            {
                hp = value;
                hp = Mathf.Clamp(hp, 0, maxHp);
                if (hp < 0.1)
                {
                    Die();
                }
            }
        }
    }

    /// <summary>
    /// �޸��� �������� �ӵ� 
    /// </summary>
    public float runningSpeed = 2.0f;

    /// <summary>
    /// ĳ���� ������Ʈ ������ ���� ���� ����
    /// </summary>
    private CharacterController cc;

    /// <summary>
    /// ��ǲ �ý��� ������ ����
    /// </summary>
    private PlayerMove inputActions;

    /// <summary>
    /// ������ ��ǥ����� ����? ����
    /// </summary>
    private Vector2 movementInput;

    /// <summary>
    /// �Ʒ��� �������� �߷�
    /// </summary>
    private float gravity = -20f;

    /// <summary>
    /// ĳ������ ���� �ӵ� ����
    /// </summary>
    private float yVelocity = 0;

    /// <summary>
    /// �޸��� ����
    /// </summary>
    private bool isRunning = false;
    private Vector3 moveDirection = Vector3.zero;
    public float limitWeight = 20f; // ���԰� �� ������ Ŭ �� �ӵ��� �پ��� ����
    public float MaxWeight = 40f; // ���԰� �� ������ Ŭ �� �̵��� ���ߴ� ����
    public float currentWeight = 10f; // ���� ����

    /// <summary>
    /// �÷��̾��� ����� �˸��� ��������Ʈ
    /// </summary>
    public Action onDie;

    public float defense = 0.0f;

    public Animator animator;
    private void Awake()
    {
        inputActions = new PlayerMove();
    }


    // ������ ���̸� �̿��� ������ �� �ִ� ȯ������ Ȯ��
    private void OnEnable()
    {
        inputActions.Player.Enable();
        inputActions.Player.Move.performed += OnMovePerformed;  // �̵� ����
        inputActions.Player.Move.canceled += OnMoveCanceled;    // �̵� ����
        inputActions.Player.Jump.performed += OnJump;           // ����
        inputActions.Player.Run.performed += OnRunPerformed;    // �޸��� ����
        inputActions.Player.Run.canceled += OnRunCanceled;      // �޸��� ����
        //inputActions.Player.InteractAction.performed += OnInteract;            // ��ȣ�ۿ�
        inputActions.Player.InventoryAction.performed += OnInventoryAction;    // �κ��丮 ���
        inputActions.Player.InventoryAction.canceled += OnInventoryAction;     // �κ��丮 ���
        inputActions.Player.Exit.performed += OnExit;                          // ����(�Ͻ�����)
        inputActions.Player.Reload.performed += OnReload;                      // ������
        inputActions.Player.LeftMouse.performed += OnLeftMouse;                // ���� ���콺 �Է�
        inputActions.Player.RightMouse.performed += OnRightMouse;              // ������ ���콺 �Է�
        inputActions.Player.HotbarKey.performed += OnHotbarKey;                // �ֹ�Ű ���
    }

    private void OnDisable()
    {
        inputActions.Player.Move.performed -= OnMovePerformed;
        inputActions.Player.Move.canceled -= OnMoveCanceled;
        inputActions.Player.Jump.performed -= OnJump;
        inputActions.Player.Run.performed -= OnRunPerformed;
        inputActions.Player.Run.canceled -= OnRunCanceled;
        //inputActions.Player.InteractAction.performed -= OnInteract;
        inputActions.Player.InventoryAction.performed -= OnInventoryAction;
        inputActions.Player.InventoryAction.canceled -= OnInventoryAction;
        inputActions.Player.Exit.performed -= OnExit;
        inputActions.Player.Reload.performed -= OnReload;
        inputActions.Player.LeftMouse.performed -= OnLeftMouse;
        inputActions.Player.RightMouse.performed -= OnRightMouse;
        inputActions.Player.HotbarKey.performed -= OnHotbarKey;
        inputActions.Player.Disable();
    }

    private void OnMovePerformed(InputAction.CallbackContext ctx)
    {
        movementInput = ctx.ReadValue<Vector2>();
    }

    private void OnMoveCanceled(InputAction.CallbackContext ctx)
    {
        movementInput = Vector2.zero;
    }

    private void OnJump(InputAction.CallbackContext ctx)
    {
        if (IsGrounded())
        {
            yVelocity = jumpPower;
        }
    }
    private bool IsGrounded()
    {
        float distanceToGround = cc.bounds.extents.y;
        bool isGrounded = Physics.Raycast(transform.position, -Vector3.up, distanceToGround + 0.2f);

        // ���� �ð�ȭ�� ���� �ڵ�
        if (isGrounded)
        {
            Debug.DrawLine(transform.position, transform.position + (-Vector3.up * (distanceToGround + 0.2f)), Color.green);
        }
        else
        {
            Debug.DrawLine(transform.position, transform.position + (-Vector3.up * (distanceToGround + 0.2f)), Color.red);
        }

        return isGrounded;
    }

    private void OnRunPerformed(InputAction.CallbackContext ctx)
    {
        isRunning = true;   // �޸��� ��
    }

    private void OnRunCanceled(InputAction.CallbackContext ctx)
    {
        isRunning = false;  // �޸��� ����
    }

    private void OnInventoryAction(InputAction.CallbackContext ctx)
    {
    }

    private void OnExit(InputAction.CallbackContext ctx)
    {
    }

    private void OnReload(InputAction.CallbackContext ctx)
    {
    }

    private void OnLeftMouse(InputAction.CallbackContext ctx)
    {
    }

    private void OnRightMouse(InputAction.CallbackContext ctx)
    {
    }

    private void OnHotbarKey(InputAction.CallbackContext ctx)
    {
        // int hotbarIndex = /* ���⿡�� �ֹ� Ű ��ȣ�� �������� �ڵ� �ۼ� */;
        // HotberKey(hotbarIndex);
    }


    private void Start()
    {
        cc = GetComponent<CharacterController>();
    }

    private void FixedUpdate()
    {
        
        // Vector3 moveDirection = new Vector3(movementInput.x, 0, movementInput.y);
        if (Camera.main != null)
        {
            Vector3 cameraForward = Camera.main.transform.forward;
            cameraForward.y = 0;
            Vector3 cameraRight = Camera.main.transform.right;
            cameraRight.y = 0;
            moveDirection = movementInput.x * cameraRight + movementInput.y * cameraForward;
        }
        float currentSpeed = moveSpeed;

        // ���� ���Կ� ���� �̵� �ӵ� ����
        if (currentWeight > limitWeight && currentWeight <= MaxWeight)
        {
            currentSpeed -= (currentWeight - limitWeight) / (MaxWeight - limitWeight) * (moveSpeed - 0f);
        }
        else if (currentWeight > MaxWeight)
        {
            currentSpeed = 0f;
        }

        currentSpeed = isRunning ? currentSpeed + runningSpeed : currentSpeed;
        moveDirection *= currentSpeed;
        if (IsGrounded())
        {
            yVelocity = Mathf.Max(0, yVelocity);
        }
        else
        {
            yVelocity += gravity * Time.deltaTime;
        }

        moveDirection.y = yVelocity;
        cc.Move(moveDirection * Time.deltaTime);
    }

    public void Damege(float damege) 
    {
        hp -= (damege/defense);
    }

    public void Die()
    {
        // ������ �Ұ��ϵ��� �����.
        inputActions.Player.Disable();


        // ��������Ʈ�� �׾����� �˸���(onDie ��������Ʈ)
        onDie?.Invoke();
    }
}

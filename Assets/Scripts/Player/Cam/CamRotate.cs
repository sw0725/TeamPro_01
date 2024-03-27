using UnityEngine;
using UnityEngine.InputSystem;

public class CamRotate : MonoBehaviour
{
    public float mouseSensitivity = 5f; // ���콺 ����
    public Transform playerBody; // �÷��̾� ��ü�� Transform ������Ʈ
    public LayerMask interactableLayerMask; // ��ȣ�ۿ� ������ ���̾� ����ũ

    private Vector2 lookInput; // ���콺 �Է� ���� ������ ����
    private float xRotation = 0f; // ī�޶��� ���� ȸ���� ������ ����

    private InputAction lookAction; // ���콺�� ���� ������ �����ϴ� �׼�
    private InputAction interactAction; // ��ȣ�ۿ� �׼�
    private PlayerMove inputActions; // ����� �Է��� ó���ϴ� ��ũ��Ʈ

    private void Awake()
    {
        inputActions = new PlayerMove(); // �Է� �׼� �ʱ�ȭ
        lookAction = inputActions.Player.Look;
        interactAction = inputActions.Player.InteractAction;

        lookAction.Enable();
        interactAction.Enable(); // �׼� Ȱ��ȭ
    }

    private void OnEnable()
    {
        lookAction.performed += OnLook;
        interactAction.performed += OnInteract; // �̺�Ʈ ������ ���
    }

    private void OnDisable()
    {
        lookAction.performed -= OnLook;
        interactAction.performed -= OnInteract; // �̺�Ʈ ������ ����

        lookAction.Disable();
        interactAction.Disable(); // �׼� ��Ȱ��ȭ
    }

    private void OnInteract(InputAction.CallbackContext context)
    {
        Ray ray = new Ray(transform.position, transform.forward); // ���� ��ġ���� �������� ����
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactableLayerMask))
        {
            // ��ȣ�ۿ� ������ ������Ʈ�� �浹���� ���� ����
            Debug.Log("Interacted with " + hit.collider.name); // �α׷� ��ȣ�ۿ� ǥ��
            hit.collider.SendMessage("Interact", SendMessageOptions.DontRequireReceiver); // Interact �޼��� ȣ��
        }
    }

    private void OnLook(InputAction.CallbackContext context)
    {
        lookInput = context.ReadValue<Vector2>(); // ���콺 �Է°� �б�

        float mouseX = lookInput.x * mouseSensitivity * Time.deltaTime;
        float mouseY = lookInput.y * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // ���� ȸ�� ����

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f); // ī�޶� ���� ȸ�� ����
        playerBody.Rotate(Vector3.up * mouseX); // �÷��̾� ��ü �¿� ȸ�� ����
    }
}

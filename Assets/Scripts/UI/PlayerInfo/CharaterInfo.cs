using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharaterInfo : MonoBehaviour
{
    //[Tooltip("���Ե� �迭 ����")]
    private int slotLength = 0;
    [Tooltip("���Ե� �迭. ���̴� ����â ���� ���� : 8")]
    [SerializeField] private Slot[] slots = new Slot[8];

    private Slot_UI slotDrag;


    private void Awake()
    {

    }

    private void CollectSlotsData()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i] = GetComponentsInChildren<Slot>()[i];
            Debug.Log(slots[i]);
        }
    }

    /// <summary>
    /// ������ �߰� �Լ� ȣ���ؼ� �߰�
    /// </summary>
    /// <param name="item">�߰��� ������</param>
    private void AddItemData(ItemData item)
    {
        bool addSuccess = false;

        for (int i = 0; i < slots.Length; i++)
        {
            addSuccess = slots[i].AddItem(item);

            if (addSuccess)
            {
                break;
            }
        }
    }
}

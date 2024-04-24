using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotEx : MonoBehaviour
{
    [Tooltip("�ش� ������ Ÿ�� �Ҵ�")]
    public List<ItemType> slotType = new List<ItemType>();
    [Tooltip("������ ������ ����")]
    [HideInInspector] public ItemData itemData = null;
    [Tooltip("itemData�� ������ ������ ���� �ӽ� �����")]
    private ItemData itemTemData = null;

    private ItemData ItemData
    {
        get
        {
            return itemData;
        }
        set
        {
            itemTemData = value;

            if (CompareItemType)
            {
                itemData = value;
            }
            else
            {
                itemData = null;
            }
        }
    }

    /// <summary>
    /// �ش� �������� ���Կ� �������� �˻� �� ����� ���� �� / ���� ����
    /// </summary>
    private bool CompareItemType
    {
        get
        {
            bool typeCheck = false;

            foreach (ItemType type in slotType)
            {
                if (itemTemData.itemType == type || itemTemData == null)
                {
                    typeCheck = true;

                    return typeCheck;
                }
            }

            return typeCheck;
        }
    }

    /// <summary>
    /// ���Կ� ������ ����. �κ��丮�� ������ �߰�.
    /// </summary>
    /// <returns>������ �߰��� �����̸� true ��ȯ, ���и� false ��ȯ</returns>
    public bool AddItem(ItemData item)
    {
        bool addItem = false;

        if (item != null)
        {
            ItemData = item;
            addItem = true;
        }
        else
        {
            ItemData = null;
            addItem = false;
        }

        return addItem;
    }
}

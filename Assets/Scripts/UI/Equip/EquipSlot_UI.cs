using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipSlot_UI : Slot_UI
{
    protected override void Awake()
    {
        Transform child = transform.GetChild(0);
        itemImage = child.GetComponent<Image>();
    }

    /// <summary>
    /// ����UI�� ȭ�� ����
    /// </summary>
    private void Refresh()
    {
        if (ItemSlot.IsEmpty)
        {
            // ��� ���� ��
            itemImage.color = Color.clear;   // ������ �����ϰ�
            itemImage.sprite = null;         // ��������Ʈ ����
            //itemText.text = string.Empty;  // ���ڵ� ����
        }
        else
        {
            // �������� ���������
            //if (!IsEquipment)
            //{
            //    // �������� ���������� �ƴϸ�
            //    itemImage.sprite = ItemSlot.ItemData.itemImage;      // ��������Ʈ �̹��� ����
            //    itemImage.color = Color.white;                       // �̹��� ���̰� �����
            //    //itemText.text = ItemSlot.ItemCount.ToString();    // ������ ���� ����
            //    //itemText.alpha = 1;

            //}
            //else
            //{
            //    itemImage.sprite = ItemSlot.ItemData.itemImage;      // ��������Ʈ �̹��� ����
            //    itemImage.color = Color.white;                       // �̹��� ���̰� �����
            //    //itemText.text = ItemSlot.ItemCount.ToString();    // ������ ���� ����
            //    //itemText.alpha = 0;
            //}

            itemImage.sprite = ItemSlot.ItemData.itemImage;      // ��������Ʈ �̹��� ����
            itemImage.color = Color.white;                       // �̹��� ���̰� �����
        }
    }
}

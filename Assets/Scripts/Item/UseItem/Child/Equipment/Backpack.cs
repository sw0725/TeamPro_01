using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class Backpack : ItemBase
{

    //public float maxWeight = 100.0f; // �ִ� ����
    //private float currentWeight = 0.0f; // ���� ����

    //// �������� ���濡 �߰��ϴ� �Լ�?
    //public bool AddItem(ItemBase item)
    //{
    //    if (currentWeight + item.weight <= maxWeight)
    //    {
    //        items.Add(item);
    //        currentWeight += item.weight;
    //        return true;
    //    }
    //    else
    //    {
    //        return false;
    //    }
    //}

    // �������� ���濡�� �����ϴ� �Լ�?
    //public void RemoveItem(ItemBase item)
    //{
    //    if (items.Remove(item))
    //    {
    //        currentWeight -= item.weight;
    //        Debug.Log(item.name + "��(��) ���濡�� ���ŵǾ����ϴ�.");
    //    }
    //}

    public override void Use()
    {

    }

    //public override void UnUse()
    //{

    //}
}

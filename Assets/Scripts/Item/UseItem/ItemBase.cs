using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class ItemBase : MonoBehaviour
{
    public int Price = 1000;
    public float Weight = 3.0f;

    public virtual void Use()
    {

    }

    //public ItemData Interaction(ItemCode itemCode)
    //{

    // gpt �̿��� �ڵ�� ���� ���ɼ��� �ֽ��ϴ�. ���� �´� �ڵ����� �� ���� ������ �ۼ��߽��ϴ�.

        //ItemData itemData = new ItemData();
        //itemData.Price = Price;
        //itemData.Weight = Weight;
        //// ������ �����Ϳ� ���� ������ ������ �� �ֽ��ϴ�.

        //// �����δ� ������ �������� �����Ͽ� �κ��丮�� ���� �� �ֽ��ϴ�.
        //// ���⼭�� �ܼ��� ������ �����͸� �����Ͽ� ��ȯ�մϴ�.

        //return itemData;

        //(ItemCode itemCode) ����

        //-----------------------------------------

        //Inventory.instance.AddItem(item);   // �κ��丮�� ������ �߰��ϱ�
        //Destroy(gameObject);

        //------------------------------------------------------------

        //// �÷��̾ �������� �ֿ��ٰ� �����ϰ�, ������ �����͸� �����մϴ�.
        //ItemData itemData = new ItemData(Price, Weight);

        //// ������ ������ �����͸� �κ��丮�� �߰��մϴ�.
        //Inventory.Instance.AddItem(itemData);

        //// ������ ������Ʈ�� ��Ȱ��ȭ�Ͽ� ȭ�鿡�� �����մϴ�.
        //gameObject.SetActive(false);

        // -----------------------------------------------------------

        //// �÷��̾ �������� �ֿ��ٰ� �����ϰ�, ������ �����͸� �����մϴ�.
        //ItemData itemData = new ItemData(Price, Weight);

        //// ������ ������ �����͸� �κ��丮�� �߰��մϴ�.
        //Inventory.Instance.AddItem(itemData);

        //// ������ ������Ʈ�� ��Ȱ��ȭ�Ͽ� ȭ�鿡�� �����մϴ�.
        //gameObject.SetActive(false);

        //// ������ �����͸� ��ȯ�մϴ�.
        //return itemData;

        // ------------------------------------------------

        //ItemData itemData = GameManager.Instance.ItemData[itemCode]; // ������ �����͸� ������ �ڵ带 �̿��Ͽ� ������

        //// �κ��丮�� �������� �߰��ϴµ� �����ϸ� true�� ��ȯ�ϰ�, �����ϸ� false�� ��ȯ
        //bool success = AddItem(itemCode);

        //if (success)
        //{
        //    return itemData; // �������� ���������� �߰����� ��� ������ ������ ��ȯ
        //}
        //else
        //{
        //    return null; // ������ �߰��� �������� ��� null ��ȯ
        //}
        //}
    }






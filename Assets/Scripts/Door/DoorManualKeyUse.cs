using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManualKeyUse : DoorManual
{
    public bool isExitDoor = false;
    bool isOpend = false;

    public override void Interect()
    {
        Open();
    }

    protected override bool preOpen()
    {
        return true; //GameManager.Instance.InventoryUI.Inventory.FindItem(ItemCode.Key);   ��ġ�� Ǳ�ô�
    }

    protected override void Open()
    {
        if (isExitDoor)
        {
            if (preOpen())
            {
                Debug.Log("Ż��");
                //ȭ����ȯ(���Ӹ޴�)
            }
        }
        else 
        {

            if (!isOpend)
            {
                if (preOpen())
                {
                    animator.SetBool(IsOpenHash, true);
                    isOpend = true;
                }
            }
        }
    }
}

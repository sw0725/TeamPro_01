using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManualKeyUse : DoorManual
{
    public override void Interect()
    {
        Open();
    }

    protected override bool preOpen()
    {
        bool result = true;
        //�÷��̾� �κ��丮 �˻� (Ű������ true)
        return result;
    }

    protected override void Open()
    {
        if (preOpen()) 
        {
            Debug.Log("Ż��");
            //ȭ����ȯ(���Ӹ޴�)
        }
    }
}

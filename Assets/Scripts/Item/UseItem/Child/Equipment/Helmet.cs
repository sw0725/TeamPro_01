using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helmet : ArmorBase
{
    public override void Use()
    {
         player.defense +=amountDefense; // �����ϸ� ������ �����ϴ� �ڵ�?
    }

    //public override void UnUse()
    //{
    //    player.defense -=amountDefense; // �����ϸ� ������ �پ��� �ڵ�?
    //}
}

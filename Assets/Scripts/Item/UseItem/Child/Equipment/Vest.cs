using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vest : ArmorBase
{
    public override void Use()
    {
        player.defense += amountDefense; // �����ϸ� ������ �����ϴ� �ڵ�?
    }

    public override void UnUse()
    {
        player.defense -= amountDefense; // �����ϸ� ������ �پ��� �ڵ�?
    }

    // ������ ���� �� ������ �ǰݴ��ҽ� ���� ���̴� �ڵ�?
    //public void TakeDamege(float damege)
    //{
    //    if (currentDefense > 0)
    //    {
    //        // ������ �ִ� ���
    //        int remainingDamage = damage - currentDefense;
    //        if (remainingDamage < 0)
    //        {
    //            // ������ ���������� �� ū ���
    //            currentDefense -= damage;
    //        }
    //        else
    //        {
    //            // ������ ���������� �۰ų� ���� ���
    //            currentDefense = 0;
    //            currentHealth -= remainingDamage;
    //        }
    //    }
    //    else
    //    {
    //        // ������ ���� ���
    //        currentHealth -= damage;
    //    }
    //}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vest : ArmorBase
{
    public override void Use()
    {
        if (player != null)
        {
            player.defense += amountDefense;    // �����ϸ� ������ �����ϴ� �ڵ�?
            Debug.Log("������ �����Ͽ����ϴ�.");
        }
        else
        {
            Debug.LogError("Player is not assigned when trying to use Heal.");
        }
    }

    public override void UnUse()
    {
        player.defense -= amountDefense; // �����ϸ� ������ �پ��� �ڵ�?
    }

#if UNITY_EDITOR
    public void Test_Use()
    {
        Use();
    }
#endif
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : BuffBase
{
    public override void Use()
    {
        if (player != null)
        {
            player.Heal(amountBuff);
            Debug.Log("HP�� ȸ���Ǿ����ϴ�.");
            base.Use();
            Destroy(gameObject);
        }
        else
        {
            Debug.LogError("Player is not assigned when trying to use Heal.");
        }
    }

#if UNITY_EDITOR
    public void Test_Use()
    {
        Use();
    }
#endif
}

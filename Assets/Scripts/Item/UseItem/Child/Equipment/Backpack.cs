using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class Backpack : ItemBase
{
    public float amountIncrese = 30.0f;
    public override void Use()
    {
        if (player != null)
        {
            player.limitWeight += amountIncrese;
            player.MaxWeight += amountIncrese;
            Debug.Log("������ �����Ͽ����ϴ�.");
        }
        else
        {
            Debug.LogError("Player is not assigned when trying to use Heal.");
        }
    }

    public override void UnUse()
    {
        player.limitWeight -= amountIncrese;
        player.MaxWeight -= amountIncrese; 
    }
}

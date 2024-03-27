using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorBase : ItemBase
{
    protected Player player;

    private void Awake()
    {
        player = GameManager.Instance.Player;
    }

    public override void Use()
    {

    }
}

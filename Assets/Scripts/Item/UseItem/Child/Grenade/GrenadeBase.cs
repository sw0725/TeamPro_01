using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeBase : ItemBase
{
    [Tooltip("������ ��")]
    public float throwForce = 100.0f;
    [Tooltip("������")]
    public float grenadeDamage = 100.0f;
    [Tooltip("�ݰ�")]
    public float range = 1.0f;
    [Tooltip("����")]
    public float weight = 0f;
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeBase : ItemBase
{
    [Tooltip("던지는 힘")]
    public float throwForce = 100.0f;
    [Tooltip("데미지")]
    public float grenadeDamage = 100.0f;
    [Tooltip("반경")]
    public float range = 1.0f;
    [Tooltip("무게")]
    public float weight = 0f;
}
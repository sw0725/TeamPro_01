using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType    //아이템 그룹 분리용 type
{
    Buff = 0,
    Grenade,
    Gun,
    Bullet,
    Trap,
    Key,
    Price,

}

public enum ItemCode    //아이템 개별 식별용 ID
{
    SmallHeal = 0,
    MiddleHeal = 1,
    BigHeal = 2,
    SmallSpeed = 3,
    MiddleSpeed = 4,
    BigSpeed = 5,
    SmallStrength = 6,
    MiddleStrength = 7,
    BigStrength = 8,
    ExplodeGrenade = 9,
    FireGrenade = 10,
    NoiseGrenade = 11,
    BoomTrap = 12,
    SlowTrap = 13,
    StunTrap = 14,
    Pistol = 15,
    Rifle = 16,
    Shotgun = 17,
    Sniper = 18,
    Key = 19,
    PistolBullet = 20,
    RifleBullet = 21,
    ShotgunBullet = 22,
    SniperBullet = 23,
    OneHundreadDol = 24,
    OneThousandDol = 25,
    TenThousandDol = 26,

}

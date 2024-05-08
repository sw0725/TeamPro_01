using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType    //������ �׷� �и��� type
{
    Buff = 0,
    Grenade,
    Gun,
    Pistol,   // pistol �߰�
    Bullet,
    Trap,
    Key,
    Price,
    Armor,
    BackPack,
    Helmet,
    Vest,     // Armor ��� backpack,helmet,vest �߰�
    WeaponParts

}

public enum ItemCode    //������ ���� �ĺ��� ID
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
    LowHelmet = 27,
    MiddleHelmet = 28,
    HighHelmet = 29,
    LowVest = 30,
    MiddleVest = 31,
    HighVest = 32,
    LowBackpack = 33,
    MiddleBackpack = 34,
    HighBackpack = 35,
    PistolSilencer = 36,
    RifleMagazine = 37,
    RifleScope = 38,
    RifleSilencer = 39,
    RifleStock = 40,
    ShotgunStock = 41,
    SniperMagazine = 42,
    SniperScope = 43,
    SniperSilencer = 44,
    SniperStock = 45,

}

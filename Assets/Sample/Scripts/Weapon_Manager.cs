using UnityEngine;
using System;

public class Weapon_Manager
{
    public Weapon Equipped_Weapon {get; private set;}

    private readonly Weapon[] weapons;

    public Weapon_Manager(Weapon[] weapons)
    {
        this.weapons = weapons;
    }

    public void Equip_Weapon(int index)
    {
        if(index < 0 || index >= weapons.Length)
            throw new ArgumentOutOfRangeException(nameof(index), index, "Index is out of range");

        Equipped_Weapon = weapons[index];
        Debug.Log("Weapon equipped: " + Equipped_Weapon.Name);
    }
}
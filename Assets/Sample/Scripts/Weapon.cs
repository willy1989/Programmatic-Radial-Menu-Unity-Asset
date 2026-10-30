using UnityEngine;

public class Weapon
{
    public readonly string Name;
    private readonly int damage;

    public Weapon(string name, int damage)
    {
        this.Name = name;
        this.damage = damage;
    }

    public void Strike()
    {
        Debug.Log(Name + " inflicted "+ damage + " damage points.");
    }
}
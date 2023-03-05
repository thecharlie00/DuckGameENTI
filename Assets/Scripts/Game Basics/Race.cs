using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Race
{
    public int idDatabase;
    public string name;
    public float health;
    public float speed;
    public float rateOfFire;
    public float damage;
    public float jumpPower;
    public enum RaceType { REPTILE, MOUSE, FELINE }
    RaceType raceType;
    public Race()
    {

    }

    public Race(string name, float speed, float rateoffire, float health, float damage, float jumppower, int id)
    {
        this.name = name;
        this.speed = speed;
        this.health = health;
        this.rateOfFire = rateoffire;
        this.damage = damage;
        this.jumpPower = jumppower;
        this.idDatabase = id;

        switch (name)
        {
            case "Reptile":
                raceType = RaceType.REPTILE;
                break;
            case "Mouse":
                raceType = RaceType.MOUSE;
                break;
            case "Feline":
                raceType = RaceType.FELINE;
                break;
        }

    }
    public string GetNameClass()
    {
        return name;
    }
    public float GetSpeed()
    {
        return speed;
    }
    public float GetRateOfFire()
    {
        return rateOfFire;
    }
    public float GetHealth()
    {
        return health;
    }
    public RaceType GetRaceType()
    {
        return raceType;
    }
    public void SetClass(RaceType raceType)
    {
        this.raceType = raceType;
    }
    public float GetDamage()
    {
        return damage;
    }
    public int GetIdBDD()
    {
        return idDatabase;
    }
}

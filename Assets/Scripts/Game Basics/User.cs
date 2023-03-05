using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class User 
{
    public string nick;
    public int idInDatabase;
    public int idRaceAssigned;
    public Race races;

    public User()
    {

    }
    public User(string username, int idDB, int idClass)
    {
        this.nick = username;
        this.idInDatabase = idDB;
        this.idRaceAssigned = idClass;
    }

    public string GetNickName()
    {
        return nick;
    }
    public int GetId()
    {
        return idInDatabase;
    }
    public void SetRace(Race _race)
    {
        this.races = _race;
    }
    public Race GetRace()
    {
        return races;
    }
    public int GetIdRaceAssigned()
    {
        return idRaceAssigned;
    }
}

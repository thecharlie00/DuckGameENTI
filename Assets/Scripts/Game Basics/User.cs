using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class User 
{
    public string nick;
    public int idInDatabase;
   
    

    public User()
    {

    }
    public User(string username, int idDB)
    {
        this.nick = username;
        this.idInDatabase = idDB;
    }

    public string GetNickName()
    {
        return nick;
    }
    public int GetId()
    {
        return idInDatabase;
    }
    
}

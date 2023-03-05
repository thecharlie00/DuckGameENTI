using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;
using System.IO;
using System;

public class Network_Manager : MonoBehaviour
{
    public static Network_Manager _NETWORK_MANAGER;

    private TcpClient socket;
    private NetworkStream stream;
    private StreamWriter writer;
    private StreamReader reader;
    private bool connected = false;
    public static User currentUser = new User();
    [SerializeField]
    List<Race> avaiableRaces = new List<Race>();
    //Server variables
    const string host = "192.168.1.42";
    const int port = 6543;

    private void Awake()
    {
        
        if (_NETWORK_MANAGER != null && _NETWORK_MANAGER != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            
            _NETWORK_MANAGER = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    private void Update()
    {
        
        if (connected)
        {
            
            if (stream.DataAvailable)
            {
                
                string data = reader.ReadLine();
                
               
                if (data != null)
                {
                    ManageData(data);
                }
            }
        }
    }

    public void ConnectToServer(string nick, string password)
    {
        try
        {   
            
            socket = new TcpClient(host, port);
            stream = socket.GetStream();
            
            
            connected = true;
            
            
            writer = new StreamWriter(stream);
            reader = new StreamReader(stream);
            
            
            writer.WriteLine("0" + "/" + nick + "/" + password);
            
           
            writer.Flush();

        }
        catch (Exception e)
        {
            Debug.Log(e.ToString());
        }
    }

    private void ManageData(string data)
    {
        
        if (data == "ping")
        {
            Debug.Log("Recibo ping");
            writer.WriteLine("1");
            writer.Flush();
        }
        else if (data.Split('/')[0] == "2")
        {
            Debug.Log("Logeo Correcto");
            

            
            writer.Flush();
        }
        else if (data == "3")
        {
            Debug.Log("Logeo Incorrecto");
            writer.Flush();
        }
        else if (data == "UserNick")
        {
            Debug.Log(data.Split('/')[1]);
            writer.Flush();
        }
        else if (data.Split('|')[0] == "GetAllRaces")
        {
            string[] races = data.Split('|');
            SetAvaiableRaces(races);
        }
        else if (data.Split('|')[0] == "4")
        {
            string[] userAndClasses = data.Split('|');

            string[] dataUser = userAndClasses[1].Split('/');

            
            SetNewUserWithRace(dataUser);

            //LoadRoomsScene();

            Debug.Log("Logeo con clase asignada");
        }
        else if (data.Split('|')[0] == "Class")
        {
            
            Photon_Manager._PHOTON_MANAGER.enemyRace = GetRaceByString(data.Split('|')[1]);
           

        }
    }

    public void LogIn(string nick, string password)
    {
        try
        {
           
            socket = new TcpClient(host, port);
            stream = socket.GetStream();

            
            connected = true;

            
            writer = new StreamWriter(stream);
            reader = new StreamReader(stream);

            
            writer.WriteLine("Login" + "/" + nick + "/" + password);

            
            writer.Flush();

        }
        catch (Exception e)
        {
            Debug.Log(e.ToString());
        }
    }
    public void Register(string nick, string password)
    {
        try
        {
            
            socket = new TcpClient(host, port);
            stream = socket.GetStream();

            
            connected = true;

            
            writer = new StreamWriter(stream);
            reader = new StreamReader(stream);

            
            writer.WriteLine("Register" + "/" + nick + "/" + password);

            
            writer.Flush();

        }
        catch (Exception e)
        {
            Debug.Log(e.ToString());
        }
    }

    public void SendInfoToAddRaceAndPlayer(string raceName)
    {
        try
        {
            
            socket = new TcpClient(host, port);
            stream = socket.GetStream();

           
            connected = true;

            
            writer = new StreamWriter(stream);
            reader = new StreamReader(stream);

            
            writer.WriteLine("AddRaceUser" + "/" + currentUser.GetId() + "/" + raceName);

            
            writer.Flush();

        }
        catch (Exception e)
        {
            Debug.Log(e.ToString());
        }

    }

    public void SendNickToGetRace(string nick)
    {
        try
        {
           
            socket = new TcpClient(host, port);
            stream = socket.GetStream();

          
            connected = true;

          
            writer = new StreamWriter(stream);
            reader = new StreamReader(stream);

            
            writer.WriteLine("GetRaceByNickName" + "/" + nick);

            
            writer.Flush();

        }
        catch (Exception e)
        {
            Debug.Log(e.ToString());
        }
    }
    public User GetCurrentUser()
    {
        return currentUser;
    }

    public List<Race> GetAvaiableRaces()
    {
        return avaiableRaces;
    }

    void SetNewUserWithRace(string[] userWithRace)
    {
        string nick = userWithRace[0];
        string id = userWithRace[1];
        string idRaceAssignded = userWithRace[2];
        Photon_Manager._PHOTON_MANAGER.name = nick;

        currentUser.nick = nick;
        currentUser.idInDatabase = int.Parse(id);
        currentUser.idRaceAssigned = int.Parse(idRaceAssignded);

    }
    void SetAvaiableRaces(string[] races)
    {
        
        List<String> listStringsAvatars = new List<string>(races);
        if (listStringsAvatars[0].Contains("GetAllRaces"))
        {
            listStringsAvatars.RemoveAt(0);
        }
        
        for (int i = 0; i < listStringsAvatars.Count; i++)
        {
            
            string[] fieldsRace = listStringsAvatars[i].Split('/');

            string name = fieldsRace[0];
            float health = float.Parse(fieldsRace[1]);
            float damage = float.Parse(fieldsRace[2]);
            float speed = float.Parse(fieldsRace[3]);
            float jumpower = float.Parse(fieldsRace[4]);
            float rateoffire = float.Parse(fieldsRace[5]);
            int idDatabase = int.Parse(fieldsRace[6]);

            Race race = new Race();
            race.name = name;
            race.speed = speed;
            race.rateOfFire = rateoffire;
            race.health = health;
            race.damage = damage;
            race.idDatabase = idDatabase;


            avaiableRaces.Add(race);
        }
        foreach (Race r in avaiableRaces)
        {
            if (r.idDatabase == currentUser.GetIdRaceAssigned())
            {
                currentUser.SetRace(r);
            }
        }
    }

    public Race GetRaceByString(string _race)
    {
        string[] fieldsRace = _race.Split('/');
        string name = fieldsRace[0];
        float health = float.Parse(fieldsRace[1]);
        float damage = float.Parse(fieldsRace[2]);
        float speed = float.Parse(fieldsRace[3]);
        float jumpower = float.Parse(fieldsRace[4]);
        float rateoffire = float.Parse(fieldsRace[5]);
        int idDatabase = int.Parse(fieldsRace[6]);

        Race classToReturn = new Race(name, speed, rateoffire, health, damage, jumpower, idDatabase);
        return classToReturn;
    }
    public Race GetRaceByNickname(string nicknamePlayer)
    {
        foreach (Race r in avaiableRaces)
        {
            if (r.GetIdBDD() == currentUser.GetIdRaceAssigned())
            {
                if (nicknamePlayer == currentUser.GetNickName())
                {
                    currentUser.SetRace(r);
                    currentUser.SetRace(r);
                    Debug.Log("Mi raza es: " + r.GetNameClass());
                    return r;

                }
            }
        }
        return null;
    }


    public void AssingRaceToUser(Race _race)
    {
        currentUser.SetRace(_race);
    }



    

}



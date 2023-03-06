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
    public bool connected = false;
    public static User currentUser = new User();
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

   

    private void ManageData(string data)
    {

        if (data == "1")
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

   
    public User GetCurrentUser()
    {
        return currentUser;
    }

}



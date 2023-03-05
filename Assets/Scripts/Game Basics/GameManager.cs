using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject spawnPlayer1;

    [SerializeField]
    private GameObject spawnPlayer2;

    private void Awake()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            if(spawnPlayer1 != null)
            {
                PhotonNetwork.Instantiate("Player", spawnPlayer1.transform.position, Quaternion.identity);
            }
            
        }
        else
        {
            if (spawnPlayer2 != null)
            {
                PhotonNetwork.Instantiate("Player", spawnPlayer2.transform.position, Quaternion.identity);
            }
                
        }
    }

    public void ToLogin()
    {
        SceneManager.LoadScene("LoginScreen");
    }

    public void ToRegister()
    {
        SceneManager.LoadScene("RegisterScreen");
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RegisterBehaviour : MonoBehaviour
{
    [SerializeField] Button registerButton;
    //[SerializeField] Button backButton;
    [SerializeField] Text nickRegisterText;
    [SerializeField] Text registerPasswordText;
    
    public void OnRegisterButtonClick()
    {
        if (nickRegisterText.text != ""  && registerPasswordText.text != "")
        {
            Network_Manager._NETWORK_MANAGER.Register(nickRegisterText.text, registerPasswordText.text);
            SceneManager.LoadScene("LoginScreen");
        }
    }
}

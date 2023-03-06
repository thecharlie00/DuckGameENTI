using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Login_Screen : MonoBehaviour
{
    [SerializeField] private Button loginButton;
    [SerializeField] private Text loginText;
    [SerializeField] private Text passwordText;
    private void Awake()
    {
        //Defino el listener para cada vez que se haga click al boton
        loginButton.onClick.AddListener(Clicked);
    }

     

        

        private void Clicked()
        {
            if (loginText.text != "" && passwordText.text != "")
            {
                Network_Manager._NETWORK_MANAGER.LogIn(loginText.text, passwordText.text);
                SceneManager.LoadScene("JoinCreate");
            }
        }
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Button createButton;
    public Button joinButton;
    public Text createText;
    public Text joinText;

    private void Awake()
    {
        createButton.onClick.AddListener(CreateRoom);
        joinButton.onClick.AddListener(JoinRoom);
    }
    private void CreateRoom()
    {
        Photon_Manager._PHOTON_MANAGER.CreateRoom(createText.text.ToString());

    }
    private void JoinRoom()
    {

        Photon_Manager._PHOTON_MANAGER.JoinRoom(joinText.text.ToString());
    }
}

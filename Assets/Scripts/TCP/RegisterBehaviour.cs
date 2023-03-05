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
    [SerializeField] Dropdown raceSelector;
    [SerializeField] string raceSelected;
    Race race;

    private void Awake()
    {
        Network_Manager._NETWORK_MANAGER.GetAvaiableRaces();
    }
    public void OnRegisterButtonClick()
    {
        if (nickRegisterText.text != ""  && registerPasswordText.text != "" && raceSelected != null)
        {
            Network_Manager._NETWORK_MANAGER.Register(nickRegisterText.text, registerPasswordText.text);

            //Network_Manager._NETWORK_MANAGER.AssingRaceToUser(race);
            //Network_Manager._NETWORK_MANAGER.SendInfoToAddRaceAndPlayer(raceSelected);
            SceneManager.LoadScene("LoginScreen");



        }
    }

    public void RaceSelector()
    {
        if (raceSelector.value == 0)
        {
            
            race.SetClass(Race.RaceType.REPTILE);
            raceSelected = "Reptile";

        }
        else if (raceSelector.value == 1)
        {
            race.SetClass(Race.RaceType.MOUSE);
            raceSelected = "Mouse";
        }
        else if (raceSelector.value == 2)
        {
            race.SetClass(Race.RaceType.FELINE);
            raceSelected = "Feline";
        }
    }
}

using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
public class CheckInternet : MonoBehaviour
{

    public GameObject InternetPanel;
    string m_ReachabilityText;
    void Update()
    {

       
        //Check if the device cannot reach the internet
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            //Change the Text
            InternetPanel.SetActive(true);
            Time.timeScale = 0;
        }

        //Check if the device can reach the internet via a carrier data network
        else if (Application.internetReachability == NetworkReachability.ReachableViaCarrierDataNetwork)
        {
            InternetPanel.SetActive(false);
            Time.timeScale = 1;

        }

        //Check if the device can reach the internet via a LAN
        else if (Application.internetReachability == NetworkReachability.ReachableViaLocalAreaNetwork)
        {
            InternetPanel.SetActive(false);
            Time.timeScale = 1;

        }
    }
}
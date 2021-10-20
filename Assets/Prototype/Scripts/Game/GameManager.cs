using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int _coins;
    public int _energy = 25;
    public int _shield;
    public float _minutes;

    public int _maxEnergy = 50;
    private bool mIsFull = true; 

    private void Start()
    {
        DontDestroyOnLoad(this.gameObject); //Stays throughout the game
        StartCoroutine(AutomaticEnergyRefiller());
    }

    private void Update()
    {
        if (_energy == _maxEnergy)
        {
            mIsFull = false;
            return;
        }
        else
        {
            mIsFull = true;
        }

        ///Changing From MainScene to the EnergyScene  
        if (Input.GetKeyDown(KeyCode.E))
        {
            SceneManager.LoadScene(1);
        }
        ///Changing From MainScene to the CoinScene  
        if (Input.GetKeyDown(KeyCode.C))
        {
            SceneManager.LoadScene(2);
        }
        //This is used Fot loading Spinwheel Scene
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(7);
        }
    }
    private IEnumerator AutomaticEnergyRefiller()
    {
        while (mIsFull)
        {
            yield return new WaitForSeconds(MinutesToSecondsConverter(_minutes));
            _energy += 1;
        }
    }

    /// <summary>
    /// Converts the minutes given at Inspector into seconds and passes it to the coroutine function
    /// </summary>
    /// <param name="inMinutes"></param>
    /// <returns></returns>
    private float MinutesToSecondsConverter(float inMinutes) 
    {
        float seconds = inMinutes * 60;
        return seconds;
    }

}

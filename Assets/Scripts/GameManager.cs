using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int _coins;
    public int _energy;
    public int _shield;

    public int _minutes;

    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        StartCoroutine(AutomaticEnergyRefiller());
    }

    private void Update()
    {
        LoadData();
    }

    private IEnumerator AutomaticEnergyRefiller()
    {
        while (true)
        {
            yield return new WaitForSeconds(MinutesToSecondsConverter(_minutes));
            _energy += 1;
        }
    }

    /// <summary>
    /// Loads the data everytime if they are changed
    /// </summary>
    private void LoadData()
    {
        _energy = _energy;
    }

    /// <summary>
    /// Converts the minutes given at Inspector into seconds and passes it to the coroutine function
    /// </summary>
    /// <param name="inMinutes"></param>
    /// <returns></returns>
    private int MinutesToSecondsConverter(int inMinutes)
    {
        int seconds = inMinutes * 60;
        return seconds;
    }
}

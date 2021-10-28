using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int _coins;
    public int _energy = 25;
    public int _shield;
    public float _minutes;

    public int _maxEnergy = 50;
    private bool mIsFull = true;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this); //Singleton
            return;
        }
        Destroy(this.gameObject);
    }

    private void Start()
    {
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

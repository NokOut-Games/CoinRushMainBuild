using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigSpawner : MonoBehaviour
{
    public GameObject[] _CoinPigs;
    public List<Transform> _PigsSpawnPoint;

    private void Awake()
    {
        for (int i = 0; i < this.gameObject.transform.childCount; i++)
        {
            _PigsSpawnPoint.Add(this.transform.GetChild(i).transform);
        }
        PigShuffle();
    }

    void Start()
    {
        for (int i = 0; i < _CoinPigs.Length; i++)
        {
            Instantiate(_CoinPigs[i].gameObject, _PigsSpawnPoint[i].position, _PigsSpawnPoint[i].rotation);
        }
        
    }

    private void PigShuffle()
    {
        int j = 0;
        for (int i = 0; i <= _CoinPigs.Length - 1; i++)
        {
            int randomValue = Random.Range(i, _CoinPigs.Length);
            GameObject RandomNumber = _CoinPigs[randomValue];
            _CoinPigs[randomValue] = _CoinPigs[i];
            _CoinPigs[i] = RandomNumber;
           
        }
    }
    public void BackToMainScene()
    {
        LevelLoadManager.instance.BacktoHome(); //Need to change it from zero to some other value. Will be doing that when scene save system is Done.
    }
}

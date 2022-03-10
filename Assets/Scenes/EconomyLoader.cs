using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class EconomyLoader : MonoBehaviour
{
    public List<string> ListOfStringsInExcel = new List<string>();

    public List<string> Building = new List<string>();

    public MiniGameEconomy minigameEconomy = new MiniGameEconomy();


    public void OnUploadButtonClick(string inName)
    {
        ListOfStringsInExcel.Clear();
        TextAsset LevelLoader = Resources.Load(inName) as TextAsset; //Using Resources to Load Level Files 
        string text = LevelLoader.text;
        string[] line = Regex.Split(text, "\n");     //Splits Line by Line
        int row = line.Length - 1;                      //Store as row and pass on to levelbase
        string[][] levelbase = new string[row][];

        for (int i = 0; i < row; i++)
        {
            string[] stringLine = Regex.Split(line[i], ",");
            levelbase[i] = stringLine;
        }
        for (int x = 0; x < levelbase.Length; x++)
        {
            for (int z = 0; z < levelbase[0].Length; z++)
            {
                if (levelbase[x][z].Trim() != "")
                {
                    ListOfStringsInExcel.Add(levelbase[x][z].Trim());
                }
            }
        }
        switch (inName)
        {
            case "Economy":
                UploadBuildingData();
                break;
            case "EnergyReward":
                UploadData(minigameEconomy.EnergyReward, inName);
                break;
            case "CoinReward":
                UploadData(minigameEconomy.CoinReward, inName);
                break;
            case "SlotReward":
                UploadData(minigameEconomy.SlotReward, inName);
                break;
            case "AttackReward":
                UploadData(minigameEconomy.AttackReward, inName);
                break;
            case "SpinReward":
                UploadData(minigameEconomy.SpinReward, inName);
                break;
            default:
                break;
        }
    }

    public void UploadBuildingData()
    {
        for (int i = 0; i < ListOfStringsInExcel.Count; i += 2)
        {
            Building.Add( ListOfStringsInExcel[i + 1]);
        }
        int levelNo = 1;
        List<string> costes = new List<string>();
        int j = 0;
        foreach (var item in Building)
        {
            costes.Add(item);
            if (j % 40 == 39)
            {
                FirebaseManager.Instance.WriteEconomy("Level"+levelNo, costes);
                levelNo += 1;
                costes.Clear();
            }
            j++;
        }
    }
    public void UploadData(List<string> list,string inTitle)
    {
        for (int i = 0; i < ListOfStringsInExcel.Count; i += 2)
        {
            list.Add(ListOfStringsInExcel[i + 1]);
        }
    }
    public void WriteTofirebase()
    {
        FirebaseManager.Instance.WriteMiniGameEconomy(minigameEconomy);
    }
}

[System.Serializable]
public class LevelsCost
{
    public List<Structure> Buildings = new List<Structure>();
}

[System.Serializable]
public class Structure
{
    public List<Upgrade> UpGrades = new List<Upgrade>();
}

[System.Serializable]
public class Upgrade
{
    public string cost;
}

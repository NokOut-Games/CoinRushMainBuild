using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class EconomyLoader : MonoBehaviour
{
    public List<string> levels = new List<string>();
    public List<string> ListOfStringsInExcel = new List<string>();
    Dictionary<string, string> Building = new Dictionary<string, string>();

    void Start()
    {
        TextAsset LevelLoader = Resources.Load("Economy") as TextAsset; //Using Resources to Load Level Files 
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

        CreateDictionary();

    }

    public void CreateDictionary()
    {
        
        for (int i = 0; i < ListOfStringsInExcel.Count ; i+=2)
        {
            Building.Add(ListOfStringsInExcel[i], ListOfStringsInExcel[i + 1]);
            levels.Add(ListOfStringsInExcel[i + 1]);
        }

        int levelNo = 1;
        Dictionary<string, string> costes = new Dictionary<string, string>();
        int j = 0;
        foreach (var item in Building)
        {
            
            costes.Add(item.Key, item.Value);
            Debug.Log(j % 40);
            if (j % 40 == 39)
            {
               FirebaseManager.Instance.WriteEconomy(levelNo, costes);
                levelNo += 1;
                costes.Clear();
            }
            j++;
        }
       
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

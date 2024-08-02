using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ScenarioManager : MonoBehaviour
{
    [System.Serializable]
    public class Scenario
    {
        public int ID;
        public string Character;
        public string Dialogue;
    }

    public List<Scenario> scenarios = new List<Scenario>();
    public string csvFileName = "Tutorial";

    void Start()
    {
        LoadCSV();
    }

    void LoadCSV()
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, csvFileName);
        //Debug.Log("File path: " + filePath);

        if (File.Exists(filePath))
        {
            string[] lines = File.ReadAllLines(filePath);

            for (int i = 1; i < lines.Length; i++)
            {
                string[] fields = lines[i].Split(',');

                Scenario scenario = new Scenario();
                scenario.ID = int.Parse(fields[0]);
                scenario.Character = fields[1];
                scenario.Dialogue = fields[2];

                scenarios.Add(scenario);
            }

            Debug.Log("CSV Loaded Successfully.");
        }
        else
        {
            Debug.LogError("CSV file not found.");
        }
    }
}

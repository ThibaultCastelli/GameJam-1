using CustomVariablesTC;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveManager : MonoBehaviour
{
    [SerializeField] IntReference highscore;

    private string fullPath = "";
    private Save currSave;

    private void Awake()
    {
        fullPath = Application.persistentDataPath + "/save.json";
        LoadSave();
    }

    private void LoadSave()
    {
        if (File.Exists(fullPath))
        {
            string json = File.ReadAllText(fullPath);
            currSave = JsonUtility.FromJson<Save>(json);
        }
        else
            currSave = new Save();

        highscore.Value = currSave.highscore;
    }

    public void Save()
    {
        currSave.highscore = highscore.Value;
        string json = JsonUtility.ToJson(currSave);
        File.WriteAllText(fullPath, json);        
    }
}

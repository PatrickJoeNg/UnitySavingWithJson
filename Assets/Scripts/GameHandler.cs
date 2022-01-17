using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;

public class GameHandler : MonoBehaviour
{

    [Header("References")]
    [SerializeField] TMP_InputField xPosInputField;
    [SerializeField] TMP_InputField yPosInputField;
    [SerializeField] TMP_InputField zPosInputField;
    [SerializeField] TMP_InputField healthInputField;

    [Header("Text References")]
    [SerializeField] TextMeshProUGUI statusText;
    [SerializeField] TextMeshProUGUI statusLabel;

   // [Header("General Parameters")]
    private float xPosInput;
    private float yPosInput;
    private float zPosInput;
    private int healthInput;

    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("Is Running");
        //PlayerData playerData = new PlayerData();
        //playerData.position = new Vector3(5, 0);
        //playerData.health = 80;

        //string json = JsonUtility.ToJson(playerData);

        //Debug.Log(json);

        //File.WriteAllText(Application.dataPath + "/saveFile.json", json);

        DefaultInputFields();
    }

    public void SaveGameData()
    {
        PlayerData playerData = new PlayerData();

        float.TryParse(xPosInputField.text, out xPosInput);
        float.TryParse(yPosInputField.text, out yPosInput);
        float.TryParse(zPosInputField.text, out zPosInput);

        //xPosInput = float.Parse(xPosInputField.text);
        //yPosInput = float.Parse(yPosInputField.text);
        //zPosInput = float.Parse(zPosInputField.text);

        int.TryParse(healthInputField.text, out healthInput);

        //healthInput = int.Parse(healthInputField.text);

        playerData.position = new Vector3(xPosInput, yPosInput, zPosInput);
        playerData.health = healthInput;

        SetStatusText(FileStatus.Save, playerData);
        DefaultInputFields();
        SaveToFile(playerData);
    }

    public void LoadGameData()
    {
        PlayerData playerData = new PlayerData();

        string json = File.ReadAllText(Application.dataPath + "/saveFile.json");
        PlayerData loadedPlayerData = JsonUtility.FromJson<PlayerData>(json);

        playerData.position = loadedPlayerData.position;
        playerData.health = loadedPlayerData.health;

        Debug.Log(playerData.position);
        Debug.Log(playerData.health);

        SetStatusText(FileStatus.Load, playerData);
        DefaultInputFields();
    }

    private void SaveToFile(PlayerData playerData)
    {
        string json = JsonUtility.ToJson(playerData);
        Debug.Log(json);
        File.WriteAllText(Application.dataPath + "/saveFile.json", json);
    }

    public void SetStatusText(FileStatus fileStatus, PlayerData playerData)
    {
        if (fileStatus == FileStatus.Save)
        {
            statusLabel.text = "Data Saved!";
            statusText.text = $"Position: {playerData.position}\nHealth: {playerData.health}";
        }
        else if (fileStatus == FileStatus.Load)
        {
            statusLabel.text = "Data Loaded!";
            statusText.text = $"Position: {playerData.position}\nHealth: {playerData.health}";
        }
        else if (fileStatus == FileStatus.Delete)
        {
            statusLabel.text = "Data Deleted!";
            statusText.text = "Position:\nHealth:";
        }
    }

    public void DefaultInputFields()
    {
        xPosInputField.text = "0.0";
        yPosInputField.text = "0.0";
        zPosInputField.text = "0.0";
        healthInputField.text = "0";
    }

    public void ClearStatusText()
    {
        statusLabel.text = "Status:";
        statusText.text = "Position:\nHealth:";
    }

    public void DeleteSaveData()
    {
        PlayerData playerData = new PlayerData();

        File.Delete(Application.dataPath + "/saveFile.json");

        DefaultInputFields();
        SetStatusText(FileStatus.Delete,playerData);
    }
}

using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SaveData : MonoBehaviour
{
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] Settings settings;
    [SerializeField] TMP_Text waterVolumeValue;
    [SerializeField] TMP_Text natureVolumeValue;
    [SerializeField] TMP_Text UIVolumeValue;
    [SerializeField] TMP_Dropdown graphicsQualityDropdown;
    [SerializeField] List<Toggle> graphicsToggles;

    string path;

    [System.Serializable]
    class DataSave
    {
        // All the data that can be saved
        public float natureVolume;
        public float waterVolume;
        public float uiVolume;
        public int graphicsQuality;
    }

    private void Awake()
    {
        // Init
        path = Application.persistentDataPath + "/savefile.json";
    }

    private void Start()
    {
        // Load
        LoadSettings();
    }

    public void SaveSettings()
    {
        // Fill all the data that will be saved
        DataSave data = new DataSave();

        foreach (Toggle toggle in graphicsToggles)
        {
            if (toggle.isOn)
            {
                data.graphicsQuality = graphicsToggles.IndexOf(toggle);
            }
        }
        data.natureVolume = (float)settings.natureVolume / 100;
        data.waterVolume = (float)settings.waterVolume / 100;
        data.uiVolume = (float)settings.uiVolume / 100;

        // Serialize
        string json = JsonUtility.ToJson(data);

        // Write to file
        File.WriteAllText(path, json);
    }

    private void LoadSettings()
    {
        // Check path
        if (File.Exists(path))
        {
            // Read file
            string json = File.ReadAllText(path);

            try
            {
                // Read data from the file and setup all the settings
                DataSave data = JsonUtility.FromJson<DataSave>(json);

                settings.natureVolume = Mathf.RoundToInt(data.natureVolume * 100);
                settings.waterVolume = Mathf.RoundToInt(data.waterVolume * 100);
                settings.uiVolume = Mathf.RoundToInt(data.uiVolume * 100);


                //Audio volume controls
                settings.SetNatureVolume(data.natureVolume);
                settings.SetWaterVolume(data.waterVolume);
                settings.SetUIVolume(data.uiVolume);

                // Graphics quality settings
                QualitySettings.SetQualityLevel(data.graphicsQuality, true);
                graphicsQualityDropdown.value = data.graphicsQuality;
                graphicsToggles[data.graphicsQuality].isOn = true;
            }
            catch
            {
            }         
        }
    }
}

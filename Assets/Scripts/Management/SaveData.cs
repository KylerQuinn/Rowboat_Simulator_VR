using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SaveData : MonoBehaviour
{
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] Slider natureVolumeSlider;
    [SerializeField] Slider waterVolumeSlider;
    [SerializeField] Slider uiVolumeSlider;
    [SerializeField] TMP_Dropdown graphicsQualityDropdown;

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

        LoadSettings();
    }

    public void SaveSettings()
    {
        // Fill all the data that will be saved
        DataSave data = new DataSave();

        data.graphicsQuality = graphicsQualityDropdown.value;
        data.natureVolume = natureVolumeSlider.value;
        data.waterVolume = waterVolumeSlider.value;
        data.uiVolume = uiVolumeSlider.value;

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
                
                // Audio volume sliders
                natureVolumeSlider.value = data.natureVolume;
                waterVolumeSlider.value = data.waterVolume;
                uiVolumeSlider.value = data.uiVolume;

                // Volume settings in mixer
                audioMixer.SetFloat("NatureVolume", Mathf.Log10(data.natureVolume) * 20);
                audioMixer.SetFloat("WaterVolume", Mathf.Log10(data.waterVolume) * 20);
                audioMixer.SetFloat("UIVolume", Mathf.Log10(data.uiVolume) * 20);

                // Graphics quality settings
                QualitySettings.SetQualityLevel(data.graphicsQuality, true);
                graphicsQualityDropdown.value = data.graphicsQuality;
            }
            catch
            {
            }         
        }
    }
}

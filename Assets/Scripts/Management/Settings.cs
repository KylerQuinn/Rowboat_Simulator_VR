using TMPro;
using UnityEngine;
using UnityEngine.Audio;

public class Settings : MonoBehaviour
{
    [HideInInspector] public int waterVolume = 100;
    [HideInInspector] public int natureVolume = 100;
    [HideInInspector] public int uiVolume = 100;

    [SerializeField] TMP_Text waterVolumeValue;
    [SerializeField] TMP_Text natureVolumeValue;
    [SerializeField] TMP_Text UIVolumeValue;

    // Class to set the values of mixer and graphics when user change settings

    [SerializeField] AudioMixer audioMixer;

    public void OnWaterSliderValueChanged(float value)
    {
        waterVolume = (int)value * 100;
        SetWaterVolume(value);
    }

    public void OnNatureSliderValueChanged(float value)
    {
        natureVolume = (int)value * 100;
        SetNatureVolume(value);
    }

    public void OnUISliderValueChanged(float value)
    {
        uiVolume = (int)value * 100;
        SetUIVolume(value);
    }

    public void ChangeWaterVolume(int value)
    {
        waterVolume += value;
        waterVolume = Mathf.Clamp(waterVolume, 0, 100);
        SetWaterVolume((float)waterVolume / 100);
    }

    public void ChangeNatureVolume(int value)
    {
        natureVolume += value;
        natureVolume = Mathf.Clamp(natureVolume, 0, 100);
        SetNatureVolume((float)natureVolume / 100);
    }

    public void ChangeUIVolume(int value)
    {
        uiVolume += value;
        uiVolume = Mathf.Clamp(uiVolume, 0, 100);
        SetUIVolume((float)uiVolume / 100);
    }

    public void SetWaterVolume(float value)
    {
        if (value == 0) value = 0.0001f;
        audioMixer.SetFloat("WaterVolume", Mathf.Log10(value) * 20);
        waterVolumeValue.text = waterVolume.ToString();
    }

    public void SetNatureVolume(float value)
    {
        if (value == 0) value = 0.0001f;
        audioMixer.SetFloat("NatureVolume", Mathf.Log10(value) * 20);
        natureVolumeValue.text = natureVolume.ToString();
    }

    public void SetUIVolume(float value)
    {
        if (value == 0) value = 0.0001f;
        audioMixer.SetFloat("UIVolume", Mathf.Log10(value) * 20);
        UIVolumeValue.text = uiVolume.ToString();
    }

    public void OnGraphicsChanged(int value)
    {
        int count = QualitySettings.count;
        QualitySettings.SetQualityLevel(Mathf.Min(value, count) , true);
    }
}

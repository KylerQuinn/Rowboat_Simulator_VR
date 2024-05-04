using UnityEngine;
using UnityEngine.Audio;

public class Settings : MonoBehaviour
{
    // Class to set the values of mixer and graphics when user change settings
    
    [SerializeField] AudioMixer audioMixer;

    public void OnWaterSliderValueChanged(float value)
    {
        audioMixer.SetFloat("WaterVolume", Mathf.Log10(value) * 20);
    }

    public void OnNatureSliderValueChanged(float value)
    {
        audioMixer.SetFloat("NatureVolume", Mathf.Log10(value) * 20);
    }

    public void OnUISliderValueChanged(float value)
    {
        audioMixer.SetFloat("UIVolume", Mathf.Log10(value) * 20);
    }

    public void OnGraphicsChanged(int value)
    {
        int count = QualitySettings.count;
        QualitySettings.SetQualityLevel(Mathf.Min(value, count) , true);
    }
}

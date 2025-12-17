using UnityEngine;
using UnityEngine.UI;
using EasyPeasyFirstPersonController;

public class OptionSlider : MonoBehaviour
{
    public Slider masterVolumeSlider;
    public Slider sensitivitySlider;

    public FirstPersonController player; // might be better to initialize than repeatedly calling instance


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (player == null)
        {
            player = FirstPersonController.Instance;
        }

        // Load up default settings for volume and sensitivity from the data
        float db = PlayerPrefs.GetFloat("NewMasterVolume", 1f);
        float sensitivity = PlayerPrefs.GetFloat("NewMoouseSensitivity", 50f);

        // Reflect back the default values to the volume and sensitivity
        AudioListener.volume = db;
        player.mouseSensitivity = sensitivity;

        // Adjust the sliders to match the default
        masterVolumeSlider.value = db;
        sensitivitySlider.value = sensitivity;

    }

    public void SetVolume(float db)
    {
        AudioListener.volume = db;
        PlayerPrefs.SetFloat("NewMasterVolume", db); // Save the data
    }

    public void SetSensitivity(float sens)
    {
        player.mouseSensitivity = sens;
        PlayerPrefs.SetFloat("NewMoouseSensitivity", sens); // Save the data
    }
}

using UnityEngine;
using UnityEngine.UI;

public class Audio_ModifieVolume : MonoBehaviour
{
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;

    private void Start()
    {
        masterSlider.value = Audio_SaveVolume.Get().masterValue;
        musicSlider.value = Audio_SaveVolume.Get().musicValue;
        sfxSlider.value = Audio_SaveVolume.Get().sfxValue;
    }

    public void SetMasterVolume(float value)
    {
        Audio_SaveVolume.Get().masterValue = value;

        AkSoundEngine.SetRTPCValue("Master_volume", value);
    }

    public void SetMusicVolume(float value)
    {
        Audio_SaveVolume.Get().musicValue = value;

        AkSoundEngine.SetRTPCValue("Music_volume", value);
    }

    public void SetSFXVolume(float value)
    {
        Audio_SaveVolume.Get().sfxValue = value;

        AkSoundEngine.SetRTPCValue("Sfx_volume", value);
    }
}
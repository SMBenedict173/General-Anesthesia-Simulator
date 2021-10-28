using UnityEngine.UI;
using UnityEngine;
using UnityEditor;
using System;
using System.Collections.Generic;
using System.IO;

public class Settings : MonoBehaviour
{
	[Header("Default Settings")]
	public float Volume;
	public float Brightness;
	public int TextSize;

	[Header("Menu Inputs")]
	public Slider VolumeSlider;
	public Slider BrightnessSlider;
	public Slider TextSizeSlider;

	private List<AudioSource> sources = new List<AudioSource>();
	private CurrentSettings settings;
	private readonly string fileName = "usersettings.json";
	private List<Text> textSources = new List<Text>();
	
	void Start()
    {

        Text[] text = GameObject.FindObjectsOfType<Text>();
        for (int i = 0; i < text.Length; i++)
        {
            textSources.Add(text[i]);
        }

        VolumeSlider.onValueChanged.AddListener(delegate { ChangeVolume(VolumeSlider.value); });
        BrightnessSlider.onValueChanged.AddListener(delegate { ChangeBrightness(BrightnessSlider.value); });
        TextSizeSlider.onValueChanged.AddListener(delegate { ChangeTextSize(TextSizeSlider.value); });

        if (!File.Exists(this.fileName))
        {
			settings = new CurrentSettings()
			{
				Volume = this.Volume,
				Brightness = this.Brightness
			};
			string stringifiedSettings = JsonUtility.ToJson(this.settings);
			using (StreamWriter sw = File.CreateText(this.fileName))
            {
				sw.WriteLine(stringifiedSettings);
            }
        }
        else
        {
			using (StreamReader sr = File.OpenText(this.fileName))
            {
				this.settings = JsonUtility.FromJson<CurrentSettings>(sr.ReadToEnd());
            }
        }

		AudioSource[] sourcesArray = GameObject.FindObjectsOfType<AudioSource>();
		for (int i = 0; i < sourcesArray.Length; i++)
		{
			sources.Add(sourcesArray[i]);
		}

		ChangeVolume(this.settings.Volume);

		LightingSettings lightingSettings = new LightingSettings();

		// Configure the LightingSettings object
		lightingSettings.albedoBoost = Brightness;

		// Assign the LightingSettings object to the active Scene
		Lightmapping.lightingSettings = lightingSettings;

		//UnityEngine.Rendering.VolumeProfile volumeProfile = GetComponent<UnityEngine.Rendering.Volume>()?.profile;
		//if (!volumeProfile) throw new System.NullReferenceException(nameof(UnityEngine.Rendering.VolumeProfile));

		//if (!volumeProfile.TryGet(out vignette)) throw new System.NullReferenceException(nameof(vignette));

		ChangeBrightness(this.settings.Brightness);
		ChangeTextSize(this.settings.TextSize);
	}

	public void ChangeVolume(float sliderValue)
    {
		this.Volume = sliderValue;

		sources.ForEach(source =>
		{
			source.volume = source.volume * this.Volume; //multiplication in case we treat this like a percentage
		});
	}

	public void ChangeBrightness(float sliderValue)
    {
		this.Brightness = sliderValue;

		LightingSettings lightingSettings = new LightingSettings();

		// Configure the LightingSettings object
		lightingSettings.albedoBoost = this.Brightness;

		// Assign the LightingSettings object to the active Scene
		Lightmapping.lightingSettings = lightingSettings;
	}

	public void ChangeTextSize(float sliderValue)
	{
		this.TextSize = (int)sliderValue;
		this.textSources.ForEach(text =>
		{
			text.fontSize = this.TextSize;
		});
	}

	public void SaveChanges()
    {
		this.settings.Brightness = this.Brightness;
		this.settings.Volume = this.Volume;
		this.settings.TextSize = this.TextSize;

		string stringifiedSettings = JsonUtility.ToJson(this.settings);
		using (StreamWriter sw = File.CreateText(this.fileName))
		{
			sw.WriteLine(stringifiedSettings);
		}
	}
	
	[Serializable]
	private class CurrentSettings
    {		
		public float Volume { get; set; }
		
		public float Brightness { get; set; }

		public int TextSize { get; set; }
    }
}

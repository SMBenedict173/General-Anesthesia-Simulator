using UnityEngine.UIElements;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

public class Settings : MonoBehaviour
{
	[Header("Default Settings")]
	public float Volume;
	public float Brightness;
	public int TextSize;

	[Header("Menu Inputs")]
	public Slider VolumeSlider;
	public Slider BrightnessSlider;
	public SliderInt TextSizeSlider;

	private List<AudioSource> sources = new List<AudioSource>();
	private CurrentSettings settings;
	private readonly string fileName = "usersettings.json";
	private UnityEngine.Rendering.Universal.Vignette vignette;
	private List<TextElement> textSources = new List<TextElement>();
	
	void Start()
    {
		Text[] text = GameObject.FindObjectsOfType<TextElement>();
		for (int i = 0; i < text.Length; i++)
        {
			textSources.Add(text[i]);
        }

		VolumeSlider.onValueChanged.addListener(delegate { ChangeVolume(VolumeSlider.value); });
		BrightnessSlider.onValueChanged.addListener(delegate { ChangeBrightness(BrightnessSlider.value); });
		TextSizeSlider.onValueChanged.addListener(delegate { ChangeTextSize(TextSizeSlider.value); });

		if (!File.Exists(this.fileName))
        {
			settings = new CurrentSettings()
			{
				Volume = this.Volume,
				Brightness = this.Brightness
			};
			string stringifiedSettings = JsonSerializer.Serialize(this.settings, typeof(CurrentSettings));
			using (StreamWriter sw = File.CreateText(this.fileName))
            {
				sw.WriteLine(stringifiedSettings);
            }
        }
        else
        {
			using (StreamReader sr = File.OpenText(this.fileName))
            {
				this.settings = JsonSerializer.Deserialize(sr.ReadToEnd(), typeof(CurrentSettings));
            }
        }

		AudioSource[] sourcesArray = GameObject.FindObjectsOfType<AudioSource>();
		for (int i = 0; i < sourcesArray.Length; i++)
		{
			sources.Add(sourcesArray[i]);
		}

		ChangeVolume(this.settings.Volume);

		UnityEngine.Rendering.VolumeProfile volumeProfile = GetComponent<UnityEngine.Rendering.Volume>()?.profile;
		if (!volumeProfile) throw new System.NullReferenceException(nameof(UnityEngine.Rendering.VolumeProfile));

		if (!volumeProfile.TryGet(out vignette)) throw new System.NullReferenceException(nameof(vignette));

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

		vignette.intensity.Override(this.Brightness);
	}

	public void ChangeTextSize(int sliderValue)
	{
		this.TextSize = sliderValue;
		this.textSources.ForEach(text =>
		{
			text.style.fontSize = this.TextSize;
		});
	}

	public void SaveChanges()
    {
		this.settings.Brightness = this.Brightness;
		this.settings.Volume = this.Volume;
		this.settings.TextSize = this.TextSize;

		string stringifiedSettings = JsonSerializer.Serialize(this.settings, typeof(CurrentSettings));
		using (StreamWriter sw = File.CreateText(this.fileName))
		{
			sw.WriteLine(stringifiedSettings);
		}
	}

	public void ChangeTextSize(int sliderValue)
    {
		this.TextSize = sliderValue;
		this.textSources.ForEach(text =>
		{
			text.style.fontSize = this.TextSize;
		});
    }

	private class CurrentSettings
    {
		[JsonProperty("volume")]
		public float Volume { get; set; }

		[JsonProperty("brightness")]
		public float Brightness { get; set; }

		[JsonProperty("textsize")]
		public int TextSize { get; set; }
    }
}

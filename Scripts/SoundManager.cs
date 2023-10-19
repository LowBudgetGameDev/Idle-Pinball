using Godot;
using System;
using System.Collections.Generic;

public partial class SoundManager : Node2D
{
	public static SoundManager Instance { get; private set; }

	public enum Sound{
		BallBounce1,
		BallBounce2,
		BallBounce3,
		BallDispense1,
		BallDispense2,
		BallDispense3,
		PegBounce1,
		PegBounce2,
		PegBounce3,
		ShopBuy1,
		ShopBuy2,
		ShopBuy3,
		ShopOpen,
		ShopClose,
	}

	public enum SoundType
    {
        BallBounce,
        BallDispense,
        PegBounce,
        ShopBuy
    }

    private Dictionary<Sound, AudioStream> soundAudioStreamDictionary;
    private Dictionary<SoundType, List<Sound>> soundTypeListDictionary;

	public SoundManager(){
		Instance = this;

        soundAudioStreamDictionary = new Dictionary<Sound, AudioStream>();
        soundTypeListDictionary = new Dictionary<SoundType, List<Sound>>();

        foreach (Sound sound in Enum.GetValues(typeof(Sound)))
        {
            soundAudioStreamDictionary[sound] = (AudioStream) ResourceLoader.Load("res://Sounds/" + sound.ToString() + ".mp3");
        }

        foreach (SoundType soundType in Enum.GetValues(typeof(SoundType)))
        {
            soundTypeListDictionary[soundType] = new List<Sound>();

            foreach (Sound sound in Enum.GetValues(typeof(Sound)))
            {
                if (sound.ToString().Contains(soundType.ToString()))
                {
                    soundTypeListDictionary[soundType].Add(sound);
                }
            }
        }
	}

	public void PlaySound(AudioStreamPlayer2D audioStreamPlayer2D, Sound sound)
    {
		audioStreamPlayer2D.Stream = soundAudioStreamDictionary[sound];
        audioStreamPlayer2D.Play();
    }

    public void PlayRandomSound(AudioStreamPlayer2D audioStreamPlayer2D, SoundType soundType)
    {
        List<Sound> soundList = soundTypeListDictionary[soundType];
        int soundIndex = new Random().Next(0, soundList.Count);

        PlaySound(audioStreamPlayer2D, soundList[soundIndex]);
    }
}

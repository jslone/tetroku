using UnityEngine;
using System.Collections;

public enum SOUND_EFFECTS {
	POSITIVE,
	NEGATIVE,
	NEUTRAL,
	ERROR
}

public class SoundManager : MonoBehaviour {
	private static SoundManager _instance = null;
	public AudioSource music;
	public AudioSource[] soundEffects;
	
	// Use this for initialization
	void Start () {
		if(_instance == null) {
			float svol = PlayerPrefs.GetFloat("soundVolume",1.0f);
			float mvol = PlayerPrefs.GetFloat("musicVolume",1.0f);
			
			AudioListener.volume = svol;
			music.volume = mvol;
			
			DontDestroyOnLoad(gameObject);
			_instance = this;
		} else {
			Destroy(gameObject);
		}
	}
	
	public static void ChangeMusicVolume(float v) {
		_instance.music.volume = v;
	}
	
	public static void ChangeSoundVolume(float v) {
		foreach(AudioSource audio in _instance.soundEffects) {
			audio.volume = v;
		}
	}
	
	public static void Play(SOUND_EFFECTS effect) {
		_instance.soundEffects[(int)(effect)].Play();
	}
}

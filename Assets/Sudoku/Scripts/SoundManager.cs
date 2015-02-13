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
	void Awake () {
		if(_instance == null) {
			DontDestroyOnLoad(gameObject);
			_instance = this;
			
			float svol = PlayerPrefs.GetFloat("soundVolume",1.0f);
			float mvol = PlayerPrefs.GetFloat("musicVolume",1.0f);
			
			ChangeSoundVolume(svol);
			ChangeMusicVolume(mvol);
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
	
	public void PlayPositive() {
		Play (SOUND_EFFECTS.POSITIVE);
	}
	
	public void PlayNegative() {
		Play (SOUND_EFFECTS.NEGATIVE);
	}
	
	public void PlayNeutral() {
		Play (SOUND_EFFECTS.NEUTRAL);
	}
	
	public void PlayError() {
		Play (SOUND_EFFECTS.ERROR);
	}
}

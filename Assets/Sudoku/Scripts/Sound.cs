using UnityEngine;
using System.Collections;

public class Sound : MonoBehaviour {
	private static Sound _instance = null;
	public AudioSource music;
	
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
	
	public void ChangeMusicVolume(float v) {
		music.volume = v;
	}
	
	public void ChangeSoundVolume(float v) {
		AudioListener.volume = v;
	}
}

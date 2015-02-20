using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public enum SOUND_TYPE {
	MUSIC,
	SOUND
};

public class UISound : MonoBehaviour {
	public SOUND_TYPE type;
	
	// Use this for initialization
	void Start () {
		float volume = 0.0f;
		switch(type) {
			case SOUND_TYPE.MUSIC:
				volume = PlayerPrefs.GetFloat("musicVolume",1.0f);
				break;
			case SOUND_TYPE.SOUND:
				volume = PlayerPrefs.GetFloat("soundVolume",1.0f);
				break;
		}
		GetComponent<Slider>().value = volume;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void OnSlide(float v) {
		switch(type) {
			case SOUND_TYPE.MUSIC:
				PlayerPrefs.SetFloat("musicVolume",v);
				SoundManager.ChangeMusicVolume(v);
				break;
			case SOUND_TYPE.SOUND:
				PlayerPrefs.SetFloat("soundVolume",v);
				SoundManager.ChangeSoundVolume(v);
				break;
		}
	}
}

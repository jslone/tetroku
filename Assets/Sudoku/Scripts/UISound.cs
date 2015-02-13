using UnityEngine;
using System.Collections;

public enum SOUND_TYPE {
	MUSIC,
	SOUND
};

public class UISound : MonoBehaviour {
	public SOUND_TYPE type;
	
	// Use this for initialization
	void Start () {
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

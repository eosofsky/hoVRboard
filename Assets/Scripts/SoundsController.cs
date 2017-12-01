using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundsController : MonoBehaviour {

	private AudioSource bgm;
	private AudioSource board;
	private AudioSource sfx;

	private Dictionary<string, AudioClip> sounds;

	public GameObject player;
	public float startHeight;
	public float doubleHeight = 50;
	private float doubleDist;
	private float pitch;

	// Use this for initialization
	void Awake () {	
		sounds = new Dictionary<string, AudioClip>();

		bgm = GetComponents<AudioSource> () [0];
		bgm.loop = true;
		bgm.volume = 0.33f;

		board = GetComponents<AudioSource> () [1];
		board.loop = true;
		board.volume = 0.075f;

		sfx = GetComponents<AudioSource> () [2];


		var snowballFight = Resources.Load ("Dee_Yan-Key_-_18_-_snowball_fight", typeof(AudioClip)) as AudioClip;

		var hoverHum = Resources.Load ("noisyhum", typeof(AudioClip)) as AudioClip;


		sounds.Add ("snowball-fight", snowballFight);
		sounds.Add ("hover-hum", hoverHum);


		bgm.clip = snowballFight;
		bgm.Play ();

		board.clip = hoverHum;
		board.Play ();
	}

	void Start() {
		player = GameObject.FindGameObjectWithTag ("Player");
		startHeight = player.transform.position.y;
	}

	
	// Update is called once per frame
	void Update () {
		ChangePitch ();
	}

	void ChangePitch () {
		float temp = (player.transform.position.y - startHeight) / (doubleDist);
		pitch = Mathf.Min (temp + 1, 3);
		board.pitch = pitch;
	}
}

using UnityEngine;
using UnityEngine.Video;

public class StaticMovie : MonoBehaviour {

	[SerializeField] VideoClip staticClip;
	static VideoPlayer vp;

	void Start () {
		GameObject camera = GameObject.Find ("Camera (eye)");
		vp = camera.AddComponent<VideoPlayer>();
		vp.playOnAwake = false;
		vp.renderMode = VideoRenderMode.CameraNearPlane;
		vp.clip = staticClip;
		vp.isLooping = true;
		vp.enabled = false;
	}

	public static void PlayStatic () {
		vp.Play();
		vp.enabled = true;
	}

	public static void StopStatic () {
		vp.enabled = false;
		vp.Stop();
	}

}

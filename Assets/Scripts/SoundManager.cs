using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource source;
    [SerializeField] private AudioClip emptyShot;
    [SerializeField] private AudioClip reload;
    [SerializeField] private AudioClip shoot;

    private void PlayAudio(AudioClip clip) {
        source.PlayOneShot(clip);
    }

	private void Start() {
		GameManager.Instance.OnShootBlank += Instance_OnShootBlank;
		GameManager.Instance.OnReload += Instance_OnReload;
		GameManager.Instance.OnShootReal += Instance_OnShootReal;
	}

	private void Instance_OnShootReal(object sender, System.EventArgs e) {
		PlayAudio(shoot);
	}

	private void Instance_OnReload(object sender, System.EventArgs e) {
		PlayAudio(reload);
	}

	private void Instance_OnShootBlank(object sender, System.EventArgs e) {
		PlayAudio(emptyShot);
	}
}

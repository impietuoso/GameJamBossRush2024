using UnityEngine;

public class PlaySound : MonoBehaviour {
    public static PlaySound instance;
    AudioSource source;
    void Awake() {
        instance = this;
        source = GetComponent<AudioSource>();
    }

    public void Play(AudioClip c) {
        source.PlayOneShot(c);
    }
}
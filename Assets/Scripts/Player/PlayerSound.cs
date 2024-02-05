using UnityEngine;

public class PlayerSound : MonoBehaviour {
    public AudioClip hurtSound;
    public AudioClip attackSound;

    public void SoundHurt() {
        if (hurtSound != null) PlaySound.instance.Play(hurtSound);
    }

    public void SoundAttack() {
        if (attackSound != null) PlaySound.instance.Play(attackSound);
    }
}
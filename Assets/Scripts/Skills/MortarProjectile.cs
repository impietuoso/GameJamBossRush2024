using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MortarProjectile : MonoBehaviour {
    public float disableTime;
    float cTime = 0;
    public bool playOnAwake;
    public AudioClip clip;

    private void OnEnable() {
        cTime = 0;
        if (playOnAwake) PlayOnAwake();
    }

    private void OnDisable() {
        cTime = 0;
    }

    private void Update() {
        cTime += Time.deltaTime;
        if (cTime >= disableTime) {
            cTime = 0;
            DisableObj();
        }
    }

    void DisableObj() {
        gameObject.SetActive(false);
    }

    public void PlayOnAwake() {
        if (clip) PlaySound.instance.Play(clip);
    }
}
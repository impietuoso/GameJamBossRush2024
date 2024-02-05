using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMovement : MonoBehaviour {
    public float speed;
    public float direction;
    public float lifeTime;
    float cTime = 0;
    public string tagName;
    public bool playOnAwake;
    public AudioClip clip;

    Rigidbody2D rb;

    private void OnEnable() {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(speed * direction, 0);
        cTime = 0;
        if (playOnAwake) PlayOnAwake();
    }

    private void OnDisable() {
        cTime = 0;
    }

    private void Update() {
        cTime += Time.deltaTime;
        if(cTime >= lifeTime) {
            cTime = 0;
            DisableProjectile();
        }
    }

    public void DisableProjectile() {
        rb.velocity = Vector2.zero;
        gameObject.SetActive(false);
    }

    public void PlayOnAwake() {
        if(clip) PlaySound.instance.Play(clip);
    }

    private void OnTriggerEnter2D(Collider2D col) {
        if(col.tag == tagName) {
            col.transform.parent.GetComponent<Boss>().TakeDamage(5);
            DisableProjectile();
        }
    }
}
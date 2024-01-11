using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMovement : MonoBehaviour
{
    public float speed;
    public float direction;
    public float lifeTime;
    public string tagName;

    Rigidbody2D rb;

    private void OnEnable() {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(speed * direction, 0);
        Invoke("DisableProjectile", lifeTime);
    }

    public void DisableProjectile() {
        rb.velocity = Vector2.zero;
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D col) {
        if(col.tag == tagName) {
            //Call Action
            DisableProjectile();
        }
    }
}

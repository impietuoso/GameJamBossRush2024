using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttacks : MonoBehaviour
{
    Animator anim;
    public GameObject bullet;
    public Transform pos;
    public Transform bulletPool;

    [SerializeField]
    bool canAttack;

    private void Awake() {
        anim = GetComponent<Animator>();
    }

    public void CanAttack() {
        canAttack = true;
    }

    public void StopAttack() {
        canAttack = false;
    }

    public void NormalAttack() {
        if(canAttack) {
            anim.SetTrigger("Attack");
            List<GameObject> reusableBullets = new List<GameObject>();
            for (int i = 0; i < bulletPool.childCount; i++) {
                if (bulletPool.GetChild(i).gameObject.activeInHierarchy == false) reusableBullets.Add(bulletPool.GetChild(i).gameObject);
            }
            if (reusableBullets.Count > 0) {
                reusableBullets[0].transform.position = pos.position;
                reusableBullets[0].SetActive(true);
                reusableBullets.Remove(reusableBullets[0]);
            } else
                Instantiate(bullet, pos.position, Quaternion.identity, bulletPool);
        }        
    }
}
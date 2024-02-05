using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MortarProjectile : MonoBehaviour {
    public float disableTime;
    private void OnEnable() {
        Invoke("DisableObj", disableTime);
    }

    void DisableObj() {
        gameObject.SetActive(false);
    }
}
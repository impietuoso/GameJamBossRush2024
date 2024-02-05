using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningChainProjectile : MonoBehaviour {
    public float disableTime;
    private void OnEnable() {
        Invoke("DisableObj", disableTime);
    }

    void DisableObj() {
        gameObject.SetActive(false);
    }
}
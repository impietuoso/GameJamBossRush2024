using UnityEngine;
public class ObjectPools : MonoBehaviour {
    public static ObjectPools instance;
    public Transform normalOP;
    public Transform mortarOP;
    public Transform lightningOP;
    private void Awake() { instance = this; }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour {
    [SerializeField] TileMovement tm;
    [SerializeField] PlayerAttacks pa;
    [SerializeField] bool canMove;

    private void Awake() {
        tm = GetComponent<TileMovement>();
        pa = GetComponent<PlayerAttacks>();
    }

    public void CanMove() { canMove = true; }

    public void StopMove() { canMove = false; }

    void Update() {
        if (canMove && Time.timeScale == 1) {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) tm.MoveUp();
            if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) tm.MoveDown();
            if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) tm.MoveRight();
            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) tm.MoveLeft();
            if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.J)) pa.NormalAttack();
            //if (Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.K)) pa.SpecialSkill1();
            //if (Input.GetKeyDown(KeyCode.C) || Input.GetKeyDown(KeyCode.L)) pa.SpecialSkill2();
        }
    }
}
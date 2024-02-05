using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Boss : MonoBehaviour, ISkillUser {
    public Transform pos;
    public Skill[] skills;
    public int life;
    int maxLife;
    [Range(0,10)]
    public float timer;
    [Range(0, 10)]
    public float movetimer;
    public float moveRate;
    private bool canAttack = true;
    public bool canMove = true;
    Animator anim;
    public SpriteRenderer lifeBar;


    [SerializeField] TileMovement tm;

    Vector2 ISkillUser.pos => pos.position;

    public team team => team.Enemy;

    private void Awake() {
        tm = GetComponent<TileMovement>();
        anim = GetComponentInChildren<Animator>();
        maxLife = life;
        UpdateLifeBar();
    }

    public void TakeDamage(int damage) {
        anim.SetTrigger("Damage");
        life -= damage;
        UpdateLifeBar();
        if (life <= 0) OnDeath();
    }

    public void Update() {
        if (canAttack) {
            if (timer >= 10) {
                timer = 0;
                int rSkill = Random.Range(0, skills.Length);
                skills[rSkill].UseSkill(this, true);
                StopAttack();
            } else timer += Time.deltaTime;
        }

        if (movetimer >= moveRate) {
            movetimer = 0;
            Move();
        } else movetimer += Time.deltaTime;
    }

    void Move() {
        if (canMove) {
            int dir = Random.Range(0, 4);
            switch (dir) {
                case 0:
                    tm.MoveUp();
                    break;
                case 1:
                    tm.MoveDown();
                    break;
                case 2:
                    tm.MoveRight();
                    break;
                case 3:
                    tm.MoveLeft();
                    break;
                default:
                    break;
            }
        }
    }

    public void CanAttack() {
        canAttack = true;
    }

    public void StopAttack() {
        canAttack = false;
    }

    void UpdateLifeBar() {
        float value = (float)life / maxLife;
        lifeBar.size = new Vector2(0.66f * value, 0.08f);
    }

    public void OnDeath() {
        EndGame.instance.GameWin();
    }

}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttacks : MonoBehaviour, ISkillUser {
    Animator anim;
    public GameObject bullet;
    public Transform pos;
    public Skill skill;
    public Skill skill2;
    public int life;
    int maxLife;
    public bool chargedAttack;
    public SpriteRenderer lifeBar;

    [SerializeField]
    bool canAttack;

    Vector2 ISkillUser.pos => pos.position;

    public team team => team.Player;

    private void Awake() {
        anim = GetComponentInChildren<Animator>();
        maxLife = life;
        UpdateLifeBar();
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
        }
    }

    public void Shot() {
        Transform objectPool = ObjectPools.instance.normalOP;
        List<GameObject> reusableBullets = new List<GameObject>();
        for (int i = 0; i < ObjectPools.instance.normalOP.childCount; i++) {
            if (objectPool.GetChild(i).gameObject.activeInHierarchy == false) reusableBullets.Add(objectPool.GetChild(i).gameObject);
        }
        if (reusableBullets.Count > 0) {
            reusableBullets[0].transform.position = pos.position;
            reusableBullets[0].SetActive(true);
            reusableBullets.Remove(reusableBullets[0]);
        } else
            Instantiate(bullet, pos.position, Quaternion.identity, objectPool);
    }

    public void SpecialSkill1() {
        skill?.UseSkill(this, chargedAttack);
    }

    public void SpecialSkill2() {
        skill2?.UseSkill(this, chargedAttack);
    }

    void UpdateLifeBar() {
        float value = (float)life / maxLife;
        lifeBar.size = new Vector2(0.66f * value, 0.08f);
    }

    public void TakeDamage(int damage) {
        anim.SetTrigger("Damage");
        life -= damage;
        UpdateLifeBar();
        if (life <= 0) OnDeath();
    }

    public void OnDeath() {
        EndGame.instance.Lose();
    }
}
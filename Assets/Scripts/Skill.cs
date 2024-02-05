using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skill : ScriptableObject {
    public string skillName;
    public int bossDamage;
    public int playerDamage;
    public float attackDelay;
    public float fireRate;
    public GameObject skillObj;

    public abstract void UseSkill(ISkillUser caster, bool charged);

    public abstract void SpawnSkillObj(Vector2Int tilePos);

}

public interface ISkillUser {
    Vector2 pos { get; }
    team team { get; }
    void TakeDamage(int damage);
    void CanAttack();
    void OnDeath();
}
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Lightning Chain")]
public class LightningChain : Skill {
    List<Vector2Int[]> enemyPatterns = new List<Vector2Int[]>(){
        new Vector2Int[] { new(0, 0), new(1, 0), new(2, 0) },
        new Vector2Int[] { new(0, 1), new(1, 1), new(2, 1) },
        new Vector2Int[] { new(0, 2), new(1, 2), new(2, 2) }
    };

    [SerializeField]
    List<Vector2Int[]> playerPatterns = new List<Vector2Int[]>(){
        new Vector2Int[] { new(3, 0), new(4, 0), new(5, 0) },
        new Vector2Int[] { new(3, 1), new(4, 1), new(5, 1) },
        new Vector2Int[] { new(3, 2), new(4, 2), new(5, 2) }
    };

    public override void UseSkill(ISkillUser caster, bool charged) {
        if (caster.team == team.Player)
            Field.instance.StartCoroutine(FirePlayerPatters(charged));
        else if (caster.team == team.Enemy)
            Field.instance.StartCoroutine(FireEnemyPatters(caster));
    }

    public override void SpawnSkillObj(Vector2Int tilePos) {
        Transform objectPool = ObjectPools.instance.lightningOP;
        List<GameObject> reusableBullets = new List<GameObject>();
        for (int i = 0; i < objectPool.childCount; i++) {
            if (objectPool.GetChild(i).gameObject.activeInHierarchy == false ) reusableBullets.Add(objectPool.GetChild(i).gameObject);
        }
        if (reusableBullets.Count > 0) {
            reusableBullets[0].transform.position = Field.instance.GetTile(tilePos).position;
            reusableBullets[0].SetActive(true);
            reusableBullets.Remove(reusableBullets[0]);
        } else
            Instantiate(skillObj, Field.instance.GetTile(tilePos).position, Quaternion.identity, objectPool);
    }

    IEnumerator FirePlayerPatters(bool charged) {
        Field.instance.StartCoroutine(SelectPlayerPattern(charged));
        if (!charged) yield break;
        yield return new WaitForSeconds(fireRate);
        Field.instance.StartCoroutine(SelectPlayerPattern(charged));
        yield return new WaitForSeconds(fireRate);
        Field.instance.StartCoroutine(SelectPlayerPattern(charged));
        yield return new WaitForSeconds(fireRate);
        Field.instance.StartCoroutine(SelectPlayerPattern(charged));
        yield return new WaitForSeconds(fireRate);
        Field.instance.StartCoroutine(SelectPlayerPattern(charged));
        
    }

    IEnumerator FireEnemyPatters(ISkillUser caster) {
        Field.instance.StartCoroutine(SelectEnemyPattern());
        yield return new WaitForSeconds(fireRate);
        Field.instance.StartCoroutine(SelectEnemyPattern());
        yield return new WaitForSeconds(fireRate);
        Field.instance.StartCoroutine(SelectEnemyPattern());
        yield return new WaitForSeconds(fireRate);
        Field.instance.StartCoroutine(SelectEnemyPattern());
        yield return new WaitForSeconds(fireRate);
        Field.instance.StartCoroutine(SelectEnemyPattern());
        caster.CanAttack();
    }

    IEnumerator SelectPlayerPattern(bool charged) {
        List<int> choices = new List<int>() { 0, 1, 2 };
        int pos1 = Random.Range(0, choices.Count);
        List<Vector2Int> patter1 = new List<Vector2Int>();
        patter1.AddRange(playerPatterns[choices[pos1]]);

        if (charged) {
            choices.Remove(pos1);
            int pos2 = Random.Range(0, choices.Count);
            patter1.AddRange(playerPatterns[choices[pos2]]);
        }

        foreach (var tilePos in patter1) {
            Field.instance.TelegraphAttack(tilePos, attackDelay);
        }

        yield return new WaitForSeconds(attackDelay);

        foreach (var tilePos in patter1) {
            ISkillUser newTarget = Field.instance[tilePos].target;
            SpawnSkillObj(tilePos);
            newTarget?.TakeDamage(charged ? bossDamage : playerDamage);
        }

        yield return new WaitForEndOfFrame();
    }

    IEnumerator SelectEnemyPattern() {
        List<int> choices = new List<int>() { 0, 1, 2 };
        int pos1 = Random.Range(0, choices.Count);
        List<Vector2Int> patter1 = new List<Vector2Int>();
        patter1.AddRange(enemyPatterns[choices[pos1]]);

        choices.Remove(pos1);
        int pos2 = Random.Range(0, choices.Count);
        patter1.AddRange(enemyPatterns[choices[pos2]]);

        foreach (var tilePos in patter1) {
            Field.instance.TelegraphAttack(tilePos, attackDelay);
        }

        yield return new WaitForSeconds(attackDelay);

        foreach (var tilePos in patter1) {
            ISkillUser newTarget = Field.instance[tilePos].target;
            SpawnSkillObj(tilePos);
            newTarget?.TakeDamage(bossDamage);
        }

        yield return new WaitForEndOfFrame();
    }
}

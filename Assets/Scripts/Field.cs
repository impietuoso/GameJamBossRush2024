using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum team {Player, Enemy}

public class Field : MonoBehaviour {
    public static Field instance;
    public Sprite playerTile;
    public Sprite enemyTile;
    public Sprite blockedTile;
    public Sprite damageTile;
    [SerializeField] List<Collum> colluns = new List<Collum>();
    public Vector2 playerPos;
    public GameObject attackSelector;

    public Tile this[Vector2Int pos] {
        get {
            try {
                return colluns[pos.x].tiles[pos.y];
            } catch {
                return null;
            }
        }
    }

    private void Awake() { instance = this; }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Alpha1)) BlockCollun(0);
        if (Input.GetKeyDown(KeyCode.Alpha2)) BlockCollun(1);
        if (Input.GetKeyDown(KeyCode.Alpha3)) BlockCollun(2);
        if (Input.GetKeyDown(KeyCode.Alpha4)) ClearField();
        if (Input.GetKeyDown(KeyCode.Alpha5)) DomainExpansion(enemyTile, team.Enemy);
        if (Input.GetKeyDown(KeyCode.Alpha6)) DomainExpansion(playerTile, team.Player);
        if (Input.GetKeyDown(KeyCode.Alpha7)) TelegraphAttack(new Vector2Int(0,0), 1);

        CheckPlayerPosition();
    }

    void CheckPlayerPosition() {
        for (int i = 0; i < colluns.Count; i++) {
            for (int j = 0; j < colluns[i].tiles.Count; j++) {
                if (colluns[i].tiles[j].target != null) playerPos = new Vector2(i, j);
            }
        }
    }

    public void BlockTile(Vector2Int blockThis) {
        Tile willbeBlockedTile = this[blockThis];
        if(willbeBlockedTile != null) willbeBlockedTile.BlockTile(blockedTile, true);
    }

    public void BlockCollun(int collunId) {
        foreach (var item in colluns[collunId].tiles) {
            item.BlockTile(blockedTile, true);
        }
    }

    public void DomainExpansion(Sprite newSprite, team newOwner) {
        foreach (var item in colluns[2].tiles) {
            item.ChangeOwner(newSprite, newOwner);
        }
    }
    
    public void ClearTile(Vector2Int pos) {
        Tile dirtTile = this[pos];
        if (dirtTile != null) {
            if (dirtTile.owner == team.Player)
                dirtTile.BlockTile(playerTile, false);
            else
                dirtTile.BlockTile(enemyTile, false);
        }        
    }

    public void ClearField() {
        foreach (var item in colluns) {
            foreach (var item2 in item.tiles) {
                if (item2.owner == team.Player)
                    item2.BlockTile(playerTile, false);
                else
                    item2.BlockTile(enemyTile, false);
            }
        }
    }

    public void TelegraphAttack(Vector2Int pos, float timeTelegraph) {
        Tile attackTile = this[pos];
        if (attackTile != null) {
            attackTile.tileSprite.sprite = damageTile;
            Instantiate(attackSelector,attackTile.tileSprite.transform.position, Quaternion.identity);
            StartCoroutine(ClearNewTile(pos, timeTelegraph));
        }
    }

    public Transform GetTile(Vector2Int pos) {
        Tile returnedTile = this[pos];
        return returnedTile.tileSprite.GetComponent<Transform>();
    }

    IEnumerator ClearNewTile(Vector2Int pos, float timeTelegraph) {
        yield return new WaitForSeconds(timeTelegraph);
        UpdateTileSprite(Field.instance[pos]);
    }

    void UpdateTileSprite(Tile tile) {
        if (tile.blocked) tile.tileSprite.sprite = blockedTile;
        else {
            if(tile.owner == team.Player) tile.tileSprite.sprite = playerTile;
            else tile.tileSprite.sprite = enemyTile;
        }
    }
}

[Serializable]
public class Tile {
    public team owner;
    public bool blocked;
    public SpriteRenderer tileSprite;
    public ISkillUser target;

    public void ChangeOwner(Sprite newSprite, team newOwner) {
        owner = newOwner;
        tileSprite.sprite = newSprite;
    }

    public void BlockTile(Sprite newSprite, bool state) {
        blocked = state;
        tileSprite.sprite = newSprite;
    }
}

[Serializable]
class Collum {
    public List<Tile> tiles = new List<Tile>();
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMovement : MonoBehaviour {
    [SerializeField] private int c;
    [SerializeField] private int r;
    
    public List<Transform> blockedTiles;
    public List<TileCollum> Colluns = new List<TileCollum>();

    public Sprite playerTile;
    public Sprite blockedTile;

    private void Awake() {
        UpdateBlockedTiles();
        MoveToNewTile(c, r, true, false);
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.B)) RandomTile();
        if (Input.GetKeyDown(KeyCode.C)) DomainExpansion();
        if (Input.GetKeyDown(KeyCode.L)) RemoveBlockedTiles();
    }

    #region Movement
    public void MoveUp() {
        if (r > 0) {
            r--;
            MoveToNewTile(c, r, false, false);
        }
    }

    public void MoveDown() {
        if (r < Colluns[c].Rows.Count - 1) {
            r++;
            MoveToNewTile(c, r, false, true);
        }
    }

    public void MoveLeft() {
        if (c > 0) {
            c--;
            MoveToNewTile(c, r, true, false);
        }
    }

    public void MoveRight() {
        if (c < Colluns.Count - 1) {
            c++;
            MoveToNewTile(c, r, true, true);
        }
    }

    void MoveToNewTile(int newC, int newR, bool wasC, bool wasMore) {
        c = newC;
        r = newR;
        foreach (var item in blockedTiles) {
            if (item.position == Colluns[c].Rows[r].position) {
                if (wasC && wasMore) c--;
                else if(wasC && !wasMore) c++;
                else if (!wasC && wasMore) r--;
                else if (!wasC && !wasMore) r++;
                return;
            }
        }
        transform.position = Colluns[c].Rows[r].position;
    }
    #endregion

    #region TileBlock
    public void AddBlockedTile(Transform newTile) {
        if (!blockedTiles.Contains(newTile)) blockedTiles.Add(newTile);
        UpdateBlockedTiles();
    }

    public void AddBlockedTile(List<Transform> newTiles) {
        foreach (var item in newTiles) {
            if (!blockedTiles.Contains(item)) blockedTiles.Add(item);
        }
        UpdateBlockedTiles();
    }

    public void RemoveBlockedTile(Transform newTile) {
        if (blockedTiles.Contains(newTile)) blockedTiles.Remove(newTile);
        UpdateBlockedTiles();
    }

    public void RemoveBlockedTile(List<Transform> newTiles) {
        foreach (var item in newTiles) {
            if (blockedTiles.Contains(item)) blockedTiles.Remove(item);
        }
        UpdateBlockedTiles();
    }

    public void UpdateBlockedTiles() {
        List<Transform> allTiles = new();
        foreach (var item in Colluns) {
            foreach (var item2 in item.Rows) {
                allTiles.Add(item2);
                item2.GetComponent<SpriteRenderer>().sprite = playerTile;
            }
        }
        foreach (var item in blockedTiles) {
            if (allTiles.Contains(item)) item.GetComponent<SpriteRenderer>().sprite = blockedTile;
        }
    }
    #endregion

    #region BlockAttacks

    public void RandomTile() {
        RemoveBlockedTiles();
        AddBlockedTile(Colluns[UnityEngine.Random.Range(0, 3)].Rows[UnityEngine.Random.Range(0, 3)]);
        UpdateBlockedTiles();
    }

    public void PilarBlock(int c, int r) {
        RemoveBlockedTiles();
        AddBlockedTile(Colluns[c].Rows[r]);
        UpdateBlockedTiles();
    }

    public void DomainExpansion() {
        RemoveBlockedTiles();
        AddBlockedTile(Colluns[2].Rows[0]);
        AddBlockedTile(Colluns[2].Rows[1]);
        AddBlockedTile(Colluns[2].Rows[2]);
        UpdateBlockedTiles();
    }

    public void RemoveBlockedTiles() {
        RemoveBlockedTile(Colluns[0].Rows);
        RemoveBlockedTile(Colluns[1].Rows);
        RemoveBlockedTile(Colluns[2].Rows);
        UpdateBlockedTiles();
    }
    #endregion

}

[Serializable]
public class TileCollum {
    public List<Transform> Rows = new List<Transform>();
}
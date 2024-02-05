using UnityEngine;

public class TileMovement : MonoBehaviour {
    Field field => Field.instance;
    public bool isPlayer;

    [SerializeField] Vector2Int pos;
    
    public Sprite playerTile;
    public Sprite blockedTile;

    private void Start() {
        MoveToNewTile(Vector2Int.zero);
    }

    #region Movement
    public void MoveUp() {
        MoveToNewTile(Vector2Int.up);        
    }

    public void MoveDown() {
        MoveToNewTile(Vector2Int.down);        
    }

    public void MoveLeft() {
        MoveToNewTile(Vector2Int.left);
    }

    public void MoveRight() {
        MoveToNewTile(Vector2Int.right);
    }

    void MoveToNewTile(Vector2Int delta) {
        Tile oldTile = field[pos];
        Tile newTile = field[pos + delta];
        if (isPlayer) {
            if (newTile != null && newTile.blocked == false && newTile.owner == team.Player) {
                pos += delta;
                transform.position = (Vector2)pos;
                oldTile.target = null;
                newTile.target = GetComponent<PlayerAttacks>();
            }
        } else {
            if (newTile != null && newTile.blocked == false && newTile.owner == team.Enemy) {
                pos += delta;
                transform.position = (Vector2)pos;
                oldTile.target = null;
                newTile.target = GetComponent<Boss>();
            }
        }
    }
    #endregion
}
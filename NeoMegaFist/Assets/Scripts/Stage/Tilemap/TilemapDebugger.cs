using UnityEngine;
using UnityEngine.Tilemaps;

[ExecuteInEditMode, RequireComponent(typeof(Tilemap))]
public class TilemapDebugger : MonoBehaviour
{
    [SerializeField] private Tilemap tilemap;
    [SerializeField] private bool isActive;

    private void Start()
    {
        tilemap = GetComponent<Tilemap>();
    }

    private void Update()
    {
        if (!isActive) return;
        if(Input.GetMouseButtonDown(0))
        {
            ClickedPosition();
        }
    }

    private void ClickedPosition()
    {
        // Vector3でマウス位置座標を取得する
        Vector3 position = Input.mousePosition;
        // Z軸修正
        position.z = 10f;
        // マウス位置座標をスクリーン座標からワールド座標に変換する
        Vector3 screenToWorldPointPosition = Camera.main.ScreenToWorldPoint(position);

        Vector3Int clickPosition = tilemap.WorldToCell(screenToWorldPointPosition);

        Debug.Log(gameObject.name + ":" + clickPosition);

        //Tilemap.SetTile(clickPosition, testTile);
    }
}

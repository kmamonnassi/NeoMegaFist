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
        // Vector3�Ń}�E�X�ʒu���W���擾����
        Vector3 position = Input.mousePosition;
        // Z���C��
        position.z = 10f;
        // �}�E�X�ʒu���W���X�N���[�����W���烏�[���h���W�ɕϊ�����
        Vector3 screenToWorldPointPosition = Camera.main.ScreenToWorldPoint(position);

        Vector3Int clickPosition = tilemap.WorldToCell(screenToWorldPointPosition);

        Debug.Log(gameObject.name + ":" + clickPosition);

        //Tilemap.SetTile(clickPosition, testTile);
    }
}

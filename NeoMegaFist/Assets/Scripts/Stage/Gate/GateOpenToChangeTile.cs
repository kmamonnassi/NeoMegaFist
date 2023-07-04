using UnityEngine;
using UnityEngine.Tilemaps;

namespace Stage
{
    public class GateOpenToChangeTile : MonoBehaviour
    {
        [SerializeField] private Tilemap tilemap;
        [SerializeField] private Gate gate;
        [SerializeField] private Tile changeTile;
        [SerializeField] private Vector3Int[] tilePositions;

        private void Start()
        {
            gate.OnOpen += () =>
            {
                for (int i = 0; i < tilePositions.Length; i++)
                {
                    tilemap.SetTile(tilePositions[i], changeTile);
                }
            };
        }
    }
}


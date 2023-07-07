using UnityEngine;
using UnityEngine.Tilemaps;

namespace Stage
{
    public class TilemapGetter : MonoBehaviour, ITilemapGetter
    {
        [SerializeField] Tilemap grounds;
        [SerializeField] Tilemap walls;
        [SerializeField] Tilemap lowWalls;

        public Tilemap GetGrounds => grounds;
        public Tilemap GetWalls => walls;
        public Tilemap GetLowWalls => lowWalls;

    }

    public interface ITilemapGetter
    {
        Tilemap GetGrounds { get; }
        Tilemap GetWalls { get; }
        Tilemap GetLowWalls { get; }
    }
}


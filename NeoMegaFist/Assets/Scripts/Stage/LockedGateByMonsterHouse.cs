using UnityEngine;
using UnityEngine.UI;

namespace Stage
{
    public class LockedGateByMonsterHouse : MonoBehaviour
    {
        [SerializeField] private MonsterHouse targetMonsterHouse;
        [SerializeField] private Collider2D gateCol;

        private void Start()
        {
            targetMonsterHouse.OnClearMonsterHouse += Open;
        }

        public void Open()
        {

        }
    }
}
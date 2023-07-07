using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Stage
{
    public class AStarPath : MonoBehaviour
    {
        //public TileBase replaceTile;            // �ړ�����Ɉʒu����^�C���̐F��ウ��

        private List<RouteCellInfo> cellInfoList;    // �����Z�����L�����Ă������X�g
        private Vector3 goalPos;                   // �S�[���̈ʒu���
        private Tilemap map;                     // �ړ��͈�
        private bool exitFlg;                   // �������I���������ǂ���

        private List<RouteCellInfo> routes = new List<RouteCellInfo>();
        public IReadOnlyList<RouteCellInfo> Routes => routes;

        /// <summary>
        /// AStar�A���S���Y��
        /// </summary>
        public void AstarSearchPathFinding(Tilemap map, Vector3 startPos, Vector3 goalPos)
        {
            // �S�[���̓v���C���[�̈ʒu���
            this.goalPos = goalPos;
            this.map = map;

            //�o�H������
            routes = new List<RouteCellInfo>();
            cellInfoList = new List<RouteCellInfo>();

            // �X�^�[�g�̏���ݒ肷��(�X�^�[�g�͓G)
            RouteCellInfo start = new RouteCellInfo();
            start.pos = startPos;
            start.cost = 0;
            start.heuristic = Vector2.Distance(startPos, this.goalPos);
            start.sumConst = start.cost + start.heuristic;
            start.parent = new Vector3(-9999, -9999, 0);    // �X�^�[�g���̐e�̈ʒu�͂��肦�Ȃ��l�ɂ��Ă����܂�
            start.isOpen = true;
            cellInfoList.Add(start);

            exitFlg = false;

            // �I�[�v�������݂�����胋�[�v
            while (cellInfoList.Where(x => x.isOpen == true).Select(x => x).Count() > 0 && exitFlg == false)
            {
                // �ŏ��R�X�g�̃m�[�h��T��
                RouteCellInfo minCell = cellInfoList.Where(x => x.isOpen == true).OrderBy(x => x.sumConst).Select(x => x).First();

                OpenSurround(minCell);

                // ���S�̃m�[�h�����
                minCell.isOpen = false;
            }
        }
        private void OpenSurround(RouteCellInfo center)
        {
            // �|�W�V������Vector3Int�֕ϊ�
            Vector3Int centerPos = map.WorldToCell(center.pos);

            for (int i = -1; i < 2; i++)
            {
                for (int j = -1; j < 2; j++)
                {
                    // �㉺���E�̂݉Ƃ���A���A���S�͏��O
                    if (((i != 0 && j == 0) || (i == 0 && j != 0)) && !(i == 0 && j == 0))
                    {
                        Vector3Int posInt = new Vector3Int(centerPos.x + i, centerPos.y + j, centerPos.z);
                        if (!map.HasTile(posInt) && !(i == 0 && j == 0))
                        {
                            // ���X�g�ɑ��݂��Ȃ����T��
                            Vector3 pos = map.CellToWorld(posInt);
                            pos = new Vector3(pos.x + 0.5f, pos.y + 0.5f, pos.z);
                            if (cellInfoList.Where(x => x.pos == pos).Select(x => x).Count() == 0)
                            {
                                // ���X�g�ɒǉ�
                                RouteCellInfo cell = new RouteCellInfo();
                                cell.pos = pos;
                                cell.cost = center.cost + 1;
                                cell.heuristic = Vector2.Distance(pos, goalPos);
                                cell.sumConst = cell.cost + cell.heuristic;
                                cell.parent = center.pos;
                                cell.isOpen = true;
                                cellInfoList.Add(cell);

                                // �S�[���̈ʒu�ƈ�v������I��
                                if (map.WorldToCell(goalPos) == map.WorldToCell(pos))
                                {
                                    RouteCellInfo preCell = cell;
                                    while (preCell.parent != new Vector3(-9999, -9999, 0))
                                    {
                                        //map.SetTile(map.WorldToCell(preCell.pos), replaceTile);
                                        preCell = cellInfoList.Where(x => x.pos == preCell.parent).Select(x => x).First();
                                        routes.Add(preCell);
                                    }

                                    exitFlg = true;
                                    return;
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    public class RouteCellInfo
    {
        public Vector3 pos { get; set; }        // �Ώۂ̈ʒu���
        public float cost { get; set; }         // ���R�X�g(���܂ŉ�����������)
        public float heuristic { get; set; }    // ����R�X�g(�S�[���܂ł̋���)
        public float sumConst { get; set; }     // ���R�X�g = ���R�X�g + ����R�X�g
        public Vector3 parent { get; set; }     // �e�Z���̈ʒu���
        public bool isOpen { get; set; }        // �����ΏۂƂȂ��Ă��邩�ǂ���
    }
}
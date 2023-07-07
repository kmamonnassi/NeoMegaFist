using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Stage
{
    public class AStarPath : MonoBehaviour
    {
        //public TileBase replaceTile;            // 移動線上に位置するタイルの色を代える

        private List<RouteCellInfo> cellInfoList;    // 調査セルを記憶しておくリスト
        private Vector3 goalPos;                   // ゴールの位置情報
        private Tilemap map;                     // 移動範囲
        private bool exitFlg;                   // 処理が終了したかどうか

        private List<RouteCellInfo> routes = new List<RouteCellInfo>();
        public IReadOnlyList<RouteCellInfo> Routes => routes;

        /// <summary>
        /// AStarアルゴリズム
        /// </summary>
        public void AstarSearchPathFinding(Tilemap map, Vector3 startPos, Vector3 goalPos)
        {
            // ゴールはプレイヤーの位置情報
            this.goalPos = goalPos;
            this.map = map;

            //経路初期化
            routes = new List<RouteCellInfo>();
            cellInfoList = new List<RouteCellInfo>();

            // スタートの情報を設定する(スタートは敵)
            RouteCellInfo start = new RouteCellInfo();
            start.pos = startPos;
            start.cost = 0;
            start.heuristic = Vector2.Distance(startPos, this.goalPos);
            start.sumConst = start.cost + start.heuristic;
            start.parent = new Vector3(-9999, -9999, 0);    // スタート時の親の位置はありえない値にしておきます
            start.isOpen = true;
            cellInfoList.Add(start);

            exitFlg = false;

            // オープンが存在する限りループ
            while (cellInfoList.Where(x => x.isOpen == true).Select(x => x).Count() > 0 && exitFlg == false)
            {
                // 最小コストのノードを探す
                RouteCellInfo minCell = cellInfoList.Where(x => x.isOpen == true).OrderBy(x => x.sumConst).Select(x => x).First();

                OpenSurround(minCell);

                // 中心のノードを閉じる
                minCell.isOpen = false;
            }
        }
        private void OpenSurround(RouteCellInfo center)
        {
            // ポジションをVector3Intへ変換
            Vector3Int centerPos = map.WorldToCell(center.pos);

            for (int i = -1; i < 2; i++)
            {
                for (int j = -1; j < 2; j++)
                {
                    // 上下左右のみ可とする、かつ、中心は除外
                    if (((i != 0 && j == 0) || (i == 0 && j != 0)) && !(i == 0 && j == 0))
                    {
                        Vector3Int posInt = new Vector3Int(centerPos.x + i, centerPos.y + j, centerPos.z);
                        if (!map.HasTile(posInt) && !(i == 0 && j == 0))
                        {
                            // リストに存在しないか探す
                            Vector3 pos = map.CellToWorld(posInt);
                            pos = new Vector3(pos.x + 0.5f, pos.y + 0.5f, pos.z);
                            if (cellInfoList.Where(x => x.pos == pos).Select(x => x).Count() == 0)
                            {
                                // リストに追加
                                RouteCellInfo cell = new RouteCellInfo();
                                cell.pos = pos;
                                cell.cost = center.cost + 1;
                                cell.heuristic = Vector2.Distance(pos, goalPos);
                                cell.sumConst = cell.cost + cell.heuristic;
                                cell.parent = center.pos;
                                cell.isOpen = true;
                                cellInfoList.Add(cell);

                                // ゴールの位置と一致したら終了
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
        public Vector3 pos { get; set; }        // 対象の位置情報
        public float cost { get; set; }         // 実コスト(今まで何歩歩いたか)
        public float heuristic { get; set; }    // 推定コスト(ゴールまでの距離)
        public float sumConst { get; set; }     // 総コスト = 実コスト + 推定コスト
        public Vector3 parent { get; set; }     // 親セルの位置情報
        public bool isOpen { get; set; }        // 調査対象となっているかどうか
    }
}
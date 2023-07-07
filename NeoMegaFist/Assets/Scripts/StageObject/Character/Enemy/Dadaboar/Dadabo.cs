using DG.Tweening;
using Stage;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace StageObject
{
    public class Dadabo : CharacterBase
    {
        [SerializeField] private ObjectSearcher objectSearcher;
        [SerializeField] private EffectCollider assultCollider;
        [SerializeField] private AStarPath aStarPath;
        [SerializeField] private float moveSpeed = 1000;
        [SerializeField] private float walkAreaRadius = 64;
        [SerializeField] private float stayDurationMin = 1;
        [SerializeField] private float stayDurationMax = 3;
        [SerializeField] private float walkDurationMin = 1;
        [SerializeField] private float walkDurationMax = 2;
        [SerializeField] private float followPlayerSpeed = 100;
        [SerializeField] private float followValidateInterval = 2;
        [SerializeField] private float assultDistance = 128;
        [SerializeField] private float assultPreparationTime = 2;
        [SerializeField] private float assultSpeed = 2;

        [Inject] private Player player;
        [Inject] private ITilemapGetter tilemapGetter;

        public override StageObjectID ID => StageObjectID.Dadabo;
        public override StageObjectType Type => StageObjectType.Enemy;
        public override Size DefaultSize => Size.Small;

        private State state = State.Stay;
        private bool isNotice = false;
        private Vector2 walkPoint;
        private Vector2 walkDir;
        private float walkDuration;
        private float nowFollowValidateTime;
        List<RouteCellInfo> routes;
        private int routesIndex;
        private float nowWalkDuration;
        private float nowAssultPreparationTime;
        private Vector2 assultDir;

        public enum State
        {
            Stay,
            Walk,
            Notice,
            FollowPlayer,
            AssultPreparation,
            Assult,
        }

        protected override void OnAwake_Virtual()
        {
            base.OnAwake_Virtual();
            walkPoint = transform.position;
            nowFollowValidateTime = followValidateInterval;
            OnDamageByCollider += (col) => Notice();
            objectSearcher.OnSearched += (obj) => Notice();
            assultCollider.OnHitWall += wall =>
            {
                if(state == State.Assult)
                {
                    HitWallToStun();
                }
            };

        }

        protected override void OnUpdate_Virtual()
        {
            base.OnUpdate_Virtual();
            if (IsStun) return;
            if (isNotice)
            {
                if (state == State.FollowPlayer)
                {
                    if (assultDistance > Vector2.Distance(transform.position, player.transform.position))
                    {
                        state = State.AssultPreparation;
                        nowAssultPreparationTime = 0;
                    }
                }
                else
                if (state == State.AssultPreparation)
                {
                    nowAssultPreparationTime += Time.deltaTime;
                    if(nowAssultPreparationTime >= assultPreparationTime)
                    {
                        nowAssultPreparationTime = 0;
                        state = State.Assult;
                        assultCollider.enabled = true;
                    }

                    assultDir = ((Vector2)(player.transform.position - transform.position)).normalized;
                    transform.eulerAngles = new Vector3(0, 0, Vector2.zero.GetAngle(assultDir) + 90);
                }

            }
            else
            {
                nowWalkDuration += Time.deltaTime;
                if(nowWalkDuration > walkDuration)
                {
                    nowWalkDuration = 0;
                    if (state == State.Stay)
                    {
                        state = State.Walk;
                        walkDir = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
                        walkDuration = Random.Range(walkDurationMin, walkDurationMax);
                        transform.DORotate(new Vector3(0, 0, Vector2.zero.GetAngle(walkDir) + 90), 0.5f);
                    }
                    else
                    if (state == State.Walk)
                    {
                        state = State.Stay;
                        walkDir = Vector2.zero;
                        walkDuration = Random.Range(stayDurationMin, stayDurationMax);
                    }
                }
            }
        }

        protected override void OnFixedUpdate_Virtual()
        {
            base.OnFixedUpdate_Virtual();
            if (IsStun) return;

            if(state == State.Walk && !isNotice)
            {
                if (nowWalkDuration <= walkDuration)
                {
                    rb.velocity = walkDir * moveSpeed;
                    
                    if (Vector2.Distance(walkPoint, transform.position) > walkAreaRadius)
                    {
                        walkDir *= -1;
                        transform.DORotate(new Vector3(0, 0, Vector2.zero.GetAngle(walkDir) + 90), 0.5f);
                    }
                    transform.position = Vector2.ClampMagnitude((Vector2)transform.position - walkPoint, walkAreaRadius) + walkPoint;
                }
            }
            else
            if(state == State.FollowPlayer && isNotice)
            {
                nowFollowValidateTime += Time.deltaTime;
                if(nowFollowValidateTime > followValidateInterval)
                {
                    nowFollowValidateTime = 0;
                    aStarPath.AstarSearchPathFinding(tilemapGetter.GetWalls, player.transform.position, transform.position);
                    routesIndex = 0;
                    routes = new List<RouteCellInfo>(aStarPath.Routes);
                }

                Vector2 pos = routes[routesIndex].pos + new Vector3(24, 24, 0);
                if (Vector2.Distance(transform.position, pos) > 5)
                {
                    Vector2 dir = (pos - (Vector2)transform.position).normalized;
                    rb.velocity = dir * followPlayerSpeed;
                    transform.eulerAngles = new Vector3(0, 0, Vector2.zero.GetAngle(dir) + 90);
                }
                else
                if(routesIndex < routes.Count - 1)
                {
                    routesIndex++;
                }
            }
            else
            if(state == State.AssultPreparation && isNotice)
            {
                rb.velocity = Vector2.zero;
            }
            else
            if (state == State.Assult && isNotice)
            {
                rb.velocity = assultDir * assultSpeed;
            }
        }

        private void Notice()
        {
            if (isNotice) return;
            isNotice = true;
            state = State.FollowPlayer;
            objectSearcher.gameObject.SetActive(false);
        }

        private void HitWallToStun()
        {
            state = State.FollowPlayer;
            assultCollider.enabled = false;
            StunDamage(10000);
            rb.velocity = Vector2.zero;
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            if(col.gameObject.GetComponent<Wall>())
            {
                HitWallToStun();
            }
        }
    }
}
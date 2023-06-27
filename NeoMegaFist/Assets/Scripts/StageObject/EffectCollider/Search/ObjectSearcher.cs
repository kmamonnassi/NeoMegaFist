using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace StageObject
{
    [RequireComponent(typeof(CircleCollider2D))]
    public class ObjectSearcher : MonoBehaviour
    {
        [SerializeField] private StageObjectType[] targets;
        [SerializeField] private float angle = 22.5f;
        [SerializeField] private float distance = 64;

        public event Action<StageObjectBase> OnSearched;

        private CircleCollider2D mySearcherCollider;
        private List<StageObjectBase> hitObject = new List<StageObjectBase>();

        private void Start()
        {
            mySearcherCollider = GetComponent<CircleCollider2D>();
            mySearcherCollider.radius = distance;
        }

        private void Update()
        {
            foreach(StageObjectBase obj in hitObject)
            {
                //Ž‹ŠE‚ÌŠp“x“à‚ÉŽû‚Ü‚Á‚Ä‚¢‚é‚©
                float target_angle = GetAngle(obj.transform.position, transform.position);
                //target_angle‚ªangle‚ÉŽû‚Ü‚Á‚Ä‚¢‚é‚©‚Ç‚¤‚©
                if (Mathf.Repeat(transform.eulerAngles.z - target_angle, 360) < Mathf.Repeat(angle, 360) || Mathf.Repeat(transform.eulerAngles.z - target_angle, 360) > Mathf.Repeat(-angle, 360))
                {
                    Vector2 dir = ((Vector2)(obj.transform.position - transform.position)).normalized;
                    //Ray‚ðŽg—p‚µ‚Ätarget‚É“–‚½‚Á‚Ä‚¢‚é‚©”»•Ê
                    //Debug.DrawRay(transform.position, dir * distance, Color.green, 1);
                    var hit = Physics2D.Raycast(transform.position, dir, distance, LayerMask.GetMask("Wall", "StageObject"));
                    if (hit)
                    {
                        Debug.Log(hit.collider.name + "/" + obj.name);
                        //Debug.Log(hit.collider.gameObject.name);
                        if (hit.collider.gameObject == obj.gameObject)
                        {
                            OnSearched?.Invoke(obj);
                        }
                    }
                }
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject == gameObject) return;
            StageObjectBase stageObject = other.gameObject.GetComponent<StageObjectBase>();
            if (stageObject == null) return;
            if (!targets.ToList().Contains(stageObject.Type)) return;
            hitObject.Add(stageObject);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject == gameObject) return;
            StageObjectBase stageObject = other.gameObject.GetComponent<StageObjectBase>();
            if (stageObject == null) return;
            if (!targets.ToList().Contains(stageObject.Type)) return;
            hitObject.Remove(stageObject);
        }

        private float GetAngle(Vector2 start, Vector2 target)
        {
            Vector2 dt = target - start;
            float rad = Mathf.Atan2(dt.y, dt.x);
            float degree = rad * Mathf.Rad2Deg;

            return degree + 90;
        }
    }
}
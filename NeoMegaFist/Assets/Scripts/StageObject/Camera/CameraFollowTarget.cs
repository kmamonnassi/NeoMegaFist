using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility.PostEffect;
using Zenject;

namespace Stage
{
    public class CameraFollowTarget : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private float moveSpeed = 10;

        [Inject] private IPostEffectCamera cam;

        private void Update()
        {
            Vector2 targetPos = new Vector2(target.transform.position.x, target.transform.position.y);
            Vector2 pos = Vector2.Lerp(cam.GetPosition(), targetPos, Time.deltaTime * moveSpeed);
            cam.SetPosition(pos);
        }
    }

}
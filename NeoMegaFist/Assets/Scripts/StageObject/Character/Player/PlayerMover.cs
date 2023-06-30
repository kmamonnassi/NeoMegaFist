using System;
using System.Collections.Generic;
using UnityEngine;
using Utility;

namespace StageObject
{
    public class PlayerMover : MonoBehaviour, IFixedUpdate
    {
        [SerializeField] private Rigidbody2D rb;
        private List<IPlayerMove> moves = new List<IPlayerMove>();

        public void ManagedFixedUpdate()
        {
            if (moves.Count == 0) return;

            IPlayerMove move = moves[0];
            for(int i = 1; i < moves.Count;i++)
            {
                if(move.MovePriority < moves[i].MovePriority && moves[i].MoveIsActive)
                {
                    move = moves[i]; 
                }
            }
            rb.velocity = move.MoveVelocity;
        }

        public void Add(IPlayerMove move)
        {
            moves.Add(move);
        }

        public void Remove(IPlayerMove playerWalk)
        {
            moves.Remove(playerWalk);
        }
    }
}

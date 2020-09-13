using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

namespace Assets.Script.States
{
    public class WayPoints : MonoBehaviour
    {
        [SerializeField]
        protected float debugDrawRadius = 1.0F;

        public virtual void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, debugDrawRadius);
        }

    }
}

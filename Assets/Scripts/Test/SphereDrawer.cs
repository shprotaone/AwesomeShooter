using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Test
{
    public class SphereDrawer : MonoBehaviour
    {
        [SerializeField] private Color _color;
        private void OnDrawGizmos()
        {
            Gizmos.color = _color;
            Gizmos.DrawSphere(transform.position,1);
        }
    }

}

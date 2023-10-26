using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace RPG.Control
{
    public class PatrolPath : MonoBehaviour
    {
        [SerializeField] private float radiusPoint = .3f;
        private void OnDrawGizmos()
        {
            int j = 0;
            for (int i = 0; i < transform.childCount; i++)
            {
                j = CyclePoint(j);
                Gizmos.DrawSphere(GetPointPosition(i), radiusPoint);
                Gizmos.DrawLine(GetPointPosition(i), GetPointPosition(j));
            }
        }

        public Vector3 GetPointPosition(int i)
        {
            return transform.GetChild(i).position;
        }

        public int CyclePoint(int j)
        {
            if(j < transform.childCount - 1)
            {
                j++;
            }
            else
            {
                j = 0;
            }
            return j;
        }

    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


namespace RPG.Control 
{
    public class PatrolPath : MonoBehaviour
    {
        const float wayPointGizmosRadius = 0.3f;

        private void OnDrawGizmos()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                Gizmos.DrawSphere(transform.GetChild(i).position, wayPointGizmosRadius);
                int j = GetNextIndex(i);
                Gizmos.DrawLine(GetWayPoint(i), GetWayPoint(j));

            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.cyan;
        }

        public int GetNextIndex(int i)
        {
            if (i + 1 == transform.childCount)
            {
                return 0;
            }
            return i + 1;
        }

        public Vector3 GetWayPoint(int i)
        {
            return transform.GetChild(i).position;
        }
    }
}



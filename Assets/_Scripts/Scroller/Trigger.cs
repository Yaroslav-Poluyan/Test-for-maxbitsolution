using UnityEngine;

namespace _Scripts.Scroller
{
    public class Trigger : MonoBehaviour
    {
#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position + Vector3.down * 5, transform.position + Vector3.up * 5);
        }
#endif
    }
}
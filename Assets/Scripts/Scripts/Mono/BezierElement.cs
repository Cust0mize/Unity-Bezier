using UnityEngine;

namespace Bezier.Scripts.Mono {
#if UNITY_EDITOR
    [ExecuteAlways]
#endif
    public class BezierElement : MonoBehaviour {
        [field: SerializeField] public BezierPoint StartPoint { get; private set; }
        [field: SerializeField] public BezierPoint EndPoint { get; private set; }

#if UNITY_EDITOR
        private void Update() {
            if (!Application.isPlaying) {
                StartPoint.transform.localPosition = Vector3.zero;
                EndPoint.transform.localPosition = Vector3.zero;
            }
        }
#endif
    }
}
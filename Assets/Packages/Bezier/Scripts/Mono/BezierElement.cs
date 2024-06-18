using UnityEngine;

[ExecuteAlways]
public class BezierElement : MonoBehaviour {
    [field: SerializeField] public BezierPoint StartPoint { get; private set; }
    [field: SerializeField] public BezierPoint EndPoint { get; private set; }

    private void Update() {
        if (!Application.isPlaying) {
            StartPoint.transform.localPosition = Vector3.zero;
            EndPoint.transform.localPosition = Vector3.zero;
        }
    }
}

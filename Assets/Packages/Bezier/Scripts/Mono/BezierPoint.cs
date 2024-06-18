using UnityEngine;

public class BezierPoint : MonoBehaviour {
    [SerializeField] private Transform _mainPoint;
    [SerializeField] private Transform _helpPoint;
    public BezierPointModel BezierPointModel { get; private set; }

    public void StartInit() {
        BezierPointModel = new(_mainPoint.transform.position, _helpPoint.transform.position);

        BezierPointModel.OnUpdateMainPosition -= SetMainPosition;
        BezierPointModel.OnUpdateHelpPosition -= SetHelpPosition;
        BezierPointModel.OnUpdateMainLocalPosition -= SetMainLocalPosition;
        BezierPointModel.OnUpdateHelpLocalPosition -= SetHelpLocalPosition;

        BezierPointModel.OnUpdateMainPosition += SetMainPosition;
        BezierPointModel.OnUpdateHelpPosition += SetHelpPosition;
        BezierPointModel.OnUpdateMainLocalPosition += SetMainLocalPosition;
        BezierPointModel.OnUpdateHelpLocalPosition += SetHelpLocalPosition;
    }

    private void SetMainPosition(Vector3 newPosition) {
        _mainPoint.transform.position = newPosition;
    }

    private void SetHelpPosition(Vector3 newPosition) {
        _helpPoint.transform.position = newPosition;
    }

    private void SetHelpLocalPosition(Vector3 newPosition) {
        _helpPoint.localPosition = newPosition;
    }

    private void SetMainLocalPosition(Vector3 newPosition) {
        _mainPoint.localPosition = newPosition;
    }
}

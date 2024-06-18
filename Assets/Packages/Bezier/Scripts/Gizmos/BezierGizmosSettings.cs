using UnityEditor;
using UnityEngine;

public class BezierGizmosSettings : MonoBehaviour {
    public BezierLineModel BezierLineModel { get; private set; }

    [SerializeField] private bool _disableAll;

    [Header("Center Sphere Settings")]
    [SerializeField] private bool _showCenterSphere;
    [SerializeField] private float _centerSphereRadius;
    [SerializeField] private Color _centerSphereColor;

    [Header("Center Line Settings")]
    [SerializeField] private bool _showCenterLine;
    [SerializeField] private Color _centerLineColor;

    [Header("Perpendicular Point Sphere Settings")]
    [SerializeField] private bool _showPerpendicularPoint;
    [SerializeField] private bool _showLineBetweenPerpendicularPoint;
    [SerializeField] private float _perpendicularPointSphereRadius;
    [SerializeField] private Color _perpendicularPointSphereColor;

    [Header("Inverce Perpendicular Point Sphere Settings")]
    [SerializeField] private bool _showInversePerpendicularPoint;
    [SerializeField] private bool _showLineBetweenInvercePerpendicularPoint;
    [SerializeField] private float _inversePerpendicularPointSphereRadius;
    [SerializeField] private Color _inversePerpendicularPointSphereColor;

    [Header("Anchor and Main Points Settings")]
    [SerializeField] private Color _anchorLineColor;

    public void Init(BezierLineModel bezierLineModel) {
        BezierLineModel = bezierLineModel;
    }

#if UNITY_EDITOR 
    private void OnDrawGizmos() {
        if (_disableAll || Application.isPlaying) {
            return;
        }

        ShowCenterPoint();
        ShowPerpendicularPoint();
        ShowInvercePerpendicularPoint();
        ShowLineBetwenPoint();
    }

    private void OnDrawGizmosSelected() {
        ShowAnchorAndMainPointAndLine();
    }

    private void ShowAnchorAndMainPointAndLine() {
        for (int elementIndex = 0; elementIndex < BezierLineModel.ElementsLength; elementIndex++) {
            BezierElementModel bezierElementModel = BezierLineModel.GetBezierElementModelByIndex(elementIndex);

            for (int vertexIndex = 0; vertexIndex < BezierLineModel.ResolutionSettings.GetResolutionByIndex(elementIndex); vertexIndex++) {
                Gizmos.color = _anchorLineColor;
                Gizmos.DrawLine(bezierElementModel.StartPoint.MainPointPosition, bezierElementModel.StartPoint.HelpPointPosition);
                Gizmos.DrawLine(bezierElementModel.EndPoint.MainPointPosition, bezierElementModel.EndPoint.HelpPointPosition);
            }
        }
    }


    private void ShowLineBetwenPoint() {
        Bezier2DPoint preveousePoint = BezierLineModel.GetBezierPoint2DByIndex(0);

        for (int i = 1; i < BezierLineModel.PointLength; i++) {
            Bezier2DPoint currentPoint = BezierLineModel.GetBezierPoint2DByIndex(i);

            if (_showLineBetweenPerpendicularPoint) {
                ShowBetweenLine(preveousePoint.PerpendicularPointPoint, currentPoint.PerpendicularPointPoint, _perpendicularPointSphereColor);
            }

            if (_showLineBetweenInvercePerpendicularPoint) {
                ShowBetweenLine(preveousePoint.InversePerpendicularPointPoint, currentPoint.InversePerpendicularPointPoint, _inversePerpendicularPointSphereColor);
            }

            if (_showCenterLine) {
                ShowBetweenLine(preveousePoint.VerticesPoint, currentPoint.VerticesPoint, _centerLineColor);
            }
            preveousePoint = currentPoint;
        }
    }

    private void ShowInvercePerpendicularPoint() {
        if (_showInversePerpendicularPoint) {
            for (int i = 0; i < BezierLineModel.PointLength; i++) {
                ShowSpherePoint(BezierLineModel.GetBezierPoint2DByIndex(i).InversePerpendicularPointPoint, _inversePerpendicularPointSphereColor, _inversePerpendicularPointSphereRadius);
            }
        }
    }

    private void ShowPerpendicularPoint() {
        if (_showPerpendicularPoint) {
            for (int i = 0; i < BezierLineModel.PointLength; i++) {
                ShowSpherePoint(BezierLineModel.GetBezierPoint2DByIndex(i).PerpendicularPointPoint, _perpendicularPointSphereColor, _perpendicularPointSphereRadius);
            }
        }
    }

    private void ShowBetweenLine(Vector3 startPoint, Vector3 endPoint, Color color) {
        Gizmos.color = color;
        Gizmos.DrawLine(startPoint, endPoint);
    }

    private void ShowSpherePoint(Vector3 center, Color color, float radius) {
        Gizmos.color = color;
        Gizmos.DrawSphere(center, radius);
    }

    private void ShowCenterPoint() {
        if (_showCenterSphere) {
            for (int i = 0; i < BezierLineModel.PointLength; i++) {
                ShowSpherePoint(BezierLineModel.GetBezierPoint2DByIndex(i).VerticesPoint, _centerLineColor, _centerSphereRadius);
            }
        }
    }
#endif
}

[CustomEditor(typeof(BezierGizmosSettings))]
public class BezierLineHandless : Editor {
    private BezierLineModel _bezierLineModel;
    private BezierGizmosSettings _target;

    public void OnSceneGUI() {
        _target = target as BezierGizmosSettings;
        _bezierLineModel = _target.BezierLineModel;
        ShowBezierHandles();
    }

    private void ShowBezierHandles() {
        for (int elementIndex = 0; elementIndex < _bezierLineModel.ElementsLength; elementIndex++) {
            bool isShowStart = elementIndex == 0;

            BezierPointModel startPointModel = _bezierLineModel.GetAnchornPoint(elementIndex, 0);
            BezierPointModel endPointModel = _bezierLineModel.GetAnchornPoint(elementIndex, 1);

            Vector2 updateStartPosition = default;

            if (isShowStart) {
                updateStartPosition = Handles.FreeMoveHandle(startPointModel.MainPointPosition, 0.1f, Vector3.one, Handles.SphereHandleCap);
            }

            Vector3 updateStartTangentPosition = Handles.FreeMoveHandle(startPointModel.HelpPointPosition, 0.1f, Vector3.one, Handles.SphereHandleCap);
            Vector3 updateEndPosition = Handles.FreeMoveHandle(endPointModel.MainPointPosition, 0.1f, Vector3.one, Handles.SphereHandleCap);
            Vector3 updateEndTangentPosition = Handles.FreeMoveHandle(endPointModel.HelpPointPosition, 0.1f, Vector3.one, Handles.SphereHandleCap);

            if (isShowStart) {
                startPointModel.UpdateMainPosition(updateStartPosition);
            }

            startPointModel.UpdateHelpPosition(updateStartTangentPosition);
            endPointModel.UpdateMainPosition(updateEndPosition);
            endPointModel.UpdateHelpPosition(updateEndTangentPosition);
        }
    }
}
#if UNITY_EDITOR
using Assets.Packages.Bezier.Scripts.Models;
using UnityEngine;

namespace Assets.Packages.Bezier.Scripts.Gizmo {
    public class BezierGizmosSettings : MonoBehaviour {
        public BezierLineModel BezierLineModel { get; private set; }

        [SerializeField] private bool _disableAll;
        [field: SerializeField] public float HandleSize { get; private set; }

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

        [Header("Inverse Perpendicular Point Sphere Settings")]
        [SerializeField] private bool _showInversePerpendicularPoint;
        [SerializeField] private bool _showLineBetweenInvercePerpendicularPoint;
        [SerializeField] private float _inversePerpendicularPointSphereRadius;
        [SerializeField] private Color _inversePerpendicularPointSphereColor;

        [Header("Anchor and Main Points Settings")]
        [SerializeField] private Color _anchorLineColor;

        public void Init(BezierLineModel bezierLineModel) {
            BezierLineModel = bezierLineModel;
        }

        public void SelectedLine() {
            ShowCenterPoint(Color.red, _centerSphereRadius * 1.5f);
        }

        private void OnDrawGizmos() {
            if (_disableAll || Application.isPlaying) {
                return;
            }

            ShowCenterPoint(_centerLineColor, _centerSphereRadius);
            ShowPerpendicularPoint();
            ShowInversePerpendicularPoint();
            ShowLineBetweenPoint();
        }

        private void OnDrawGizmosSelected() {
            ShowAnchorAndMainPointAndLine();
        }

        private void ShowAnchorAndMainPointAndLine() {
            for (int elementIndex = 0; elementIndex < BezierLineModel.ElementsLength; elementIndex++) {
                BezierElementModel bezierElementModel = BezierLineModel.GetBezierElementModelByIndex(elementIndex);

                for (int vertexIndex = 0; vertexIndex < BezierLineModel.ResolutionSettings.GetResolutionByIndex(elementIndex); vertexIndex++) {
                    Gizmos.color = _anchorLineColor;
                    BezierPointModel startPointModel = bezierElementModel.GetPoint(BezierPointType.Start);
                    BezierPointModel endPointModel = bezierElementModel.GetPoint(BezierPointType.End);
                    Gizmos.DrawLine(startPointModel.MainPointPosition, startPointModel.HelpPointPosition);
                    Gizmos.DrawLine(endPointModel.MainPointPosition, endPointModel.HelpPointPosition);
                }
            }
        }

        private void ShowLineBetweenPoint() {
            Bezier2DPoint previousPoint = BezierLineModel.GetBezierPoint2DByIndex(0);

            for (int i = 1; i < BezierLineModel.PointLength; i++) {
                Bezier2DPoint currentPoint = BezierLineModel.GetBezierPoint2DByIndex(i);

                if (_showLineBetweenPerpendicularPoint) {
                    ShowBetweenLine(previousPoint.PerpendicularPointPoint, currentPoint.PerpendicularPointPoint, _perpendicularPointSphereColor);
                }

                if (_showLineBetweenInvercePerpendicularPoint) {
                    ShowBetweenLine(previousPoint.InversePerpendicularPointPoint, currentPoint.InversePerpendicularPointPoint, _inversePerpendicularPointSphereColor);
                }

                if (_showCenterLine) {
                    ShowBetweenLine(previousPoint.VerticesPoint, currentPoint.VerticesPoint, _centerLineColor);
                }
                previousPoint = currentPoint;
            }
        }

        private void ShowInversePerpendicularPoint() {
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

        private void ShowCenterPoint(Color color, float radius) {
            if (_showCenterSphere) {
                for (int i = 0; i < BezierLineModel.PointLength; i++) {
                    ShowSpherePoint(BezierLineModel.GetBezierPoint2DByIndex(i).VerticesPoint, color, radius);
                }
            }
        }
    }
}
#endif
using Bezier.Scripts.Models;
using Bezier.Scripts.Gizmo;
using UnityEngine;
using UnityEditor;

namespace Bezier.Scripts.Editors {
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

                BezierPointModel startPointModel = _bezierLineModel.GetBezierPointModel(elementIndex, BezierPointType.Start);
                BezierPointModel endPointModel = _bezierLineModel.GetBezierPointModel(elementIndex, BezierPointType.End);

                Vector2 updateStartPosition = default;

                if (isShowStart) {
                    updateStartPosition = Handles.FreeMoveHandle(startPointModel.MainPointPosition, _target.HandleSize, Vector3.one, Handles.SphereHandleCap);
                }

                Vector3 updateStartTangentPosition = Handles.FreeMoveHandle(startPointModel.HelpPointPosition, _target.HandleSize, Vector3.one, Handles.SphereHandleCap);
                Vector3 updateEndPosition = Handles.FreeMoveHandle(endPointModel.MainPointPosition, _target.HandleSize, Vector3.one, Handles.SphereHandleCap);
                Vector3 updateEndTangentPosition = Handles.FreeMoveHandle(endPointModel.HelpPointPosition, _target.HandleSize, Vector3.one, Handles.SphereHandleCap);

                if (isShowStart) {
                    startPointModel.UpdateMainPosition(updateStartPosition);
                }

                startPointModel.UpdateHelpPosition(updateStartTangentPosition);
                endPointModel.UpdateMainPosition(updateEndPosition);
                endPointModel.UpdateHelpPosition(updateEndTangentPosition);
            }
        }
    }
}
using UnityEditor;
using UnityEngine;

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
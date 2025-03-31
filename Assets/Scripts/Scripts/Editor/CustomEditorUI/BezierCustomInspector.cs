using UnityEditor.UIElements;
using UnityEngine.UIElements;
using Bezier.Scripts.Models;
using Bezier.Scripts.Mono;
using UnityEditor;
using UnityEngine;

namespace Bezier.Scripts.Editors.CustomEditorUI {
    [CustomEditor(typeof(BezierLine))]
    public class BezierCustomInspector : Editor {
        private BezierLineModel _model;
        private VisualElement _root;
        private BezierLine _target;
        public VisualTreeAsset Asset;

        private void OnEnable() {
            UpdateTargetAndModel();
        }

        public override VisualElement CreateInspectorGUI() {
            _root = new VisualElement();
            Asset.CloneTree(_root);
            SetDefaultInspector();
            Button setLineButton = _root.Q<Button>("SetLineButton");
            Button magnetButton = _root.Q<Button>("Magnet");
            Button setLineToStartButton = _root.Q<Button>("SetLineToStart");
            Button setLineStartAndMagnetButton = _root.Q<Button>("SetLineStartAndMagnet");
            Button connectAllButton = _root.Q<Button>("ConnectAll");
            setLineButton.clicked += SetLine;
            magnetButton.clicked += () => MagnateToPoint(_target);
            setLineToStartButton.clicked += SetLineStart;
            setLineStartAndMagnetButton.clicked += MagneticAndSetLine;
            connectAllButton.clicked += ConnectAll;
            return _root;
        }

        private void SetDefaultInspector() {
            VisualElement defaultInspector = new VisualElement();
            InspectorElement.FillDefaultInspector(defaultInspector, serializedObject, this);
            _root.Add(defaultInspector);
        }

        private void SetLine() {
            UpdateTargetAndModel();

            for (int i = 0; i < _model.ElementsLength; i++) {
                BezierPointModel startPointModel = _model.GetBezierPointModel(i, BezierPointType.Start);
                BezierPointModel endPointModel = _model.GetBezierPointModel(i, BezierPointType.End);
                startPointModel.UpdateHelpPosition(startPointModel.MainPointPosition);
                endPointModel.UpdateHelpPosition(endPointModel.MainPointPosition);
            }
        }

        public void MagneticAndSetLine() {
            UpdateTargetAndModel();
            MagnateToPoint(_target);
            SetLineStart();
        }

        public void SetLineStart() {
            UpdateTargetAndModel();

            for (int i = 0; i < _model.ElementsLength; i++) {
                BezierPointModel startPointModel = _model.GetBezierPointModel(i, BezierPointType.Start);
                BezierPointModel endPointModel = _model.GetBezierPointModel(i, BezierPointType.End);

                if (Mathf.Abs(startPointModel.MainPointPosition.y - endPointModel.MainPointPosition.y) < Mathf.Abs(startPointModel.MainPointPosition.x - endPointModel.MainPointPosition.x)) {
                    startPointModel.UpdateHelpPosition(new Vector3(startPointModel.HelpPointPosition.x, startPointModel.MainPointPosition.y));
                    endPointModel.UpdateHelpPosition(new Vector3(endPointModel.HelpPointPosition.x, startPointModel.MainPointPosition.y));
                    endPointModel.UpdateMainPosition(new Vector3(endPointModel.MainPointPosition.x, startPointModel.MainPointPosition.y));
                }
                else {
                    startPointModel.UpdateHelpPosition(new Vector3(startPointModel.MainPointPosition.x, startPointModel.HelpPointPosition.y));
                    endPointModel.UpdateHelpPosition(new Vector3(startPointModel.MainPointPosition.x, endPointModel.HelpPointPosition.y));
                    endPointModel.UpdateMainPosition(new Vector3(startPointModel.MainPointPosition.x, endPointModel.MainPointPosition.y));
                }
            }
        }

        public void MagnateToPoint(BezierLine bezierLine) {
            UpdateTargetAndModel();
            BezierPointModel startPointModel = bezierLine.BezierLineModel.GetBezierPointModel(0, BezierPointType.Start);
            BezierPointModel endPointModel = bezierLine.BezierLineModel.GetBezierPointModel(bezierLine.BezierLineModel.ElementsLength - 1, BezierPointType.End);
            startPointModel.UpdateMainPosition(bezierLine.MinStartPoint);
            endPointModel.UpdateMainPosition(bezierLine.MinEndPoint);
        }

        private void UpdateTargetAndModel() {
            _target = target as BezierLine;
            _model = _target.BezierLineModel;
        }

        public void ConnectAll() {
            BezierLine[] allLine = FindObjectsOfType<BezierLine>();

            for (int i = 0; i < allLine.Length; i++) {
                MagnateToPoint(allLine[i]);
            }
        }
    }
}

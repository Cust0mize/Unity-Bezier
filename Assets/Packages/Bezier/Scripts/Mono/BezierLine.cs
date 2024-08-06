using Assets.Packages.Bezier.Scripts.Models;
using Assets.Packages.Bezier.Scripts.Mono;
using Assets.Scripts.Extension;
using UnityEngine;

#if UNITY_EDITOR
using Assets.Packages.Bezier.Scripts.Gizmo;
#endif

namespace Assets.Scripts.Games.Free.ID29.Bezier.Mono {
#if UNITY_EDITOR
    [ExecuteAlways, RequireComponent(typeof(BezierGizmosSettings))]
#endif
    public class BezierLine : MonoBehaviour {
        [SerializeField] private BezierResolutionSettings _resolutionSettings;
        [SerializeField] private OffsetSettings _offsetSettings;

        public BezierLineModel BezierLineModel { get; private set; }

        private BezierElement[] _bezierElements;
        private bool _isAddNewElement;
        private int _oldElementCount;

#if UNITY_EDITOR
        public BezierGizmosSettings BezierGizmosSettings { get; private set; }
#endif

        public void StartInit() {
            _bezierElements = transform.GetComponentsInChildren<BezierElement>(true);
            _resolutionSettings.UpdateSegments(_bezierElements.Length);
            BezierElementModel[] bezierElementModels = new BezierElementModel[_bezierElements.Length];

            for (int i = 0; i < _bezierElements.Length; i++) {
                BezierElement bezierElement = _bezierElements[i];
                bezierElement.StartPoint.StartInit();
                bezierElement.EndPoint.StartInit();
                bezierElementModels[i] = new BezierElementModel(bezierElement.StartPoint.BezierPointModel, bezierElement.EndPoint.BezierPointModel);
            }

            _isAddNewElement = _oldElementCount != _bezierElements.Length;
            _oldElementCount = _bezierElements.Length;
            BezierLineModel = new BezierLineModel(bezierElementModels, _resolutionSettings, _offsetSettings);


#if UNITY_EDITOR
            BezierGizmosSettings = GetComponent<BezierGizmosSettings>();
            BezierGizmosSettings.Init(BezierLineModel);
#endif
        }

#if UNITY_EDITOR
        private BezierPoint[] _points;

        private Vector3 _minStartPoint;
        public Vector3 MinStartPoint {
            get { return _minStartPoint; }
        }

        private Vector3 _minEndPoint;
        public Vector3 MinEndPoint {
            get { return _minEndPoint; }
        }

        private void OnValidate() {
            StartInit();
        }

        private void Update() {
            if (!Application.isPlaying) {
                StartInit();

                for (int i = 0; i < _bezierElements.Length; i++) {
                    _bezierElements[i].transform.localPosition = Vector3.zero;
                }

                if (BezierLineModel.ElementsLength > 1) {
                    for (int i = BezierLineModel.ElementsLength - 1; i >= 0; i--) {
                        if (i == 0) {
                            BezierLineModel.GetBezierElementModelByIndex(i).GetPoint(BezierPointType.End).UpdateMainPosition(BezierLineModel.GetBezierElementModelByIndex(i + 1).GetPoint(BezierPointType.Start).MainPointPosition);
                        }
                        else if (i == BezierLineModel.ElementsLength - 1) {
                            BezierElement lastElement = _bezierElements[i];
                            lastElement.StartPoint.BezierPointModel.UpdateMainPosition(BezierLineModel.GetBezierElementModelByIndex(i - 1).GetPoint(BezierPointType.End).MainPointPosition);

                            if (_isAddNewElement) {
                                _isAddNewElement = false;
                                BezierElement previousElement = _bezierElements[i - 1];
                                lastElement.EndPoint.BezierPointModel.UpdateMainLocalPosition(previousElement.EndPoint.BezierPointModel.MainPointPosition - previousElement.StartPoint.BezierPointModel.MainPointPosition);
                                lastElement.EndPoint.BezierPointModel.UpdateMainPosition(lastElement.EndPoint.BezierPointModel.MainPointPosition);
                                lastElement.StartPoint.BezierPointModel.UpdateHelpPosition(lastElement.StartPoint.BezierPointModel.MainPointPosition);
                            }
                        }
                    }
                }

                _points = FindObjectsOfType<BezierPoint>();

                BezierPointModel startPointModel = BezierLineModel.GetBezierPointModel(0, BezierPointType.Start);
                BezierPointModel endPointModel = BezierLineModel.GetBezierPointModel(BezierLineModel.ElementsLength - 1, BezierPointType.End);

                float minStartDistance = float.MaxValue;
                _minStartPoint = default;
                float minEndDistance = float.MaxValue;
                _minEndPoint = default;

                for (int i = 0; i < _points.Length; i++) {
                    TryChangePoint(startPointModel, _points[i], ref minStartDistance, ref _minStartPoint);
                    TryChangePoint(endPointModel, _points[i], ref minEndDistance, ref _minEndPoint);
                }
            }
        }

        public bool TryChangePoint(BezierPointModel bezierPointModel, BezierPoint targetPoint, ref float currentMinValue, ref Vector3 minStartPoint) {
            bool isSelectNewPoint = false;
            float distance = Vector2.Distance(targetPoint.BezierPointModel.MainPointPosition, bezierPointModel.MainPointPosition);
            bool isNotThisPoint = true;

            for (int i = 0; i < _bezierElements.Length; i++) {
                if (_bezierElements[i].StartPoint == targetPoint || _bezierElements[i].EndPoint == targetPoint) {
                    isNotThisPoint = false;
                    break;
                }
            }

            if (currentMinValue > distance && isNotThisPoint) {
                currentMinValue = distance;
                minStartPoint = targetPoint.BezierPointModel.MainPointPosition;
                isSelectNewPoint = true;
            }

            return isSelectNewPoint;
        }

        private void OnDrawGizmosSelected() {
            if (_points != null && _points.ContainsAnyValue()) {
                Gizmos.color = Color.yellow;
                Gizmos.DrawSphere(_minStartPoint, 0.02f);
                Gizmos.color = Color.red;
                Gizmos.DrawSphere(_minEndPoint, 0.02f);
            }
        }
#endif
    }
}




//Check submodule


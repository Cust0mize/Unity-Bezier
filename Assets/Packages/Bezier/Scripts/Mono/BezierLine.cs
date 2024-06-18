using UnityEngine;

namespace Assets.Scripts.Games.Free.ID29.Bezier.Mono {
    [ExecuteAlways, RequireComponent(typeof(BezierGizmosSettings))]
    public class BezierLine : MonoBehaviour {
        [SerializeField] private BezierResolutionSettings _resolutionSettings;
        [SerializeField] private OffsetSettings _offsetSettings;

        public BezierLineModel BezierLineModel { get; private set; }
        private BezierElement[] _bezierElements;
        private bool _isAddNewElement;
        private int _oldElementCount;

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
            GetComponent<BezierGizmosSettings>().Init(BezierLineModel);
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
                            BezierLineModel.GetBezierElementModelByIndex(i).EndPoint.UpdateMainPosition(BezierLineModel.GetBezierElementModelByIndex(i + 1).StartPoint.MainPointPosition);
                        }
                        else if (i == BezierLineModel.ElementsLength - 1) {
                            BezierElement lastElement = _bezierElements[i];
                            lastElement.StartPoint.BezierPointModel.UpdateMainPosition(BezierLineModel.GetBezierElementModelByIndex(i - 1).EndPoint.MainPointPosition);

                            if (_isAddNewElement) {
                                _isAddNewElement = false;
                                BezierElement previusElement = _bezierElements[i - 1];
                                lastElement.EndPoint.BezierPointModel.UpdateMainLocalPosition(previusElement.EndPoint.BezierPointModel.MainPointPosition - previusElement.StartPoint.BezierPointModel.MainPointPosition);
                                lastElement.EndPoint.BezierPointModel.UpdateMainPosition(lastElement.EndPoint.BezierPointModel.MainPointPosition);
                                lastElement.StartPoint.BezierPointModel.UpdateHelpPosition(lastElement.StartPoint.BezierPointModel.MainPointPosition);
                            }
                        }
                    }
                }
            }
        }
    }
}

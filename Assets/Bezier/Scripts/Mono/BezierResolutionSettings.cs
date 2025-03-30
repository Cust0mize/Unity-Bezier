using UnityEngine;
using System;

namespace Bezier.Scripts.Mono {
    [Serializable]
    public struct BezierResolutionSettings {
        [field: SerializeField] public int StandartSegmentResolution { get; private set; }
        [SerializeField] private int[] _segmentResolutions;

        public BezierResolutionSettings(int standartSegmentResolution, int[] segmentsResolution) {
            StandartSegmentResolution = standartSegmentResolution;
            _segmentResolutions = segmentsResolution;
        }

        public void UpdateSegments(int newLength) {
            int[] tempResolutions = new int[_segmentResolutions.Length];
            StandartSegmentResolution = Mathf.Clamp(StandartSegmentResolution, 1, int.MaxValue);

            if (_segmentResolutions != null) {
                tempResolutions = new int[_segmentResolutions.Length];

                for (int i = 0; i < tempResolutions.Length; i++) {
                    tempResolutions[i] = _segmentResolutions[i];
                }
            }

            _segmentResolutions = new int[newLength];

            for (int i = 0; i < _segmentResolutions.Length; i++) {
                if (tempResolutions != null) {
                    if (i < tempResolutions.Length) {
                        if (tempResolutions[i] > 1) {
                            _segmentResolutions[i] = tempResolutions[i];
                            continue;
                        }
                    }
                }

                _segmentResolutions[i] = StandartSegmentResolution;
            }
        }

        public int GetResolutionByIndex(int index) {
            return _segmentResolutions[index];
        }
    }
}

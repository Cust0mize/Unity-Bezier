using Assets.Scripts.Games.Free.ID29.Bezier.Mono;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Packages.Bezier.Scripts.Models {
    public class BezierLineModel {
        public int ElementsLength => _bezierElementModel.Length;
        public readonly BezierResolutionSettings ResolutionSettings;
        private readonly OffsetSettings _offsetSettings;
        private BezierElementModel[] _bezierElementModel;
        private Dictionary<int, Vector3[]> _squarePoints = new();
        private List<Bezier2DPoint> _bezier2dPoint = new();

        public int PointLength => _bezier2dPoint.Count;

        public BezierLineModel(BezierElementModel[] bezierElementModel, BezierResolutionSettings resolutionSettings, OffsetSettings offsetSettings) {
            _bezierElementModel = bezierElementModel;
            ResolutionSettings = resolutionSettings;
            _offsetSettings = offsetSettings;

            for (int elementIndex = 0; elementIndex < bezierElementModel.Length; elementIndex++) {
                int resolution = resolutionSettings.GetResolutionByIndex(elementIndex);

                for (int i = 0; i <= resolution; i++) {
                    float time = (float)i / resolution;
                    _bezier2dPoint.Add(Get2DPoint(time + elementIndex));
                }
            }

            for (int i = 0; i < _bezier2dPoint.Count; i++) {
                _squarePoints.Add(i, GetSquarePointFromRealTime(i));
            }
        }

        private Vector3 GetVerticesPoint(float time) {
            int index = GetIndexByTime(time);

            time = time - index;

            if (time < 0.0001f) {
                time = 0;
            }

            return GetCenterPoint(_bezierElementModel[index], time);
        }

        public BezierElementModel GetBezierElementModelByIndex(int index) {
            return _bezierElementModel[index];
        }

        public BezierElementModel GetBezierElementByTime(float time, out float bezierElementTime) {
            int index = GetIndexByTime(time);
            bool isLastPoint = ElementsLength == time;

            if (time == 1 && !isLastPoint || time > 1) {
                if (isLastPoint) {
                    bezierElementTime = 1;
                }
                else {
                    bezierElementTime = time % (int)time;

                    if (bezierElementTime < 0.0001f) {
                        bezierElementTime = 0;
                    }
                }
            }
            else {
                bezierElementTime = time;
            }

            return _bezierElementModel[index];
        }

        public List<Vector3> GetAllSquareFromCache() {
            List<Vector3> result = new();

            for (int i = 0; i < _squarePoints.Count; i++) {
                result.AddRange(_squarePoints[i]);
            }

            for (int i = 0; i < result.Count; i++) {
                result[i] = new Vector3(result[i].x, result[i].y, 0);
            }

            return result;
        }

        public Bezier2DPoint GetNearPoint(Vector2 inputPoint, out float minDistance, bool isStart, float tolerance = 0.01f, int maxIterations = 1000) {
            float time = GetTimeForPoint(inputPoint, tolerance, maxIterations);
            List<Bezier2DPoint> bezierPoints = GetPointsToTime(time, isStart);
            minDistance = Vector2.Distance(inputPoint, bezierPoints[bezierPoints.Count - 1].VerticesPoint);
            return bezierPoints[bezierPoints.Count - 1];
        }

        public BezierPointModel GetAnchornPoint(int elementIndex, int pointIndex) {
            return _bezierElementModel[elementIndex].GetAnchorPoint(pointIndex);
        }

        public Bezier2DPoint Get2DPoint(float time) {
            Vector3 verticesPoint = GetVerticesPoint(time);
            BezierElementModel bezierElementModel = GetBezierElementByTime(time, out float bezierElementTime);
            Vector2 helpPoint = GetHelpPointFromTime(bezierElementModel, bezierElementTime);
            Bezier2DPoint bezier2DPoint = new Bezier2DPoint(verticesPoint, helpPoint, _offsetSettings.PerpendicularPointOffsetOfCenter);
            return bezier2DPoint;
        }

        public Vector2 GetHelpPointFromTime(BezierElementModel bezierElementModel, float time) {
            Vector3 helpPoint;

            if (time == 0) {
                Vector3 point = GetCenterPoint(bezierElementModel, 0.01f);
                Vector3 zeroPoint = GetCenterPoint(bezierElementModel, time);
                Vector3 direction = zeroPoint - point;
                Vector3 longZeroPoint = zeroPoint + direction;
                helpPoint = longZeroPoint;
            }
            else {
                helpPoint = GetCenterPoint(bezierElementModel, time - 0.01f);
            }

            return helpPoint;
        }

        public List<Bezier2DPoint> GetPointsToTime(float time, bool isStart, float step = 0.01f) {
            List<Bezier2DPoint> lastPoints = new();

            if (isStart) {
                lastPoints.AddRange(GetPointsUpToTime(time, step));
                return lastPoints;
            }
            else {
                float maxTime = ElementsLength;
                lastPoints.AddRange(GetPointsDownToTime(maxTime, time, step));
                return lastPoints;
            }
        }

        public float GetTimeForPoint(Vector3 targetPoint, float tolerance = 0.01f, int maxIterations = 1000) {
            float timeMin = 0f;
            float timeMax = ElementsLength;
            float time = timeMax / 2;

            for (int i = 0; i < maxIterations; i++) {
                Vector3 point = GetVerticesPoint(time);
                float distance = Vector3.Distance(point, targetPoint);

                if (distance < tolerance) {
                    return time;
                }

                Vector3 direction = GetVerticesPoint(time + tolerance) - point;
                Vector3 toTarget = targetPoint - point;

                if (Vector3.Dot(direction, toTarget) > 0) {
                    timeMin = time;
                }
                else {
                    timeMax = time;
                }

                time = (timeMin + timeMax) / 2f;
            }

            return time;
        }

        public Bezier2DPoint GetBezierPoint2DByIndex(int index) {
            return _bezier2dPoint[index];
        }

        private List<Bezier2DPoint> GetPointsUpToTime(float time, float step = 0.01f) {
            List<Bezier2DPoint> points = new List<Bezier2DPoint>();

            for (float stepTime = 0f; stepTime < time; stepTime += step) {
                points.Add(Get2DPoint(stepTime));
            }

            points.Add(Get2DPoint(time));
            return points;
        }

        private List<Bezier2DPoint> GetPointsDownToTime(float maxTime, float time, float step = 0.01f) {
            List<Bezier2DPoint> points = new List<Bezier2DPoint>();


            for (float stepTime = maxTime; stepTime > time; stepTime -= step) {
                points.Add(Get2DPoint(stepTime));
            }

            points.Add(Get2DPoint(time));
            return points;
        }

        private Vector3 GetCenterPoint(BezierElementModel bezierElementModel, float bezierElementTime) {
            bezierElementTime = Mathf.Clamp01(bezierElementTime);
            float oneMinusT = 1f - bezierElementTime;

            Vector2 p0 = bezierElementModel.GetAnchorPoint(0).MainPointPosition;
            Vector2 p1 = bezierElementModel.GetAnchorPoint(0).HelpPointPosition;
            Vector2 p2 = bezierElementModel.GetAnchorPoint(1).HelpPointPosition;
            Vector2 p3 = bezierElementModel.GetAnchorPoint(1).MainPointPosition;

            return oneMinusT * oneMinusT * oneMinusT * p0 +
            3f * oneMinusT * oneMinusT * bezierElementTime * p1 +
            3f * oneMinusT * bezierElementTime * bezierElementTime * p2 +
            bezierElementTime * bezierElementTime * bezierElementTime * p3;
        }

        private int GetIndexByTime(float time) {
            time = Mathf.Clamp(time, 0, ElementsLength);
            int index = Mathf.FloorToInt(time);
            return Mathf.Clamp(index, 0, _bezierElementModel.Length - 1);
        }

        private Vector3[] GetSquarePointFromRealTime(int pointIndex) {
            Vector3[] result;

            if (pointIndex == 0 || pointIndex == _bezier2dPoint.Count - 1) {
                result = new Vector3[4];

                if (pointIndex == 0) {
                    result[0] = _bezier2dPoint[pointIndex].PerpendicularPointPoint;
                    result[1] = _bezier2dPoint[pointIndex].InversePerpendicularPointPoint;
                    result[2] = _bezier2dPoint[pointIndex + 1].PerpendicularPointPoint;
                    result[3] = _bezier2dPoint[pointIndex + 1].InversePerpendicularPointPoint;
                }
                else {
                    result[0] = _bezier2dPoint[pointIndex - 1].PerpendicularPointPoint;
                    result[1] = _bezier2dPoint[pointIndex - 1].InversePerpendicularPointPoint;
                    result[2] = _bezier2dPoint[pointIndex].PerpendicularPointPoint;
                    result[3] = _bezier2dPoint[pointIndex].InversePerpendicularPointPoint;
                }
            }
            else {
                result = new Vector3[6];

                for (int i = 0; i < result.Length; i++) {
                    if (i % 2 == 0) {
                        result[i] = _bezier2dPoint[pointIndex - 1 + i / 2].PerpendicularPointPoint;
                    }
                    else {
                        result[i] = _bezier2dPoint[pointIndex - 1 + i / 2].InversePerpendicularPointPoint;
                    }
                }
            }
            return result;
        }
    }
}
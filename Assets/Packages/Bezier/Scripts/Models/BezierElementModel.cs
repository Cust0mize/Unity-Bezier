using UnityEngine;

namespace Assets.Packages.Bezier.Scripts.Models {
    public class BezierElementModel {
        public BezierPointModel StartPoint { get; private set; }
        public BezierPointModel EndPoint { get; private set; }

        public BezierElementModel(BezierPointModel startPoint, BezierPointModel endPoint) {
            StartPoint = startPoint;
            EndPoint = endPoint;
        }

        public BezierPointModel GetAnchorPoint(int pointIndex) {
            pointIndex = Mathf.Clamp(pointIndex, 0, 1);
            BezierPointModel result;
            result = pointIndex == 0 ? StartPoint : EndPoint;
            return result;
        }
    }
}
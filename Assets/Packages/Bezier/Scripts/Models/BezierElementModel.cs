using System.Collections.Generic;

namespace Assets.Packages.Bezier.Scripts.Models {
    public class BezierElementModel {
        private Dictionary<BezierPointType, BezierPointModel> _bezierPoints = new();

        public BezierElementModel(BezierPointModel startPoint, BezierPointModel endPoint) {
            _bezierPoints.Add(BezierPointType.Start, startPoint);
            _bezierPoints.Add(BezierPointType.End, endPoint);
        }

        public BezierPointModel GetPoint(BezierPointType bezierPointType) {
            return _bezierPoints[bezierPointType];
        }
    }

    public enum BezierPointType {
        Start,
        End
    }
}
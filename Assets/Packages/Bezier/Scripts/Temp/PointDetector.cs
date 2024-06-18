using UnityEngine;

public class PointDetector {
    public bool IsPointInSquare(Vector3[] points, Vector3 point) {
        if (points.Length > 4) {
        }
        var firstTriangle = new Vector3[points.Length - 1];
        var secondTriangle = new Vector3[points.Length - 1];

        firstTriangle[0] = points[0];
        firstTriangle[1] = points[1];
        firstTriangle[2] = points[3];

        secondTriangle[0] = points[3];
        secondTriangle[1] = points[2];
        secondTriangle[2] = points[0];

        if (IsPointInTriangleBarycentric(point, firstTriangle)) {
            return true;
        }
        else {
            if (IsPointInTriangleBarycentric(point, secondTriangle)) {
                return true;
            }
            else {
                return false;
            }
        }
    }

    public bool IsPointInTriangleBarycentric(Vector3 point, Vector3[] triangle) {
        Vector2 direction02 = triangle[2] - triangle[0];
        Vector2 direction01 = triangle[1] - triangle[0];
        Vector2 direction0Target = point - triangle[0];

        float dot00 = Vector2.Dot(direction02, direction02);
        float dot01 = Vector2.Dot(direction02, direction01);
        float dot02 = Vector2.Dot(direction02, direction0Target);
        float dot11 = Vector2.Dot(direction01, direction01);
        float dot12 = Vector2.Dot(direction01, direction0Target);

        float invDenom = 1 / (dot00 * dot11 - dot01 * dot01);
        float u = (dot11 * dot02 - dot01 * dot12) * invDenom;
        float v = (dot00 * dot12 - dot01 * dot02) * invDenom;

        return (u >= 0) && (v >= 0) && (u + v < 1);
    }
}
using System.Collections.Generic;
using UnityEngine;

public static class Bezier {
    //public static Vector3 GetPoint(BezierElementModel bezierElement, float time) {
    //    time = Mathf.Clamp01(time);
    //    float oneMinusT = 1f - time;
    //    return oneMinusT * oneMinusT * oneMinusT * bezierElement.GetAnchorPoint(0) +
    //    3f * oneMinusT * oneMinusT * time * bezierElement.GetAnchorPoint(1) +
    //    3f * oneMinusT * time * time * bezierElement.GetAnchorPoint(2) +
    //    time * time * time * bezierElement.GetAnchorPoint(3);
    //}

    //public static Bezier2DPoint Get2DPoint(BezierLineModel lineModel, float time) {
    //    Vector3 verticesPoint = lineModel.GetVerticesPoint(time);
    //    BezierElementModel bezierElementModel = lineModel.GetBezierElementByTime(time, out float bezierElementTime);
    //    Vector2 helpPoint = GetHelpPointFromTime(bezierElementModel, bezierElementTime);
    //    Bezier2DPoint bezier2DPoint = new Bezier2DPoint(verticesPoint, helpPoint, bezierElementModel.PerpendicularPointOffsetOfCenter);
    //    return bezier2DPoint;
    //}

    //public static Bezier2DPoint Get2DPoint(BezierElementModel bezierElementModel, float time) {
    //    Vector3 point = GetPoint(bezierElementModel, time);
    //    Vector2 helpPoint = GetHelpPointFromTime(bezierElementModel, time);
    //    Bezier2DPoint bezier2DPoint = new Bezier2DPoint(point, helpPoint, bezierElementModel.PerpendicularPointOffsetOfCenter);
    //    return bezier2DPoint;
    //}

    //public static Vector3 GetFirstDerivative(BezierElementModel bezierElement, float time) {
    //    time = Mathf.Clamp01(time);
    //    float oneMinusT = 1f - time;
    //    return 3f * oneMinusT * oneMinusT * (bezierElement.GetAnchorPoint(1) - bezierElement.GetAnchorPoint(0)) +
    //    6f * oneMinusT * time * (bezierElement.GetAnchorPoint(2) - bezierElement.GetAnchorPoint(1)) +
    //    3f * time * time * (bezierElement.GetAnchorPoint(3) - bezierElement.GetAnchorPoint(2));
    //}

    //public static Vector3 GetNormal(BezierElementModel bezierElement, float time) {
    //    Vector3 tangent = GetTangent(bezierElement, time);
    //    return new Vector3(-tangent.z, tangent.y, 0);
    //}

    //public static Vector3 GetTangent(BezierElementModel bezierElement, float time) {
    //    return GetFirstDerivative(bezierElement, time);
    //}

    //public static float GetArcLength(BezierElementModel bezierElementModel, int numPoints = 100) {
    //    float length = 0f;
    //    Vector3 previousPoint = bezierElementModel.GetAnchorPoint(0);
    //    for (int i = 1; i <= numPoints; i++) {
    //        float t = (float)i / numPoints;
    //        Vector3 point = GetPoint(bezierElementModel, t);
    //        length += Vector3.Distance(previousPoint, point);
    //        previousPoint = point;
    //    }
    //    return length;
    //}

    //public static float GetTimeForDistance(BezierElementModel bezierElementModel, float distance, int numIterations = 20) {
    //    float lower = 0f;
    //    float upper = 1f;
    //    float time = 0.5f;

    //    for (int i = 0; i < numIterations; i++) {
    //        float length = GetArcLength(bezierElementModel, 100);

    //        if (length < distance) {
    //            lower = time;
    //        }
    //        else {
    //            upper = time;
    //        }

    //        time = (lower + upper) / 2f;
    //    }

    //    return time;
    //}

    //public static List<Bezier2DPoint> GetPointsUpToTime(BezierLineModel bezierLineModel, float time, float step = 0.01f) {
    //    List<Bezier2DPoint> points = new List<Bezier2DPoint>();

    //    for (float stepTime = 0f; stepTime < time; stepTime += step) {
    //        points.Add(Get2DPoint(bezierLineModel, stepTime));
    //    }

    //    points.Add(Get2DPoint(bezierLineModel, time));
    //    return points;
    //}

    //public static List<Bezier2DPoint> GetPointsDownToTime(BezierLineModel bezierLineModel, float maxTime, float time, float step = 0.01f) {
    //    List<Bezier2DPoint> points = new List<Bezier2DPoint>();


    //    for (float stepTime = maxTime; stepTime > time; stepTime -= step) {
    //        points.Add(Get2DPoint(bezierLineModel, stepTime));
    //    }

    //    points.Add(Get2DPoint(bezierLineModel, time));
    //    return points;
    //}

    //public static List<Bezier2DPoint> GetPointsToTime(BezierLineModel lineModel, float time, bool isStart, float step = 0.01f) {
    //    List<Bezier2DPoint> lastPoints = new();

    //    if (isStart) {
    //        lastPoints.AddRange(GetPointsUpToTime(lineModel, time, step));
    //        return lastPoints;
    //    }
    //    else {
    //        float maxTime = lineModel.ElementsLength;
    //        lastPoints.AddRange(GetPointsDownToTime(lineModel, maxTime, time, step));
    //        return lastPoints;
    //    }
    //}

    //public static float GetTimeForPoint(BezierLineModel lineModel, Vector3 targetPoint, float tolerance = 0.01f, int maxIterations = 1000) {
    //    float timeMin = 0f;
    //    float timeMax = lineModel.ElementsLength;
    //    float time = timeMax / 2;

    //    for (int i = 0; i < maxIterations; i++) {
    //        Vector3 point = lineModel.GetVerticesPoint(time);
    //        float distance = Vector3.Distance(point, targetPoint);

    //        if (distance < tolerance) {
    //            return time;
    //        }

    //        Vector3 direction = lineModel.GetVerticesPoint(time + tolerance) - point;
    //        Vector3 toTarget = targetPoint - point;

    //        if (Vector3.Dot(direction, toTarget) > 0) {
    //            timeMin = time;
    //        }
    //        else {
    //            timeMax = time;
    //        }

    //        time = (timeMin + timeMax) / 2f;
    //    }

    //    return time;
    //}

    //public static Vector2 GetPerpendicularPoint(Vector2 helpPoint, Vector2 vertices, float pointOffsetPosition = 1) {
    //    Vector2 directionVector = helpPoint - vertices;
    //    Vector2 perpendicularVector = new Vector2(-directionVector.y, directionVector.x);
    //    Vector2 result = vertices + perpendicularVector.normalized * pointOffsetPosition;
    //    return result;
    //}

    //public static Vector2 GetInversePerpendicularPoint(Vector2 vertices, Vector2 perpendicularPoint) {
    //    Vector2 directionBA = vertices - perpendicularPoint;
    //    return vertices + directionBA;
    //}

    //public static Vector2 GetHelpPointFromTime(BezierElementModel bezierElementModel, float time) {
    //    Vector3 helpPoint;

    //    if (time == 0) {
    //        Vector3 point = GetPoint(bezierElementModel, 0.01f);
    //        Vector3 zeroPoint = GetPoint(bezierElementModel, time);
    //        Vector3 direction = zeroPoint - point;
    //        Vector3 longZeroPoint = zeroPoint + direction;
    //        helpPoint = longZeroPoint;
    //    }
    //    else {
    //        helpPoint = GetPoint(bezierElementModel, time - 0.01f);
    //    }

    //    return helpPoint;
    //}

    //public static Vector2[] GetPerpendicularPoints(Vector2[] helpPoint, Vector2[] vertices, float pointOffsetPosition = 1) {
    //    Vector2[] results = new Vector2[vertices.Length];

    //    for (int i = 0; i < vertices.Length; i++) {
    //        results[i] = GetPerpendicularPoint(helpPoint[i], vertices[i], pointOffsetPosition);
    //    }
    //    return results;
    //}

    //public static Vector2[] GetInversePerpendicularPoints(Vector2[] vertices, Vector2[] perpendicularPoint) {
    //    Vector2[] inversePerpendicularPoints = new Vector2[perpendicularPoint.Length];

    //    for (int i = 0; i < vertices.Length; i++) {
    //        inversePerpendicularPoints[i] = GetInversePerpendicularPoint(vertices[i], perpendicularPoint[i]);
    //    }

    //    return inversePerpendicularPoints;
    //}
}
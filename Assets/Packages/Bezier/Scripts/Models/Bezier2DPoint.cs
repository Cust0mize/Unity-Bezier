using UnityEngine;

public struct Bezier2DPoint {
    public Vector3 VerticesPoint { get; }
    public Vector3 HelpPoint { get; }
    public Vector3 PerpendicularPointPoint { get; }
    public Vector3 InversePerpendicularPointPoint { get; }

    public Bezier2DPoint(Vector3 verticesPoint, Vector2 helpPoint, float perpendicularPointOffsetOfCenter) {
        VerticesPoint = verticesPoint;
        HelpPoint = helpPoint;
        PerpendicularPointPoint = GetPerpendicularPoint(HelpPoint, VerticesPoint, perpendicularPointOffsetOfCenter);
        InversePerpendicularPointPoint = GetInversePerpendicularPoint(VerticesPoint, PerpendicularPointPoint);
    }

    private static Vector2 GetPerpendicularPoint(Vector3 helpPoint, Vector3 vertices, float pointOffsetPosition = 1) {
        Vector3 directionVector = helpPoint - vertices;
        Vector3 perpendicularVector = new Vector3(-directionVector.y, directionVector.x);
        Vector3 result = vertices + perpendicularVector.normalized * pointOffsetPosition;
        return result;
    }

    private static Vector3 GetInversePerpendicularPoint(Vector3 vertices, Vector3 perpendicularPoint) {
        Vector3 directionBA = vertices - perpendicularPoint;
        return vertices + directionBA;
    }
}
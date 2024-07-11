using Assets.Packages.Bezier.Scripts.Models;
using Assets.Scripts.Games.Free.ID29.Bezier.Mono;
using UnityEngine;

public class ParabolaDrawer : MonoBehaviour {
    [SerializeField] private Transform[] points;
    public int resolution = 50;

    private void OnDrawGizmos() {
        BezierPointModel startPoint = new(points[0].transform.position, points[1].transform.position);
        BezierPointModel endPointPoint = new(points[2].transform.position, points[3].transform.position);
        BezierElementModel bezierElementModel = new(startPoint, endPointPoint);
        var asdf = new BezierElementModel[] { bezierElementModel };
        BezierResolutionSettings bezierResolutionSettings = new(resolution, new int[] { resolution });
        OffsetSettings offsetSettings = new(0, 0);
        BezierLineModel bezierLineModel = new(asdf, bezierResolutionSettings, offsetSettings);

        for (int i = 0; i < bezierLineModel.PointLength; i++) {
            Gizmos.DrawSphere(bezierLineModel.GetBezierPoint2DByIndex(i).VerticesPoint, 0.04f);
        }
    }
}
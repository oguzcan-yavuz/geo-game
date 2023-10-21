using UnityEngine;

public class LineSegment
{
    public Vector2 start;
    public Vector2 end;

    public LineSegment(Vector2 start, Vector2 end)
    {
        this.start = start;
        this.end = end;
    }

    public float distance()
    {
        var distance = Mathf.Sqrt(Mathf.Pow(this.start.x - this.end.x, 2) + Mathf.Pow(this.start.y - this.end.y, 2));
        Debug.Log("Distance: " + distance);

        return distance;
    }

    public bool isPointOnSegment(Vector2 point)
    {
        var distance = this.distance();
        var startToPoint = new LineSegment(start, point);
        var pointToEnd = new LineSegment(point, end);

        var isEqualDistance = distance == startToPoint.distance() + pointToEnd.distance();

        return isEqualDistance;
    }
}

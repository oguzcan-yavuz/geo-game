using UnityEngine;

public class LineSegment
{
	public Vector2 start;
	public Vector2 end;
	public float distance;

	public LineSegment(Vector2 start, Vector2 end)
	{
		this.start = start;
		this.end = end;
		this.distance = this.Distance();
	}

	public override bool Equals(object other)
	{
		if (other == null || !(other is LineSegment))
		{
			return false;
		}

		LineSegment otherLineSegment = other as LineSegment;

		return this.start == otherLineSegment.start && this.end == otherLineSegment.end;
	}

	public override int GetHashCode()
	{
		return $"{start},{end}".GetHashCode();
	}

	public override string ToString()
	{
		return $"({start}, {end})";
	}

	private float Distance()
	{
		var xDiff = this.start.x - this.end.x;
		var yDiff = this.start.y - this.end.y;
		var distance = Mathf.Sqrt(Mathf.Pow(xDiff, 2) + Mathf.Pow(yDiff, 2));

		return distance;
	}

	public bool IsPointOnSegment(Vector2 point)
	{
		var startToPoint = new LineSegment(start, point);
		var pointToEnd = new LineSegment(point, end);

		var isEqualDistance = this.distance == startToPoint.distance + pointToEnd.distance;

		return isEqualDistance;
	}
}

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

	public override bool Equals(object other)
	{
		if (other == null || !(other is LineSegment))
		{
			return false;
		}

		LineSegment otherLineSegment = other as LineSegment;

		return this.start == otherLineSegment.start && this.end == otherLineSegment.end;
	}

	public override string ToString()
	{
		return $"({start}, {end})";
	}

	public float Distance()
	{
		var distance = Mathf.Sqrt(Mathf.Pow(this.start.x - this.end.x, 2) + Mathf.Pow(this.start.y - this.end.y, 2));

		return distance;
	}

	public bool IsPointOnSegment(Vector2 point)
	{
		var distance = this.Distance();
		var startToPoint = new LineSegment(start, point);
		var pointToEnd = new LineSegment(point, end);

		var isEqualDistance = distance == startToPoint.Distance() + pointToEnd.Distance();

		return isEqualDistance;
	}
}

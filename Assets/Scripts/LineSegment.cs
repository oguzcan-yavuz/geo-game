using System;
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

	public static bool AlmostEqual(double double1, double double2, double precision)
	{
		return Math.Abs(double1 - double2) <= precision;
	}

	public bool IsPointOnSegment(Vector2 point)
	{
		var startToPoint = new LineSegment(start, point);
		var pointToEnd = new LineSegment(point, end);
		var totalDistance = startToPoint.distance + pointToEnd.distance;

		var isEqualDistance = AlmostEqual(this.distance, totalDistance, 0.00001);

		return isEqualDistance;
	}

	private float Lerp(float A, float B, float t)
	{
		return A + (B - A) * t;
	}

	public Vector2? FindIntersectionPoint(LineSegment lineSegment)
	{
		// https://www.youtube.com/watch?v=fHOLQJo0FjQ
		var tTop = (lineSegment.end.x - lineSegment.start.x) * (this.start.y - lineSegment.start.y) - (lineSegment.end.y - lineSegment.start.y) * (this.start.x - lineSegment.start.x);
		var uTop = (lineSegment.start.y - this.start.y) * (this.start.x - this.end.x) - (lineSegment.start.x - this.start.x) * (this.start.y - this.end.y);
		var bottom = (lineSegment.end.y - lineSegment.start.y) * (this.end.x - this.start.x) - (lineSegment.end.x - lineSegment.start.x) * (this.end.y - this.start.y);

		if (bottom != 0)
		{
			var t = tTop / bottom;
			var u = uTop / bottom;
			if (t >= 0 && t <= 1 && u >= 0 && u <= 1)
			{
				var x = this.Lerp(this.start.x, this.end.x, t);
				var y = this.Lerp(this.start.y, this.end.y, t);
				var result = new Vector2(x, y);

				return result;
			}
		}

		return null;
	}
}

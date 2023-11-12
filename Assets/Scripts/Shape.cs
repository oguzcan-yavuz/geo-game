using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Shape
{
	public List<LineSegment> lineSegments;
	public List<Vector2> corners;
	public List<Vector2> points;

	public Shape(List<LineSegment> lineSegments)
	{
		this.lineSegments = lineSegments;
		this.FindAllCorners();
		this.FindAllPoints();
	}

	public Shape(List<LineSegment> lineSegments, List<Vector2> corners, List<Vector2> points)
	{
		this.lineSegments = lineSegments;
		this.corners = corners;
		this.points = points;
	}

	public static Shape operator +(Shape a, Shape b)
	{
		var lineSegments = a.lineSegments.Concat(b.lineSegments).ToList();
		var corners = a.corners.Concat(b.corners).ToList();
		var points = a.points.Concat(b.points).ToList();

		return new Shape(lineSegments, corners, points);
	}


	public List<Vector2> FindAllCorners()
	{
		var corners = this.lineSegments
			.SelectMany(lineSegment => new List<Vector2> { lineSegment.start, lineSegment.end })
			.Distinct()
			.ToList();
		this.corners = corners;

		return corners;
	}

	// TODO: remove points
	public List<Vector2> FindAllPoints()
	{
		var points = new List<Vector2>(this.corners);
		points.Add(points[0]);
		this.points = points;

		return points;
	}
}

public class Square : Shape
{
	public Square(Vector2 center, float size) : base(GenerateEdges(center, size))
	{

	}

	private static List<LineSegment> GenerateEdges(Vector2 center, float size)
	{
		var edges = new List<LineSegment>
		{
			new LineSegment(new Vector2(center.x - (size / 2), center.y - (size / 2)), new Vector2(center.x + (size / 2), center.y - (size / 2))),
			new LineSegment(new Vector2(center.x + (size / 2), center.y - (size / 2)), new Vector2(center.x + (size / 2), center.y + (size / 2))),
			new LineSegment(new Vector2(center.x + (size / 2), center.y + (size / 2)), new Vector2(center.x - (size / 2), center.y + (size / 2))),
			new LineSegment(new Vector2(center.x - (size / 2), center.y + (size / 2)), new Vector2(center.x - (size / 2), center.y - (size / 2))),
		};

		return edges;
	}
}

public class Diamond : Shape
{
	public Diamond(Vector2 center, float size) : base(GenerateEdges(center, size))
	{

	}

	private static List<LineSegment> GenerateEdges(Vector2 center, float size)
	{
		var edges = new List<LineSegment>
		{
			new LineSegment(new Vector2(center.x - (size / 2), center.y), new Vector2(center.x, center.y - (size / 2))),
			new LineSegment(new Vector2(center.x, center.y - (size / 2)), new Vector2(center.x + (size / 2), center.y)),
			new LineSegment(new Vector2(center.x + (size / 2), center.y), new Vector2(center.x, center.y + (size / 2))),
			new LineSegment(new Vector2(center.x, center.y + (size / 2)), new Vector2(center.x - (size / 2), center.y)),
		};

		return edges;
	}
}

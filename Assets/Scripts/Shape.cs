using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Shape
{
	public List<LineSegment> lineSegments;
	public List<Vector2> corners;

	public Shape(List<LineSegment> lineSegments)
	{
		this.lineSegments = lineSegments;
		this.FindAllCorners();
	}

	public Shape(List<LineSegment> lineSegments, List<Vector2> corners)
	{
		this.lineSegments = lineSegments;
		this.corners = corners;
	}

	public static Shape operator +(Shape a, Shape b)
	{
		var lineSegments = a.lineSegments.Concat(b.lineSegments).ToList();
		var corners = a.corners.Concat(b.corners).ToList();

		return new Shape(lineSegments, corners);
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

	public bool AddLineSegment(LineSegment lineSegment)
	{
		// TODO: instead of calculating this every time, can we keep an available corner dictionary and update it with every insert?
		var exists = lineSegments.Any(s =>
			(s.start == lineSegment.start && s.end == lineSegment.end) ||
			(s.start == lineSegment.end && s.end == lineSegment.start));
		if (exists)
		{
			return false;
		}

		var sameStartAndEnd = lineSegment.start == lineSegment.end;
		if (sameStartAndEnd)
		{
			return false;
		}

		this.lineSegments.Add(lineSegment);
		this.FindAllCorners();

		return true;
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

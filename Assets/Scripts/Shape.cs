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

	public List<Vector2> FindAllCorners()
	{
		var corners = this.lineSegments
			.SelectMany(lineSegment => new List<Vector2> { lineSegment.start, lineSegment.end })
			.Distinct()
			.ToList();

		this.corners = corners;

		return corners;
	}
}

public class Square : Shape
{
	public Square(Vector2 center, int size) : base(GenerateEdges(center, size))
	{

	}

	private static List<LineSegment> GenerateEdges(Vector2 center, int size)
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
	public Diamond(Vector2 center, int size) : base(GenerateEdges(center, size))
	{

	}

	private static List<LineSegment> GenerateEdges(Vector2 center, int size)
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

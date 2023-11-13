using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Shape
{
	public List<LineSegment> lineSegments;
	public List<Vector2> dots;

	public Shape(List<LineSegment> lineSegments)
	{
		this.lineSegments = lineSegments;
		this.FindAllDots();
	}

	public Shape(List<LineSegment> lineSegments, List<Vector2> dots)
	{
		this.lineSegments = lineSegments;
		this.dots = dots;
	}

	public static Shape operator +(Shape a, Shape b)
	{
		var lineSegments = a.lineSegments.Concat(b.lineSegments).ToList();
		var dots = a.dots.Concat(b.dots).ToList();

		return new Shape(lineSegments, dots);
	}


	public List<Vector2> FindAllDots()
	{
		var dots = this.lineSegments
			.SelectMany(lineSegment => new List<Vector2> { lineSegment.start, lineSegment.end })
			.Distinct()
			.ToList();
		this.dots = dots;

		return dots;
	}

	public bool AddLineSegment(LineSegment lineSegment, out List<Vector2> intersectionPoints)
	{
		var exists = lineSegments.Any(ls => ls.IsPointOnSegment(lineSegment.start) && ls.IsPointOnSegment(lineSegment.end));
		if (exists)
		{
			intersectionPoints = null;
			return false;
		}

		intersectionPoints = lineSegments
			.Select(ls => ls.FindIntersectionPoint(lineSegment))
			.Where(point => point.HasValue && !this.dots.Contains((Vector2)point))
			.Distinct()
			.Select(point => point.Value)
			.ToList();

		this.lineSegments.Add(lineSegment);
		this.dots = this.dots.Concat(intersectionPoints).ToList();

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

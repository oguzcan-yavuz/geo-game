using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Map
{
	public Shape shape;

	public Map()
	{
		// Level 1: diamond inside square
		var square = new List<LineSegment>
		{
			new LineSegment(new Vector2(0, 0), new Vector2(0, 10)),
			new LineSegment(new Vector2(0, 10), new Vector2(10, 10)),
			new LineSegment(new Vector2(10, 10), new Vector2(10, 0)),
			new LineSegment(new Vector2(10, 0), new Vector2(0, 0))
		};

		var diamond = new List<LineSegment>
		{
			new LineSegment(new Vector2(0, 5), new Vector2(5, 10)),
			new LineSegment(new Vector2(5, 10), new Vector2(10, 5)),
			new LineSegment(new Vector2(10, 5), new Vector2(5, 0)),
			new LineSegment(new Vector2(5, 0), new Vector2(0, 0))
		};

		var diamondInsideSquare = square.Concat(diamond).ToList();

		var shape = new Shape(diamondInsideSquare);

		this.shape = shape;
	}
}

using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using UnityEngine;

public class ShapeTests
{
	[Test]
	public void ShouldInitializeShapeWithCorrectLength()
	{
		var square = new List<LineSegment>
		{
			new LineSegment(new Vector2(0, 0), new Vector2(0, 10)),
			new LineSegment(new Vector2(0, 10), new Vector2(10, 10)),
			new LineSegment(new Vector2(10, 10), new Vector2(10, 0)),
			new LineSegment(new Vector2(10, 0), new Vector2(0, 0))
		};

		var shape = new Shape(square);
		var length = shape.lineSegments.Count;

		Assert.AreEqual(4, length);
	}

	[Test]
	public void ShouldInitializeSquareWithCorrectEdges()
	{
		var expectedLineSegments = new List<LineSegment>
		{
			new LineSegment(new Vector2(0, 0), new Vector2(10, 0)),
			new LineSegment(new Vector2(10, 0), new Vector2(10, 10)),
			new LineSegment(new Vector2(10, 10), new Vector2(0, 10)),
			new LineSegment(new Vector2(0, 10), new Vector2(0, 0))
		};

		var square = new Square(new Vector2(5, 5), 10);

		CollectionAssert.AreEqual(expectedLineSegments, square.lineSegments);
	}

	[Test]
	public void ShouldInitializeDiamondWithCorrectEdges()
	{
		var expectedLineSegments = new List<LineSegment>
		{
			new LineSegment(new Vector2(0, 5), new Vector2(5, 0)),
			new LineSegment(new Vector2(5, 0), new Vector2(10, 5)),
			new LineSegment(new Vector2(10, 5), new Vector2(5, 10)),
			new LineSegment(new Vector2(5, 10), new Vector2(0, 5))
		};

		var diamond = new Diamond(new Vector2(5, 5), 10);

		CollectionAssert.AreEqual(expectedLineSegments, diamond.lineSegments);
	}

	[Test]
	public void ShouldFindAllCorners()
	{
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
		var expectedCorners = new List<Vector2>
		{
			new Vector2(0, 0),
			new Vector2(0, 10),
			new Vector2(10, 10),
			new Vector2(10, 0),
			new Vector2(0, 5),
			new Vector2(5, 10),
			new Vector2(10, 5),
			new Vector2(5, 0)
		};

		var shape = new Shape(diamondInsideSquare);

		var corners = shape.FindAllCorners();

		CollectionAssert.AreEqual(expectedCorners, corners);
	}
}

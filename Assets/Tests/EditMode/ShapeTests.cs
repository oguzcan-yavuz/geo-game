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
		var lineSegmentsLength = shape.lineSegments.Count;
		var cornerLength = shape.corners.Count;

		Assert.AreEqual(4, lineSegmentsLength);
		Assert.AreEqual(4, cornerLength);
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
		var square = new Square(new Vector2(5, 5), 10);
		var diamond = new Diamond(new Vector2(5, 5), 10);
		var diamondInsideSquare = square.lineSegments.Concat(diamond.lineSegments).ToList();
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

		CollectionAssert.AreEquivalent(expectedCorners, corners);
	}
}

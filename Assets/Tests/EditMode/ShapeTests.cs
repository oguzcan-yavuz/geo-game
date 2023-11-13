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
		var dotLength = shape.dots.Count;

		Assert.AreEqual(4, lineSegmentsLength);
		Assert.AreEqual(4, dotLength);
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
	public void ShouldFindAllDots()
	{
		var square = new Square(new Vector2(5, 5), 10);
		var diamond = new Diamond(new Vector2(5, 5), 10);
		var diamondInsideSquare = square.lineSegments.Concat(diamond.lineSegments).ToList();
		var expectedDots = new List<Vector2>
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

		var dots = shape.FindAllDots();

		CollectionAssert.AreEquivalent(expectedDots, dots);
	}

	[Test]
	public void ShouldOverloadWithCustomDots()
	{
		var square = new List<LineSegment>
		{
			new LineSegment(new Vector2(0, 0), new Vector2(0, 10)),
			new LineSegment(new Vector2(0, 10), new Vector2(10, 10)),
			new LineSegment(new Vector2(10, 10), new Vector2(10, 0)),
			new LineSegment(new Vector2(10, 0), new Vector2(0, 0))
		};
		var dots = new List<Vector2>
		{
			new Vector2(0, 0),
			new Vector2(0, 10),
			new Vector2(10, 10),
			new Vector2(10, 0),
		};

		var shape = new Shape(square, dots);

		CollectionAssert.AreEqual(shape.lineSegments, square);
		CollectionAssert.AreEqual(shape.dots, dots);
	}

	[Test]
	public void ShouldAddTwoShapesCorrectlyWithPlusOperator()
	{
		var square = new Square(new Vector2(5, 5), 10);
		var diamond = new Diamond(new Vector2(5, 5), 10);
		var expectedLineSegments = new List<LineSegment>
		{
			new LineSegment(new Vector2(0, 0), new Vector2(10, 0)),
			new LineSegment(new Vector2(10, 0), new Vector2(10, 10)),
			new LineSegment(new Vector2(10, 10), new Vector2(0, 10)),
			new LineSegment(new Vector2(0, 10), new Vector2(0, 0)),
			new LineSegment(new Vector2(0, 5), new Vector2(5, 0)),
			new LineSegment(new Vector2(5, 0), new Vector2(10, 5)),
			new LineSegment(new Vector2(10, 5), new Vector2(5, 10)),
			new LineSegment(new Vector2(5, 10), new Vector2(0, 5))
		};
		var expectedDots = new List<Vector2>
		{
			new Vector2(0, 0),
			new Vector2(10, 0),
			new Vector2(10, 10),
			new Vector2(0, 10),
			new Vector2(0, 5),
			new Vector2(5, 0),
			new Vector2(10, 5),
			new Vector2(5, 10)
		};

		var shape = square + diamond;

		CollectionAssert.AreEqual(expectedLineSegments, shape.lineSegments);
		CollectionAssert.AreEqual(expectedDots, shape.dots);
	}

	[Test]
	public void ShouldNotAddALineSegmentIfTheSameExists()
	{
		var square = new List<LineSegment>
		{
			new LineSegment(new Vector2(0, 0), new Vector2(0, 10)),
			new LineSegment(new Vector2(0, 10), new Vector2(10, 10)),
			new LineSegment(new Vector2(10, 10), new Vector2(10, 0)),
			new LineSegment(new Vector2(10, 0), new Vector2(0, 0))
		};
		var newLineSegment = new LineSegment(new Vector2(10, 0), new Vector2(0, 0));
		List<Vector2> intersectionPoints;

		var shape = new Shape(square);
		var added = shape.AddLineSegment(newLineSegment, out intersectionPoints);

		Assert.IsFalse(added);
		Assert.IsNull(intersectionPoints);
	}

	[Test]
	public void ShouldNotAddALineSegmentIfTheSameWithReverseDirectionExists()
	{
		var square = new List<LineSegment>
		{
			new LineSegment(new Vector2(0, 0), new Vector2(0, 10)),
			new LineSegment(new Vector2(0, 10), new Vector2(10, 10)),
			new LineSegment(new Vector2(10, 10), new Vector2(10, 0)),
			new LineSegment(new Vector2(10, 0), new Vector2(0, 0))
		};
		var newLineSegment = new LineSegment(new Vector2(0, 0), new Vector2(10, 0));
		List<Vector2> intersectionPoints;

		var shape = new Shape(square);
		var added = shape.AddLineSegment(newLineSegment, out intersectionPoints);

		Assert.IsFalse(added);
		Assert.IsNull(intersectionPoints);
	}

	[Test]
	public void ShouldNotAddALineSegmentIfTheStartAndEndPointsAreTheSame()
	{
		var square = new List<LineSegment>
		{
			new LineSegment(new Vector2(0, 0), new Vector2(0, 10)),
			new LineSegment(new Vector2(0, 10), new Vector2(10, 10)),
			new LineSegment(new Vector2(10, 10), new Vector2(10, 0)),
			new LineSegment(new Vector2(10, 0), new Vector2(0, 0))
		};
		var newLineSegment = new LineSegment(new Vector2(0, 0), new Vector2(0, 0));
		List<Vector2> intersectionPoints;

		var shape = new Shape(square);
		var added = shape.AddLineSegment(newLineSegment, out intersectionPoints);

		Assert.IsFalse(added);
		Assert.IsNull(intersectionPoints);
	}

	[Test]
	public void ShouldNotAddALineSegmentIfItIsASmallerLineSegmentOfAnExisting()
	{
		var diamondInsideSquare = new List<LineSegment>
		{
			new LineSegment(new Vector2(0, 0), new Vector2(10, 0)),
			new LineSegment(new Vector2(10, 0), new Vector2(10, 10)),
			new LineSegment(new Vector2(10, 10), new Vector2(0, 10)),
			new LineSegment(new Vector2(0, 10), new Vector2(0, 0)),
			new LineSegment(new Vector2(0, 5), new Vector2(5, 0)),
			new LineSegment(new Vector2(5, 0), new Vector2(10, 5)),
			new LineSegment(new Vector2(10, 5), new Vector2(5, 10)),
			new LineSegment(new Vector2(5, 10), new Vector2(0, 5))
		};
		var shape = new Shape(diamondInsideSquare);
		var newLineSegment = new LineSegment(new Vector2(0, 0), new Vector2(0, 5));
		List<Vector2> intersectionPoints;

		var added = shape.AddLineSegment(newLineSegment, out intersectionPoints);

		Assert.IsFalse(added);
		Assert.IsNull(intersectionPoints);
	}

	[Test]
	public void ShouldNotAddALineSegmentIfItIsAReverseAndSmallerLineSegmentOfAnExisting()
	{
		var diamondInsideSquare = new List<LineSegment>
		{
			new LineSegment(new Vector2(0, 0), new Vector2(10, 0)),
			new LineSegment(new Vector2(10, 0), new Vector2(10, 10)),
			new LineSegment(new Vector2(10, 10), new Vector2(0, 10)),
			new LineSegment(new Vector2(0, 10), new Vector2(0, 0)),
			new LineSegment(new Vector2(0, 5), new Vector2(5, 0)),
			new LineSegment(new Vector2(5, 0), new Vector2(10, 5)),
			new LineSegment(new Vector2(10, 5), new Vector2(5, 10)),
			new LineSegment(new Vector2(5, 10), new Vector2(0, 5))
		};
		var shape = new Shape(diamondInsideSquare);
		var newLineSegment = new LineSegment(new Vector2(0, 5), new Vector2(0, 0));
		List<Vector2> intersectionPoints;

		var added = shape.AddLineSegment(newLineSegment, out intersectionPoints);

		Assert.IsFalse(added);
		Assert.IsNull(intersectionPoints);
	}

	[Test]
	public void ShouldNotAddALineSegmentIfItIsAdSmallerLineSegmentWithIrregularAngle()
	{
		var diamondInsideSquare = new List<LineSegment>
		{
			new LineSegment(new Vector2(0, 0), new Vector2(10, 0)),
			new LineSegment(new Vector2(10, 0), new Vector2(10, 10)),
			new LineSegment(new Vector2(10, 10), new Vector2(0, 10)),
			new LineSegment(new Vector2(0, 10), new Vector2(0, 0)),
			new LineSegment(new Vector2(0, 5), new Vector2(5, 0)),
			new LineSegment(new Vector2(5, 0), new Vector2(10, 5)),
			new LineSegment(new Vector2(10, 5), new Vector2(5, 10)),
			new LineSegment(new Vector2(5, 10), new Vector2(0, 5)),
			new LineSegment(new Vector2(0, 0), new Vector2(10, 10)) // this should prevent the new line segment
		};
		var shape = new Shape(diamondInsideSquare);
		var newLineSegment = new LineSegment(new Vector2(0, 0), new Vector2(5, 5));
		List<Vector2> intersectionPoints;

		var added = shape.AddLineSegment(newLineSegment, out intersectionPoints);

		Assert.IsFalse(added);
		Assert.IsNull(intersectionPoints);
	}

	[Test]
	public void ShouldAddLineSegment()
	{
		var square = new List<LineSegment>
		{
			new LineSegment(new Vector2(0, 0), new Vector2(0, 10)),
			new LineSegment(new Vector2(0, 10), new Vector2(10, 10)),
			new LineSegment(new Vector2(10, 10), new Vector2(10, 0)),
			new LineSegment(new Vector2(10, 0), new Vector2(0, 0))
		};
		var newLineSegment = new LineSegment(new Vector2(0, 0), new Vector2(10, 10));
		var expectedLineSegments = new List<LineSegment>
		{
			new LineSegment(new Vector2(0, 0), new Vector2(0, 10)),
			new LineSegment(new Vector2(0, 10), new Vector2(10, 10)),
			new LineSegment(new Vector2(10, 10), new Vector2(10, 0)),
			new LineSegment(new Vector2(10, 0), new Vector2(0, 0)),
			new LineSegment(new Vector2(0, 0), new Vector2(10, 10))
		};
		List<Vector2> intersectionPoints;

		var shape = new Shape(square);
		var added = shape.AddLineSegment(newLineSegment, out intersectionPoints);

		Assert.IsTrue(added);
		CollectionAssert.AreEqual(expectedLineSegments, shape.lineSegments);
		Assert.NotNull(intersectionPoints);
	}
}

using NUnit.Framework;
using UnityEngine;

public class LineSegmentTests
{
	[Test]
	public void ShouldInitializeLineSegment()
	{
		var start = new Vector2(0, 0);
		var end = new Vector2(0, 1);

		var lineSegment = new LineSegment(start, end);

		Assert.AreEqual(start, lineSegment.start);
		Assert.AreEqual(end, lineSegment.end);
	}
	[Test]
	public void ShouldCalculateDistance()
	{
		var start = new Vector2(0, 0);
		var end = new Vector2(3, 4);
		var expectedDistance = 5;

		var lineSegment = new LineSegment(start, end);

		Assert.AreEqual(expectedDistance, lineSegment.distance);
	}

	[Test]
	public void ShouldNotContainPoint()
	{
		var lineSegment = new LineSegment(new Vector2(0, 0), new Vector2(0, 10));
		var point = new Vector2(1, 5);

		var containingPoint = lineSegment.IsPointOnSegment(point);

		Assert.IsFalse(containingPoint);
	}

	[Test]
	public void ShouldContainPoint()
	{
		var lineSegment = new LineSegment(new Vector2(0, 0), new Vector2(0, 10));
		var point = new Vector2(0, 5);

		var containingPoint = lineSegment.IsPointOnSegment(point);

		Assert.IsTrue(containingPoint);
	}

	[Test]
	public void ShouldNotContainPointInSegmentWithSlope()
	{
		var lineSegment = new LineSegment(new Vector2(1, 1), new Vector2(5, 3));
		var point = new Vector2(1, 2);

		var containingPoint = lineSegment.IsPointOnSegment(point);

		Assert.IsFalse(containingPoint);
	}

	[Test]
	public void ShouldContainPointInSegmentWithSlope()
	{
		var lineSegment = new LineSegment(new Vector2(1, 1), new Vector2(5, 3));
		var point = new Vector2(3, 2);

		var containingPoint = lineSegment.IsPointOnSegment(point);

		Assert.IsTrue(containingPoint);
	}
}

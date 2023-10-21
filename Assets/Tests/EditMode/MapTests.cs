using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class MapTests
{
	[Test]
	public void ShouldInitializeMapWithCorrectLength()
	{
		var map = new Map();

		var length = map.shape.lineSegments.Count;

		Assert.AreEqual(8, length);
	}
}

using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class MapTests
{
    [Test]
    public void ShouldInitializeMapWithCorrectLength()
    {
        var map = new Map();

        var length = map.lineSegments.Count;

        Assert.AreEqual(8, length);
    }

    [Test]
    public void ShouldFindAllCorners()
    {
        var map = new Map();
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

        var corners = map.findAllCorners();

        Assert.AreEqual(expectedCorners, corners);
    }
}

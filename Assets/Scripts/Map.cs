using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Map
{
    public List<LineSegment> lineSegments;
    public List<Vector2> corners;

    public Map()
    {
        // TODO: create a shape class that takes a list of line segments in the constructor. move the shape logic into it
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

        // Level 1: diamond inside square
        var diamondInsideSquare = square.Concat(diamond).ToList();

        this.lineSegments = diamondInsideSquare;
    }

    public List<Vector2> findAllCorners()
    {
        var corners = this.lineSegments
            .SelectMany(lineSegment => new List<Vector2> { lineSegment.start, lineSegment.end })
            .Distinct()
            .ToList();

        this.corners = corners;

        return corners;
    }
}

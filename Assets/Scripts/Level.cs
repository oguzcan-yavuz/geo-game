using UnityEngine;
using System.Linq;

public class Level
{
	public Shape shape;

	public Level()
	{
		// Level 1: diamond inside square
		var square = new Square(new Vector2(5, 5), 10);
		var diamond = new Diamond(new Vector2(5, 5), 10);

		var diamondInsideSquare = square.lineSegments.Concat(diamond.lineSegments).ToList();

		var shape = new Shape(diamondInsideSquare);

		this.shape = shape;
	}
}

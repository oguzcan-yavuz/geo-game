using UnityEngine;

public class Level
{
	public Shape shape;

	public Level()
	{
		// Level 1: diamond inside square
		var square = new Square(new Vector2(0, 0), 8);
		var diamond = new Diamond(new Vector2(0, 0), 8);

		var shape = square + diamond;

		this.shape = shape;
	}
}

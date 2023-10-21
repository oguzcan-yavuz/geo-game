using NUnit.Framework;

public class LevelTests
{
	[Test]
	public void ShouldInitializeLevelWithCorrectLength()
	{
		var level = new Level();

		var length = level.shape.lineSegments.Count;

		Assert.AreEqual(8, length);
	}
}

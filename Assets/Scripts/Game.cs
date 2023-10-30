using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
	private Level level;
	public GameObject linePrefab;


	void Start()
	{
		this.level = new Level();
		List<LineSegment> lineSegments = level.shape.lineSegments;

		for (var i = 0; i < lineSegments.Count; i++)
		{
			GameObject line = Instantiate(linePrefab, new Vector3(0, 0, 0), Quaternion.identity);
			LineRenderer lineRenderer = line.GetComponent<LineRenderer>();
			lineRenderer.SetPosition(0, new Vector3(lineSegments[i].start.x, lineSegments[i].start.y, 0));
			lineRenderer.SetPosition(1, new Vector3(lineSegments[i].end.x, lineSegments[i].end.y, 0));
		}
	}
}

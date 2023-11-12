using System;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
	private Level level;

	[Header("Line")]
	[SerializeField] private GameObject linePrefab;

	[Header("Dot")]
	[SerializeField] private Transform dotCanvasPosition;
	[SerializeField] private GameObject dotPrefab;

	public Shape GameShape { get; private set; }

	private void Start()
	{
		this.level = new Level();
		this.GameShape = this.level.shape;
		this.InitLines(level.shape.lineSegments);
		this.InitDots(level.shape.corners);
	}

	private void InitLines(List<LineSegment> lineSegments)
	{
		for (var i = 0; i < lineSegments.Count; i++)
		{
			GameObject line = Instantiate(linePrefab, new Vector3(0, 0, 0), Quaternion.identity);
			LineRenderer lineRenderer = line.GetComponent<LineRenderer>();
			lineRenderer.SetPosition(0, new Vector3(lineSegments[i].start.x, lineSegments[i].start.y, 0));
			lineRenderer.SetPosition(1, new Vector3(lineSegments[i].end.x, lineSegments[i].end.y, 0));
		}
	}

	private void InitDots(List<Vector2> corners)
	{
		for (var i = 0; i < corners.Count; i++)
		{
			Instantiate(dotPrefab, new Vector3(corners[i].x, corners[i].y, 0), Quaternion.identity, dotCanvasPosition);
		}
	}
}

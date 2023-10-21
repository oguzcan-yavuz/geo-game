using UnityEngine;

public class Game : MonoBehaviour
{
	private Level level;

	// Start is called before the first frame update
	void Start()
	{
		this.level = new Level();
	}

	// Update is called once per frame
	void Update()
	{
		// TODO: this is getting the LineRenderer globally, under the assumption of there is only one of them. 
		// Instead, we can first find the GameObject by tag, then we can retrieve the LineRenderer component attached to it.
		LineRenderer lineRenderer = GetComponent<LineRenderer>();
		lineRenderer.positionCount = level.shape.lineSegments.Count * 2;

		for (int i = 0; i < level.shape.lineSegments.Count; i++)
		{
			var lineSegment = level.shape.lineSegments[i];
			lineRenderer.SetPosition(i * 2, new Vector3(lineSegment.start.x, lineSegment.start.y, 0));
			lineRenderer.SetPosition(i * 2 + 1, new Vector3(lineSegment.end.x, lineSegment.end.y, 0));
		}
	}
}

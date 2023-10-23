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
		LineRenderer lineRenderer = GetComponent<LineRenderer>();
		lineRenderer.positionCount = level.shape.points.Count;
		lineRenderer.widthMultiplier = 0.2f;

		for (int i = 0; i < level.shape.points.Count; i++)
		{
			var point = level.shape.points[i];
			lineRenderer.SetPosition(i, new Vector3(point.x, point.y, 0));
		}
	}
}

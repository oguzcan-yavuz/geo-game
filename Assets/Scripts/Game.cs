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
		lineRenderer.positionCount = level.shape.corners.Count;
		lineRenderer.widthMultiplier = 0.2f;
		lineRenderer.loop = true;

		for (int i = 0; i < level.shape.corners.Count; i++)
		{
			var corner = level.shape.corners[i];
			lineRenderer.SetPosition(i, new Vector3(corner.x, corner.y, 0));
		}
	}
}

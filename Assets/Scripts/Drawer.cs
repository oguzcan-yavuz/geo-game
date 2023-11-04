using UnityEngine;

public class Drawer : MonoBehaviour
{
	private LineRenderer lineRenderer;
	private bool? draw;
	private Vector3? start;
	private Vector3? end;
	public GameObject linePrefab;
	private Vector3 updatedCurrentPosition;

	void Start()
	{
		this.lineRenderer = GetComponent<LineRenderer>();
		this.lineRenderer.positionCount = 2;
		this.lineRenderer.startWidth = 0.10f;
		this.lineRenderer.endWidth = 0.10f;
	}

	void Update()
	{
		this.updatedCurrentPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		this.updatedCurrentPosition.z = 0f;

		// TODO: refactor lol :D
		if (Input.GetMouseButtonDown(0))
		{
			if (draw == true)
			{
				draw = false;
			}
			else if (draw == false)
			{
				draw = true;
			}
			else
			{
				draw = true;
			}
		}

		if (draw == true)
		{
			StartDrawing();
		}
		else if (draw == false)
		{
			StopDrawing();
		}
	}

	private void StartDrawing()
	{
		if (this.end != null)
		{
			this.start = this.end;
		}
		else
		{
			this.start = transform.position;
			// TODO: this doesn't work because the start and the end is the same since they are both called inside the Update
			// this.start = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		}
		this.lineRenderer.SetPosition(0, (Vector3)this.start);
		this.lineRenderer.SetPosition(1, this.updatedCurrentPosition);
	}

	private void StopDrawing()
	{
		this.end = updatedCurrentPosition;

		GameObject line = Instantiate(linePrefab, new Vector3(0, 0, 0), Quaternion.identity);
		LineRenderer lineRenderer = line.GetComponent<LineRenderer>();
		lineRenderer.SetPosition(0, (Vector3)this.start);
		lineRenderer.SetPosition(1, (Vector3)this.end);
		draw = true;
	}
}

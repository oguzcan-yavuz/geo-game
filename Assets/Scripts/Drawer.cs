using System.Collections.Generic;
using UnityEngine;

public class Drawer : MonoBehaviour
{
	private LineRenderer lineRenderer;
	private Vector3? start;
	private Vector3? end;
	private Vector3 updatedCurrentPosition;

	[Header("Line")]
	[SerializeField]
	private GameObject linePrefab;

	[Header("Event Managers")]
	[SerializeField]
	private DotClickEventManager dotClickEventManager;

	[SerializeField]
	private DotCreateEventManager dotCreateEventManager;

	[Header("Dot")]
	[SerializeField]
	private Transform dotCanvasPosition;
	[SerializeField]
	private GameObject dotPrefab;

	private void Start()
	{
		this.lineRenderer = GetComponent<LineRenderer>();
		this.lineRenderer.positionCount = 2;
		this.lineRenderer.startWidth = 0.10f;
		this.lineRenderer.endWidth = 0.10f;

		dotClickEventManager.dotClickedEvent.AddListener(HandleDotClicked);
		dotCreateEventManager.dotCreatedEvent.AddListener(HandleDotCreated);
	}

	private void HandleDotClicked(Vector3 dotPosition)
	{
		if (start == null)
		{
			start = dotPosition;
			return;
		}
		this.end = dotPosition;

		Shape shape = FindObjectOfType<Game>().GameShape;
		List<Vector2> intersectionPoints;

		var valid = shape.AddLineSegment(new LineSegment((Vector2)this.start, (Vector2)this.end), out intersectionPoints);

		if (!valid)
		{
			return;
		}

		intersectionPoints.ForEach(intersectionPoint => dotCreateEventManager.PublishDotCreatedEvent(intersectionPoint));

		AddLine();
		this.start = dotPosition;
	}

	private void HandleDotCreated(Vector3 dotPosition)
	{
		Instantiate(dotPrefab, dotPosition, Quaternion.identity, dotCanvasPosition);
	}

	private void Update()
	{
		this.updatedCurrentPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		this.updatedCurrentPosition.z = 0f;

		if (start == null)
		{
			return;
		}

		DrawCurrentLine();
	}

	private void DrawCurrentLine()
	{
		this.lineRenderer.SetPosition(0, (Vector3)this.start);
		this.lineRenderer.SetPosition(1, this.updatedCurrentPosition);
	}

	private void AddLine()
	{

		GameObject line = Instantiate(linePrefab, new Vector3(0, 0, 0), Quaternion.identity);
		LineRenderer lineRenderer = line.GetComponent<LineRenderer>();
		lineRenderer.SetPosition(0, (Vector3)this.start);
		lineRenderer.SetPosition(1, (Vector3)this.end);
	}
}

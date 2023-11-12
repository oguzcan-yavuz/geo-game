using UnityEngine;

public class Drawer : MonoBehaviour
{
	private LineRenderer lineRenderer;
	private Vector3? start;
	private Vector3? end;
	public GameObject linePrefab;
	private Vector3 updatedCurrentPosition;

	[SerializeField]
	private DotClickEventManager dotClickEventManager;

	private void Start()
	{
		this.lineRenderer = GetComponent<LineRenderer>();
		this.lineRenderer.positionCount = 2;
		this.lineRenderer.startWidth = 0.10f;
		this.lineRenderer.endWidth = 0.10f;

		dotClickEventManager.dotClickedEvent.AddListener(HandleDotClicked);
	}

	private void HandleDotClicked(Vector3 dotPosition)
	{
		if (start == null)
		{
			start = dotPosition;
			return;
		}

		this.end = dotPosition;
		AddLine();
		this.start = dotPosition;
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

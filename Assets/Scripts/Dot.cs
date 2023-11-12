using UnityEngine;

public class Dot : MonoBehaviour
{
	[SerializeField]
	private DotClickEventManager dotClickEventManager;

	private void Update()
	{
		if (!Input.GetMouseButtonDown(0))
		{
			return;
		}

		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;

		var hitting = Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Dots"));
		if (!hitting)
		{
			return;
		}

		var hittingToDot = hit.collider.gameObject == gameObject;

		if (hittingToDot)
		{
			Vector3 dotPosition = hit.collider.transform.position;
			dotClickEventManager.PublishDotClickedEvent(dotPosition);
		}
	}
}

using UnityEngine;

public class Dot : MonoBehaviour
{
	private void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;

			if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Dots")))
			{
				if (hit.collider.gameObject == gameObject)
				{
					Vector3 dotPosition = hit.collider.transform.position;
					Debug.Log("dot position" + dotPosition);
				}
			}
		}
	}
}

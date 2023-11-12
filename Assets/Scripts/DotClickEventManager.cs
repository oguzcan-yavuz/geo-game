using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class DotClickedEvent : UnityEvent<Vector3>
{

}

public class DotClickEventManager : MonoBehaviour
{
	public DotClickedEvent dotClickedEvent = new DotClickedEvent();

	public void PublishDotClickedEvent(Vector3 dotPosition)
	{
		dotClickedEvent.Invoke(dotPosition);
	}
}

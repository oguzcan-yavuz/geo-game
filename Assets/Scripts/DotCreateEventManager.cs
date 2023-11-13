using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class DotCreatedEvent : UnityEvent<Vector3>
{

}

public class DotCreateEventManager : MonoBehaviour
{
	public DotCreatedEvent dotCreatedEvent = new DotCreatedEvent();

	public void PublishDotCreatedEvent(Vector3 dotPosition)
	{
		dotCreatedEvent.Invoke(dotPosition);
	}
}

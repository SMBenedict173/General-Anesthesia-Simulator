using UnityEngine;

public class Hose : MonoBehaviour
{
	private bool isConnectedToSource;
	private bool isConnectedToDestination;
	public bool CanHosePassO2;
	[SerializeField]
	private HoseConnection source;
	[SerializeField]
	private HoseConnection destination;
	[SerializeField]
	private HoseEnd endOne;
	[SerializeField]
	private HoseEnd endTwo;

	void Start() 
	{
		isConnectedToDestination = false; //Maybe true... we can decide later whether the hose starts connected to something
		isConnectedToSource = false;
		CanHosePassO2 = false;
	}
	void Update()
	{
		isConnectedToSource = (endOne.connectedTo == source ^ endTwo.connectedTo == source) ? true : false;
		isConnectedToDestination = (endTwo.connectedTo == destination ^ endOne.connectedTo == destination) ? true : false;
		CanHosePassO2 = (isConnectedToSource && isConnectedToDestination && endOne.ConnectedTo != endTwo.ConnectedTo) ? true : false;
	}
}
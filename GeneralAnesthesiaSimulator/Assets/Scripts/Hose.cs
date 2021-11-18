using UnityEngine;

public class Hose : MonoBehaviour
{
    private bool isConnectedToSource;
    [SerializeField]
    private HoseConnection source;
    [SerializeField]
    private HoseConnection destination;
    [SerializeField]
    private HoseEnd interactableEnd;
    

    void Start()
    {
        
        isConnectedToSource = false;
    }
    
    void Update()
    {
        isConnectedToSource = (interactableEnd.connectedTo == source ) ? true : false;
    }

    public bool GetConnectionStatus()
    {
        return this.isConnectedToSource;
    }
}
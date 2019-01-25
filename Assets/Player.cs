using UnityEngine.Networking;
using UnityEngine;

public class Player : NetworkBehaviour
{
    [SerializeField] private bool _isPlayerOne ;
    private bool _isConnectionSet;
    private short movementmsg = 101;

    public class CustomMessage : MessageBase
    {
        //public GameObject gobj;
    }

    private void Awake()
    {
        Application.runInBackground = true;
        NetworkServer.RegisterHandler(movementmsg, OnServerReceivedMovementMessage);
    }
    void Update()
    {   
        if( isServer )
        {   
            if( !_isPlayerOne )
            {
                return;
            }
            
            if (Input.GetMouseButton(0))
            {
                Movement(gameObject);
            }
        }
        else if( !isServer )
        {
            if (_isPlayerOne)
            {
                return;
            }
            if (Input.GetMouseButton(0))
            {
                CustomMessage msg = new CustomMessage();
                //msg.gobj = gameObject;
                SendMovementCall(msg);
            }
        }
    }
    
    void Movement(GameObject obj = null)
    {   
        if( obj == null )
        {
            obj = GameManager.Instance.Players[1];
        }
        Vector3 tmp = obj.transform.position;
        tmp.y++;
        obj.transform.position = tmp;
    }
    void SendMovementCall(CustomMessage msg)
    {
        NetworkClient client = NetworkManager.singleton.client;
        client.Send(movementmsg, msg);
    }
    void OnServerReceivedMovementMessage(NetworkMessage msg)
    {
        //CustomMessage m = msg.ReadMessage<CustomMessage>();
        Movement();
    }
}

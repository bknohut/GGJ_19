using UnityEngine.Networking;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Player : NetworkBehaviour
{
    [SerializeField] private bool _isPlayerOne ;
    private bool _isConnectionSet;
    private short movementmsg = 101;
    NetworkClient client;
    public Text txt;
    
    public CharacterAnimator anim;

    public class CustomMessage : MessageBase
    {
        public float x;
        public float y;
    }
    private void Awake()
    {
        NetworkServer.RegisterHandler(movementmsg, OnServerReceivedMovementMessage);
    }
    private void Start()
    {
        UIManager.EnrollJoystick(GetMovementDirection);
        //anim = transform.GetComponent<CharacterAnimator>();
    }

    void GetMovementDirection(Vector2 dir)
    {
        if (isServer)
        {
            if (!_isPlayerOne)
            {
                return;
            }

            ServerMovement(dir, gameObject);
        }
        else
        {
            if (_isPlayerOne)
            {
                return;
            }
            ClientMovement(dir);
        }
    }
    private void Move(Vector2 dir, GameObject obj)
    {
        dir *= 10f;
        Vector3 tmp = obj.transform.position;
        tmp.x += dir.x;
        tmp.y += dir.y;
        obj.transform.position = tmp;
    }
    void Movement(Vector2 dir)
    {
        GameObject obj = GameManager.Instance.Players[1];
        ChooseFaceDirection(dir);
        obj.GetComponent<CharacterAnimator>().RunAnimation();
        Move(dir, obj);
    }
    void ServerMovement(Vector2 dir, GameObject obj)
    {
        ChooseFaceDirection(dir);
        obj.GetComponent<CharacterAnimator>().RunAnimation();
        Move(dir, obj);
    }
    void ClientMovement(Vector2 dir)
    {
        CustomMessage msg = new CustomMessage();
        msg.x = dir.x;
        msg.y = dir.y;
        ChooseFaceDirection(dir);
        SendMovementCall(msg);
    }

    void SendMovementCall(CustomMessage msg)
    {   
        if(NetworkManager.singleton.client != null)
        {
            client = NetworkManager.singleton.client;
            client.Send(movementmsg, msg);
        }

    }
    void OnServerReceivedMovementMessage(NetworkMessage msg)
    {
        CustomMessage m = msg.ReadMessage<CustomMessage>();
        Vector2 tmp = new Vector2(m.x, m.y);
        Movement(tmp);
    }
    void ChooseFaceDirection(Vector2 dir)
    {
        if (dir.x < 0 && dir.y < 0)
        {
            anim.Turn(CharacterAnimator.LookPosition.DOWN);
        }
        else if (dir.x < 0 && dir.y >= 0)
        {
            anim.Turn(CharacterAnimator.LookPosition.LEFT);
        }
        else if (dir.x >= 0 && dir.y < 0)
        {
            anim.Turn(CharacterAnimator.LookPosition.RIGHT);
        }
        else
        {
            anim.Turn(CharacterAnimator.LookPosition.UP);
        }
    }
}

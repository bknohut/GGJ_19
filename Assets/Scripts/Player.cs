﻿using UnityEngine.Networking;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Player : NetworkBehaviour
{
    [SerializeField] private bool _isPlayerOne ;
    private bool _isConnectionSet;
    private short movementmsg = 101;
    NetworkClient client;

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
    }

    void GetMovementDirection(Vector2 dir)
    {
        if (isServer)
        {
            if (!_isPlayerOne)
            {
                return;
            }

            Movement(dir, gameObject);
        }
        else
        {
            if (_isPlayerOne)
            {
                return;
            }

            CustomMessage msg = new CustomMessage();
            msg.x = dir.x;
            msg.y = dir.y;
            SendMovementCall(msg);
        }
    }
    void Movement(Vector2 dir, GameObject obj = null)
    {
        dir *= 10f;
        if( obj == null )
        {
            obj = GameManager.Instance.Players[1];
        }
        Vector3 tmp = obj.transform.position;
        tmp.x += dir.x;
        tmp.y += dir.y;
        obj.transform.position = tmp;
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
}
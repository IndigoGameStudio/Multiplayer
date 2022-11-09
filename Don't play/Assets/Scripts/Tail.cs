using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

[RequireComponent(typeof(LineRenderer))]
[RequireComponent(typeof(EdgeCollider2D))]
public class Tail : MonoBehaviour
{
    [SerializeField] float pointSpacing = 0.1f;
    [SerializeField] Transform Head;
    List<Vector2> points;
    [SerializeField] List<Gradient> _colorPlayerID;
    [SerializeField] PhotonView photonView;

    [SerializeField] LineRenderer line;
    EdgeCollider2D col;

    private void Awake()
    {
        if(PhotonNetwork.IsConnected)
            line.colorGradient = _colorPlayerID[Int32.Parse(PhotonNetwork.LocalPlayer.UserId)];
    }

    void Start()
    {
        col = GetComponent<EdgeCollider2D>();

        points = new List<Vector2>();

        SetPoint();
    }


    void FixedUpdate()
    {
        if (Vector3.Distance(points.Last(), Head.position) > pointSpacing)
            SetPoint();
    }


    private void SetPoint()
    {
        if (points.Count > 1)
            col.points = points.ToArray<Vector2>();

        points.Add(Head.position);

        line.positionCount = points.Count;
        line.SetPosition(points.Count - 1, Head.position);
    }
}

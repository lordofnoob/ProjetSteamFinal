using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mb_Connection : MonoBehaviour
{
    private Collider2D collider;
    private LineRenderer lr;

    private void Awake()
    {
        lr = GetComponent<LineRenderer>();
        collider = GetComponent<Collider2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        lr.useWorldSpace = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartDrawing()
    {
        lr.enabled = true;
        collider.enabled = true;
    }
    public void StopDrawing()
    {
        lr.enabled = false;
        collider.enabled = false;
    }

    public void SetStartLinePosition(Vector3 startPos)
    {
        lr.SetPosition(0, startPos);
        CenteredCollider();
    }

    public void SetEndLinePosition(Vector3 endPos)
    {
        lr.SetPosition(lr.positionCount - 1, endPos);
        CenteredCollider();
    }

    public void SetLineColor(Color color, float alpha)
    {
        lr.endColor = new Color(color.r, color.g, color.b, alpha);
        lr.startColor = new Color(color.r, color.g, color.b, alpha);
    }

    public void CenteredCollider()
    {
        collider.transform.position = Vector3.Lerp(lr.GetPosition(0), lr.GetPosition(lr.positionCount - 1), 0.5f);
    }
}

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

    public void CenteredCollider()
    {
        collider.transform.position = Vector3.Lerp(lr.GetPosition(0), lr.GetPosition(lr.positionCount - 1), 0.5f);
    }
}

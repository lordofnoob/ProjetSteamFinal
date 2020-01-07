using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mb_Terminal : MonoBehaviour
{
    public Sc_Terminal scriptableTerminal;

    public Transform inputNodesContainer;
    public Transform ouputNodesContainer;

    public List<Mb_Node> inputNodes = new List<Mb_Node>();
    public List<Mb_Node> outputNodes = new List<Mb_Node>();


    private void Awake()
    {
        inputNodes.AddRange(inputNodesContainer.GetComponentsInChildren<Mb_Node>());
        outputNodes.AddRange(ouputNodesContainer.GetComponentsInChildren<Mb_Node>());
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mb_Rope : MonoBehaviour
{
	public Transform target;
	public float resolution = 0.5F;                           //  Sets the amount of joints there are in the rope (1 = 1 joint for every 1 unit)
	public float ropeDrag = 0.1F;                                //  Sets each joints Drag
	public float ropeMass = 0.1F;                           //  Sets each joints Mass
	public float ropeColRadius = 0.5F;                  //  Sets the radius of the collider in the SphereCollider component
														//public float ropeBreakForce = 25.0F;					 //-------------- TODO (Hopefully will break the rope in half...
	private Vector3[] segmentPos;           //  DONT MESS!	This is for the Line Renderer's Reference and to set up the positions of the gameObjects
	private GameObject[] joints;            //  DONT MESS!	This is the actual joint objects that will be automatically created
	private LineRenderer line;                          //  DONT MESS!	 The line renderer variable is set up when its assigned as a new component
	private int segments = 0;                   //  DONT MESS!	The number of segments is calculated based off of your distance * resolution
	private bool rope = false;                       //  DONT MESS!	This is to keep errors out of your debug window! Keeps the rope from rendering when it doesnt exist...

	//Joint Settings
	public Vector3 swingAxis = new Vector3(1, 1, 1);                 //  Sets which axis the character joint will swing on (1 axis is best for 2D, 2-3 axis is best for 3D (Default= 3 axis))
	public float lowTwistLimit = -100.0F;                   //  The lower limit around the primary axis of the character joint. 
	public float highTwistLimit = 100.0F;                   //  The upper limit around the primary axis of the character joint.
	public float swing1Limit = 20.0F;                   //	The limit around the primary axis of the character joint starting at the initialization point.

	void Awake()
	{
		BuildRope();
	}

	void Update()
	{
		// Put rope control here!


		//Destroy Rope Test	(Example of how you can use the rope dynamically)
		if (rope && Input.GetKeyDown("d"))
		{
			DestroyRope();
		}
		if (!rope && Input.GetKeyDown("r"))
		{
			BuildRope();
		}
	}
	void LateUpdate()
	{
		// Does rope exist? If so, update its position
		if (rope)
		{
			for (int i = 0; i < segments; i++)
			{
				if (i == 0)
				{
					line.SetPosition(i, transform.position);
				}
				else
				if (i == segments - 1)
				{
					line.SetPosition(i, target.transform.position);
				}
				else
				{
					line.SetPosition(i, joints[i].transform.position);
				}
			}
			line.enabled = true;
		}
		else
		{
			line.enabled = false;
		}
	}



	void BuildRope()
	{
		line = gameObject.GetComponent<LineRenderer>();

		// Find the amount of segments based on the distance and resolution
		// Example: [resolution of 1.0 = 1 joint per unit of distance]
		segments = (int)(Vector3.Distance(transform.position, target.position) * resolution);
		line.positionCount = segments;
		//line.SetVertexCount(segments);
		segmentPos = new Vector3[segments];
		joints = new GameObject[segments];
		segmentPos[0] = transform.position;
		segmentPos[segments - 1] = target.position;

		// Find the distance between each segment
		var segs = segments - 1;
		var seperation = ((target.position - transform.position) / segs);

		for (int s = 1; s < segments; s++)
		{
			// Find the each segments position using the slope from above
			Vector3 vector = (seperation * s) + transform.position;
			segmentPos[s] = vector;

			//Add Physics to the segments
			AddJointPhysics(s);
		}

		// Attach the joints to the target object and parent it to this object	
		CharacterJoint end = target.gameObject.AddComponent<CharacterJoint>();
		end.connectedBody = joints[joints.Length - 1].transform.GetComponent<Rigidbody>();
		end.swingAxis = swingAxis;
		SoftJointLimit limit_setter = end.lowTwistLimit;
		limit_setter.limit = lowTwistLimit;
		end.lowTwistLimit = limit_setter;
		limit_setter = end.highTwistLimit;
		limit_setter.limit = highTwistLimit;
		end.highTwistLimit = limit_setter;
		limit_setter = end.swing1Limit;
		limit_setter.limit = swing1Limit;
		end.swing1Limit = limit_setter;
		target.parent = transform;

		// Rope = true, The rope now exists in the scene!
		rope = true;
	}

	void AddJointPhysics(int n)
	{
		joints[n] = new GameObject("Joint_" + n);
		joints[n].transform.parent = transform;
		Rigidbody rigid = joints[n].AddComponent<Rigidbody>();
		SphereCollider col = joints[n].AddComponent<SphereCollider>();
		CharacterJoint ph = joints[n].AddComponent<CharacterJoint>();
		ph.swingAxis = swingAxis;
		SoftJointLimit limit_setter = ph.lowTwistLimit;
		limit_setter.limit = lowTwistLimit;
		ph.lowTwistLimit = limit_setter;
		limit_setter = ph.highTwistLimit;
		limit_setter.limit = highTwistLimit;
		ph.highTwistLimit = limit_setter;
		limit_setter = ph.swing1Limit;
		limit_setter.limit = swing1Limit;
		ph.swing1Limit = limit_setter;
		//ph.breakForce = ropeBreakForce; <--------------- TODO

		joints[n].transform.position = segmentPos[n];

		rigid.drag = ropeDrag;
		rigid.mass = ropeMass;
		col.radius = ropeColRadius;

		if (n == 1)
		{
			ph.connectedBody = transform.GetComponent<Rigidbody>();
		}
		else
		{
			ph.connectedBody = joints[n - 1].GetComponent<Rigidbody>();
		}

	}

	void DestroyRope()
	{
		// Stop Rendering Rope then Destroy all of its components
		rope = false;
		for (int dj = 0; dj < joints.Length - 1; dj++)
		{
			Destroy(joints[dj]);
		}

		segmentPos = new Vector3[0];
		joints = new GameObject[0];
		segments = 0;
	}
}
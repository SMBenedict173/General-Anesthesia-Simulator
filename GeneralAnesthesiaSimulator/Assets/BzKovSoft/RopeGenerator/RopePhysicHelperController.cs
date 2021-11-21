using System;
using System.Collections.Generic;
using UnityEngine;

namespace BzKovSoft.RopeGenerator
{
	public class RopePhysicHelperController : MonoBehaviour
	{
		Transform[] _ropeNodes;
		float[] _lengthFromPrev;
		public float precision = 0.05f;

		private void Start()
		{
			_ropeNodes = GetBoneNodes();
			_lengthFromPrev = new float[_ropeNodes.Length];

			Vector3 prevPos = _ropeNodes[0].position;
			for (int i = 0; i < _ropeNodes.Length; i++)
			{
				var c = _ropeNodes[i];

				_lengthFromPrev[i] = (c.position - prevPos).magnitude;
				prevPos = c.position;
			}
		}

		private Transform[] GetBoneNodes()
		{
			var joints = new List<Joint>(transform.childCount);
			for (int i = 0; i < transform.childCount; i++)
			{
				var c = transform.GetChild(i);
				var j = c.GetComponent<Joint>();
				if (j != null)
				{
					joints.Add(j);
				}
			}

			LinkedList<Joint> list = new LinkedList<Joint>();
			list.AddLast(joints[joints.Count - 1]);
			joints.RemoveAt(joints.Count - 1);

			int checkJointCount = joints.Count;
			while (joints.Count != 0)
			{
				for (int i = 0; i < joints.Count; i++)
				{
					var j = joints[i];
					if (list.Last.Value.transform == j.connectedBody.transform)
					{
						list.AddLast(j);
						joints.RemoveAt(i);
						--i;
					}
					else if (list.First.Value.connectedBody.transform == j.transform)
					{
						list.AddFirst(j);
						joints.RemoveAt(i);
						--i;
					}
				}
				if (checkJointCount == joints.Count)
				{
					throw new InvalidOperationException("Invalid rope joint connection order");
				}

				checkJointCount = joints.Count;
			}

			var ropeNodes = new Transform[list.Count + 1];
			ropeNodes[0] = list.First.Value.connectedBody.transform;
			int ropeNodeCount = 0;
			foreach (var j in list)
			{
				ropeNodes[++ropeNodeCount] = j.transform;
			}

			return ropeNodes;
		}

		private void FixedUpdate()
		{
			for (int i = 1; i < _ropeNodes.Length; i++)
			{
				var child1 = _ropeNodes[i - 1];
				var child2 = _ropeNodes[i];

				var joint = child2.GetComponent<ConfigurableJoint>();

				if (child1 != joint.connectedBody.transform)
				{
					throw new InvalidOperationException("Wrong bone order");
				}

				Vector3 upperPoint = child1.position;
				Vector3 lowerPoint = child2.position;

				var length = _lengthFromPrev[i];

				var oldChainV = lowerPoint - upperPoint;
				float currentLength = oldChainV.magnitude - length;
				if (Math.Abs(currentLength) > precision)
				{
					lowerPoint = upperPoint + oldChainV.normalized * (length + precision * Mathf.Sign(currentLength));
					child2.position = lowerPoint;

					Debug.DrawLine(upperPoint, lowerPoint, Color.red);
				}
			}
		}
	}
}
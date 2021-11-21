using BzKovSoft.RopeGenerator.MeshGenerator;
using System;
using UnityEngine;

namespace BzKovSoft.RopeGenerator.Samples
{
	public class TwoPoint : MonoBehaviour
	{
		[SerializeField]
		float _extraLength = 0.1f;
		Vector3? _startPoint;

		void Update()
		{
			if (Input.GetMouseButtonDown(0))
			{
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				var point = ray.GetPoint(10);

				if (_startPoint.HasValue)
				{
					Vector3 startPoint = _startPoint.Value;
					Vector3 endPoint = point;
					_startPoint = null;

					MakeRope(startPoint, endPoint);
				}
				else
				{
					_startPoint = point;
				}
			}
		}

		private void MakeRope(Vector3 startPoint, Vector3 endPoint)
		{
			float length = Vector3.Distance(startPoint, endPoint);
			length = length + length * _extraLength;

			var go = new GameObject("New Rope (" + (++_genCounter).ToString() + ")");
			go.transform.position = startPoint;
			var goEnd = new GameObject("EndPoint");
			goEnd.transform.parent = go.transform;
			goEnd.transform.position = endPoint;

			var ropeBones = MakeRope(go, length).Bones;

			var lastBone = ropeBones[ropeBones.Length - 1];


			var rigid = goEnd.AddComponent<Rigidbody>();
			rigid.isKinematic = true;

			var joint = goEnd.AddComponent<ConfigurableJoint>();
			joint.connectedBody = lastBone.GetComponent<Rigidbody>();

			joint.projectionMode = JointProjectionMode.PositionAndRotation;

			joint.xMotion = ConfigurableJointMotion.Locked;
			joint.yMotion = ConfigurableJointMotion.Locked;
			joint.zMotion = ConfigurableJointMotion.Locked;

			{
				joint.angularXMotion = ConfigurableJointMotion.Free;
				joint.angularYMotion = ConfigurableJointMotion.Free;
				joint.angularZMotion = ConfigurableJointMotion.Free;
			}

			joint.autoConfigureConnectedAnchor = false;
			joint.connectedAnchor = Vector3.zero;
			joint.anchor = Vector3.zero;


			var dir = endPoint - startPoint;
			var quat = Quaternion.FromToRotation(Vector3.right, dir.normalized);
			for (int i = 0; i < ropeBones.Length; i++)
			{
				var bone = ropeBones[i].transform;
				bone.rotation = quat;

				float r = i / ((float)ropeBones.Length - 1);

				bone.position = Vector3.Lerp(startPoint, endPoint, r);
			}
		}

		static int _genCounter = 0;

		private RopeGenerationResult MakeRope(GameObject go, float length)
		{
			var meshGenerator = new RopeMeshCylinderGenerator();
			meshGenerator.RadiusStart = 0.1f;
			meshGenerator.RadiusStop = 0.1f;

			var tmpRope = new BzKovSoft.RopeGenerator.RopeGenerator(meshGenerator, go);

			tmpRope.Length = length;
			tmpRope.BoneCount = Math.Max((int)length, 2);

			return tmpRope.MakeOne();
		}
	}
}
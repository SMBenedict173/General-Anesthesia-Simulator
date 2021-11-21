using BzKovSoft.RopeGenerator.MeshGenerator;
using System;
using UnityEngine;

namespace BzKovSoft.RopeGenerator
{
	public class RopeGenerator
	{
		readonly IRopeMeshGenerator _meshGenerator;
		GameObject _parrent;
		float _colliderRadius = 0.1f;
		float _length = 10f;
		float _massOfBone = 0.1f;
		int _boneCount = 10;
		int _jointAngleLimit = 90;
		int _rotateAngleLimit = 45;
		bool _restrictFirstBone = false;
		float _angularDriveSpringStart = 0f;
		float _angularDriveSpringStop = 0f;


		public RopeGenerator(IRopeMeshGenerator meshGenerator, GameObject parrent)
		{
			_meshGenerator = meshGenerator;
			_parrent = parrent;
		}

		public int JointAngleLimit
		{
			get { return _jointAngleLimit; }
			set { _jointAngleLimit = value; }
		}

		public int RotateAngleLimit
		{
			get { return _rotateAngleLimit; }
			set { _rotateAngleLimit = value; }
		}

		public float AngularDriveSpringStart
		{
			get { return _angularDriveSpringStart; }
			set { _angularDriveSpringStart = value; }
		}

		public float AngularDriveSpringStop
		{
			get { return _angularDriveSpringStop; }
			set { _angularDriveSpringStop = value; }
		}

		public int BoneCount
		{
			get { return _boneCount; }

			set
			{
				if (value < 2)
					throw new ArgumentException("'BoneCount' must be >= 2");

				_boneCount = value;
			}
		}

		public float MassOfBone
		{
			get { return _massOfBone; }

			set
			{
				if (value < 0)
					throw new ArgumentException("'MassOfBone' must be >= 0");

				_massOfBone = value;
			}
		}

		public float Length
		{
			get { return _length; }

			set
			{
				if (value <= 0)
					throw new ArgumentException("'Length' must be > 0");

				_length = value;
			}
		}

		/// <summary>
		/// Sets collider radius of the rope.
		/// </summary>
		public float ColliderRadius
		{
			get { return _colliderRadius; }

			set
			{
				if (value <= 0)
					throw new ArgumentException("'Collider Radius' must be > 0");

				_colliderRadius = value;
			}
		}

		public bool RestrictFirstBone
		{
			get
			{
				return _restrictFirstBone;
			}

			set
			{
				_restrictFirstBone = value;
			}
		}

		public RopeGenerationResult MakeOne()
		{
			var transform = _parrent.transform;
			SkinnedMeshRenderer rend = _parrent.AddComponent<SkinnedMeshRenderer>();

			rend.updateWhenOffscreen = true;

			MeshGenerationResult result = _meshGenerator.Create(_colliderRadius, _length, _boneCount, _restrictFirstBone);
			if (result.materials != null)
				rend.materials = result.materials;
			Mesh mesh = result.mesh;
			// Create Bone Transforms and Bind poses
			// One bone at the bottom and one at the top
			GameObject[] bones = new GameObject[_boneCount];
			Transform[] bonesT = new Transform[_boneCount];
			Matrix4x4[] bindPoses = new Matrix4x4[_boneCount];


			for (int i = 0; i < _boneCount; i++)
			{
				bones[i] = new GameObject("Bone_" + (i + 1).ToString());
			}


			for (int i = 0; i < _boneCount; i++)
			{
				float r = (float)i / (_boneCount - 1);

				var boneT = bones[i].transform;
				bonesT[i] = boneT;
				boneT.parent = transform;
				// Set the position relative to the parent
				boneT.localRotation = Quaternion.identity;
				boneT.localPosition = new Vector3(r * _length, 0, 0);

				// The bind pose is bone's inverse transformation matrix
				// In this case the matrix we also make this matrix relative to the root
				// So that we can move the root game object around freely
				bindPoses[i] = boneT.worldToLocalMatrix * transform.localToWorldMatrix;
			}





			for (int i = 0; i < _boneCount; i++)
			{
				var bone = bones[i];

				var rigid = bone.AddComponent<Rigidbody>();
				rigid.mass = _massOfBone;

				if (i == 0)
				{
					rigid.isKinematic = true;
				}
				else
				{
					float unitHeight = _length / (_boneCount - 1);

					var collider = bone.AddComponent<CapsuleCollider>();
					collider.radius = _colliderRadius;
					collider.height = unitHeight;
					collider.center = new Vector3(-unitHeight / 2, 0, 0);
					collider.direction = 0;


					var joint = bone.AddComponent<ConfigurableJoint>();
					joint.connectedBody = bones[i - 1].GetComponent<Rigidbody>();

					joint.projectionMode = JointProjectionMode.PositionAndRotation;

					joint.xMotion = ConfigurableJointMotion.Locked;
					joint.yMotion = ConfigurableJointMotion.Locked;
					joint.zMotion = ConfigurableJointMotion.Locked;

					bool firstBoneRistrict = i != 1 | _restrictFirstBone;
					if (firstBoneRistrict & _jointAngleLimit > 0f)
					{
						joint.angularYMotion = ConfigurableJointMotion.Limited;
						joint.angularZMotion = ConfigurableJointMotion.Limited;
					}
					else
					{
						joint.angularYMotion = ConfigurableJointMotion.Free;
						joint.angularZMotion = ConfigurableJointMotion.Free;
					}

					if (firstBoneRistrict & _rotateAngleLimit > 0f)
					{
						joint.angularXMotion = ConfigurableJointMotion.Limited;
					}
					else
					{
						joint.angularXMotion = ConfigurableJointMotion.Free;
					}

					joint.angularXLimitSpring = new SoftJointLimitSpring
					{
						spring = 1000f,
						damper = 100f,
					};

					joint.autoConfigureConnectedAnchor = false;
					joint.connectedAnchor = new Vector3(0, 0, 0);
					joint.anchor = new Vector3(-unitHeight, 0, 0);

					var xLowLimits = joint.lowAngularXLimit;
					xLowLimits.limit = -_rotateAngleLimit;
					joint.lowAngularXLimit = xLowLimits;

					var xHighLimits = joint.highAngularXLimit;
					xHighLimits.limit = _rotateAngleLimit;
					joint.highAngularXLimit = xHighLimits;

					var zLimits = joint.angularZLimit;
					zLimits.limit = _jointAngleLimit;
					joint.angularZLimit = zLimits;

					var yLimits = joint.angularYLimit;
					yLimits.limit = _jointAngleLimit;
					joint.angularYLimit = yLimits;

					var angularDrive = new JointDrive();
					float psr = (float)(i - 1) / (_boneCount - 2);
					angularDrive.positionSpring = Mathf.Lerp(_angularDriveSpringStart, _angularDriveSpringStop, psr);
					angularDrive.maximumForce = float.MaxValue;
					joint.slerpDrive = angularDrive;
					joint.rotationDriveMode = RotationDriveMode.Slerp;
				}
			}

			// assign the bindPoses array to the bindposes array which is part of the mesh. 
			mesh.bindposes = bindPoses;

			// Assign bones and bind poses
			rend.bones = bonesT;
			rend.sharedMesh = mesh;

			return new RopeGenerationResult
			{
				Mesh = mesh,
				Bones = bones,
			};
		}
	}
}

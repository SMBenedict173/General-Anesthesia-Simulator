using BzKovSoft.RopeGenerator.MeshGenerator;
using System;
using UnityEditor;
using UnityEngine;

namespace BzKovSoft.RopeGenerator.Editor
{
	public sealed class RopeGenDialog : EditorWindow
	{
		IRopeMeshGenerator _meshGenerator;
		int _genCounter = 0;
		GameObject _parrent;
		float _colliderRadius = 0.1f;
		float _length = 10f;
		float _massOfBone = 0.5f;
		int _boneCount = 10;
		int _jointAngleLimit = 10;
		int _rotateAngleLimit = 45;
		float _angularDriveSpringStart = 0f;
		float _angularDriveSpringStop = 0f;
		bool _restrictFirstBone = true;
		bool _optimizeMesh = true;
		bool _saveMesh = false;
		MeshGeneratorType _meshGeneratorType = MeshGeneratorType.Cylinder;
		RangeType _RangeType = RangeType.Interval;

		[MenuItem("Window/BzSoft/Rope Generator")]
		private static void ShowWindow()
		{
			EditorWindow.GetWindow(typeof(RopeGenDialog), false, "Rope Generator");
		}

		void OnGUI()
		{

			Handles.BeginGUI();
			GUILayout.BeginVertical("box");

			_parrent = (GameObject)EditorGUILayout.ObjectField("Parrent", _parrent, typeof(GameObject), true);

			_colliderRadius = EditorGUILayout.FloatField("Collider radius", _colliderRadius);
			_massOfBone = EditorGUILayout.FloatField("Mass of bone", _massOfBone);
			_boneCount = EditorGUILayout.IntField("Bone Count", _boneCount);
			_jointAngleLimit = EditorGUILayout.IntField("Joint angle limit", _jointAngleLimit);
			_rotateAngleLimit = EditorGUILayout.IntField("Rotate angle limit", _rotateAngleLimit);
			_angularDriveSpringStart = EditorGUILayout.FloatField("Angular drive spring start", _angularDriveSpringStart);
			_angularDriveSpringStop = EditorGUILayout.FloatField("Angular drive spring stop", _angularDriveSpringStop);
			_restrictFirstBone = EditorGUILayout.Toggle("Restrict first bone", _restrictFirstBone);
			_optimizeMesh = EditorGUILayout.Toggle("Optimize mesh", _optimizeMesh);
			_saveMesh = EditorGUILayout.Toggle("Save mesh", _saveMesh);

			GUILayout.Label("Length type:");
			_RangeType = (RangeType)EditorGUILayout.EnumPopup(_RangeType);
			++EditorGUI.indentLevel;

			switch (_RangeType)
			{
				case RangeType.Length:
					_length = EditorGUILayout.FloatField("Length", _length);
					break;
				case RangeType.Interval:
					float interval = _length / _boneCount;
					interval = EditorGUILayout.FloatField("Interval", interval);
					_length = interval * _boneCount;
					break;

				default: throw new NotSupportedException();
			}
			--EditorGUI.indentLevel;


			GUILayout.Label("Mesh type:");
			_meshGeneratorType = (MeshGeneratorType)EditorGUILayout.EnumPopup(_meshGeneratorType);

			++EditorGUI.indentLevel;
			switch (_meshGeneratorType)
			{
				case MeshGeneratorType.Cylinder:
					DrawPanelMeshGeneratorCylinder();
					break;
				case MeshGeneratorType.Chain:
					DrawPanelMeshGeneratorChain();
					break;

				default: throw new NotSupportedException();
			}
			--EditorGUI.indentLevel;

			if (GUILayout.Button("Make Rope"))
			{
				MakeRope();
			}

			GUILayout.EndVertical();
			Handles.EndGUI();
		}

		private void DrawPanelMeshGeneratorCylinder()
		{
			RopeMeshCylinderGenerator meshGenerator = _meshGenerator as RopeMeshCylinderGenerator;
			if (meshGenerator == null)
			{
				meshGenerator = new RopeMeshCylinderGenerator();
				_meshGenerator = meshGenerator;
			}
			meshGenerator.Sides = EditorGUILayout.IntField("Sides", meshGenerator.Sides);
			meshGenerator.RadiusStart = EditorGUILayout.FloatField("Radius Start", meshGenerator.RadiusStart);
			meshGenerator.RadiusStop = EditorGUILayout.FloatField("Radius Stop", meshGenerator.RadiusStop);
			meshGenerator.SegmentsPerBone = EditorGUILayout.IntField("Segments per bone", meshGenerator.SegmentsPerBone);
			meshGenerator._material = (Material)EditorGUILayout.ObjectField("Material", meshGenerator._material, typeof(Material), true);
		}

		Vector3 _initialRotation = new Vector3(0f, 0f, 0f);
		Vector3 _stepRotation = new Vector3(90f, 0f, 0f);

		private void DrawPanelMeshGeneratorChain()
		{
			RopeMeshChainGenerator meshGenerator = _meshGenerator as RopeMeshChainGenerator;
			if (meshGenerator == null)
			{
				meshGenerator = new RopeMeshChainGenerator();
				_meshGenerator = meshGenerator;
			}
			meshGenerator.SourceObject = (GameObject)EditorGUILayout.ObjectField(meshGenerator.SourceObject, typeof(GameObject), true);
			meshGenerator.ChainsPerBone = EditorGUILayout.IntField("Chains per bone", meshGenerator.ChainsPerBone);
			meshGenerator.Scale = EditorGUILayout.FloatField("Scale", meshGenerator.Scale);

			_initialRotation = EditorGUILayout.Vector3Field("Initial rotation", _initialRotation);
			meshGenerator.InitialRotation = Quaternion.Euler(_initialRotation);

			_stepRotation = EditorGUILayout.Vector3Field("Step rotation", _stepRotation);
			meshGenerator.StepRotation = Quaternion.Euler(_stepRotation);

			if (meshGenerator.SourceObject != null)
			{
				var renderer = meshGenerator.SourceObject.GetComponent<Renderer>();
				if (renderer == null)
				{
					throw new ArgumentException("No renderer found");
				}
				meshGenerator.Materials = renderer.sharedMaterials;
			}
			else
			{
				meshGenerator.Materials = null;
			}
		}

		private void MakeRope()
		{
			if (_meshGenerator == null)
			{
				Debug.LogError("MeshGenerator is not specified");
				return;
			}

			var go = new GameObject("New Rope (" + (++_genCounter).ToString() + ")");
			go.transform.localPosition = new Vector3(0, 0, 0);
			go.transform.rotation = Quaternion.FromToRotation(Vector3.right, Vector3.down);

			if (_parrent != null)
				go.transform.parent = _parrent.transform;

			var tmpRope = new RopeGenerator(_meshGenerator, go);
			ApplayParams(tmpRope);
			var result = tmpRope.MakeOne();

			if (_optimizeMesh)
				MeshUtility.Optimize(result.Mesh);

			if (_saveMesh)
			{
				var path = EditorUtility.SaveFilePanel(
					"Save mesh",
					"",
					"new_mesh.asset",
					"asset");

				path = FileUtil.GetProjectRelativePath(path);

				if (path.Length != 0)
				{
					AssetDatabase.CreateAsset(result.Mesh, path);
					AssetDatabase.SaveAssets();
				}
			}
		}

		private void ApplayParams(RopeGenerator tmpRope)
		{
			tmpRope.ColliderRadius = _colliderRadius;
			tmpRope.Length = _length;
			tmpRope.MassOfBone = _massOfBone;
			tmpRope.BoneCount = _boneCount;
			tmpRope.JointAngleLimit = _jointAngleLimit;
			tmpRope.RotateAngleLimit = _rotateAngleLimit;
			tmpRope.AngularDriveSpringStart = _angularDriveSpringStart;
			tmpRope.AngularDriveSpringStop = _angularDriveSpringStop;
			tmpRope.RestrictFirstBone = _restrictFirstBone;
		}

		enum MeshGeneratorType
		{
			Cylinder,
			Chain
		}
		enum RangeType
		{
			Length,
			Interval
		}
	}
}

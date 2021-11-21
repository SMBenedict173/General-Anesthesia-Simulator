using System;
using UnityEngine;

namespace BzKovSoft.RopeGenerator.MeshGenerator
{

	/// <summary>
	/// Mesh generator that produces a chain
	/// </summary>
	public class RopeMeshChainGenerator : IRopeMeshGenerator
	{
		Material[] _materials;
		GameObject _sourceObject;
		int _chainsPerBone = 3;
		float _scale = 1f;

		public Quaternion StepRotation { get; set; }
		public Quaternion InitialRotation { get; set; }

		public RopeMeshChainGenerator()
		{
			StepRotation = Quaternion.identity;
			InitialRotation = Quaternion.identity;
		}

		public float Scale { get { return _scale; } set { _scale = value; } }
		public int ChainsPerBone { get { return _chainsPerBone; } set { _chainsPerBone = value; } }
		public GameObject SourceObject { get { return _sourceObject; } set { _sourceObject = value; } }
		public Material[] Materials { get { return _materials; } set { _materials = value; } }

		public MeshGenerationResult Create(float radius, float length, int boneCount, bool restrictFirstBone)
		{
			if (_chainsPerBone < 2)
			{
				throw new InvalidOperationException("Minimum number of chains per bone is 2");
			}

			var mesh = CreateMesh(radius, boneCount, length);
			//CalculateWeights(mesh, boneCount, totalSegments, restrictFirstBone);
			MeshGenerationResult rez;
			rez.mesh = mesh;
			rez.materials = _materials;
			return rez;
		}

		private Mesh CreateMesh(float radius, int boneCount, float length)
		{
			Mesh originalMesh = _sourceObject.GetComponent<MeshFilter>().sharedMesh;

			var originalVertices = originalMesh.vertices;
			var originalTangents = originalMesh.tangents;
			var originalUV = originalMesh.uv;
			var originalUV2 = originalMesh.uv2;
			var originalUV3 = originalMesh.uv3;
			var originalUV4 = originalMesh.uv4;
			var originalColors = originalMesh.colors;
			var originalColors32 = originalMesh.colors32;
			var originalNormals = originalMesh.normals;
			var originalSubmeshes = new int[originalMesh.subMeshCount][];
			for (int i = 0; i < originalMesh.subMeshCount; i++)
			{
				originalSubmeshes[i] = originalMesh.GetTriangles(i);
			}

			int originalVertCount = originalMesh.vertices.Length;

			int chainCount = (_chainsPerBone - 1) * (boneCount - 1) + 1;
			Vector3[] vertices = new Vector3[originalVertCount * chainCount];
			Vector4[] tangents = new Vector4[originalTangents.Length * chainCount];
			Vector2[] uv = new Vector2[originalUV.Length * chainCount];
			Vector2[] uv2 = new Vector2[originalUV2.Length * chainCount];
			Vector2[] uv3 = new Vector2[originalUV3.Length * chainCount];
			Vector2[] uv4 = new Vector2[originalUV4.Length * chainCount];
			Color[] colors = new Color[originalColors.Length * chainCount];
			Color32[] colors32 = new Color32[originalColors32.Length * chainCount];
			Vector3[] normals = new Vector3[originalNormals.Length * chainCount];
			int[][] subMeshes = new int[originalSubmeshes.Length][];
			for (int m = 0; m < originalSubmeshes.Length; m++)
			{
				int submeshLength = originalSubmeshes[m].Length;
				subMeshes[m] = new int[submeshLength * chainCount];
			}

			Vector3 shift = Vector3.right * (length / (chainCount - 1f));
			Vector3 vShiftAccum = Vector3.zero;
			Quaternion rot = StepRotation;
			Quaternion rotAccum = InitialRotation;

			for (int c = 0; c < chainCount; c++)
			{
				int chainVStart = c * originalVertCount;

				var trV = Matrix4x4.TRS(vShiftAccum, rotAccum, Vector3.one * _scale);
				var trN = Matrix4x4.TRS(Vector3.zero, rotAccum, Vector3.one);
				vShiftAccum += shift;
				rotAccum = rotAccum * rot;

				// vertices
				int count = originalVertices.Length;

				GenerateMeshValues(originalVertices, vertices, trV, chainVStart);
				GenerateMeshValues(originalTangents, tangents, chainVStart);
				GenerateMeshValues(originalUV, uv, chainVStart);
				GenerateMeshValues(originalUV2, uv2, chainVStart);
				GenerateMeshValues(originalUV3, uv3, chainVStart);
				GenerateMeshValues(originalUV4, uv4, chainVStart);
				GenerateMeshValues(originalColors, colors, chainVStart);
				GenerateMeshValues(originalColors32, colors32, chainVStart);
				GenerateMeshValues(originalNormals, normals, trN, chainVStart);

				for (int m = 0; m < originalSubmeshes.Length; m++)
				{
					// for each submesh
					int triangleCount = originalSubmeshes[m].Length;
					int chainTStart = c * triangleCount;
					GenerateMeshValues(originalSubmeshes[m], subMeshes[m], chainVStart, chainTStart);
				}
			}

			Mesh mesh = new Mesh();
			mesh.vertices = vertices;
			mesh.tangents = tangents;
			mesh.uv = uv;
			mesh.uv2 = uv2;
			mesh.uv3 = uv3;
			mesh.uv4 = uv4;
			mesh.colors = colors;
			mesh.colors32 = colors32;
			mesh.normals = normals;
			for (int m = 0; m < subMeshes.Length; m++)
			{
				mesh.SetTriangles(subMeshes[m], m);
			}

			CalculateWeights(boneCount, length, originalMesh, originalVertCount, chainCount, mesh);

			mesh.RecalculateBounds();

			return mesh;
		}

		private void CalculateWeights(int boneCount, float length, Mesh originalMesh, int originalVertCount, int chainCount, Mesh mesh)
		{
			var originalWeights = originalMesh.boneWeights;
			BoneWeight[] weights = new BoneWeight[originalVertCount * chainCount];

			//StringBuilder sb = new StringBuilder(1024);

			for (int bi = 0; bi < boneCount - 1; bi++)
			{
				//sb.AppendFormat("Bone {0}" + Environment.NewLine, bi);
				for (int ci = 0; ci < _chainsPerBone; ci++)
				{
					//sb.AppendFormat("Chain {0}" + Environment.NewLine, ci);

					if (bi != 0 & ci == 0)
					{
						continue;
					}

					BoneWeight bone = new BoneWeight();
					bone.boneIndex0 = bi;
					bone.boneIndex1 = bi + 1;

					float rate = (float)ci / (_chainsPerBone - 1);
					bone.weight0 = 1f - rate;
					bone.weight1 = rate;

					for (int i = 0; i < originalVertCount; i++)
					{
						int index = bi * (_chainsPerBone - 1) * originalVertCount + ci * originalVertCount + i;
						weights[index] = bone;
						/*
						sb.AppendFormat("Set index {0}", index, bone);
						sb.AppendFormat(" BoneIndexes: {0}, {1}, {2}, {3}",
							bone.boneIndex0,
							bone.boneIndex1,
							bone.boneIndex2,
							bone.boneIndex3);
						sb.AppendFormat(" BoneWeights: {0}, {1}, {2}, {3}" + Environment.NewLine,
							bone.weight0,
							bone.weight1,
							bone.weight2,
							bone.weight3);*/
					}
				}
			}

			//Debug.Log(sb.ToString());

			mesh.boneWeights = weights;
		}

		private static void GenerateMeshValues(Vector3[] originalValues, Vector3[] result, Matrix4x4 m, int chainStart)
		{
			int count = originalValues.Length;

			for (int i = 0; i < count; i++)
			{
				int index = chainStart + i;
				var v = originalValues[i];
				v = m.MultiplyPoint3x4(v);
				result[index] = v;
			}
		}

		private static void GenerateMeshValues(int[] originalValues, int[] result, int shift, int chainStart)
		{
			int count = originalValues.Length;

			for (int i = 0; i < count; i++)
			{
				int index = chainStart + i;
				var v = originalValues[i];
				v += shift;
				result[index] = v;
			}
		}

		private static void GenerateMeshValues<T>(T[] originalValues, T[] result, int chainStart)
		{
			int count = originalValues.Length;

			for (int i = 0; i < count; i++)
			{
				int index = chainStart + i;
				var v = originalValues[i];
				result[index] = v;
			}
		}
	}
}

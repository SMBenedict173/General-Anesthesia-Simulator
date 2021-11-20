using BzKovSoft.RopeGenerator.MeshGenerator;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace BzKovSoft.RopeGenerator.Tests
{
	public class RopeMeshChainGeneratorTests
	{
		// boneWeights
		// bindposes
		// tangents
		// triangles
		// no data

		[Test]
		public void CheckVertexLength()
		{
			int chainCountPerBone = 3;
			Quaternion rotation = Quaternion.identity;
			var go = GenerateGO();

			var meshGen = new RopeMeshChainGenerator();
			meshGen.SourceObject = go;


			meshGen.StepRotation = rotation;

			meshGen.ChainsPerBone = chainCountPerBone;

			int bonesCount = 2;
			var result = meshGen.Create(1f, 100f, bonesCount, false);

			var oldMeshVertices = go.GetComponent<MeshFilter>().sharedMesh.vertices;
			var newMeshVertices = result.mesh.vertices;
			int chainCount = (chainCountPerBone - 1) * (bonesCount - 1) + 1;

			Assert.AreEqual(oldMeshVertices.Length * chainCount, newMeshVertices.Length);
		}

		[Test]
		public void CheckVertices()
		{
			int chainCountPerBone = 3;
			int bonesCount = 2;
			float length = 100f;
			Quaternion rotation = Quaternion.Euler(0f, 180f, 0);
			var go = GenerateGO();

			var meshGen = new RopeMeshChainGenerator();
			meshGen.SourceObject = go;
			meshGen.StepRotation = rotation;
			meshGen.ChainsPerBone = chainCountPerBone;

			var result = meshGen.Create(1f, length, bonesCount, false);

			int chainCount = (chainCountPerBone - 1) * (bonesCount - 1) + 1;
			float chainStep = length / (chainCount - 1);

			CheckVertexes(go, result.mesh, rotation, chainCount, chainStep);
		}

		[Test]
		public void CheckWeights()
		{
			int chainCountPerBone = 3;
			int bonesCount = 3;
			float length = 100f;
			Quaternion rotation = Quaternion.Euler(0f, 180f, 0);
			var go = GenerateGO();

			var meshGen = new RopeMeshChainGenerator();
			meshGen.SourceObject = go;
			meshGen.StepRotation = rotation;
			meshGen.ChainsPerBone = chainCountPerBone;

			var result = meshGen.Create(1f, length, bonesCount, false);

			var weights = result.mesh.boneWeights;

			int chainCount = (chainCountPerBone - 1) * (bonesCount - 1) + 1;
			int orVertCount = go.GetComponent<MeshFilter>().sharedMesh.vertexCount;

			Assert.AreEqual(chainCount * orVertCount, weights.Length);

			AssertWeights(weights, 0 * orVertCount, orVertCount, 0, 1, 1f, 0f);
			AssertWeights(weights, 1 * orVertCount, orVertCount, 0, 1, .5f, .5f);
			AssertWeights(weights, 2 * orVertCount, orVertCount, 0, 1, 0f, 1f);

			AssertWeights(weights, 2 * orVertCount, orVertCount, 1, 2, 1f, 0f);
			AssertWeights(weights, 3 * orVertCount, orVertCount, 1, 2, .5f, .5f);
			AssertWeights(weights, 4 * orVertCount, orVertCount, 1, 2, 0f, 1f);
		}

		private void AssertWeights(BoneWeight[] weights,
			int startIndex, int count,
			int boneIndex1, int boneIndex2,
			float expected1, float expected2)
		{
			float w0 = 0f;
			float w1 = 0f;

			for (int i = 0; i < count; i++)
			{
				int index = startIndex + i;
				var weight = weights[index];

				w0 = GetBoneWeight(weight, boneIndex1);
				w1 = GetBoneWeight(weight, boneIndex2);

				Assert.AreEqual(expected1, w0);
				Assert.AreEqual(expected2, w1);
			}
		}

		private float GetBoneWeight(BoneWeight weight, int boneIndex)
		{
			float boneWeight = 0f;

			if (weight.boneIndex0 == boneIndex) boneWeight += weight.weight0;
			if (weight.boneIndex1 == boneIndex) boneWeight += weight.weight1;
			if (weight.boneIndex2 == boneIndex) boneWeight += weight.weight2;
			if (weight.boneIndex3 == boneIndex) boneWeight += weight.weight3;

			return boneWeight;
		}

		[Test]
		public void CheckNormals()
		{
			int chainCountPerBone = 3;
			int bonesCount = 2;
			float length = 100f;
			Quaternion rotation = Quaternion.Euler(0f, 180f, 0);
			var go = GenerateGO();

			var meshGen = new RopeMeshChainGenerator();
			meshGen.SourceObject = go;
			meshGen.StepRotation = rotation;
			meshGen.ChainsPerBone = chainCountPerBone;

			var result = meshGen.Create(1f, length, bonesCount, false);

			int chainCount = (chainCountPerBone - 1) * (bonesCount - 1) + 1;
			float chainStep = length / (chainCount - 1);

			CheckNormals(go, result.mesh, rotation, chainCount, chainStep);
		}

		[TestCase(0)]
		[TestCase(1)]
		[TestCase(2)]
		[TestCase(3)]
		public void CheckUVs(int channel)
		{
			int chainCountPerBone = 3;
			int bonesCount = 2;
			float length = 100f;
			Quaternion rotation = Quaternion.Euler(0f, 180f, 0);
			var go = GenerateGO();

			var meshGen = new RopeMeshChainGenerator();
			meshGen.SourceObject = go;
			meshGen.StepRotation = rotation;
			meshGen.ChainsPerBone = chainCountPerBone;

			var result = meshGen.Create(1f, length, bonesCount, false);

			int chainCount = (chainCountPerBone - 1) * (bonesCount - 1) + 1;
			float chainStep = length / (chainCount - 1);

			CheckUVs(go, result.mesh, channel, chainCount, chainStep);
		}

		[Test]
		public void CheckColors()
		{
			int chainCountPerBone = 3;
			int bonesCount = 2;
			float length = 100f;
			Quaternion rotation = Quaternion.Euler(0f, 180f, 0);
			var go = GenerateGO();

			var meshGen = new RopeMeshChainGenerator();
			meshGen.SourceObject = go;
			meshGen.StepRotation = rotation;
			meshGen.ChainsPerBone = chainCountPerBone;

			var result = meshGen.Create(1f, length, bonesCount, false);

			int chainCount = (chainCountPerBone - 1) * (bonesCount - 1) + 1;

			var colorsOld = new List<Color>(go.GetComponent<MeshFilter>().sharedMesh.colors);
			var colorsNew = new List<Color>(result.mesh.colors);
			CheckDataDupls<Color>(chainCount, colorsOld, colorsNew);
		}

		[Test]
		public void CheckColors32()
		{
			int chainCountPerBone = 3;
			int bonesCount = 2;
			float length = 100f;
			Quaternion rotation = Quaternion.Euler(0f, 180f, 0);
			var go = GenerateGO();

			var meshGen = new RopeMeshChainGenerator();
			meshGen.SourceObject = go;
			meshGen.StepRotation = rotation;
			meshGen.ChainsPerBone = chainCountPerBone;

			var result = meshGen.Create(1f, length, bonesCount, false);

			int chainCount = (chainCountPerBone - 1) * (bonesCount - 1) + 1;

			var colorsOld = new List<Color32>(go.GetComponent<MeshFilter>().sharedMesh.colors32);
			var colorsNew = new List<Color32>(result.mesh.colors32);
			CheckDataDupls<Color32>(chainCount, colorsOld, colorsNew);
		}

		private static void CheckVertexes(GameObject go, Mesh result, Quaternion rotation,
			int chainCount, float chainStep)
		{
			var oldMeshVertices = go.GetComponent<MeshFilter>().sharedMesh.vertices;
			var newMeshVertices = result.vertices;

			Quaternion accRot = Quaternion.identity;

			Assert.AreEqual(oldMeshVertices.Length * chainCount, newMeshVertices.Length);

			float y = 0f;
			for (int chainNum = 0; chainNum < chainCount; chainNum++)
			{
				for (int i = 0; i < oldMeshVertices.Length; i++)
				{
					Vector3 v1 = oldMeshVertices[i];
					Vector3 v2 = newMeshVertices[chainNum * oldMeshVertices.Length + i];

					v1 = accRot * v1;
					v1.x += y;

					AreEqual(v1, v2);
				}

				y += chainStep;
				accRot *= rotation;
			}
		}

		private static void CheckNormals(GameObject go, Mesh result, Quaternion rotation,
			int chainCount, float chainStep)
		{
			var oldMeshNormals = go.GetComponent<MeshFilter>().sharedMesh.normals;
			var newMeshNormals = result.normals;

			Quaternion accRot = Quaternion.identity;

			Assert.AreEqual(oldMeshNormals.Length * chainCount, newMeshNormals.Length);

			for (int chainNum = 0; chainNum < chainCount; chainNum++)
			{
				for (int i = 0; i < oldMeshNormals.Length; i++)
				{
					Vector3 v1 = oldMeshNormals[i];
					Vector3 v2 = newMeshNormals[chainNum * oldMeshNormals.Length + i];

					v1 = accRot * v1;

					AreEqual(v1, v2);
				}

				accRot *= rotation;
			}
		}

		private static void CheckUVs(GameObject go, Mesh result, int uvChannel,
			int chainCount, float chainStep)
		{
			var uvsOld = new List<Vector2>();
			var uvsNew = new List<Vector2>();
			go.GetComponent<MeshFilter>().sharedMesh.GetUVs(uvChannel, uvsOld);
			result.GetUVs(uvChannel, uvsNew);

			CheckDataDupls<Vector2>(chainCount, uvsOld, uvsNew);
		}

		private static void CheckDataDupls<T>(int chainCount, List<T> oldData, List<T> newData)
		{
			Assert.AreEqual(oldData.Count * chainCount, newData.Count);

			for (int chainNum = 0; chainNum < chainCount; chainNum++)
			{
				for (int i = 0; i < oldData.Count; i++)
				{
					var d1 = oldData[i];
					var d2 = newData[chainNum * oldData.Count + i];

					Assert.AreEqual(d1, d2);
				}
			}
		}

		private static void AreEqual(Vector3 expected, Vector3 actual)
		{
			var r = expected - actual;
			if (Mathf.Abs(r.x) > 0.000001f |
				Mathf.Abs(r.y) > 0.000001f |
				Mathf.Abs(r.z) > 0.000001f)
			{
				Assert.Fail(
					"Expected: {0}" + Environment.NewLine +
					"  But was:  {1}", expected, actual);
			}
		}

		private static GameObject GenerateGO()
		{
			var go = new GameObject();
			var meshFilter = go.AddComponent<MeshFilter>();
			var meshRenderer = go.AddComponent<MeshRenderer>();

			var mesh = new Mesh();
			mesh.vertices = new Vector3[]
			{
				new Vector3(-1, -1, 0),
				new Vector3( 0,  1, 0),
				new Vector3( 1, -1, 0),
			};
			mesh.normals = new Vector3[]
			{
				Vector3.back,
				Vector3.back,
				Vector3.back,
			};
			mesh.uv = new Vector2[]
			{
				Vector3.zero,
				Vector3.up,
				Vector3.forward,
			};
			mesh.uv2 = new Vector2[]
			{
				Vector3.zero,
				Vector3.up,
				Vector3.forward,
			};
			mesh.uv3 = new Vector2[]
			{
				Vector3.zero,
				Vector3.up,
				Vector3.forward,
			};
			mesh.uv4 = new Vector2[]
			{
				Vector3.zero,
				Vector3.up,
				Vector3.forward,
			};
			mesh.colors = new Color[]
			{
				Color.red,
				Color.green,
				Color.blue,
			};
			mesh.colors32 = new Color32[]
			{
				Color.red,
				Color.green,
				Color.blue,
			};

			meshFilter.sharedMesh = mesh;

			meshRenderer.materials = new Material[]
			{
				new Material(Shader.Find("Standard")),
			};

			return go;
		}
	}
}
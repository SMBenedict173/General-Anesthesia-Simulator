using System;
using UnityEngine;

namespace BzKovSoft.RopeGenerator.MeshGenerator
{

	/// <summary>
	/// Mesh generator that produces a cylinder
	/// </summary>
	public class RopeMeshCylinderGenerator : IRopeMeshGenerator
	{
		public Material _material;
		int _segmentsPerBone = 4;

		/// <summary>
		/// Sets the number of sides around the cylinder.
		/// </summary>
		public int Sides { get; set; }

		public float RadiusStart { get; set; }
		public float RadiusStop { get; set; }

		/// <summary>
		/// Sets the number of divisions along the bone's major axis.
		/// </summary>
		public int SegmentsPerBone
		{
			get { return _segmentsPerBone; }
			set
			{
				if (value < 1)
					throw new ArgumentException("'SegPerBone' must be >= 2");

				_segmentsPerBone = value;
			}
		}

		public RopeMeshCylinderGenerator()
		{
			Sides = 6;
			RadiusStart = 1f;
			RadiusStop = 1f;
		}

		public MeshGenerationResult Create(float radius, float length, int boneCount, bool restrictFirstBone)
		{
			int totalSegments = (boneCount - 1) * _segmentsPerBone + 1;

			var mesh = CreateMesh(length, totalSegments);
			CalculateWeights(mesh, boneCount, totalSegments, restrictFirstBone);
			MeshGenerationResult rez;
			rez.mesh = mesh;
			rez.materials = new Material[] { _material };
			return rez;
		}

		private Mesh CreateMesh(float length, int segments)
		{
			int sides = Sides;
			int sidesP = sides + 1;

			#region Vertices
			Vector3[] vertices = new Vector3[sidesP * (segments) + 2];
			vertices[0] = Vector3.zero;
			vertices[vertices.Length - 1] = new Vector3(length, 0f, 0f);

			const float _2pi = Mathf.PI * 2f;

			for (int segment = 0; segment < segments; segment++)
			{
				var r = segment / (segments - 1f);
				float a1 = r * length;
				float radius = Mathf.Lerp(RadiusStart, RadiusStop, r);

				for (int side = 0; side < sidesP; side++)
				{
					float a2 = _2pi * side / (sidesP - 1);
					float sin2 = Mathf.Sin(a2);
					float cos2 = Mathf.Cos(a2);

					vertices[
						1 +                 //
						segment * sidesP +  // lat shift
						side                // lon shift
						] = new Vector3(a1, cos2 * radius, sin2 * radius);
				}
			}
			#endregion

			#region Normales
			Vector3[] normales = new Vector3[vertices.Length];
			normales[0] = Vector3.up;
			normales[normales.Length - 1] = Vector3.down;

			for (int n = 1; n < vertices.Length - 1; n++)
			{
				var v = vertices[n];
				v.x = 0;
				normales[n] = v.normalized;
			}
			#endregion

			#region UVs

			Vector2[] uvs = new Vector2[vertices.Length];
			uvs[0] = new Vector2(0.5f, -0.8f);
			uvs[uvs.Length - 1] = new Vector2(0.5f, 0.2f);

			for (int segment = 0; segment < segments; segment++)
			{
				float a1 = 1f - segment / (segments - 1f);

				for (int side = 0; side < sidesP; side++)
				{
					float a2 = side / (float)sides;

					uvs[
						1 +
						segment * sidesP +  // lat shift
						side             // lon shift
						] = new Vector2(a2, a1);
				}
			}
			#endregion

			#region Triangles

			int countOfCapTrs = sides;
			int[] triangles = new int[((segments - 1) * sides * 2 + countOfCapTrs * 2) * 3];
			int i = 0;



			// opening cap
			for (int side = 0; side < sides; side++)
			{
				int current = 0 * sidesP + side + 1;
				int currentP1 = current + 1;

				triangles[i++] = 0;

				if (length > 0)
				{
					triangles[i++] = currentP1;
					triangles[i++] = current;
				}
				else
				{
					triangles[i++] = current;
					triangles[i++] = currentP1;
				}
			}

			// body
			for (int segment = 0; segment < segments - 1; segment++)
			{
				for (int side = 0; side < sides; side++)
				{
					int current = segment * sidesP + side + 1;
					int next = current + sidesP;

					int currentP1 = current + 1;
					int nextP1 = next + 1;

					triangles[i++] = current;

					if (length > 0)
					{
						triangles[i++] = currentP1;
						triangles[i++] = next;
						triangles[i++] = currentP1;
						triangles[i++] = nextP1;
						triangles[i++] = next;
					}
					else
					{
						triangles[i++] = next;
						triangles[i++] = currentP1;
						triangles[i++] = currentP1;
						triangles[i++] = next;
						triangles[i++] = nextP1;
					}
				}
			}

			// closing cap
			for (int side = 0; side < sides; side++)
			{
				int current = (segments - 1) * sidesP + side + 1;
				int currentP1 = current + 1;

				triangles[i++] = vertices.Length - 1;

				if (length > 0)
				{
					triangles[i++] = current;
					triangles[i++] = currentP1;
				}
				else
				{
					triangles[i++] = currentP1;
					triangles[i++] = current;
				}
			}

			#endregion

			Mesh mesh = new Mesh();
			mesh.vertices = vertices;
			mesh.normals = normales;
			mesh.uv = uvs;
			mesh.triangles = triangles;

			mesh.RecalculateBounds();

			return mesh;
		}

		private void CalculateWeights(Mesh mesh, int boneCount, int totalSegments, bool restrictFirstBone)
		{
			int sidesP = Sides + 1;
			BoneWeight[] weights = new BoneWeight[totalSegments * sidesP + 2];

			for (int bi = 0; bi < boneCount - 1; bi++)
			{
				int boneIndex0 = bi;
				int boneIndex1 = bi + 1;

				for (int segment = 0; segment < _segmentsPerBone; segment++)
				{
					float r = (float)segment / _segmentsPerBone;
					r = Mathf.SmoothStep(0, 1, r);

					for (int i = 0; i < sidesP; i++)
					{
						int index = bi * _segmentsPerBone * sidesP + segment * sidesP + i + 1;

						var weight = weights[index];


						if (bi != 0 | restrictFirstBone)
						{
							weight.boneIndex0 = boneIndex0;
							weight.weight0 = 1f - r;
							weight.boneIndex1 = boneIndex1;
							weight.weight1 = r;

							if (weight.weight0 > 1 | weight.weight1 > 1)
								throw new InvalidOperationException();
						}
						else
						{
							weight.boneIndex0 = 1;
							weight.weight0 = 1f;
						}

						weights[index] = weight;
					}
				}
			}

			for (int i = 0; i < sidesP; i++)
			{
				int index = (boneCount - 1) * _segmentsPerBone * sidesP + i + 1;

				var weight = weights[index];

				weight.boneIndex0 = boneCount - 2;
				weight.weight0 = 0f;
				weight.boneIndex1 = boneCount - 1;
				weight.weight1 = 1f;

				weights[index] = weight;
			}

			// opening cap
			var weightLast = weights[0];
			weightLast.boneIndex0 = 0;
			weightLast.weight0 = 1f;
			weights[0] = weightLast;

			// closing cap
			var weightFirst = weights[weights.Length - 1];
			weightFirst.boneIndex0 = boneCount - 2;
			weightFirst.weight0 = 0f;
			weightFirst.boneIndex1 = boneCount - 1;
			weightFirst.weight1 = 1f;
			weights[weights.Length - 1] = weightFirst;

			// A BoneWeights array (weights) was just created and the boneIndex and weight assigned.
			// The weights array will now be assigned to the boneWeights array in the Mesh.
			mesh.boneWeights = weights;
		}
	}
}

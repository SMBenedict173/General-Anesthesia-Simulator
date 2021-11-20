using UnityEngine;

namespace BzKovSoft.RopeGenerator.MeshGenerator
{
	public struct MeshGenerationResult
	{
		public Mesh mesh;
		public Material[] materials;

		public MeshGenerationResult(Mesh mesh, Material[] materials)
		{
			this.mesh = mesh;
			this.materials = materials;
		}
	}
}

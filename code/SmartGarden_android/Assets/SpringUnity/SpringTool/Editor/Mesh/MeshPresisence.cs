
using UnityEditor;
using UnityEngine;

namespace SpringTool
{
    public class MeshPresistence
    {
        [MenuItem("SpringTools/Mesh/Presistence")]
        public static void Presistence()
        {
            GameObject selectedGo = Selection.activeGameObject;
            MeshFilter meshFilter = selectedGo.GetComponent<MeshFilter>();
            meshFilter.mesh = selectedGo.GetComponent<LowPoly>().GenerateLowPoly();
        }
    }
}
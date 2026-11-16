using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace _Project.Develop.Editor.Generators
{
    public static class UnityLayersGenerator
    {
        private const string FilePath = "Assets/_Project/Develop/Runtime/Utilities/UnityLayers.cs";
        private const string ClassName = "UnityLayers";

        [MenuItem("Tools/Generate Unity Layers")]
        public static void Generate()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("using UnityEngine;");
            sb.AppendLine();
            sb.AppendLine("public static class " + ClassName);
            sb.AppendLine("{");

            for (int i = 0; i < 32; i++)
            {
                string layerName = LayerMask.LayerToName(i);
                if (string.IsNullOrEmpty(layerName)) continue;
                
                string validName = layerName.Replace(" ", "");
                
                sb.AppendLine($"    public static readonly int Layer{validName} = LayerMask.NameToLayer(\"{layerName}\");");
                sb.AppendLine($"    public static readonly int LayerMask{validName} = 1 << Layer{validName};");
            }

            sb.AppendLine("}");

            File.WriteAllText(FilePath, sb.ToString());
            AssetDatabase.Refresh();
            Debug.Log("UnityLayers generated!");
        }
    }
}
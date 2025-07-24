using UnityEditor;
using System.IO;

public class CreateReadme
{
    [MenuItem("Assets/Create/README.txt", false, 80)]
    private static void CreateReadmeFile()
    {
        string path = AssetDatabase.GetAssetPath(Selection.activeObject);
        if (string.IsNullOrEmpty(path))
            path = "Assets";

        if (!Directory.Exists(path))
            path = Path.GetDirectoryName(path);

        string filePath = Path.Combine(path, "####README####.txt");

        if (!File.Exists(filePath))
        {
            File.WriteAllText(filePath, "");
            AssetDatabase.Refresh();
        }
        else
        {
            EditorUtility.DisplayDialog("README.txt", "Esiste già un README.txt in questa cartella!", "OK");
        }
    }
}

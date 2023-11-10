using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

[CustomEditor(typeof(CubeController))]
public class CubeEditor : Editor
{
    public override VisualElement CreateInspectorGUI()
    {
    // Create a new VisualElement to be the root of our inspector UI
    VisualElement myInspector = new VisualElement();

    // Load and clone a visual tree from UXML
    VisualTreeAsset visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Scripts/Editor/CubeEditorUXML.uxml");
    visualTree.CloneTree(myInspector);

    // Return the finished inspector UI
    return myInspector;
    }

}

using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using System;

public class SpawnObject : EditorWindow
{
    [SerializeField] VisualTreeAsset tree;
    Toggle activeToggle;
    Toggle alignNormalToggle;
    LayerMaskField layerInput;
    Vector3Field minRotation;
    Vector3Field maxRotation;
    FloatField minScale;
    FloatField maxScale;

    GameObject prefab;

    [MenuItem("Tools/SpawnPrefab")]
    public static void ShowEditor()
    {
        var window = GetWindow<SpawnObject>();
        window.titleContent = new GUIContent("Prefab Spawner");
    }
    
    private void OnEnable() 
    {
        SceneView.duringSceneGui += OnSceneGUI;
    }

    private void OnSceneGUI(SceneView view)
    {
        if(!activeToggle.value) return;

        Event evt = Event.current;
        if(IsLeftMouseButtonDown(evt))
        {
            var ray = HandleUtility.GUIPointToWorldRay(evt.mousePosition);
            Physics.Raycast(ray, out var raycastHit, Mathf.Infinity, layerInput.value);
            if(raycastHit.collider)
            {
                var obj = CreatePrefab(raycastHit.point);
                ApplyRandomRotation(obj, raycastHit.normal);
                ApplyRandomScale(obj);
                Undo.RegisterCreatedObjectUndo(obj, "Spawned Object");
            }
        }
        
    }

    GameObject CreatePrefab(Vector3 position)
    {
        var obj = PrefabUtility.InstantiatePrefab(prefab) as GameObject;
        obj.transform.position = position;
        return obj;
    }


    void ApplyRandomRotation(GameObject obj, Vector3 normal)
    {
        if(alignNormalToggle.value)
        {
            obj.transform.rotation = Quaternion.FromToRotation(Vector3.up, normal);
        }

        var rotationInEuler = obj.transform.rotation.eulerAngles;
        obj.transform.rotation = Quaternion.Euler
        (
            rotationInEuler.x + UnityEngine.Random.Range(minRotation.value.x, maxRotation.value.x),
            rotationInEuler.y + UnityEngine.Random.Range(minRotation.value.y, maxRotation.value.y),
            rotationInEuler.z + UnityEngine.Random.Range(minRotation.value.z, maxRotation.value.z)
        );
    }

    void ApplyRandomScale(GameObject obj)
    {
        obj.transform.localScale = Vector3.one * UnityEngine.Random.Range(minScale.value, maxScale.value);
    }

    Boolean IsLeftMouseButtonDown(Event evt)
    {
        return evt.type == EventType.MouseDown && evt.button == 0;
    }

    void CreateGUI()
    {
        tree.CloneTree(rootVisualElement);
        activeToggle = rootVisualElement.Q<Toggle>("Active");
        alignNormalToggle = rootVisualElement.Q<Toggle>("AlignNormal");
        layerInput = rootVisualElement.Q<LayerMaskField>("Layer");
        minRotation = rootVisualElement.Q<Vector3Field>("MinRotation");
        maxRotation = rootVisualElement.Q<Vector3Field>("MaxRotation");
        minScale = rootVisualElement.Q<FloatField>("MinScale");
        maxScale = rootVisualElement.Q<FloatField>("MaxScale");

        var prefabInput = rootVisualElement.Q<ObjectField>("Prefab");
        prefabInput.RegisterValueChangedCallback(evt =>
        {
            prefab = evt.newValue as GameObject;
        });
    }
}

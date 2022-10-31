using UnityEngine;
using UnityEditor;

public enum TileTypeE
{
    Empty,
    TurnPoint,
    FinishTile,
}

public class LevelGenerator : EditorWindow
{
    public TileTypeE MyTileType = TileTypeE.Empty;
    public Object EmptyTilePrefab;
    public Object TurnPointPrefab;
    public Object FinishTilePrefab;
    
    // Add menu named "My Window" to the Window menu
    [MenuItem("Game/Level Generator")]
    static void Init()
    {
        LevelGenerator window = (LevelGenerator)EditorWindow.GetWindow(typeof(LevelGenerator));
        window.Show();
    }

    void OnGUI()
    {
        MyTileType = (TileTypeE)EditorGUILayout.EnumPopup("Primitive to create:", MyTileType);
        
        EditorGUILayout.Space(10);
        
        if (GUILayout.Button("Change"))
        {
            InstantiateTilePrefab(MyTileType);
        }
        
        EditorGUILayout.Space(30);

        // empty tile prefab field
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("empty tile", EditorStyles.boldLabel);
        EmptyTilePrefab = EditorGUILayout.ObjectField(EmptyTilePrefab, typeof(Object), true);
        EditorGUILayout.EndHorizontal();
        
        // turn point prefab field
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("turn point", EditorStyles.boldLabel);
        TurnPointPrefab = EditorGUILayout.ObjectField(TurnPointPrefab, typeof(Object), true);
        EditorGUILayout.EndHorizontal();

        // finish tile prefab field
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("finish tile", EditorStyles.boldLabel);
        FinishTilePrefab = EditorGUILayout.ObjectField(FinishTilePrefab, typeof(Object), true);
        EditorGUILayout.EndHorizontal();
    }


    void InstantiateTilePrefab(TileTypeE tileTypeE)
    {
        GameObject obj = Selection.activeGameObject;
        
        if (obj == null) return;
        if (obj.GetComponent<Tile>() == null) return;
        if (EmptyTilePrefab == null) return;
        if (TurnPointPrefab == null) return;
        
        Vector3 pos = obj.transform.position;
        Transform parent = obj.transform.parent;
        string tileName = obj.name;
        DestroyImmediate(obj);
        GameObject tile  = null;
        
        switch (tileTypeE)
        {
            case TileTypeE.Empty:
                tile = PrefabUtility.InstantiatePrefab(EmptyTilePrefab, parent) as GameObject;
                break;
            case TileTypeE.TurnPoint:
                tile = PrefabUtility.InstantiatePrefab(TurnPointPrefab, parent) as GameObject;
                break;
                case TileTypeE.FinishTile:
                tile = PrefabUtility.InstantiatePrefab(FinishTilePrefab, parent) as GameObject;
                break;
            default:
                throw new System.ArgumentOutOfRangeException(nameof(tileTypeE), tileTypeE, null);
        }

        tile.transform.position = pos;
        tile.gameObject.name = tileName;
    }
}
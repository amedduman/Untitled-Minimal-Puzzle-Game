using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

public enum TileTypeE
{
    Empty,
    TurnPoint,
    FinishTile,
    HazardTile,
    BlockTile,
}

public class LevelGenerator : EditorWindow
{
    public TileTypeE MyTileType = TileTypeE.Empty;
    public Object EmptyTilePrefab;
    public Object TurnPointPrefab;
    public Object FinishTilePrefab;
    public Object HazardTilePrefab;
    public Object BlockTilePrefab;

    int TurnPointRotVal = 1;

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

        // hazard tile prefab field
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("hazard tile", EditorStyles.boldLabel);
        HazardTilePrefab = EditorGUILayout.ObjectField(HazardTilePrefab, typeof(Object), true);
        EditorGUILayout.EndHorizontal();

        // block tile prefab field
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("block tile", EditorStyles.boldLabel);
        BlockTilePrefab = EditorGUILayout.ObjectField(BlockTilePrefab, typeof(Object), true);
        EditorGUILayout.EndHorizontal();

        GameObject obj = Selection.activeGameObject;
        if (obj != null)
        {
            if (obj.TryGetComponent(out TurnPoint tp))
            {
                EditorGUILayout.Space(15);
                EditorGUILayout.BeginHorizontal();
                TurnPointRotVal = EditorGUILayout.IntSlider(TurnPointRotVal, 1, 4);
                float angle = 0;
                switch (TurnPointRotVal)
                {
                    case 1:
                    angle = 0;
                    break;
                    case 2:
                    angle = 90;
                    break;
                    case 3:
                    angle = 180;
                    break;
                    case 4:
                    angle = 270;
                    break;
                    default:
                    Debug.LogError($"There is an error with level generator.");
                    break;
                }
                tp.transform.localRotation = Quaternion.Euler(0,0,angle);
                EditorGUILayout.EndHorizontal();
            }
        }

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
        GameObject tile = null;

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
                case TileTypeE.HazardTile:
                tile = PrefabUtility.InstantiatePrefab(HazardTilePrefab, parent) as GameObject;
                break;
                case TileTypeE.BlockTile:
                tile = PrefabUtility.InstantiatePrefab(BlockTilePrefab, parent) as GameObject;
                break;
            default:
                throw new System.ArgumentOutOfRangeException(nameof(tileTypeE), tileTypeE, null);
        }

        tile.transform.position = pos;
        tile.gameObject.name = tileName;

        EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
    }
}
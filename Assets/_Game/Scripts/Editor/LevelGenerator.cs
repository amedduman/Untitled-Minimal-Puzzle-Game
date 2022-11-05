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
    static LevelGenerator _instance;
    public TileTypeE MyTileType = TileTypeE.Empty;
    public Object EmptyTilePrefab;
    public Object TurnPointPrefab;
    public Object FinishTilePrefab;
    public Object HazardTilePrefab;
    public Object BlockTilePrefab;

    static int _turnPointRotVal = 0;

    [MenuItem("Game/Level Generator/Open Window")]
    static void Init()
    {
        LevelGenerator window = (LevelGenerator)EditorWindow.GetWindow(typeof(LevelGenerator));
        window.Show();
    }

    #region ShortcutsForGeneratingTiles

    [MenuItem("Game/Level Generator/CreateEmptyTile _1")]
    static void CreateEmptyTilePrefab()
    {
        _instance.InstantiateTilePrefab(TileTypeE.Empty);
    }

    [MenuItem("Game/Level Generator/CreateTurnTile _2")]
    static void CreateTurnTilePrefab()
    {
        _instance.InstantiateTilePrefab(TileTypeE.TurnPoint);
    }

    [MenuItem("Game/Level Generator/CreateFinishTile _3")]
    static void CreateFinishTilePrefab()
    {
        _instance.InstantiateTilePrefab(TileTypeE.FinishTile);
    }

    [MenuItem("Game/Level Generator/CreateHazardTile _4")]
    static void CreateHazardTilePrefab()
    {
        _instance.InstantiateTilePrefab(TileTypeE.HazardTile);
    }

    [MenuItem("Game/Level Generator/CreateBlockTile _5")]
    static void CreateBlockTilePrefab()
    {
        _instance.InstantiateTilePrefab(TileTypeE.BlockTile);
    }

    [MenuItem("Game/Level Generator/ChangeDirOfTurnTile _w")]
    static void ChangeDirOfTurnTile()
    {
        _turnPointRotVal += 1;
        _turnPointRotVal = _turnPointRotVal %3;
        UpdateRotOfTurnTile();
    }


    #endregion


    void OnGUI()
    {
        _instance = this;

        MyTileType = (TileTypeE)EditorGUILayout.EnumPopup("Tile to create:", MyTileType);

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
    }

    static void UpdateRotOfTurnTile()
    {
        GameObject obj = Selection.activeGameObject;
        if (obj != null)
        {
            if (obj.TryGetComponent(out TurnPoint tp))
            {
                // EditorGUILayout.Space(15);
                // EditorGUILayout.BeginHorizontal();
                //TurnPointRotVal = EditorGUILayout.IntSlider(TurnPointRotVal, 1, 4);
                float angle = 0;
                switch (_turnPointRotVal)
                {
                    case 0:
                        angle = 0;
                        break;
                    case 1:
                        angle = 90;
                        break;
                    case 2:
                        angle = 180;
                        break;
                    case 3:
                        angle = 270;
                        break;
                    default:
                        Debug.LogError($"There is an error with level generator.");
                        break;
                }
                tp.transform.localRotation = Quaternion.Euler(0, 0, angle);
                // EditorGUILayout.EndHorizontal();
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

// Texture2D t = (Texture2D)AssetDatabase.LoadAssetAtPath("Assets/Textures/texture.jpg", typeof(Texture2D));
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System.Linq;

public class Test_LevelGenerator : SerializedMonoBehaviour
{
    
    [TableMatrix(HorizontalTitle = "Square Celled Matrix", SquareCells = true, RowHeight = 40)]
    public GameObject[,] go = new GameObject[4, 8];


    [SerializeField] int _columns = 4;
    [SerializeField] int rows = 8; 

    [Button]
    void ChangeTileSize()
    {
        go = new GameObject[_columns, rows];
    }

}

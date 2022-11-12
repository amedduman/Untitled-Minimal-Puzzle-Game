using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KevinCastejon.MoreAttributes;

public class TestTagAttribute : MonoBehaviour
{
    [Tag]
    [SerializeField] private string _targetTag;
}

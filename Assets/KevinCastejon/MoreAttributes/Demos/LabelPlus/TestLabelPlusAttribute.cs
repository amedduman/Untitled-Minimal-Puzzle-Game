using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KevinCastejon.MoreAttributes;

public class TestLabelPlusAttribute : MonoBehaviour
{
    [LabelPlus("Assets/KevinCastejon/MoreAttributes/Demos/LabelPlus/Icons/greencrossicon.png", "Health Points", (int)LabelPlusColor.green)]
    [SerializeField] private int _healthPoints;
    [LabelPlus("Assets/KevinCastejon/MoreAttributes/Demos/LabelPlus/Icons/atk.png", "Damages", new float[] { 1f, 0f, 0f, 1f })]
    [SerializeField] private int _damages;
}

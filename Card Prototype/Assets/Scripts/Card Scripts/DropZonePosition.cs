using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New DropZonePosition", menuName = "DropZone Position")]
public class DropZonePosition : ScriptableObject
{
    public int Row;
    public int Column;
}
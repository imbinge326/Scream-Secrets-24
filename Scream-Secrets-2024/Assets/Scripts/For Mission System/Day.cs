using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

[System.Serializable]
public class Day
{
    public string dayName;           // Name of the day or sequence
    public List<Mission> missions;   // List of missions for the day
}

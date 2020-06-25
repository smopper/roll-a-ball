using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Jumping : MonoBehaviour
{
    public void DeleteSaves()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
    }
}

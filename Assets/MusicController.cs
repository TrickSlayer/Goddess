using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    public static MusicController instance;
    private void Awake() // Ch?y tr??c void Start ()
    {
        DontDestroyOnLoad(this.gameObject); // Kh�ng ph� h?y tr� ch?i n�y ??i t??ng khi t?i c�c c?nh kh�c nhau

        if (instance == null) // N?u bi?n phi�n b?n MusicControlScript l� null
        {
            instance = this; // ??t ??i t??ng n�y l�m ??i t??ng
        }
        else // N?u ?� c� m?t phi�n b?n MusicControlScript ?ang ho?t ??ng
        {
            Destroy(gameObject); // Ph� h?y tr� ch?i n�y
        }
    }
}
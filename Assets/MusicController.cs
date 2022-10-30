using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    public static MusicController instance;
    private void Awake() // Ch?y tr??c void Start ()
    {
        DontDestroyOnLoad(this.gameObject); // Không phá h?y trò ch?i này ??i t??ng khi t?i các c?nh khác nhau

        if (instance == null) // N?u bi?n phiên b?n MusicControlScript là null
        {
            instance = this; // ??t ??i t??ng này làm ??i t??ng
        }
        else // N?u ?ã có m?t phiên b?n MusicControlScript ?ang ho?t ??ng
        {
            Destroy(gameObject); // Phá h?y trò ch?i này
        }
    }
}
using UnityEngine;

public class WinVfx : MonoBehaviour
{
    public static void Instant()
    {
        var go = Instantiate(Resources.Load<GameObject>("win-vfx"));
        Destroy(go, 3.0f);
    }
}

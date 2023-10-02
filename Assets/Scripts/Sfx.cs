using UnityEngine;

public class Sfx : MonoBehaviour
{
    public static void Instant()
    {
        var go = Instantiate(Resources.Load<GameObject>("sfx"));
        Destroy(go, 1.0f);
    }
}

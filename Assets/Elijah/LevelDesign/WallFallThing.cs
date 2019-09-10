using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class WallFallThing : MonoBehaviour
{
    public PlayerButtonZone zone;
    public WallFall exploder;
    public List<GameObject> deleteThese = new List<GameObject>();
    public float deleteDelay = 3;

    private void Start()
    {
        zone.onPressed -= OnPressed;
        zone.onPressed += OnPressed;
    }

    private void OnDestroy()
    {
        zone.onPressed -= OnPressed;
    }

    private void OnPressed()
    {
        exploder.Explode();

        foreach (var go in deleteThese)
        {
            Destroy(go, deleteDelay);
        }
        deleteThese.Clear();

        Destroy(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPanel : MonoBehaviour
{
    private GameObject panel_;
    private GameObject panel
    {
        get { return panel_; }
        set
        {
            if (panel_) { Destroy(panel_); }
            panel_ = value;
        }
    }

    public void Begin()
    {
        StartCoroutine(PanelChanger());
    }

    private IEnumerator PanelChanger()
    {
        bool nextSafe = true;
        while (true)
        {
            var kindMap = FindObjectOfType<PanelKindMap>();
            var kind = nextSafe ? kindMap.RandomSafeKind() : kindMap.RandomKind();
            nextSafe = !nextSafe;
            panel = Instantiate(kind.prefab, transform, false);
            panel.transform.localPosition = Vector3.zero;
            panel.transform.localRotation = Quaternion.identity;
            yield return new WaitForSeconds(kind.duration);
        }
    }
}

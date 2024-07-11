using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapUI : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        UIController.Instance.TurnUIOn(UIController.UIEnum.UIPanel);
    }
    void OnDestroy()
    {
        UIController.Instance.TurnUIOff(UIController.UIEnum.UIPanel);
    }

}

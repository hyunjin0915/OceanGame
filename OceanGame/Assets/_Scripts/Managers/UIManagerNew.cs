using UnityEngine;
using UnityEngine.UI;

public class UIManagerNew : MonoBehaviour
{
    [SerializeField]
    private HealthManagerScriptableObject healthManagerScriptableObject;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        // using event 
        //healthManagerScriptableObject.healthChangeEvent.AddListener();
    }
    private void OnDisable()
    {
        //healthManagerScriptableObject.healthChangeEvent.RemoveListener();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

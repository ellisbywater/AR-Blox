using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class Builder : MonoBehaviour
{
    [SerializeField] private GameObject demoBlock;
    private ARRaycastManager _raycastManager;
    // Start is called before the first frame update
    void Start()
    {
        _raycastManager = GetComponent<ARRaycastManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnBuildButtonPressed()
    {
        List<ARRaycastHit> hits = new List<ARRaycastHit>();
        _raycastManager.Raycast(Camera.main.ViewportPointToRay(new Vector2(.5f, .5f)), hits, TrackableType.Planes);
        if (hits.Count > 0)
        {
            Vector3 buildablePositon = new Vector3(Mathf.Round(hits[0].pose.position.x / 1) * 1, hits[0].pose.position.y, Mathf.Round(hits[0].pose.position.z / 1) * 1);
            Quaternion buildableRotation = hits[0].pose.rotation;
            Build(buildablePositon, buildableRotation);
        }
    }

    void Build(Vector3 position, Quaternion rotation)
    {
        Instantiate(demoBlock, position, rotation);
    }
}

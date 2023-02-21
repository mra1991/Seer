using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class CameraFocus : MonoBehaviour
{
    [SerializeField] private float maxDistance = 20f;
    [Tooltip("Include all layers except for Player.")]
    [SerializeField] private LayerMask focusLayer;
    [SerializeField] private PostProcessVolume postProcessVol;
    private DepthOfField depthOfField;
    const float transitionSpeed = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        //Try to get the setting depthOfField from the component postProcessVol, if it exists.
        postProcessVol.sharedProfile.TryGetSettings<DepthOfField>(out depthOfField);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        RaycastHit hit;
        float pos = maxDistance; //distance of the object, if the raycast hits any

        //Raycast, start from camera's position, in forward direction, output hit, for a maximum distance of maxDistance, hitting only fucusLayer(avoid Player)
        if (Physics.Raycast(transform.position, transform.forward, out hit, maxDistance, focusLayer))
        {
            pos = hit.distance;
        }
        if (depthOfField) //making sure this setting is present and retrieved
        {
            //adjust the focus distance to the hit distance, but do so gradually, by using linear interpolation
            depthOfField.focusDistance.value = Mathf.Lerp(depthOfField.focusDistance.value, pos, transitionSpeed);
        }
    }
}

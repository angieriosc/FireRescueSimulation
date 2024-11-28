using UnityEngine;

/// <summary>
/// This follow player class will update the events from the camera that follows the vehicle.
/// Standar coding documentation can be found in 
/// https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/language-specification/documentation-comments
/// </summary>
public class FollowPlayer : MonoBehaviour
{
    public GameObject player;
    private Vector3 offset = new Vector3(0, 0, 0);

    /// <summary>
    /// This method is called before the first frame update
    /// </summary>
    void Start()
    {
        // Optional: Initialize the offset based on the camera's initial position
        offset = transform.position - player.transform.position;
    }

    /// <summary>
    /// This method is called once per frame after all Update methods
    /// </summary>
    void LateUpdate()
    {
        // Update the camera's position based on the player's position and offset
        transform.position = player.transform.position + player.transform.TransformDirection(offset);

        // Match the camera's rotation to the player's rotation
        transform.rotation = player.transform.rotation;
    }
}
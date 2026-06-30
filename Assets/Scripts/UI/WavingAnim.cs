using UnityEngine;

public class WavingAnim : MonoBehaviour
{
    public float speed = 1f;
    public float amplitude = 1f;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * Mathf.Sin(Time.time * speed) * amplitude * Time.deltaTime);
    }
}

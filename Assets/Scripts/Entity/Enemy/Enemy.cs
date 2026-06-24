using UnityEngine;

public class Enemy : BaseUnit
{
    private void FixedUpdate()
    {
        transform.Translate(Vector3.right * entitySOData.unitdata.speed * 0.05f * Time.fixedDeltaTime);

    }
    private void Update()
    {
    }
    
}

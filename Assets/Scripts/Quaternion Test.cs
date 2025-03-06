using UnityEngine;

public class QuaternionTest : MonoBehaviour
{
    public Vector4 testingRotations;//

    // Update is called once per frame
    void Update()
    {
        testingRotations.Normalize();
        transform.rotation = new Quaternion(testingRotations.x, testingRotations.y, testingRotations.z, testingRotations.w);
    }
}

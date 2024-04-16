using UnityEngine;

public class CameraFollowComponent : MonoBehaviour
{
    public Transform TargetTrans;
    public Vector3 TargetOffset;
    public float HeightOffset;

    public float MoveSpeed;
    public float RotateSpeed;

    private Vector3 targetPos;
    private Quaternion targetRotate;
    
    void Update()
    {
        if (TargetTrans == null) return;
        targetPos = TargetTrans.position + TargetTrans.forward * TargetOffset.z;
        targetPos += Vector3.up * TargetOffset.y;
        targetPos += TargetTrans.right * TargetOffset.x;

        transform.position = Vector3.Lerp(transform.position, targetPos, MoveSpeed * Time.deltaTime);

        targetRotate = Quaternion.LookRotation(TargetTrans.position + Vector3.up * HeightOffset - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation,targetRotate,RotateSpeed * Time.deltaTime);
    }

    public void SetupTargetPos(Transform target)
    {
        TargetTrans = target;
    }
}

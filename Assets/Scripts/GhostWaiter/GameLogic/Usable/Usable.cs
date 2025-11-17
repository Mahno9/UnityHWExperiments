using UnityEngine;

public abstract class Usable : MonoBehaviour
{
    private const float TAKING_FLOAT_EPSILON = 0.01f;

    [SerializeField] private float _takingFloatSpeed = 10f;

    private Holder _holder;
    protected Holder Holder => _holder;

    public abstract void Use();

    public void SetHolderInstant(Holder holder)
    {
        _holder = holder;
        transform.SetParent(holder.GetJointTransform());
        transform.localPosition = Vector3.zero;
    }

    public void SetHolder(Holder holder)
    {
        _holder = holder;
        if (_holder)
            transform.SetParent(holder.GetJointTransform(), true);
    }

    private void Update()
    {
        UpdateTakingFloatAnimation();
    }

    private void UpdateTakingFloatAnimation()
    {
        if (Holder == null || transform.localPosition.Equals(Vector3.zero))
            return;

        transform.localPosition /= 1 + Time.deltaTime * _takingFloatSpeed;
        if (transform.localPosition.magnitude < TAKING_FLOAT_EPSILON)
            transform.localPosition = Vector3.zero;
    }

}

using UnityEngine;

public interface IDraggable
{
    void StoreHold(Ray intersectRay);
    void MoveTo(Ray    movedRay);
}
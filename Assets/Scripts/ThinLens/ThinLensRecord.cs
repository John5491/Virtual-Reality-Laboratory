using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThinLensRecord
{
    private float objectDistance;
    private float imageDistance;
    private float focalLength;
    private float inverseObjectDistance;
    private float inverseImageDistance;

    public ThinLensRecord(float objectDistance, float imageDistance)
    {
        this.objectDistance = objectDistance;
        this.imageDistance = imageDistance;
    }

    public float getObjectDistance()
    {
        return objectDistance;
    }

    public void setObjectDistance(float objectDistance)
    {
        this.objectDistance = objectDistance;
    }

    public float getImageDistance()
    {
        return imageDistance;
    }

    public void setImageDistance(float imageDistance)
    {
        this.imageDistance = imageDistance;
    }

    public float getFocalLength()
    {
        return focalLength;
    }

    public void setFocalLength(float focalLength)
    {
        this.focalLength = focalLength;
    }

    public float getInverseObjectDistance()
    {
        return inverseObjectDistance;
    }

    public void setInverseObjectDistance(float inverseObjectDistance)
    {
        this.inverseObjectDistance = inverseObjectDistance;
    }

    public float getInverseImageDistance()
    {
        return inverseImageDistance;
    }

    public void setInverseImageDistance(float inverseImageDistance)
    {
        this.inverseImageDistance = inverseImageDistance;
    }

    public void UpdateVariables(float imageDistance, float focalLength, float inverseObjectDistance, float inverseImageDistance)
    {
        this.imageDistance = imageDistance;
        this.focalLength = focalLength;
        this.inverseObjectDistance = inverseObjectDistance;
        this.inverseImageDistance = inverseImageDistance;
    }
}

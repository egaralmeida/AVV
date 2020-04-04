using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ExtensionMethods
{
    // LookAt but for 2D | Thanks robertbu http://answers.unity.com/answers/641238/view.html
    public static void lookAt2D(this Transform myTransform, Vector3 targetPos)
    {
        short spriteAngleCorrection = 270;
        Vector3 dir = targetPos - myTransform.position;
        float spriteAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg + spriteAngleCorrection;
        myTransform.rotation = Quaternion.AngleAxis(spriteAngle, Vector3.forward);
    }

    public static void lookAt2DLocal(this Transform myTransform, Vector3 targetPos)
    {
        short spriteAngleCorrection = 270;
        Vector3 dir = targetPos - myTransform.position;
        float spriteAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg + spriteAngleCorrection;
        myTransform.localRotation = Quaternion.AngleAxis(spriteAngle, Vector3.forward);
    }

    // Arduino map function
    public static float map(this float x, float in_min, float in_max, float out_min, float out_max)
    {
        return (x - in_min) * (out_max - out_min) / (in_max - in_min) + out_min;
    }
}

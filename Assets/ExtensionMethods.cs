using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ExtensionMethods
{
    // LookAt but for 2D | Thanks robertbu http://answers.unity.com/answers/641238/view.html
    public static void lookAt2D(this Transform myTransform, Vector3 targetPos, short spriteAngleCorrection)
    {
        Vector3 dir = targetPos - myTransform.position;
        float spriteAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg + spriteAngleCorrection;
        myTransform.rotation = Quaternion.AngleAxis(spriteAngle, Vector3.forward);
    }

    /* Arduino map function
    public static float map(this float val, float from1, float to1, float from2, float to2)
    {
        return (val - from1) / (to1 - from1) * (to2 - from2) + from2;
    }
    */
    
    // Arduino map function
    public static float map(this float x, float in_min, float in_max, float out_min, float out_max)
    {
        return (x - in_min) * (out_max - out_min) / (in_max - in_min) + out_min;
    }
}

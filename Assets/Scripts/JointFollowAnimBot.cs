using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JointFollowAnimBot : MonoBehaviour
{
   public HingeJoint Osso;
    public bool Inverter;
    public GameObject Motion;

	void Update () {
        JointSpring Js = Osso.spring;

        Js.targetPosition = Motion.transform.localEulerAngles.x;

        if (Js.targetPosition > 180)
            Js.targetPosition = Js.targetPosition - 360;
            //Debug.Log(Js);

        Js.targetPosition = Mathf.Clamp(Js.targetPosition, Osso.limits.min + 5, Osso.limits.max - 5);

            if (Inverter)
            Js.targetPosition = Js.targetPosition * -1;

        Osso.spring = Js;
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EzrealArcaneShift : Ezreal {
    public float maxShiftRange;

    // Update is called once per frame
    
    // Does the actual change itself.
    void Shift(float changeInX, float changeInZ)    {
        // In shift I need to ask a function to determine if we are adding or subtracting to our transform.position
        // two parameters I will need are, changeInX and changeInZ
        // each changeWill then need to be asked if it will be
        if(changeInX > 0){
            transform.position = new Vector3(
                transform.position.x + changeInX,
                transform.position.y,
                transform.position.z
            );
        }
        else    {
            transform.position = new Vector3(
                transform.position.x + changeInX,
                transform.position.y,
                transform.position.z
            );
        }
        // if the change in transform.position.z is greater than 0 we are going towards
        if(changeInZ > 0){
            transform.position = new Vector3(
                transform.position.x,
                transform.position.y,
                transform.position.z - ( changeInZ * - 1)
            );
        }
        else    {
            transform.position = new Vector3(
                transform.position.x,
                transform.position.y,
                transform.position.z + changeInZ
            );
        }
    }
    // The following Methods are for using Shift
    public float ChangeInXDuringShift(Vector3 characterLocation, Vector3 shiftDestination) {
        float rangeOfShiftX = characterLocation.x - shiftDestination.x;
        if(characterLocation.x > shiftDestination.x){
            //Debug.Log("We are going Right or forwards" + rangeOfShiftX);
            return Mathf.Abs(rangeOfShiftX) < maxShiftRange ? Mathf.Abs(rangeOfShiftX) * -1: maxShiftRange * -1;
        }
        else{
            //Debug.Log("We are going Left or backwards" + Mathf.Abs(rangeOfShiftX) + " " + maxShiftRange);
            return Mathf.Abs(rangeOfShiftX) < maxShiftRange ? Mathf.Abs(rangeOfShiftX): maxShiftRange;
        }
    }
    // Calculates the change in X and Z to move the character correctly based on player's desired location
    public float ChangeInZDuringShift(Vector3 characterLocation, Vector3 shiftDestination) {
        float rangeOfShiftZ = characterLocation.z - shiftDestination.z;
        if(characterLocation.z > shiftDestination.z){
            //Debug.Log("We are going Up or upwards " + rangeOfShiftZ);
            return Mathf.Abs(rangeOfShiftZ) < maxShiftRange ? rangeOfShiftZ: maxShiftRange * -1;
        }
        else{
            //Debug.Log("We are going down or downwards " + Mathf.Abs(rangeOfShiftZ) + " " + maxShiftRange);
            return Mathf.Abs(rangeOfShiftZ) < maxShiftRange ? Mathf.Abs(rangeOfShiftZ): maxShiftRange;
        }
    }
    // If there is a target in a nearby area send a missile to them, prioritize champions
}
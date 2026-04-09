//using Unity.VisualScripting;
//using UnityEditor.Build;
using UnityEngine;
using UnityEngine.InputSystem;

public class GodManager : MonoBehaviour
{
    public Transform[] _Gods;
    public int GodSelected =0;
    public float MoveSpeed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_Gods[GodSelected].position.x < -8)
        {
            _Gods[GodSelected].position = new Vector2(-8, _Gods[GodSelected].position.y);
        }
        else if (_Gods[GodSelected].position.x > 2)
        {
            _Gods[GodSelected].position = new Vector2(2, _Gods[GodSelected].position.y);
        }

        if (_Gods[GodSelected].position.y < -3.8f)
        {
            _Gods[GodSelected].position = new Vector2(_Gods[GodSelected].position.x, -3.8f);
        }
        else if (_Gods[GodSelected].position.y > 1.2f)
        {
            _Gods[GodSelected].position = new Vector2(_Gods[GodSelected].position.x, 1.2f);
        }
    }

    public void OnFire()
    {
        if (_Gods[GodSelected].GetComponent<Gods>().Health>0 && _Gods[GodSelected].GetComponent<Gods>().coolDownLeft <=0)
        {
             _Gods[GodSelected].GetComponent<Gods>().GodAttack();
            _Gods[GodSelected].GetComponent<Gods>().coolDownLeft = _Gods[GodSelected].GetComponent<Gods>().coolDown;
        }
       
    }

    public void OnGodOne()
    {
        GodSelected = 0;
    }
    public void OnGodTwo()
    {
        GodSelected = 1;
    }
    public void OnGodThree()
    {
        GodSelected = 2;
    }

    public void OnMove(InputValue dir)
    {
        if (_Gods[GodSelected].GetComponent<Gods>().Health > 0)
        {
            _Gods[GodSelected].GetComponent<Rigidbody2D>().linearVelocity = dir.Get<Vector2>()*MoveSpeed;

        }
    }

}

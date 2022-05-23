using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.SceneManagement;

//deals with all the controls from the xbox 360 remote and mouse 
public class cameraMovement : MonoBehaviour
{
    PlayerControls controls; 
    public int speed; 
    Vector2 move; 
    Vector2 camPan; 
    Vector2 rotation; 
    private Vector3 velocity;

    //for camera movement
    private float xCamRot  = 0f;
    private float yCamRot  = 0f;
    public float sensitivity; 

    void Awake(){
        controls = new PlayerControls(); 

        //left joystick
        controls.gameplay.moveforwards.performed += ctx => move = ctx.ReadValue<Vector2>();
        controls.gameplay.moveforwards.canceled += ctx => move = Vector2.zero; 

        //right joystick 
        controls.gameplay.camerapan.performed += ctx => camPan = ctx.ReadValue<Vector2>();
        controls.gameplay.camerapan.canceled += ctx => camPan = Vector2.zero;

        //triggers 
        controls.gameplay.resetscene.performed += ctx => resetScene();

        //take a screenshot of the scene 
        controls.gameplay.picture.performed += ctx => takePhoto();
    }

    void OnEnable(){
        //enable the use of controls 
        controls.gameplay.Enable();
        speed = 50;
    }

    void OnDisable(){
        //disable them once finished
       controls.gameplay.Disable();
    }

    void resetScene(){
        //reload the scene so it resets and everything restarts with a new seed and positions 
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void takePhoto(){
        //saves the current view of the camera as a png 
        ScreenCapture.CaptureScreenshot("screenshot.png"); 
    }

    void Update(){
        
        //joystick movement ===============
        Vector3 movement = transform.right * move.x + transform.forward * move.y;
        transform.position += (movement * speed * Time.deltaTime);
        transform.position += (velocity * Time.deltaTime);

        //camera rotation =================
        float xAxis  = camPan.x * sensitivity * Time.deltaTime;
        float yAxis  = camPan.y * sensitivity * Time.deltaTime;

        xCamRot  -= yAxis ;
        yCamRot  += xAxis ;

        transform.localRotation = Quaternion.Euler(xCamRot , yCamRot , 0 );
        transform.Rotate(Vector3.up * xAxis );
        transform.Rotate(Vector3.left * yAxis );
        //=============================
    }
}
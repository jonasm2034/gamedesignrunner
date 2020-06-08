using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement2 : MonoBehaviour {



    public CharacterController controller;
    public GameObject follower;

    public float speedvalue = 30f;
    public float gravity = -50f;
    public float jumpHeight = 5f;
    public float maxgrappledistance = 70f;
    float swingspeed;
    float swingTimer = 0.2f;
    bool doubleJumping = false;

    public Transform groundCheck;
    public Transform hitBox;
    public float groundDistance = 0.2f;
    public LayerMask groundMask;

    Vector3 velocity;
    bool isGrounded;
    bool hittingWall;

    int layerMaskgrapple = 1 << 9;
    RaycastHit firsthit;
    public RaycastHit grapplePoint;
    public RaycastHit newgrapplePoint;
    public bool isGrappling = false;
    Vector3 lowest;
    Vector3 origin;
    Vector3 swingTarget;
    Vector3 rayExtender;
    public Vector3 ortho;
    Ray ray;
    float angle;
    public float distance;
    public float swingSize;
    bool upSwing;
    public float speed;
    bool pulling = false;
    float originangle;
    Vector3 jumpOrigin;

    public Vector3 runDirection;
    public RaycastHit finalHit;
    public bool wallRunning = false;
    bool newRun = true;
    public bool awayJump;
    public Vector3 wallDirection;
    bool wallNearby = false;
    int layerMask = 1 << 10;
    float grapplerundelay = 0;
    float wallDistance = 2;
    RaycastHit oldHit;
    int rayHit;
    int oldRayHit;
    float leftrighttest;
    bool firstrun;
    public float currentVelocity;
    bool gamestart = false;
    Vector3 currentDirection;
    float wallrunRotation;
    bool isDashing = false;
    bool reduceSpeed;
    public float dashTimer;
    bool isSliding;
    float jumpCharge;
    public float chargeFactor = 3f;
    float jumpMercy;
    public bool inGrapplingRange;
    Ray rangeRay;
    RaycastHit rangeCheck;
    float reRunTimer;
    bool stoppingWallrun;
    float doubleJumpTimer;
    bool doubleSpace;
    bool isLanding;
    float landingScale;
    bool landingGrowth;
    float landingVelocity;
    bool hasLanded;
    float landingTimer;

    void stopSliding()
    {
        isSliding = false;
        transform.localScale = new Vector3(1, 1, 1);
    }

    void resetJumpDash()
    {
        doubleJumping = false;
        isDashing = false;
    }

    void cancelGrapple()
    {
        if (upSwing)
        {
            velocity.y = angle / 90 * swingspeed;
        }

        if (!upSwing)
        {
            velocity.y = -angle / 90 * swingspeed;
        }
        speed = swingspeed * (1 - angle / 90);
        isGrappling = false;
        upSwing = false;
        jumpOrigin = transform.position;
    }

    void stopWallrun()
    {
        wallrunRotation = 0;
        stoppingWallrun = true;
        wallRunning = false;
        newRun = false;
        rayHit = 0;
        velocity.y = currentVelocity;
        jumpOrigin = transform.position;
    }

    void getGrappleInformation()
    {
        swingspeed = -velocity.y + speed;
        origin = transform.position;
        rayExtender.Set(grapplePoint.point.x, origin.y, grapplePoint.point.z);
        var mirrorVector = (origin - rayExtender);
        swingTarget = rayExtender - mirrorVector;
        lowest = new Vector3(grapplePoint.point.x, grapplePoint.point.y - (Vector3.Distance(grapplePoint.point, transform.position)), grapplePoint.point.z);
        distance = Vector3.Distance(grapplePoint.point, transform.position);
        ortho = Vector3.Cross(grapplePoint.point - transform.position, swingTarget - transform.position);
        isGrappling = true;
        pulling = false;
        angle = Vector3.Angle(transform.position - grapplePoint.point, lowest - grapplePoint.point);
    }

    void doGrapple()
    {
        currentDirection = Vector3.Cross(ortho, grapplePoint.point - transform.position);
        angle = Vector3.Angle(transform.position - grapplePoint.point, lowest - grapplePoint.point);
        swingSize = ((swingspeed / (2 * Mathf.PI) / distance) * 360);
        if (angle < 1 && angle > -1 && !upSwing)
        {
            upSwing = true;
        }

        if (!upSwing)
        {
            swingspeed += -gravity * Time.deltaTime * angle / 90 * 1.4f;
        }

        if (upSwing)
        {
            swingspeed -= -gravity * Time.deltaTime * angle / 90 * 1.45f;
        }

        transform.position = follower.transform.position;
        if(reRunTimer <= 0)
        {
            newRun = true;
        }
        else
        {
            reRunTimer -= Time.deltaTime;
        }
    }
    void doWallrun()
    {
        currentVelocity += (0.3f * gravity) * Time.deltaTime;
        wallDirection = finalHit.point - transform.position;
        if (Vector3.Angle(transform.forward.normalized, Vector3.Cross(finalHit.normal, Vector3.up)) < 90 && firstrun == true)
        {
            leftrighttest = -1;
        }

        if (Vector3.Angle(transform.forward.normalized, Vector3.Cross(finalHit.normal, Vector3.up)) > 89 && firstrun == true)
        {
            leftrighttest = 1;
        }
        //transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);

        if (wallrunRotation < 0.1f)
        {
           wallrunRotation += Time.deltaTime;
           transform.rotation = Quaternion.FromToRotation(transform.up, (5*Vector3.up + wallrunRotation * 10 * finalHit.normal).normalized) * transform.rotation;
        }
        //else
        //{

        //    transform.rotation = Quaternion.FromToRotation(transform.up, (3 * Vector3.up + finalHit.normal).normalized) * transform.rotation;
        //}
        runDirection = Vector3.Cross(finalHit.normal, Vector3.up) * leftrighttest;
        transform.position = follower.transform.position;
    }

    void landing()
    {
        transform.localScale = new Vector3(1, landingScale, 1);
    }

    private void Awake()
    {
        speed = speedvalue;
    }

    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(6);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            SceneManager.LoadScene(0);
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            gamestart = true;
        }

        if (gamestart)
        {
            Debug.Log(wallRunning);

            if (landingTimer >= 1 && !isGrounded)
            {
                isLanding = true;
            }
            if (!isGrounded && !isLanding)
            {
                landingTimer += Time.deltaTime * 3f;
            }
            if(isLanding && !isGrounded)
            {
                landingVelocity = velocity.y;
            }

            if(isLanding && isGrounded)
            {
                hasLanded = true;
                isLanding = false;
                landingScale = 1 - (-landingVelocity / 60);
                landingGrowth = true;

                if(landingVelocity > -1)
                {
                    hasLanded = false;
                }

                if(landingVelocity < -54)
                {
                    landingScale = 0.1f;
                }
            }

            if(hasLanded)
            {
              
                if (landingGrowth)
                {
                    if(landingScale < 1f)
                    {
                        landingScale += Time.deltaTime * 3f;
                    }

                    transform.localScale = new Vector3(1, landingScale, 1);

                    if (landingScale >= 1)
                    {
                        transform.localScale = new Vector3(1, 1, 1);
                        hasLanded = false;
                        landingGrowth = false;
                    }
                }
            }


            if (Input.GetButton("Jump") && !isGrappling && !wallRunning)
            {
                if (jumpCharge < 0.5f)
                {
                    jumpCharge += Time.deltaTime;
                }
            }

            if (!isGrounded)
            {
                if(jumpMercy < 0.15f)
                {
                    jumpMercy += Time.deltaTime;
                }
            }

            if(dashTimer > 0)
            {
                dashTimer -= Time.deltaTime;
            }

            Ray ray7 = new Ray(transform.position, Vector3.forward);
            Ray ray8 = new Ray(transform.position, Vector3.back);
            Ray ray1 = new Ray(transform.position, Vector3.left);
            Ray ray2 = new Ray(transform.position, Vector3.right);
            Ray ray3 = new Ray(transform.position, Vector3.forward - Vector3.left);
            Ray ray4 = new Ray(transform.position, Vector3.forward - Vector3.right);
            Ray ray5 = new Ray(transform.position, Vector3.back - Vector3.right);
            Ray ray6 = new Ray(transform.position, Vector3.back - Vector3.left);

            RaycastHit hit;

            if (Physics.Raycast(ray1, out hit, 2f, layerMask) && Vector3.Distance(transform.position, hit.point) < wallDistance)
            {
                wallDistance = Vector3.Distance(transform.position, hit.point);
                finalHit = hit;
                wallNearby = true;
                rayHit = 1;
            }


            if (Physics.Raycast(ray2, out hit, 2f, layerMask) && Vector3.Distance(transform.position, hit.point) < wallDistance)
            {
                wallDistance = Vector3.Distance(transform.position, hit.point);
                finalHit = hit;
                wallNearby = true;
                rayHit = 2;
            }


            if (Physics.Raycast(ray3, out hit, 2f, layerMask) && Vector3.Distance(transform.position, hit.point) < wallDistance)
            {
                wallDistance = Vector3.Distance(transform.position, hit.point);
                finalHit = hit;
                wallNearby = true;
                rayHit = 3;
            }


            if (Physics.Raycast(ray4, out hit, 2f, layerMask) && Vector3.Distance(transform.position, hit.point) < wallDistance)
            {
                wallDistance = Vector3.Distance(transform.position, hit.point);
                finalHit = hit;
                wallNearby = true;
                rayHit = 4;
            }


            if (Physics.Raycast(ray5, out hit, 2f, layerMask) && Vector3.Distance(transform.position, hit.point) < wallDistance)
            {
                wallDistance = Vector3.Distance(transform.position, hit.point);
                finalHit = hit;
                wallNearby = true;
                rayHit = 5;
            }


            if (Physics.Raycast(ray6, out hit, 2f, layerMask) && Vector3.Distance(transform.position, hit.point) < wallDistance)
            {
                wallDistance = Vector3.Distance(transform.position, hit.point);
                finalHit = hit;
                wallNearby = true;
                rayHit = 6;
            }

            if (Physics.Raycast(ray7, out hit, 2f, layerMask) && Vector3.Distance(transform.position, hit.point) < wallDistance)
            {
                wallDistance = Vector3.Distance(transform.position, hit.point);
                finalHit = hit;
                wallNearby = true;
                rayHit = 7;
            }

            if (Physics.Raycast(ray8, out hit, 2f, layerMask) && Vector3.Distance(transform.position, hit.point) < wallDistance)
            {
                wallDistance = Vector3.Distance(transform.position, hit.point);
                finalHit = hit;
                wallNearby = true;
                rayHit = 8;

            }

            //Debug.Log(wallNearby);
            if (isGrappling && wallNearby)
            {
                cancelGrapple();
            }

            if (!isGrappling)
            {
                swingspeed = -velocity.y;
            }

            hittingWall = Physics.CheckSphere(hitBox.position, groundDistance, groundMask);
            isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

            if (reduceSpeed)
            {
                if (speed > speedvalue + 10)
                {
                    speed -= 4 * speedvalue * Time.deltaTime;
                    velocity.y += -gravity * Time.deltaTime;
                }
                else
                {
                    reduceSpeed = false; ;
                    speed = speedvalue + 10;
                }
            }
 

            if (speed > speedvalue && !isGrappling && !isSliding)
            {
                speed -= 4 * Time.deltaTime;
            }

            if (speed < speedvalue && !isGrappling && !isSliding)

            {
                speed += 10 * Time.deltaTime;
            }

            if (Input.GetKeyDown(KeyCode.LeftControl) && !isGrappling && !isGrounded && !isDashing && !wallRunning && dashTimer <= 0)
            {
                isDashing = true;
                speed += 2 * speedvalue;
                reduceSpeed = true;
                dashTimer = 6; // HIER DASHTIMER UMSTELLEN
                velocity.y = 1;
            }
            if (Input.GetKeyDown(KeyCode.LeftControl) && isGrounded && !isSliding)
            {
                //transform.RotateAround(transform.position, Vector3.Cross(transform.forward, Vector3.up), 20);
                isSliding = true;
                transform.localScale = new Vector3(1, 0.2f, 1);
            }
            if (isSliding)
            {
                speed -= 15 * Time.deltaTime;
            }

            if(Input.GetKeyUp(KeyCode.LeftControl) && isSliding)
            {
                stopSliding();
            }

            if (Input.GetButtonDown("Jump") && isSliding)
            {
                stopSliding();
            }

            if (isGrounded)
            {
                landingTimer = 0;
                newRun = true;
                jumpMercy = 0;
            }

            if (isGrounded && velocity.y < 0)
            {
                velocity.y = -2f;
            }

            float x = Input.GetAxis("Horizontal");

            //if (isGrappling && velocity.y < 0)
            //{
            //    velocity.y = -2f;
            //}

            if (wallRunning && velocity.y < 0)
            {
                velocity.y = -2f;
            }

            if(isGrounded)
            {
                resetJumpDash();
            }

            if (wallRunning)
            {
                resetJumpDash();
            }

            if (isGrappling)
            {
                resetJumpDash();
            }

            Vector3 move = transform.right * x + transform.forward * 1;

            controller.Move(move * speed * Time.deltaTime);

            if (Input.GetButtonDown("Jump") && jumpMercy >= 0.15f && !wallRunning && !isGrappling && !doubleJumping && doubleJumpTimer < 0.5f && doubleSpace)
            {
                velocity.y = Mathf.Sqrt(jumpHeight * - 4f * gravity);
                doubleJumping = true;
                speed = speed * 1/5;
                jumpOrigin = transform.position;
                jumpCharge = 0;
            }

            if (Input.GetButtonUp("Jump") && jumpMercy <= 0.15f)
            {
                velocity.y = Mathf.Sqrt(jumpHeight * (0.8f + chargeFactor * jumpCharge) * -2f * gravity);
                jumpOrigin = transform.position;
                jumpCharge = 0;
                doubleSpace = true;
                
            }

            if (Input.GetButtonDown("Jump") && jumpMercy >= 0.15f && !wallRunning && !isGrappling && !doubleJumping)
            {
                doubleSpace = true;
            }

            if (doubleSpace)
            {
                doubleJumpTimer += Time.deltaTime;
            }

            if(doubleJumpTimer > 0.5f)
            {
                doubleSpace = false;
                doubleJumpTimer = 0;
            }

            velocity.y += gravity * Time.deltaTime;

            if (!isGrappling && !wallRunning)
            {
                controller.Move(velocity * Time.deltaTime);
            }

           


            if (Input.GetButtonDown("Jump") && isGrappling)
            {
                cancelGrapple();
                jumpCharge = 0;
                doubleSpace = true;
            }

            if (Input.GetMouseButtonDown(1) && isGrappling)
            {
                cancelGrapple();
            }

            if (isGrappling && swingspeed <= 1)
            {
                cancelGrapple();
            }

            if (isGrappling && hittingWall)
            {
                if (!upSwing)
                {
                    velocity.y = -angle / 90 * swingspeed;
                }
                speed = swingspeed * (1 - angle / 90);
                isGrappling = false;
                upSwing = false;
                jumpOrigin = transform.position;
            }

            ////if (isGrappling && angle >= 85)
            ////{
            ////    cancelGrapple();
            ////}


            if (isGrappling && isGrounded)
            {
                cancelGrapple();
            }

            if (Input.GetMouseButtonDown(1) && !isGrappling)
            {
                ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            }

            rangeRay = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(rangeRay, out rangeCheck, maxgrappledistance, layerMaskgrapple))
            {
                inGrapplingRange = true;
            }
            else
            {
                inGrapplingRange = false;
            }

            


            if (Input.GetMouseButtonDown(1) && !isGrappling && !pulling && grapplerundelay <= 0)
            {
                if (Physics.Raycast(ray, out grapplePoint, maxgrappledistance, layerMaskgrapple))
                {
                    pulling = true;
                    originangle = Vector3.Angle(transform.position - grapplePoint.point, new Vector3(grapplePoint.point.x, grapplePoint.point.y - (Vector3.Distance(grapplePoint.point, transform.position)), grapplePoint.point.z) - grapplePoint.point);
                    if (wallRunning)
                    {
                        stopWallrun();
                        reRunTimer = 0.3f;
                    }
                }
            }

            if (pulling && velocity.y <= (angle/90) * -speed && !wallRunning)
            {
                getGrappleInformation();
            }


            //if (isGrappling)
            //{
            //    Ray rayObstacle = new Ray(transform.position, grapplePoint.point);
            //    if (Physics.Raycast(rayObstacle, out newgrapplePoint, distance - 5))
            //    {
            //        grapplePoint = newgrapplePoint;
            //        origin = transform.position;
            //        rayExtender.Set(grapplePoint.point.x, origin.y, grapplePoint.point.z);
            //        var mirrorVector = (origin - rayExtender);
            //        swingTarget = rayExtender - mirrorVector;
            //        lowest = new Vector3(grapplePoint.point.x, grapplePoint.point.y - (Vector3.Distance(grapplePoint.point, transform.position)), grapplePoint.point.z);
            //        distance = Vector3.Distance(grapplePoint.point, transform.position);
            //        ortho = Vector3.Cross(grapplePoint.point - transform.position, swingTarget - transform.position);
            //        angle = Vector3.Angle(transform.position - grapplePoint.point, lowest - grapplePoint.point);
            //    }
            //    Debug.DrawLine(transform.position, grapplePoint.point);
            //    Debug.Log(grapplePoint.point);
            //}

            //Debug.DrawLine();

            if (isGrappling)
            {
                doGrapple();
            }





            if (!wallRunning && newRun && !isGrounded && wallNearby)
            {
                currentVelocity = velocity.y;
                wallRunning = true;
                oldRayHit = rayHit;
                oldHit = finalHit;
                firstrun = true;
            }
            if (wallRunning && oldRayHit != rayHit)
            {

                if (Vector3.Magnitude(Vector3.Cross(oldHit.normal, finalHit.normal)) != 0) //&& Vector3.Angle(oldHit.normal, finalHit.normal) < 0 && Vector3.Angle(oldHit.normal, finalHit.normal) > 46)
                {
                    oldRayHit = rayHit;
                    oldHit = finalHit;
                }
                else
                {
                    stopWallrun();
                }
            }

            if (wallRunning && !wallNearby)
            {
                stopWallrun();
            }

            if (wallRunning && Input.GetButtonDown("Jump"))
            {
                stoppingWallrun = true;
                wallRunning = false;
                newRun = false;
                rayHit = 0;
                velocity.y = currentVelocity + Mathf.Sqrt(jumpHeight * -2f * gravity); ;
                wallrunRotation = 0;
                jumpOrigin = transform.position;
                jumpCharge = 0;
                doubleSpace = true;
            }

            if (wallRunning && isGrounded)
            {
                stopWallrun();
            }

            if (wallRunning && currentVelocity <= -speed)
            {
                stopWallrun();
            }

            if (wallRunning && currentVelocity <= -speed)
            {
                stopWallrun();
            }

            if (stoppingWallrun && wallrunRotation < 1f)
            {
                wallrunRotation += Time.deltaTime * 0.5f;
                transform.rotation = Quaternion.FromToRotation(transform.up, Vector3.up * wallrunRotation + transform.up) * transform.rotation;
            }


            if (stoppingWallrun && wallrunRotation >= 1f)
            {
                stoppingWallrun = false;
                transform.rotation = Quaternion.FromToRotation(transform.up, Vector3.up) * transform.rotation;
                wallrunRotation = 0;
                firstrun = true;
            }

            if (wallRunning)
            {
                doWallrun();
            }

            if (grapplerundelay > 0)
            {
                grapplerundelay -= Time.deltaTime;
            }
            else
            {
                grapplerundelay = 0;
            }

            wallDistance = 2f;
            wallNearby = false;
            firstrun = false;

            if(!stoppingWallrun && !wallRunning)
            {
                newRun = true;
            }
        }

    }
    
}

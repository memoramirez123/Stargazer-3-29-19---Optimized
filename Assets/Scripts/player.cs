using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using System.Xml;
using System;
using System.Net;
using System.Net.Sockets;
using System.Collections;

public class player : MonoBehaviour {

    // Config
    [SerializeField] float runSpeed = 5f;
    [SerializeField] public float jumpSpeed = 5f;
    [SerializeField] float climbSpeed = 5f;
    [SerializeField] Vector2 deathKick = new Vector2(20f, 20f);

    //Jump  States
    bool enableWallJump = false;
    bool hasWallJumped = false;
    bool isJumping = false;
    bool allowJump = false;
    bool allowDoubleJump = true;
    int currentDirectionState = 0;
    int oldDirectionState = 0;

    // State
    bool isAlive = true;

    // Cached component references
    Rigidbody2D myRigidBody;
    Animator myAnimator;
    CapsuleCollider2D myBodyCollider;
    BoxCollider2D myFeet;
    float gravityScaleAtStart;
    // Audio variables
    public AudioClip jumpsound;
    public AudioClip walksound;
    public AudioClip firesound;
    private AudioSource source;
    private float volLowRange = .5f;
    private float volHighRange = 1.0f;

    // Message then methods
    void Start () {
        // StartCoroutine(GetWeather());
        // StartCoroutine(Getlocation());
        // LocalIPAddress();

        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myBodyCollider = GetComponent<CapsuleCollider2D>();
        myFeet = GetComponent<BoxCollider2D>();
        source = GetComponent<AudioSource>();
        gravityScaleAtStart = myRigidBody.gravityScale; //gravityScale is the current gravity affecting the player
	}
    // IEnumerator GetWeather()
    // {
    //     //print("Shieeeeeeet");

    //     string url = "http://api.openweathermap.org/data/2.5/find?q=London&type=accurate&mode=xml&lang=nl&units=imperial&appid=4f258e4971d0c07f770b8bc4759fb32e";
    //     WWW www = new WWW(url);
    //     yield return www;
    //     //if (www.error == null)
    //     //{

    //     print("Loaded following XML " + www.text);
    //     XmlDocument xmlDoc = new XmlDocument();
    //     xmlDoc.LoadXml(www.text);
    //     print("City: " + xmlDoc.SelectSingleNode("cities/list/item/city/@name").InnerText);
    //     print("Temperature: " + xmlDoc.SelectSingleNode("cities/list/item/temperature/@value").InnerText);
    //     print("humidity: " + xmlDoc.SelectSingleNode("cities/list/item/humidity /@value").InnerText);
    //     print("Cloud : " + xmlDoc.SelectSingleNode("cities/list/item/clouds/@value").InnerText);
    //     print("Title: " + xmlDoc.SelectSingleNode("cities /list/item/weather/@value").InnerText);
    //     //This is the weathe sheeeeit
    //     // it does not print for some reason
    //     //idk 
    //     //yep

    //     //}
    //     //else
    //     //{
    //     //   Debug.Log("ERROR: " + www.error);
    //     //
    //     //}


    //     //---------------------------------

    // }
    // public string LocalIPAddress()
    // {
    //     string localIP = "";
    //     /* IPHostEntry host;
    //     string localIP = "";
    //     host = Dns.GetHostEntry(Dns.GetHostName());
    //     foreach (IPAddress ip in host.AddressList)
    //     {
    //         if (ip.AddressFamily == AddressFamily.InterNetwork)
    //         {
    //             localIP = ip.ToString();  
    //             print("hi");
    //             break;
    //         }
    //     }

    //     print("THIS IS TEH IP jsadhlkosdjflkjasd;fjk;lsdkf : "+localIP);
    //    */
    //     string url = "http://checkip.dyndns.org";
    //     System.Net.WebRequest req = System.Net.WebRequest.Create(url);
    //     System.Net.WebResponse resp = req.GetResponse();
    //     System.IO.StreamReader sr = new System.IO.StreamReader(resp.GetResponseStream());
    //     string response = sr.ReadToEnd().Trim();
    //     string[] a = response.Split(':');
    //     string a2 = a[1].Substring(1);
    //     string[] a3 = a2.Split('<');
    //     string a4 = a3[0];
    //     return a4;
    //     //localIP = new WebClient().DownloadString("http://icanhazip.com/%22");
    //     //print("THIS IS TEH IP jsadhlkosdjflkjasd;fjk;lsdkf : "+localIP);
    //     //return localIP;
    // }

    // IEnumerator Getlocation()
    // {
    //     //print("Shieeeeeeet");

    //     //string url = "http://api.openweathermap.org/data/2.5/find?q=London&type=accurate&mode=xml&lang=nl&units=imperial&appid=4f258e4971d0c07f770b8bc4759fb32e";
    //     string a = LocalIPAddress();
    //     string url = "api.ipgeolocation.io/ipgeo?apiKey=63675d7f1aa14c7fbeda269fb00bee57&ip=" + a + "&fields=city";
    //     WWW www = new WWW(url);
    //     yield return www;
    //     //if (www.error == null)
    //     //{

    //     print("Loaded following XML " + www.text);
    //     XmlDocument xmlDoc = new XmlDocument();
    //     xmlDoc.LoadXml(www.text);
    //     print("City: " + xmlDoc.SelectSingleNode("cities/list/item/city/@name").InnerText);
    //     //print("Temperature: " + xmlDoc.SelectSingleNode("cities/list/item/temperature/@value").InnerText);
    //     //print("humidity: " + xmlDoc.SelectSingleNode("cities/list/item/humidity /@value").InnerText);
    //     //print("Cloud : " + xmlDoc.SelectSingleNode("cities/list/item/clouds/@value").InnerText);
    //     //print("Title: " + xmlDoc.SelectSingleNode("cities /list/item/weather/@value").InnerText);
    //     //This is the weathe sheeeeit
    //     // it does not print for some reason
    //     //idk 
    //     //yep

    //     //}
    //     //else
    //     //{
    //     //   Debug.Log("ERROR: " + www.error);
    //     //
    //     //}


    //     //---------------------------------

    // }

    // Update is called once per frame
    void Update () {
        if (!isAlive)
        {
            return;
        }
        Run();
        ClimbLadder();
        Jump();
        FlipSprite();
        Die();
        shoot();
	}

    private void Run() {
        float controlThrow = CrossPlatformInputManager.GetAxis("Horizontal"); // value between -1 to +1
        Vector2 playerVelocity = new Vector2(controlThrow * runSpeed, myRigidBody.velocity.y); //controlThrow = x, myRigidBody = y
        myRigidBody.velocity = playerVelocity; // setting myRigidBody to playerVelocity
        // print(playerVelocity);

        bool playerHasXSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon; // Mathf.Epsilon = 0
        myAnimator.SetBool("Running", playerHasXSpeed);
        if (CrossPlatformInputManager.GetButtonDown("Horizontal"))
        {
            source.PlayOneShot(walksound, 1F);
        }
    }

    private void ClimbLadder() {
        if (!myFeet.IsTouchingLayers(LayerMask.GetMask("Climbing"))) {
            myAnimator.SetBool("Climbing", false);
            myRigidBody.gravityScale = gravityScaleAtStart;
            return;
        }

        float controlThrow = CrossPlatformInputManager.GetAxis("Vertical");
        Vector2 climbVelocity = new Vector2(myRigidBody.velocity.x, controlThrow * climbSpeed);
        myRigidBody.velocity = climbVelocity;
        myRigidBody.gravityScale = 0f;

        bool playerHasYSpeed = Mathf.Abs(myRigidBody.velocity.y) > Mathf.Epsilon;
        myAnimator.SetBool("Climbing", playerHasYSpeed);
        if (CrossPlatformInputManager.GetButtonDown("Vertical"))
        {
            source.PlayOneShot(walksound, 1F);
        }
    }

    private void Jump() {
        /*
        if (!myFeet.IsTouchingLayers(LayerMask.GetMask("Ground"))) {
            return;
        }

        if (CrossPlatformInputManager.GetButtonDown("Jump")) {
            source.PlayOneShot(jumpsound, 1F);
            Vector2 jumpVelocityToAdd = new Vector2(0f, jumpSpeed);
            myRigidBody.velocity += jumpVelocityToAdd;
        }
        */
        
        if (!myFeet.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            isJumping = true;
            allowJump = false;
        }
        if (myFeet.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            allowJump = true;
            allowDoubleJump = true;
            isJumping = false;
            hasWallJumped = false;
        }
        if (myRigidBody.IsTouchingLayers(LayerMask.GetMask("Wall")))
        {
            enableWallJump = true;
        }
        if (!(myRigidBody.IsTouchingLayers(LayerMask.GetMask("Wall"))))
        {
            enableWallJump = false;
        }
        if (CrossPlatformInputManager.GetButtonDown("Jump"))
        {
            if (enableWallJump == true && isJumping == true && hasWallJumped == false)
            {
                allowJump = true;
                hasWallJumped = true;
            }
            if (allowDoubleJump == true && isJumping == true)
            {
                allowJump = true;
                allowDoubleJump = false;
            }
            if (enableWallJump == true && isJumping == true && hasWallJumped == true)
            {
                if (Mathf.Sign(myRigidBody.velocity.x) == 1)
                {
                    currentDirectionState = 1;
                    if (oldDirectionState != currentDirectionState)
                    {
                        oldDirectionState = currentDirectionState;
                        allowJump = true;
                    }
                }
                else if (Mathf.Sign(myRigidBody.velocity.x) == -1)
                {
                    currentDirectionState = 2;
                    if (oldDirectionState != currentDirectionState)
                    {
                        oldDirectionState = currentDirectionState;
                        allowJump = true;
                    }
                }
            }
            if (allowJump)
            {
                Vector2 jumpVelocityToAdd = new Vector2(0f, jumpSpeed);
                myRigidBody.velocity = jumpVelocityToAdd;
            }
        }
    }

    private void shoot()
    {
        if (CrossPlatformInputManager.GetButtonDown("Fire1"))
        {
            source.PlayOneShot(firesound, 1F);
        }
    }

    private void Die()
    {
        if (myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemy", "Hazards")))
        {
            isAlive = false;
            myAnimator.SetTrigger("Dying");
            GetComponent<Rigidbody2D>().velocity = deathKick;
            FindObjectOfType<GameSession>().ProcessPlayerDeath();
        }
    }

    private void FlipSprite() {
        // if player is moving horizontally
        bool playerHasXSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon; // Mathf.Epsilon = 0
        if(playerHasXSpeed) {
            // reverse the current scaling of x axis
            transform.localScale = new Vector2(Mathf.Sign(myRigidBody.velocity.x), 1f); // Mathf.Sign returns -1 or +1 depending on x velocity of RigidBody
        }
    }
}
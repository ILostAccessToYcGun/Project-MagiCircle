using System.Collections;
using UnityEngine;

public class MagicCircle : MonoBehaviour
{
    /*
    This is where the magic happens (literaly gotem)
     
        * this script will house all of the parameters and will be the parental object to the magic projectiles/objects
        *
     

    // the parameters i have added will have an * infront
    enum Projectile Type { Basic, DOT, Lazer }
    enum Element Type { Fire, Water, Wind, Lightning, Earth }
    element mixing?
    
    *float Cast Time
    *float Mana Cost
    
    *float Projectile Life-span
    *float Projectile Speed
    float Projectile AOE Radius
    *float Projectile Size Radius
    float Projectile Damage
        Positive deals damage
        Negative heals damage
        at 0 status effects are unlocked
    bool is Affected by Gravity
    bool is Homing
    float Homing Radius
    
    enum Projectile Split Pattern { Radial, Linear}
    int Projectile count
    int Projectile split count
    List<float> Projectile split time
    float Projectile split angle
    float Projectile split rotation
    
    int Spell Repetition Count
    float Spell Repetition Interval
    
    int Projectile Bounce Count
    int Projectile Pierce Count
    
    scriptableObject Projectile Trajectory
     */

    [SerializeField] public float castTime;
    [SerializeField] public int manaCost;
    [SerializeField] public float lifeSpan;
    [SerializeField] public float speed;
    [SerializeField] public float sizeRadius;
    [SerializeField] public float damage;
    [SerializeField] GameObject magicProjectile;


    private IEnumerator coroutine;
    private bool isCoroutine;

    public void ExecuteMagic()
    {
        //if we have manaCost amount of mana avalible?
        //after float amount of cast time, it will spawn a projectile and pass on values into the projectile
        //such as life span, speed, and size

        /// i want the magi circle to spiiiiiiiiiiiiiiiin
        //coroutine = CastMagic(castTime);
        Debug.Log("before");
        isCoroutine = true;
        Debug.Log("afetr");
    }

    public IEnumerator ChargeMagic()
    {
        isCoroutine = false;
        float currentCastTime = 0f;
        while (currentCastTime <= castTime)
        {
            //spin the circle now
            currentCastTime += Time.deltaTime;
            //we need to rotate around it's local Z axis, so ill do that ungabunga for now
            this.transform.localEulerAngles = this.transform.localEulerAngles + new Vector3(0, 0, 5 * (currentCastTime / castTime));
            //this.transform.Rotate(transform., 5f)
            yield return null;
        }
        StartCoroutine(CastMagic());
        yield return new WaitForSeconds(castTime);
        print("Coroutine ended: " + Time.time + " seconds");
        Destroy(this.gameObject);
    }

    public IEnumerator CastMagic()
    {
        MagicProjectile mp = Instantiate(magicProjectile, transform.position, transform.rotation).GetComponent<MagicProjectile>();
        mp.InitializeProjectile(this);
        mp.gameObject.transform.localScale = new Vector3(sizeRadius * 2, sizeRadius * 2, sizeRadius * 2);

        yield return null;
    }


    void Update()
    {
        if (isCoroutine)
        {
            StartCoroutine(ChargeMagic());
            
        }
    }


    //void Start()
    //{
    //    // - After 0 seconds, prints "Starting 0.0 seconds"
    //    // - After 0 seconds, prints "Coroutine started"
    //    // - After 2 seconds, prints "Coroutine ended: 2.0 seconds"
    //    print("Starting " + Time.time + " seconds");

    //    // Start function WaitAndPrint as a coroutine.

        
    //    StartCoroutine(coroutine);

    //    print("Coroutine started");
    //}

    //private IEnumerator WaitAndPrint(float waitTime)
    //{
    //    yield return new WaitForSeconds(waitTime);
    //    print("Coroutine ended: " + Time.time + " seconds");
    //}
}

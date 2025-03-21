using System.Collections;
using UnityEngine;

public class MagicProjectile : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    //private IEnumerator coroutine;
    [SerializeField] float currentlife;
    [SerializeField] float lifeSpan;
    [SerializeField] float speed;
    [SerializeField] float damage;

    public void InitializeProjectile(MagicCircle mc)
    {
        lifeSpan = mc.lifeSpan;
        speed = mc.speed;
        damage = mc.damage;
    }
    public void InitializeProjectile(MagicProjectile mp)
    {
        currentlife = mp.currentlife;
        lifeSpan = mp.lifeSpan;
        speed = mp.speed;
        damage = mp.damage;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position += new Vector3(speed * transform.forward.x * Time.fixedDeltaTime, speed * transform.forward.y * Time.fixedDeltaTime, speed * transform.forward.z * Time.fixedDeltaTime);
    }
}

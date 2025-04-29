using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float damage;
    public bool isAttacking;
    private PlayerMovement pm;
    private PlayerAnimations anims;
    public Transform colliderPos;
    public DetectEnemy detectEnemy;
    private PlayerMetal metal;
    public GameObject dagger;
    [SerializeField] float attack1Delay;

    // Start is called before the first frame update
    void Start()
    {
        anims = GetComponent<PlayerAnimations>();
        pm = GetComponent<PlayerMovement>();
        metal = GetComponent<PlayerMetal>();
    }

    // Update is called once per frame
    void Update()
    {
        if(anims.isLookingRight)
        {
            colliderPos.localPosition = new Vector2(0.2f, 0f);
        }
        else
        {
            colliderPos.localPosition = new Vector2(-0.2f, 0f);
        }
        if(!isAttacking && Input.GetMouseButtonDown(0) &&pm.isGrounded)
        {
            StartCoroutine(Attack01());
            if (detectEnemy.enemyInRange)
            {
                StartCoroutine(DamageEnemyAttack01());
                metal.ChargeMetal();
            }
        }
        else if(!isAttacking && Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(Attack02());
        }
    }

    IEnumerator Attack01()
    {
        isAttacking = true;
        yield return new WaitForSeconds(0.5f);
        isAttacking = false;
    }
    IEnumerator DamageEnemyAttack01()
    {
        yield return new WaitForSeconds(attack1Delay);
        detectEnemy.enemy.GetComponent<BasicEnemyController>().GetHit(damage);
    }
    IEnumerator Attack02()
    {
        isAttacking = true;
        GameObject obj = Instantiate(dagger, transform.position, Quaternion.identity);
        if (anims.isLookingRight)
        {
            obj.GetComponent<Dagger>().AddForce(1);
        }
        else
        {
            obj.GetComponent<Dagger>().AddForce(-1);
        }
        yield return new WaitForSeconds(0.5f);
        isAttacking = false;
    }
}

using UnityEngine;

public class Chest : MonoBehaviour, IDamageable
{
    public SpriteRenderer sr;
    public Material whiteMat;
    private Material defMat;

    private void Start()
    {
        defMat = sr.material;
    }
 
    void ResetMat()
    {
        sr.material = defMat;
    }

    public void RecieveDamage(GameObject attacker, float damageAmt, Vector2 direction, bool isCrit)
    {
        sr.material = whiteMat;
        Invoke("ResetMat", .1f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            UpgradeManager.instance.ShowUpgradeChoices();
            Destroy(gameObject);
        }
    }

}

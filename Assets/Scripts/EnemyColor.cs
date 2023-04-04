using UnityEngine;

public class EnemyColor : MonoBehaviour
{
    [SerializeField] private Enemy Enemy;
    [SerializeField] private SpriteRenderer enemyTexture;
    private float r;
    private float g;
    private float b;
    private float rFrom;
    private float gFrom;
    private float bFrom;
    private float rTo;
    private float gTo;
    private float bTo;

    private void OnEnable()
    {
        Enemy.onHp += ColorChanger;
    }

    private void OnDisable()
    {
        Enemy.onHp -= ColorChanger;
    }

    private void ColorChanger(float hp, float fullHp)
    {
        r = rFrom - ((rFrom - rTo) / fullHp * (fullHp - hp));
        g = gFrom - ((gFrom - gTo) / fullHp * (fullHp - hp));
        b = bFrom - ((bFrom - bTo) / fullHp * (fullHp - hp));
        enemyTexture.color = new Color(r, g, b);
    }

    public void SetColor(float rFrom, float gFrom, float bFrom, float rTo, float gTo, float bTo)
    {
        this.rFrom = rFrom;
        this.gFrom = gFrom;
        this.bFrom = bFrom;
        this.rTo = rTo;
        this.gTo = gTo;
        this.bTo = bTo;
        enemyTexture.color = new Color(rFrom, gFrom, bFrom);
    }
}

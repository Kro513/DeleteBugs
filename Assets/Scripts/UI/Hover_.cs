using UnityEngine;

public class Hover_ : Singleton<Hover_>
{
    private SpriteRenderer spriteRenderer;
    private SpriteRenderer rangeSpriteRenderer;

    private void Start()
    {
        this.spriteRenderer = GetComponent<SpriteRenderer>();
        this.rangeSpriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        FollowMouse();
    }
    private void FollowMouse()
    {
        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
    }

    public void Activate(Sprite sprite)
    {
        this.spriteRenderer.sprite = sprite;
        spriteRenderer.enabled = true;

        rangeSpriteRenderer.enabled = true; // ����
    }
    public void Deactivate()
    {
        spriteRenderer.enabled = false;
        rangeSpriteRenderer.enabled = false; // ����(���� ��ư ������ ����)

        UIManager.Instance.ClickedBtn = null;

    }
}

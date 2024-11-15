using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MailBox : Interactive
{
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider;

    public Sprite openSprite;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void OnEnable()
    {
        EventHandler.AfterSceneLoadEvent += OnAfterSceneLoadEvent;
    }

    private void OnDisable()
    {
        EventHandler.AfterSceneLoadEvent -= OnAfterSceneLoadEvent;
    }

    protected override void OnClickAction()
    {
        spriteRenderer.sprite = openSprite;
        transform.GetChild(0).gameObject.SetActive(true);
    }


    private void OnAfterSceneLoadEvent()
    {
        if (!isDone)
        {
            transform.GetChild(0).gameObject.SetActive(false);
        }
        else
        {
            spriteRenderer.sprite = openSprite;
            boxCollider.enabled = false;
        }
    }
}

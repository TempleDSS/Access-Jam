using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ColorEnum { Red, Green, Blue };
public class MoveCircles : MonoBehaviour
{
    
    public int value;
    public float minspeed, maxspeed;
    public ColorEnum color;
    SpriteRenderer sprite;
    public CircleGame game;

    public Sprite redPattern;
    public Sprite greenPattern;
    public Sprite bluePattern;

    AudioSource aSource;
    public AudioClip appear, disappear;

    // Start is called before the first frame update
    void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        aSource = GetComponent<AudioSource>();
        //Reset();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Reset(ColorEnum c, int v)
    {
        aSource.PlayOneShot(appear);
        GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(0,2)==0?Random.Range(-maxspeed, -minspeed): Random.Range(minspeed, maxspeed), Random.Range(0, 2) == 0 ? Random.Range(-maxspeed, -minspeed): Random.Range(minspeed, maxspeed)));

        color = c;

        if (color == ColorEnum.Red)
            sprite.color = Color.red;
        if (color == ColorEnum.Green)
            sprite.color = Color.green;
        if (color == ColorEnum.Blue)
            sprite.color = Color.blue;

        if (game.secondRound)
        {
            if (color == ColorEnum.Red)
                sprite.sprite = redPattern;
            if (color == ColorEnum.Green)
                sprite.sprite = greenPattern;
            if (color == ColorEnum.Blue)
                sprite.sprite = bluePattern;
        }

        value = v;

        GetComponent<Rigidbody2D>().simulated = true;
    }

    private void OnMouseUp()
    {
        game.points += value;
        StartCoroutine(HideOnClick());
        GetComponent<ParticleSystem>().Play();
    }

    IEnumerator HideOnClick()
    {
        aSource.PlayOneShot(disappear);
        sprite.enabled = false;
        GetComponent<Collider2D>().enabled = false;
        GetComponent<Rigidbody2D>().simulated = false;
        yield return new WaitForSeconds(2);
        sprite.enabled = true;
        GetComponent<Collider2D>().enabled = true;
        GetComponent<Rigidbody2D>().simulated = true;
        Reset(color, value);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ADV_Suicide_Robot : MonoBehaviour
{
    Animator anim;
    [SerializeField]
    GameObject bossPart = null;
    ParticleSystem electicity;
    bool suicided = false;
    float maxSize = 6;
    float size = 3;
    bool go = false;
    void Start()
    {
        anim = GetComponent<Animator>();
        electicity = GetComponentInChildren<ParticleSystem>();
    }
    private void Update()
    {
        if (go)
        {
            size += Time.deltaTime * 3;
            transform.localScale = new Vector3(size, size, size);
            if (size >= maxSize)
            {
                go = false;
                StartCoroutine(TransformateCo());
            }
        }
    }
    public void makeSuicide()
    {
        if (suicided) return;
        suicided = true;
        anim.SetTrigger("ACTION");

    }

    public void OnBecameBig()
    {
        electicity.Play();
        go = true;
    }
    IEnumerator TransformateCo()
    {
      if (EffectDirector.instance != null)
            EffectDirector.instance.playInPlace(transform.position, "Explosion");
        if (Soundmanager.instance != null)
            Soundmanager.instance.PlaySeByName("Explosion");
        yield return new WaitForSeconds(0.25f);
        bossPart.SetActive(true);
        gameObject.SetActive(false);
    }
}

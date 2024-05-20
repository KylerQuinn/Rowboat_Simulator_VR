using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InteractiveCountdown : MonoBehaviour
{
    public int seconds = 3;
    private Animator anim;
    private TMP_Text secondsText;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        secondsText = GetComponent<TMP_Text>();

        //StartCoroutine(CountSeconds(gameObject));
    }

    IEnumerator CountSeconds(GameObject interactive)
    {
        int secondsLeft = seconds;
        if (interactive != null)
        {
            InteractiveSeconds outSeconds;
            bool haveComponent = interactive.TryGetComponent<InteractiveSeconds>(out outSeconds);
            if (haveComponent)
            {
                secondsLeft = outSeconds.GetSeconds();
            }
        }

        while (secondsLeft > 0)
        {
            anim.Play("Countdown Animation");
            secondsText.text = secondsLeft.ToString();
            yield return new WaitForSeconds(1);
            secondsLeft--;
        }

        if (interactive != null)
        {
            interactive.SendMessage("OnInteract");
        }

        gameObject.SetActive(false);
    }

    public void StartAnimation(GameObject interactive)
    {
        StartCoroutine(CountSeconds(interactive));
    }

    public void StopAnimation()
    {
        gameObject.SetActive(false);
    }
}

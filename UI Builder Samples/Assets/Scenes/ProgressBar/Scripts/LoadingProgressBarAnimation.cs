using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class LoadingProgressBarAnimation : MonoBehaviour
{
    public string progressBar, percentText;
    VisualElement root, loadingProgressBar;
    Label percentageText;
    float endWidth = 0;
    void Awake()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
        loadingProgressBar = root.Q<VisualElement>(progressBar);
        percentageText = root.Q<Label>(percentText);
    }

    private void Start() {
        AnimationProgressBar();
    }

    void AnimationProgressBar()
    {
        //grab the final width of the progress bar on the parent and remove 25px for margin
        endWidth = loadingProgressBar.parent.worldBound.width - 25;
        StartCoroutine(LinearAnimation(5));
    }

    IEnumerator LinearAnimation(float maxTime)
    {
        float perc = 0;
        float time = 0;
        maxTime = maxTime > 0 ? 1/maxTime : 1;
        float x = 0;
        float width = 0;
        while(perc < 1)
        {
            yield return null;
            time += Time.deltaTime;
            perc = time * maxTime;
            x = Mathf.Lerp(0, 100, perc);
            width = Mathf.Lerp(0, 300, perc);
            percentageText.text = $"{Mathf.RoundToInt(x)}%";
            loadingProgressBar.style.width = width;
        }
    }
}

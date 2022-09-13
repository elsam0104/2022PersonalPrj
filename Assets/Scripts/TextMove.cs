using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TextMove : MonoBehaviour
{
    public RectTransform ins_traTitle = null;
    [Header("이동 속도")]
    [SerializeField]
    private int moveSpeed = 150;
    [Header("이동 방향")]
    [SerializeField]
    private bool isRight = true;

    private RectTransform rtBg;
    private Vector2 startPos;
    private Vector2 dir = Vector2.right;
    private float endPosX;

    private void Start()
    {
        rtBg = transform.GetComponent<RectTransform>();

        //ins_traTitle이 가변 사이즈 일 수 있기 때문 
        LayoutRebuilder.ForceRebuildLayoutImmediate(ins_traTitle);
        float textHalf = ins_traTitle.rect.width / 2 + (rtBg.rect.width / 2);

        if (isRight)
        {
            dir = Vector2.right;
            endPosX += textHalf;
        }
        else
        {
            dir = Vector2.left;
            endPosX -= textHalf;
        }
        startPos = new Vector2(-endPosX, ins_traTitle.anchoredPosition.y);
        ins_traTitle.anchoredPosition = startPos;

        StartCoroutine(CorMoveText());
    }
    private IEnumerator CorMoveText()
    {
        while (true)
        {
            ins_traTitle.Translate(dir * moveSpeed * Time.deltaTime);
            if (IsEndPos())
                ins_traTitle.anchoredPosition = startPos;
            yield return null;
        }
    }
    private bool IsEndPos()
    {
        if (isRight) return endPosX < ins_traTitle.anchoredPosition.x;

        return endPosX > ins_traTitle.anchoredPosition.x;

    }
}

using UnityEngine;

public class SetBodyPartController : MonoBehaviour
{
    [SerializeField] private IconProvider_SO m_iconProvider;

    private void OnEnable()
    {
        SnakeMovement.OnTailDirectionChanged += OnTailSetTailSprite;
        SnakeMovement.OnHeadDirectionChanged += OnHeadSetSprite;
        SnakeMovement.OnSnakeDirectionChanged += OnSnakeSetSprite;
    }

    private void OnSnakeSetSprite(GameObject bodySegment, Vector2Int prevDirection, Vector2Int _bodyDifferenceVector)
    {
        SpriteRenderer bodySpriteRenderer=bodySegment.GetComponent<SpriteRenderer>();
        
        if (prevDirection == _bodyDifferenceVector)
        {
            if (prevDirection == Vector2Int.right || prevDirection == Vector2Int.left)
                bodySpriteRenderer.sprite = m_iconProvider.m_bodyHorizontalSprite;
            else
                bodySpriteRenderer.sprite = m_iconProvider.m_bodyVerticalSprite;
        }
        else
        {
            if ((prevDirection == Vector2Int.up && _bodyDifferenceVector == Vector2Int.right) ||
                (prevDirection == Vector2Int.left && _bodyDifferenceVector == Vector2Int.down))
                bodySpriteRenderer.sprite = m_iconProvider.m_bodyBottomRightSprite;
            if ((prevDirection == Vector2Int.up && _bodyDifferenceVector == Vector2Int.left) ||
                (prevDirection == Vector2Int.right && _bodyDifferenceVector == Vector2Int.down))
                bodySpriteRenderer.sprite = m_iconProvider.m_bodyBottomLeftSprite;
            if ((prevDirection == Vector2Int.down && _bodyDifferenceVector == Vector2Int.right) ||
                (prevDirection == Vector2Int.left && _bodyDifferenceVector == Vector2Int.up))
                bodySpriteRenderer.sprite = m_iconProvider.m_bodyTopRightSprite;
            if ((prevDirection == Vector2Int.down && _bodyDifferenceVector == Vector2Int.left) ||
                (prevDirection == Vector2Int.right && _bodyDifferenceVector == Vector2Int.up))
                bodySpriteRenderer.sprite = m_iconProvider.m_bodyTopLeftSprite;
        }
    }

    private void OnHeadSetSprite(Vector2Int direction,SpriteRenderer headRenderer)
    {
        var headSpriteRenderer = headRenderer;
        
        if (direction == Vector2Int.up)
            headSpriteRenderer.sprite = m_iconProvider.m_headUpSprite;
        else if (direction == Vector2Int.down)
            headSpriteRenderer.sprite = m_iconProvider.m_headDownSprite;
        else if (direction == Vector2Int.left)
            headSpriteRenderer.sprite = m_iconProvider.m_headLeftSprite;
        else if (direction == Vector2Int.right)
            headSpriteRenderer.sprite = m_iconProvider.m_headRightSprite;
    }

    private void OnTailSetTailSprite(Vector2Int lastBodyDirection,SpriteRenderer tailRenderer)
    {
        var tailSpriteRenderer = tailRenderer;

        if (lastBodyDirection == Vector2Int.up)
            tailSpriteRenderer.sprite = m_iconProvider.m_tailUp;

        if (lastBodyDirection == Vector2Int.down)
            tailSpriteRenderer.sprite = m_iconProvider.m_tailDown;

        if (lastBodyDirection == Vector2Int.left)
            tailSpriteRenderer.sprite = m_iconProvider.m_tailLeft;

        if (lastBodyDirection == Vector2Int.right)
            tailSpriteRenderer.sprite = m_iconProvider.m_tailRight;
    }

    private void OnDisable()
    {
        SnakeMovement.OnTailDirectionChanged -= OnTailSetTailSprite;
        SnakeMovement.OnHeadDirectionChanged -= OnHeadSetSprite;
        SnakeMovement.OnSnakeDirectionChanged -= OnSnakeSetSprite;
    }
}
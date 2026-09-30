using System.Collections;
using System.Collections.Generic;
//using Unity.VisualScripting.ReorderableList;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayableMove : MonoBehaviour
{
    #region 블럭 이동 담당 변수, 프로퍼티
    [SerializeField][Range(0, 20)]
    private float horizontalSpeed;
    [SerializeField][Range(0, 10)]
    private float verticalSpeed;
    [SerializeField]
    private bool isAccelerating; //기능 구현X.
    public float HorizontalSpeed { get => horizontalSpeed; set { horizontalSpeed = value; } }
    public float VerticalSpeed { get => verticalSpeed; set {  verticalSpeed = value; } }
    #endregion

    private InGameUI inGameUI;

    private Rigidbody2D rigid;
    public bool IsFalling { get; private set; } = false;
    [SerializeField]
    private bool isMoving;
    public bool IsMoving { get => isMoving; set { isMoving = value; } }

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();

        inGameUI = FindObjectOfType<InGameUI>();
        if (inGameUI == null)
        {
            Debug.LogError("InGameUI를 찾을 수 없습니다.");
            return;
        }

        MovingHorizontal();
    }

    void Update()
    {
        if ((inGameUI.gameClearUI.activeSelf || inGameUI.gameOverUI.activeSelf) && EventSystem.current != null)
        {
            // UI 외부에서 클릭이 발생했을 때 클릭 무시
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }
        }
        else
        {
            // 게임 클리어/오버 UI가 활성화되지 않은 상태에서 클릭 처리
            if ((Input.GetKeyDown(KeyCode.Return) || Input.GetMouseButtonDown(0)) && IsMoving)
            {
                IsFalling = true; // isFalling이 true가 됨
                MovingVertical(); // 수직 이동 호출
            }
        }


        if ( !IsMoving)
        {
           rigid.velocity = Vector2.zero;
        }
    }
    public void HorizontalReflect()
    {
        if (IsMoving && !IsFalling)
        {
            rigid.velocity = new Vector2(-1 * rigid.velocity.x, rigid.velocity.y);
        }
    }

    public void MovingHorizontal()
    {
        if (IsMoving && !IsFalling)
        {
            int dir = Random.Range(0, 2) == 0 ? 1 : -1;

            rigid.velocity = Vector2.right * HorizontalSpeed * dir;
            Random.Range(-1, 1);
            transform.parent = Camera.main.transform;
        }
    }
    public void MovingVertical()
    {
        if (IsMoving && IsFalling)
        {
            if (isAccelerating)
            {
                // 미사용
            }
            else
            {
                rigid.velocity = Vector2.down * VerticalSpeed;
            }

            transform.parent = null;
        }
    }

    public void StopBlock()
    {
        IsMoving = false;
        rigid.isKinematic = true;
    }
}
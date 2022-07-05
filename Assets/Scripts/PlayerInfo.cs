using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    [Header("Player")]
    public Character character;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }
    
    public void SetIdle()
    {
        _animator.SetInteger("State", 0);
    }

    public void SetWalk()
    {
        _animator.SetInteger("State", 1);
    }
}

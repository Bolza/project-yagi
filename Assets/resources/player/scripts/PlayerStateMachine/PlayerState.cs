using UnityEngine;

public class PlayerState {
    protected Player player;
    protected PlayerStateMachine stateMachine;
    protected PlayerData baseData;
    private string animBoolName;
    protected float inputX;
    protected float inputY;
    protected float startTime;
    protected float endTime;
    protected bool isGrounded;
    protected bool isWalled;
    protected bool duringAnimation;
    protected bool isLedged;
    protected bool jumpInput;
    protected bool attackInput;
    protected bool blockInput;
    protected bool rollInput;
    protected bool headIsFree;
    protected bool gotHit;
    public bool colliderShouldFitAnimation;
    protected Vector2 startPosition;
    protected float maxYMovement;
    protected float maxXMovement;
    private float animationMovementX;
    private float animationMovementY;

    protected bool isExitingState { get; private set; }

    public PlayerState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) {
        this.player = player;
        this.stateMachine = stateMachine;
        this.baseData = playerData;
        this.animBoolName = animBoolName;
    }


    public virtual void Enter() {
        maxXMovement = -1;
        maxYMovement = -1;
        animationMovementX = -1;
        animationMovementY = -1;
        startPosition = player.transform.position;
        isExitingState = false;
        duringAnimation = true;
        player.Anim.SetBool(animBoolName, true);
        startTime = Time.time;
        player.onGotBlocked += OnGotBlocked;
        player.onGotHit += OnGotHit;
        gotHit = false;
        colliderShouldFitAnimation = false;
        DoChecks();
    }

    public virtual void Exit() {
        player.onGotBlocked -= OnGotBlocked;
        player.onGotHit -= OnGotHit;
        isExitingState = true;
        duringAnimation = false;
        player.Anim.SetBool(animBoolName, false);
        colliderShouldFitAnimation = false;
        endTime = Time.time;
    }

    public virtual void LogicUpdate() {
        DoChecks();
    }

    public virtual void PhysicsUpdate() {
    }

    public virtual void DoChecks() {
        inputX = player.InputHandler.NormInputX;
        inputY = player.InputHandler.NormInputY;
        jumpInput = player.InputHandler.jump.hasInput;
        attackInput = player.InputHandler.attack.hasInput;
        blockInput = player.InputHandler.block.hasInput;
        rollInput = player.InputHandler.roll.hasInput;
        isGrounded = player.isGrounded;
        isWalled = player.isWalled;
        isLedged = player.isLedged;
        headIsFree = !isWalled;

        if (isLedged) {
            player.LedgeClimbState.setDetectedPosition(player.transform.position);
        }
    }

    protected virtual bool canMoveX() =>
        maxXMovement < 0 || Mathf.Abs(startPosition.x - player.transform.position.x) < maxXMovement;

    protected virtual bool canMoveY() =>
        maxYMovement < 0 || Mathf.Abs(startPosition.y - player.transform.position.y) < maxYMovement;

    protected virtual bool hasRemainingAnimationMovementX() =>
        animationMovementX > 0 && Mathf.Abs(startPosition.x - player.transform.position.x) < animationMovementX;

    protected virtual bool hasRemainingAnimationMovementY() =>
        animationMovementY > 0 && Mathf.Abs(startPosition.y - player.transform.position.y) < animationMovementY;

    protected virtual void setAnimationMovement(float x, float y) {
        animationMovementX = x;
        animationMovementY = y;
    }

    public virtual void AnimationTrigger() {
        duringAnimation = true;
    }

    public virtual void AnimationFinishTrigger() {
        duringAnimation = false;
    }


    public virtual void OnGotBlocked() { }

    public virtual void OnGotHit() {
        gotHit = true;
    }

}


public class e1_HitState : HitState {
    private e1AI enemy;

    public e1_HitState(Enemy entity, EnemyStateMachine stateMachine, string animBoolName, EnemyData enemyData) : base(entity, stateMachine, animBoolName, enemyData) {
        this.enemy = (e1AI)entity;
    }

    public override void AnimationFinishTrigger() {
        base.AnimationFinishTrigger();
        stateMachine.ChangeState(enemy.IdleState);
    }

    public override void LogicUpdate() {
        base.LogicUpdate();
        if (!duringAnimation) {
            // stateMachine.ChangeState(enemy.StunState);
            //stateMachine.ChangeState(enemy.HitState);
        }
        //else if (!duringAnimation) {
        //    enemy.setVelocityX(0);
        //    stateMachine.ChangeState(enemy.IdleState);
        //}
    }

    public override void Enter() {
        base.Enter();
        float kbDirection = enemy.CalculateKnockback(enemy.lastHit);
        enemy.SetVelocityX(kbDirection);
    }
}

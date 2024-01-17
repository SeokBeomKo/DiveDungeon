public interface IEnemyState
{
    EnemyController controller {get; set;}
    EnemyStateMachine stateMachine {get; set;}
    void Update();
    void FixedUpdate();
    void OnEnter();
    void OnExit();
}

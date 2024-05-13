using UnityEngine;

public class Orbiter : MonoBehaviour
{
    public Transform target; // The object to orbit around
    

    private AbilitySO ability;

    void Update()
    {
        if(GameStateManager.Instance.IsPaused()) return;
        // Rotate around the target every frame
        transform.RotateAround(target.position, Vector3.forward, ability.projectileSpeed * Time.deltaTime);
    }

    public void SetAbilitySO(AbilitySO _ability)
    {
        ability = _ability;
    }
}

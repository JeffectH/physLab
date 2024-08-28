using UnityEngine;

public class DropGasAnimateController : MonoBehaviour
{
    private Animator dropGasAnimator;
    public GameObject dropGasParticles;
    private bool isBlockDropGas = false;
    [SerializeField]
    private string nameAnimateTrigger = "isDrop";
    public void Start()
    {
        dropGasAnimator = GetComponent<Animator>();
    }

    public void StartDropGas()
    {
        if (!isBlockDropGas)
        {
            dropGasAnimator.SetTrigger(nameAnimateTrigger);
            isBlockDropGas = true;
            SpawnParticless();
        }
        
    }

    private void SpawnParticless()
    {
        GameObject tempParticles = Instantiate(dropGasParticles, transform.position, transform.rotation);
        tempParticles.transform.Translate(Vector3.back * 0.1f);
        Destroy(tempParticles, dropGasParticles.GetComponent<ParticleSystem>().main.duration);
    }

    public void setDropBlockState(bool state)
    {
        isBlockDropGas = state;
    }
}

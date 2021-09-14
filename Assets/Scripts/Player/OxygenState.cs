using UnityEngine;

public class OxygenState : MonoBehaviour
{
    [SerializeField] private float timeBetweenOxygenReductions;
    [SerializeField] private float reductionAmount;
    [SerializeField] private float collectibleOxygenAmount;
    private float oxygen;
    [SerializeField] ProgressBar oxygenBar;
    private bool oxygenLossPaused = true;
    [System.NonSerialized] public int extraOxygenContainers;

    // Start is called before the first frame update
    void Start()
    {
        oxygen = 100.0f;
        oxygenBar.BarValue = oxygen;        
    }

    private void Update()
    {
        if(GameState.GetInstance().gamePaused && !oxygenLossPaused)
        {
            PauseOxygenLoss();
            oxygenLossPaused = true;
        }
    }

    private void ReduceOxygen()
    {
        if(oxygen > 0.0f)
        {
            oxygen -= reductionAmount;
        }
        else
        {
            //You lost
            //Debug.Log("You lost");
            LevelManager.GetInstance().currentPlayerHealth = 0;
            GetComponent<PlayerHealth>().TakeHit();
            oxygen = 0.0f;
        }
        oxygenBar.BarValue = oxygen;
    }

    public void UpdateOxygen (float value)
    {        
        oxygen += value;
        if(oxygen > 100.0f)
        {
            oxygen = 100.0f;
            LevelManager.GetInstance().unsavedOxygenContainers++;
            //Change UI
            //TODO: AUDIO Add "gaining extra oxygen" sound if existing
        }
        oxygenBar.BarValue = oxygen;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Oxygen")
        {
            //TODO: AUDIO Add collecting oxygen bubble/filling up tank sound
            FMODUnity.RuntimeManager.PlayOneShot("event:/VO/Roanoke Barks/Oxygen Collection Emote", GetComponent<Transform>().position);
            UpdateOxygen(collectibleOxygenAmount);
            Destroy(collision.gameObject);
        }
    }

    public void StartOxygenLoss()
    {
        oxygenLossPaused = false;
        InvokeRepeating("ReduceOxygen", 0.0f, timeBetweenOxygenReductions);
    }

    public void PauseOxygenLoss()
    {
        CancelInvoke("ReduceOxygen");
    }
}

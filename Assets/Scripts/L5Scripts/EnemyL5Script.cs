using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;


public class EnemyL5Script : MonoBehaviour
{
    [SerializeField] public float enemySpeed = 6.0f;
    [SerializeField] float chaseDistance = 0.75f;
    [SerializeField] float wayPointTolerance = 1f;
    [SerializeField] float wayPointLifeTime = 2f;
    [SerializeField] public int currentWaypointIndex = 0;
    [Range(0, 1)]
    [SerializeField] float patrolSpeedFraction = 0.2f;
    [SerializeField] PatrolPath patrolPath;

    float timeSinceArrivedWayPoint;
    NavMeshAgent agent;

    public Material AggEnemy;
    public GameObject target; //Player is the target
    public GameObject UV1; //To detect player even if he is in blank situation
    public Image chestImage; //If the player steal the chest the enemies are getting more aggravated
    public Text gameOverText; //Game Over Text
    public Text restartText; //Restart Text

    private void Start()
    {
        agent = this.GetComponent<NavMeshAgent>();
        agent.stoppingDistance = 0.05f;
    }

    private void Update()
    {
        wpProcess(); //The Enemies are in their shift period
        if (AggravateMethod()) //The Enemies notice the player
        {
            Pursue();
            if (target.GetComponent<PlayerL5Script>().increaseSpeed)
            {
                enemySpeed = 0;
                this.GetComponentInChildren<SpriteRenderer>().enabled = true;
                Invoke("wpProcess", 2.9f);
                Invoke("GetReady", 2.9f);
                Invoke("WarningLabelOff", 2.9f);
            }
            if (DistanceToPlayer() < 0.5f) //The player was caught!
            {
                enemySpeed = 0;
                target.GetComponent<PlayerL5Script>().getCaught = true;
                gameOverText.GetComponent<Text>().enabled = true;
                gameOverText.GetComponent<Animator>().SetTrigger("GGTrig");
                StartCoroutine(SceneTransition());

            }
        }
        if (chestImage.GetComponent<Image>().enabled)
        {
            chaseDistance = 2f;
            this.GetComponent<MeshRenderer>().material = AggEnemy;
        }

        timeSinceArrivedWayPoint += Time.deltaTime;
    }

    private void WarningLabelOff()
    {
        this.GetComponentInChildren<SpriteRenderer>().enabled = false;
    }

    //Enemy Shift Process
    void wpProcess()
    {
        Vector3 nextPosition = transform.position;
        if (patrolPath != null)
        {
            if (AtWayPoint())
            {
                timeSinceArrivedWayPoint = 0;
                CycleWayPoint();
            }
            nextPosition = GetNextWayPoint();
            if (timeSinceArrivedWayPoint > wayPointLifeTime)
            {
                agent.destination = nextPosition;
                agent.speed = enemySpeed * patrolSpeedFraction;
            }
        }
    }
    private Vector3 GetNextWayPoint()
    {
        return patrolPath.GetWaypointPosition(currentWaypointIndex);
    }

    private void CycleWayPoint()
    {
        currentWaypointIndex = patrolPath.GetNextIndex(currentWaypointIndex);
    }

    private bool AtWayPoint()
    {
        float distanceWaypoint = Vector3.Distance(transform.position, GetNextWayPoint());
        return distanceWaypoint < wayPointTolerance;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseDistance);
    }

    private bool AggravateMethod()
    {
        return DistanceToPlayer() < chaseDistance || UV1.GetComponent<UVMechanism>().alarm;
    }

    void GetReady()
    {
        enemySpeed = 6f;
    }

    //Enemy Chase Process
    public void Seek(Vector3 location)
    {
        agent.SetDestination(location);
    }
    public void Pursue()
    {
        Vector3 targetDir = target.transform.position - this.transform.position;

        float relativeHeading = Vector3.Angle(this.transform.forward, this.transform.TransformVector(target.transform.forward));

        float toTarget = Vector3.Angle(this.transform.forward, this.transform.TransformVector(targetDir));

        if ((toTarget > 90 && relativeHeading < 20) || enemySpeed < 0.01f)
        {
            Seek(target.transform.position);
            return;
        }
        enemySpeed = 8f;

        float lookAhead = targetDir.magnitude / (enemySpeed + 1.5f);
        Seek(target.transform.position + target.transform.forward * lookAhead);
    }
    public float DistanceToPlayer()
    {
        return Vector3.Distance(target.transform.position, transform.position);
    }

    //Scene Management
    IEnumerator SceneTransition()
    {
        yield return new WaitForSeconds(2);
        restartText.GetComponent<Text>().enabled = true;
        restartText.GetComponent<Animator>().SetTrigger("GGTrig");
    }
}

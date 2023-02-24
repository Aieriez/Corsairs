using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; 
    [SerializeField] List<Room> rooms;
    [SerializeField] Ship ship;
    [SerializeField] GameObject respawn;
    public bool endGame = false;
    public int enemiesNumber;
    [field:SerializeField]private Room currentRoom;
    public int pointsCount;
    

    public Room CurrentRoom
    {
        get { return currentRoom; }
        set {
            currentRoom?.DisableCamera();
            currentRoom = value;
            currentRoom.EnableCamera(ship.transform);
        }
    }
    void Awake()
	{
		if(instance == null)
		{
			instance = this;
		}
		else
		{
			Destroy (gameObject);
		}
    }

    void Start() 
    {
        rooms.ForEach(room => room.OnRoomActivated += HandleRoomActivated);
        pointsCount = 0;
        UIManager.instance.backgroundBar.SetActive(true);
        Time.timeScale = 1;
    }

    private void OnDestroy() 
    {
        rooms.ForEach(room => room.OnRoomActivated -= HandleRoomActivated);
    }
    private void HandleRoomActivated(Room room)
    {
        CurrentRoom = room;
    }

    void Update() 
    {
        enemiesNumber = FindObjectsOfType<EnemyShooter>().Length + FindObjectsOfType<EnemyChaser>().Length;
        UIManager.instance.pointCount.text = pointsCount.ToString();
        UIManager.instance.enemyCount.text = enemiesNumber.ToString();

        if(enemiesNumber == 0)
        {
            endGame = true;
        }

        if(endGame)
        {
            UIManager.instance.backgroundBar.SetActive(false);
            UIManager.instance.gameMenu.Play("EndGame");
            UIManager.instance.totalPoints.text = pointsCount.ToString();
            Time.timeScale = 0;
        }
    }
}

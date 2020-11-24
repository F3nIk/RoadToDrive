using Doozy.Engine.Nody;
using Doozy.Engine.UI;
using UnityEngine;
using UnityEngine.Events;

public enum GameState
{
    Menu,
    Race,
    RefillPopup,
    GettingReward

}

public class GameStateManager : MonoBehaviour
{
    [SerializeField] private GraphController uiController;

    public static UnityAction<MoveType> RaceStarted;
    public static UnityAction RefillPopupOpened;
    public static UnityAction<MoveType> RaceEnded;
    public static UnityAction ReturnedToMenu;

    private bool isRefillShowed = false;
    private bool isRewardGetted = false;

	private void OnEnable()
	{
        PlayerVehicle.FuelEnded += OpenRefieldPopup;
	}

	private void OnDisable()
	{
        PlayerVehicle.FuelEnded -= OpenRefieldPopup;
    }

	public void StartRace()
    {
        RaceStarted?.Invoke(MoveType.Race);    
	}

    public void OpenRefieldPopup()
    {
        if (isRefillShowed == false)
        {
            uiController.GoToNodeByName("Refill window");
            isRefillShowed = true;
        }
        else if(isRefillShowed == true && isRewardGetted == false)
        {
            uiController.GoToNodeByName("Reward window");
            isRewardGetted = true;
        }
    }

    public void EndRace()
    {
        RaceEnded?.Invoke(MoveType.Preview);
        isRefillShowed = false;
        isRewardGetted = false;
    }

    public void Return()
    {
        ReturnedToMenu?.Invoke();
	}
}

using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : Singleton<GameManager>
{
    // 게임에 필요한 주요 컴포넌트들을 관리하는 변수들
    private Player player;
    private ItemDataManager itemDataManager;
    private Inventory_UI inventoryUI;
    private WorldInventory_UI worldInventoryUI;
    private InventoryManager inventoryManager;
    private TimeSystem timeSys;
    private WeaponBase weaponBase;
    private ShopInventoryUI shopInventoryUI; // 새롭게 추가된 샵 인벤토리 UI
    private Equip_UI equipUI;
    private PlayerUI playerUI;

    // 외부 접근을 위한 프로퍼티 선언
    public Player Player => player;
    public ItemDataManager ItemData => itemDataManager;
    public Inventory_UI InventoryUI => inventoryUI;
    public WorldInventory_UI WorldInventory_UI => worldInventoryUI;
    public InventoryManager InventoryManager => inventoryManager;
    public TimeSystem TimeSystem => timeSys;
    public WeaponBase WeaponBase => weaponBase;
    public ShopInventoryUI ShopInventoryUI => shopInventoryUI;
    public Equip_UI EquipUI => equipUI;
    public PlayerUI PlayerUI => playerUI;

    // 게임 씬 로딩 완료와 게임 종료 신호를 위한 델리게이트 및 이벤트
    public delegate void SceneAction();
    public event SceneAction OnGameStartCompleted; // 게임 시작 후 호출될 이벤트
    public event SceneAction OnGameEnding; // 게임 종료 전 호출될 이벤트

    // 게임 초기화 시 필요한 컴포넌트를 찾아서 할당
    protected override void OnInitialize()
    {
        base.OnInitialize(); // 상속받은 Singleton의 초기화를 먼저 호출
        LoadComponentReferences();

        Inventory inven = new Inventory(this);

        if (InventoryUI != null)
        {
            InventoryUI.InitializeInventory(inven);
        }

        WorldInventory worldInven = new WorldInventory(this);

        if(worldInventoryUI != null)
        {
            worldInventoryUI.InitializeWorldInventory(worldInven);
        }

        Equip equip = new Equip(GameManager.Instance);

        if(equipUI != null)
        {
            equipUI.InitializeInventory(equip);
        }
    }

    // 초기화 이전에 필요한 컴포넌트를 미리 설정
    protected override void OnPreInitialize()
    {
        base.OnPreInitialize();
        itemDataManager = GetComponent<ItemDataManager>(); // 아이템 데이터 관리자 컴포넌트 찾기
    }

    private void LoadComponentReferences()
    {
        player = FindAnyObjectByType<Player>();
        inventoryUI = FindAnyObjectByType<Inventory_UI>();
        worldInventoryUI = FindAnyObjectByType<WorldInventory_UI>();
        inventoryManager = FindAnyObjectByType<InventoryManager>();
        timeSys = FindAnyObjectByType<TimeSystem>();
        weaponBase = FindAnyObjectByType<WeaponBase>();
        shopInventoryUI = FindAnyObjectByType<ShopInventoryUI>();
        equipUI = FindObjectOfType<Equip_UI>();
        playerUI = FindAnyObjectByType<PlayerUI>();
    }

    public void StartGame(string sceneName)
    {
        if (sceneName == "InGameScene")
        {
            SaveWorldInventory();
            SaveInventory();
        }
        StartCoroutine(LoadScene(sceneName, OnGameStartCompleted));
    }

    public void EndGame(string sceneName)
    {
        if (sceneName == "MainMenuScene")
        {
            SaveInventory();
        }
        StartCoroutine(LoadScene(sceneName, OnGameEnding));
    }

    private IEnumerator LoadScene(string sceneName, SceneAction onLoaded)
    {
        Debug.Log($"이 씬을 로딩 중: {sceneName}");
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        while (!asyncLoad.isDone)
        {
            Debug.Log($"로드 진행 상황 {sceneName}: {asyncLoad.progress * 100}%");
            yield return null;
        }

        if (sceneName == "MainMenuScene")
        {
            LoadWorldInventory();
            LoadInventory();
        }
        else if (sceneName == "InGameScene")
        {
            LoadInventory();
        }

        onLoaded?.Invoke();
    }

    public void SaveWorldInventory()
    {
        if (worldInventoryUI != null)
            worldInventoryUI.WorldInven.SaveInventoryToJson();
    }

    public void LoadWorldInventory()
    {
        if (worldInventoryUI != null)
            worldInventoryUI.WorldInven.LoadInventoryFromJson();
    }

    public void SaveInventory()
    {
        if (inventoryUI != null)
            inventoryUI.Inventory.SaveInventoryToJson();
    }

    public void LoadInventory()
    {
        if (inventoryUI != null)
            inventoryUI.Inventory.LoadInventoryFromJson();
    }
}

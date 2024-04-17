public class GameManager : Singleton<GameManager>
{
    // �÷��̾�, ������ ������ ������, �κ��丮 UI ���� ����
    private Player player;
    private ItemDataManager itemDataManager;
    private Inventory_UI inventoryUI;
    private WorldInventory_UI worldInventoryUI;
    private TimeSystem timeSys;

    // �÷��̾�, ������ ������ ������, �κ��丮 UI �Ӽ� ����
    public Player Player => player;
    public ItemDataManager ItemData => itemDataManager;
    public Inventory_UI InventoryUI => inventoryUI;
    public WorldInventory_UI WorldInventory_UI => worldInventoryUI;
    public TimeSystem TimeSystem => timeSys;

    // �ʱ�ȭ �ܰ迡�� �÷��̾�, ������ ������ ������, �κ��丮 UI �ʱ�ȭ
    protected override void OnInitialize()
    {
        player = FindAnyObjectByType<Player>();
        inventoryUI = FindAnyObjectByType<Inventory_UI>();
        worldInventoryUI = FindAnyObjectByType<WorldInventory_UI>();
        timeSys = FindAnyObjectByType<TimeSystem>();
    }

    // ������ ������ ������ �ʱ�ȭ ���� ȣ��Ǵ� �ʱ�ȭ �ܰ�
    protected override void OnPreInitialize()
    {
        base.OnPreInitialize();
        itemDataManager = GetComponent<ItemDataManager>();
    }
}

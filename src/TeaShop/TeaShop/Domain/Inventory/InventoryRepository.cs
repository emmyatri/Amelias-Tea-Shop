namespace TeaShop.Domain.Inventory;


/// <summary>
/// Stores and manages the tea inventory. Provides read-only access
/// and controlled quantity updates.
/// </summary>
public class InventoryRepository
{
    private readonly List<InventoryItem> _items;

    public InventoryRepository()
    {
        _items = new List<InventoryItem>
        {
            new(Guid.NewGuid(), "Da Hong Pao (Big Red Robe) – Wuyi Rock Oolong", 15.85m, 42, new StarRating(3)),

            new(Guid.NewGuid(), "Tie Guan Yin (Iron Goddess of Mercy) – Anxi Oolong", 17.68m, 137, new StarRating(1)),

            new(Guid.NewGuid(), "Dragon Well (Longjing) – Premium Green", 29.90m, 8, new StarRating(5)),

            new(Guid.NewGuid(), "Bi Luo Chun (Green Snail Spring) - Premium Green", 12.36m, 163, new StarRating(2)),

            new(Guid.NewGuid(), "Keemun Mao Feng – Black Tea", 7.99m, 71, new StarRating(4)),

            new(Guid.NewGuid(), "Golden Monkey (Jin Hou) – Black Tea", 8.52m, 94, new StarRating(1)),

            new(Guid.NewGuid(), "Lapsang Souchong – Smoked Black Tea", 6.99m, 15, new StarRating(3)),

            new(Guid.NewGuid(), "Bai Hao Yin Zhen (Silver Needle) – White Tea", 10.49m, 12, new StarRating(5)),

            new(Guid.NewGuid(), "Bai Mu Dan (White Peony) – White Tea", 16.99m, 51, new StarRating(2)),

            new(Guid.NewGuid(), "Shou Mei – Aged White Tea", 23.99m, 3, new StarRating(4)),

            new(Guid.NewGuid(), "Dianhong (Golden Yunnan) - Black Tea", 25.99m, 89, new StarRating(1)),

            new(Guid.NewGuid(), "Yunnan - Oolong", 23.99m, 6, new StarRating(3)),

            new(Guid.NewGuid(), "Yunnan - Green Tea", 21.99m, 7, new StarRating(2)),

            new(Guid.NewGuid(), "Huangshan Mao Feng (Yellow Mountain Fur Peak) - Green Tea", 14.99m, 144, new StarRating(5)),

            new(Guid.NewGuid(), "Tai Ping Hou Kui (Monkey King Green) - Green Tea", 21.99m, 27, new StarRating(4)),

            new(Guid.NewGuid(), "Liu An Gua Pian (Melon Seed Green) - Green Tea", 8.99m, 33, new StarRating(1)),

            new(Guid.NewGuid(), "Wuyi Shui Xian (Water Sprite) - Oolong", 13.99m, 19, new StarRating(3)),

            new(Guid.NewGuid(), "Wuyi Rou Gui (Cinnamon Rock Tea) - Oolong", 19.99m, 97, new StarRating(2)),

            new(Guid.NewGuid(), "Phoenix Dan Cong Ya Shi (Duck Sh*t) - Oolong", 124.99m, 0, new StarRating(5)),

            new(Guid.NewGuid(), "Dian Hong (Yunnan Golden Tips) - Black Tea", 85.99m, 2, new StarRating(4)),

            new(Guid.NewGuid(), "Junshan Yinzhen – Yellow Tea", 46.99m, 97, new StarRating(3)),

            new(Guid.NewGuid(), "Dong Ting Bi Luo Chun – Green Tea", 52.99m, 6, new StarRating(1)),

            new(Guid.NewGuid(), "Mengding Huangya – Yellow Tea", 63.99m, 146, new StarRating(2)),

            new(Guid.NewGuid(), "Anji Bai Cha (White-Leaf) - Green Tea", 32.99m, 55, new StarRating(4)),

        };
    }

    /// <summary>
    /// Returns a read-only view of the inventory.
    /// </summary>
    public IReadOnlyList<InventoryItem> Items => _items.AsReadOnly();

    
    /// <summary>
    /// Reduces the quantity of the specified item after purchase.
    /// </summary>
    /// <exception cref="InvalidOperationException">
    /// Thrown when the purchase amount exceeds available stock.
    /// </exception>
    public void UpdateQuantity(Guid id, int quantityPurchased)
    {
        var item = _items.First(i => i.Id == id);
        item.ReduceQuantity(quantityPurchased);
        
    }
    
}
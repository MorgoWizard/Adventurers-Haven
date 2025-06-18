public class FoodItem : ConsumableItem
{
    private void Awake()
    {
        _needType = NeedType.Hunger;
    }
}
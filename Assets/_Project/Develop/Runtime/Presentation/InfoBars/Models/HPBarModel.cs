public class HPBarModel
{
    private int _startHP;

    public void SetStartHP(int startHP)
    {
        _startHP = startHP;
    }

    public int GetStartHP()
    {
        return _startHP;
    }

    public bool IsLowHP(int currentHP)
    {
        return currentHP <= _startHP / 4 && currentHP > 0;
    }
}

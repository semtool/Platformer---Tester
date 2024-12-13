public class HealthBar : BarScale
{
    public override void ChangeBarView()
    {
        _barScale.value = GetCurrentBarVolue();
    }
}
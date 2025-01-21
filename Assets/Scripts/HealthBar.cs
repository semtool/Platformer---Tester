public class HealthBar : BarScaler
{
    public override void ChangeBarView()
    {
        _barScale.value = GetCurrentBarVolue();
    }
}
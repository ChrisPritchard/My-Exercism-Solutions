using System;

class RemoteControlCar
{
    int power = 100;

    public static RemoteControlCar Buy() => new RemoteControlCar();

    public string DistanceDisplay() => $"Driven {(100 - power)*20} meters";

    public string BatteryDisplay() => power > 0 ? $"Battery at {power}%" : "Battery empty";

    public void Drive()
    {
        if (power > 0)
            power--;
    }
}

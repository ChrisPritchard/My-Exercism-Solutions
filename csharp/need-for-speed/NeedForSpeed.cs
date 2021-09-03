using System;

class RemoteControlCar
{
    readonly int speed, batteryDrain;
    int distance = 0;
    int battery = 100;

    public RemoteControlCar(int speed, int batteryDrain)
    {
        this.speed = speed;
        this.batteryDrain = batteryDrain;
    }

    public bool BatteryDrained() => battery <= 0;

    public int DistanceDriven() => distance;

    public void Drive()
    {
        if (BatteryDrained())
            return;
        distance += speed;
        battery -= batteryDrain;
    }

    public static RemoteControlCar TopOfTheLine() => new RemoteControlCar(50, 4);
}

class RaceTrack
{
    int distance;

    public RaceTrack(int distance) => this.distance = distance;

    public bool CarCanFinish(RemoteControlCar car)
    {
        for(;;) {
            car.Drive();
            if (car.DistanceDriven() >= this.distance)
                return true;
            if (car.BatteryDrained())
                return false;
        }
    }
}

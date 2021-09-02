class Lasagna
{
    public int ExpectedMinutesInOven() => 40;

    public int RemainingMinutesInOven(int passed) => ExpectedMinutesInOven() - passed;

    public int PreparationTimeInMinutes(int layers) => layers * 2;

    public int ElapsedTimeInMinutes(int layers, int elapsed) => PreparationTimeInMinutes(layers) + elapsed;
}

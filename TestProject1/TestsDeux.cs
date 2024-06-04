using BlazorApp1.Components.Pages;

namespace TestProject1;

public class TestsDeux
{
    private PomodoroTimer _pomodoroTimer;

    [SetUp]
    public void Setup()
    {
        _pomodoroTimer = new Pomodoro.TimerSessionData();
        _pomodoroTimer.OnInitializedAsync().GetAwaiter().GetResult(); // Initialize timer
    }

    [Test]
    public async Task TimerElapsed_UpdatesTimeCorrectly()
    {
        // Arrange
        _pomodoroTimer.StartTimer();
        _pomodoroTimer.minutes = 0;
        _pomodoroTimer.seconds = 2;

        // Act
        await Task.Delay(3000); // Wait 3 seconds

        // Assert
        Assert.AreEqual(0, _pomodoroTimer.minutes);
        Assert.AreEqual(0, _pomodoroTimer.seconds);
        Assert.IsFalse(_pomodoroTimer.isRunning);
    }

    [Test]
    public void OnRadioChange_SetsCorrectTimes()
    {
        // Arrange
        _pomodoroTimer.timer25 = true;

        // Act
        _pomodoroTimer.OnRadioChange();

        // Assert
        Assert.AreEqual(25, _pomodoroTimer.workTime);
        Assert.AreEqual(5, _pomodoroTimer.breakTime);
    }

    [Test]
    public void OnRadioChange_SetsCorrectTimesFor45_15()
    {
        // Arrange
        _pomodoroTimer.timer25 = false;

        // Act
        _pomodoroTimer.OnRadioChange();

        // Assert
        Assert.AreEqual(45, _pomodoroTimer.workTime);
        Assert.AreEqual(15, _pomodoroTimer.breakTime);
    }
}

using System.Timers;
using Microsoft.AspNetCore.Components;
using BlazorApp1.Components.Pages;
using Moq;
using Timer = System.Threading.Timer;

namespace TestProject1.Components.Pages;

[TestFixture]
[TestOf(typeof(Pomodoro))]
public class PomodoroTest
{
    private Pomodoro _pomodoro;
    private Mock<Pomodoro> _mockPomodoro;

    [SetUp]
    public void Setup()
    {
        // Arrange
        _pomodoro = new Pomodoro();
        _mockPomodoro = new Mock<Pomodoro>();
    }
    
    [Test]
    public void OnRadioChange_SetsWorkAndBreakTimeFor25MinuteMode_WhenTimer25IsTrue()
    {
        _pomodoro.timer25 = false;
        _pomodoro.OnRadioChange();
        Assert.AreEqual(25, _pomodoro.workTime);
        Assert.AreEqual(5, _pomodoro.breakTime);
    }
    
    [Test]
    public void OnRadioChange_SetsWorkAndBreakTimeFor45MinuteMode_WhenTimer25IsFalse()
    {
        _pomodoro.timer25 = true;
        _pomodoro.OnRadioChange();
        Assert.AreEqual(45, _pomodoro.workTime);
        Assert.AreEqual(15, _pomodoro.breakTime);
    }
    
    [Test]
    public void StartTimer_SetsIsRunningToTrueAdStartsTimer_WhenIsRunningIsFalse()
    {
        _pomodoro.isRunning = false;
        _pomodoro.StartTimer();
        Assert.IsTrue(_pomodoro.isRunning);
    }
    
    [Test]
    public void StartTimer_StartsTimer_WhenNotRunning()
    {
        // Arrange
        var mock = new Mock<Pomodoro> { CallBase = true };
        mock.Setup(m => m.InvokeStateHasChanged()).Verifiable();
        var pomodoro = mock.Object;
        pomodoro.isRunning = false;

        // Act
        pomodoro.StartTimer();

        // Assert
        Assert.IsTrue(pomodoro.isRunning);
        mock.Verify(m => m.InvokeStateHasChanged(), Times.Once);
    }
    
    [Test]
    public void ResetTimer_StopsTimerAndResetsValues_WhenTimerIsRunning()
    {
        // Arrange
        var mock = new Mock<Pomodoro> { CallBase = true };
        mock.Setup(m => m.InvokeStateHasChanged()).Verifiable();
        var pomodoro = mock.Object;
        pomodoro.isRunning = true;
        pomodoro.minutes = 10;
        pomodoro.seconds = 30;

        // Act
        pomodoro.ResetTimer();

        // Assert
        Assert.IsFalse(pomodoro.isRunning);
        Assert.AreEqual(pomodoro.workTime, pomodoro.minutes);
        Assert.AreEqual(0, pomodoro.seconds);
        mock.Verify(m => m.InvokeStateHasChanged(), Times.Once);
    }

}

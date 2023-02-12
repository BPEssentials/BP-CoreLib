<!-- panels:start -->
# Interval

A utility class for running code at a specified interval.

```csharp
public class Interval
```

## Constructors

## Properties
<!-- div:title-panel -->
### Time
The interval between actions. This value should be expressed in seconds, but can contain decimals. (eg. 0.5f for 500ms) 
<!-- div:left-panel -->
```csharp
public float Time { get; set; }
```
<!-- div:right-panel -->
```csharp
Interval interval = Interval.StartNew(5, () => { /* ... */ });
interval.Time // 5
```

<!-- div:title-panel -->
### MaxIterations
The maximum amount of times this interval will be called. Set to -1 for infinite.
```csharp
public int MaxIterations { get; set; } = -1;
```

<!-- div:title-panel -->
### Container
The container from which the coroutine will be started. This is usually the SvManager instance.
<!-- div:left-panel -->
```csharp
public MonoBehaviour Container { get; set; } = SvManager.Instance;
```
!> Cannot be used with the shorthand `Interval.StartNew()` method, unless you stop, change container, and start the interval again.
<!-- div:right-panel -->
```csharp
private readonly SvPlayer _player;

public void CreatePlayerLoop()
{
    // Would automatically be cleaned up when the player disconnects.
    _interval = new Interval(2, OnSignal)
    {
        Container = _player,
    };
}

public void OnSignal()
{
    if (!_player.IsInArea(GasArea))
    {
        return;
    }
    // Realistically, mask filters should also be checked here (eg. durability checks). But hey, this is just an example.
    if (_player.IsWearing(Items.GasMask))
    {
        return;
    }
    
    _player.Damage(10);
}
```

<!-- div:title-panel -->
### Action
The actual action that will be called. This action will be called every `Time` seconds.
```csharp
public Action Action { get; set; }
```
?>  It is not recommended to change this value while the interval is running. If you need to change the action, you should stop, change, and start the interval again.

<!-- div:title-panel -->
### IsRunning
Determines if the interval is currently running.
```csharp
public bool IsRunning { get; private set; }
```

<!-- div:title-panel -->
### Iteration
The current iteration of the interval. This value will be reset to 0 when the interval is started, and will be incremented every time the action is ran.
```csharp
public int Iteration { get; private set; }
```

## Computed Properties
<!-- div:title-panel -->
### RanFor
Fetches how long the interval has been running for. This is simply calculated by multiplying the `Iteration` by the `Time` value.
```csharp
public float RanFor => Iteration * Time;
```

<!-- div:title-panel -->
### RemainingIterations
Determines the remaining iterations of the interval. This is calculated by subtracting the `Iteration` from the `MaxIterations` value.
This value is `-1` if the interval is infinite.
```csharp
public int RemainingIterations => IsInfinite ? -1 : MaxIterations - Iteration;
```

<!-- div:title-panel -->
### IsInfinite
Determines if this interval has been marked as infinite.
```csharp
public bool IsInfinite => MaxIterations < 0;
```

<!-- div:title-panel -->
### IsLastIteration
Determines if this current iteration is the last iteration.
Can be used to invoke cleanup code.
```csharp
public bool IsLastIteration => Iteration == MaxIterations;
```

## Methods
<!-- div:title-panel -->
### Start
Starts the interval. If the interval is already running, the interval will be stopped and restarted.
```csharp
public void Start()
```

<!-- div:title-panel -->
### Stop
Stops the interval.
```csharp
public void Stop()
```

<!-- div:title-panel -->
### StartEnumerator
Internal enumerator used for the interval. While this should not to be called directly, but instead through `Start`.
```csharp
public IEnumerator StartEnumerator()
```

## Static Methods
<!-- div:title-panel -->
### StartNew
Starts a new interval and returns the newly created (and started) instance.
```csharp
public static Interval StartNew(int interval, Action action)
```
<!-- panels:end -->

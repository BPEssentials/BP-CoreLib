<!-- panels:start -->
# Features

<!-- div:title-panel -->
## [Interval](/Features/Interval.md)

<!-- div:left-panel -->
> A utility class for running code at a specified interval.

**Path:** `BPCoreLib.Util.Interval`  
<!-- div:right-panel -->
```csharp
using BPCoreLib.Util;

public class Race
{
    private readonly Interval _interval;
    
    public Race()
    {
        // Constructing a interval does not instantly start it,
        // yet Interval.StartNew() does.
        _interval = new Interval(1, OnInterval)
        {
            MaxIterations = 5,
        };
    }

    public void StartCountdown()
    {
        _interval.Start();
    }

    public void StopCountdown()
    {
        _interval.Stop();
    }
    
    private void StartRace()
    {
        // ...
    }

    // This method will be called every second for 5 seconds.
    private void OnInterval()
    {
        if (_interval.IsLastIteration)
        {
            // This is the final iteration, start the race!
            // Note, calling Stop() here is not necessary.
            StartRace();
            return;
        }

        InterfaceHandler.SendGameMessageToAll($"Starting in {_interval.RemainingIterations} seconds!");
    }
}
```


<!-- div:title-panel -->
## [Reader](/Features/Reader.md)

<!-- div:left-panel -->
> A class that provides methods for reading and writing JSON files.

**Path:** `BPCoreLib.Util.Reader`
<!-- div:right-panel -->
```csharp
using BPCoreLib.Util;

public class Example
{
    private readonly IReader<MyModel> _reader = new Reader<MyModel>("Example.json");

    public void Read()
    {
        MyModel model = _reader.Read();
        // Do something with the model.
    }
}
```

<!-- div:title-panel -->
## [PlayerFactory](/Features/PlayerFactory.md)

<!-- div:left-panel -->
> At basis, ExtendedPlayerFactory/PlayerFactory provides a way to extend the `ShPlayer` class with custom properties and methods.

**Path:** `BPCoreLib.PlayerFactory`
<!-- div:right-panel -->
```csharp
using BPCoreLib.PlayerFactory;

public class MyCustomType : ExtendedPlayer
{
    public int MyCustomProperty { get; set; }
    
    // Needed for ExtendedPlayerFactories.Register<T>() overload, otherwise unneeded.
    public MyCustomType()
    {
    }
    
    public MyCustomType(ShPlayer player) : base(player)
    {
    }
}

public class Core
{
    public Core()
    {
        ExtendedPlayerFactories.Register<MyCustomType>();
    }
    
    public void OnEvent(ShPlayer player)
    {
        MyCustomType customPlayer = player.GetExtended<MyCustomType>();
        customPlayer.MyCustomProperty += 5;
    }   
}
```

<!-- panels:end -->

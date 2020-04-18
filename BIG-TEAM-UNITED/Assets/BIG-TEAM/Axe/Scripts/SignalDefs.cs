
using deVoid.Utils;

/// <summary>
/// Input Events
/// </summary>
//button press by ID
public class ButtonPressedSignal : ASignal<int> { }
//button press by Id and held time
public class ButtonHeldSignal : ASignal<int, float> { }
public class ButtonReleasedSignal : ASignal<int, float> { }

/// <summary>
/// Game Events
/// </summary>
public class StartGameSignal : ASignal { }
public class EndGameSignal : ASignal { }
public class ScoreSignal : ASignal<string, int> { }


public class EnableButtonSignal : ASignal<int> { }
public class DisableButtonSignal : ASignal<int> { }

public class LightOffSignal : ASignal<int> { }
public class LightOnGreenSignal : ASignal<int> { }
public class LightOnYellowSignal : ASignal<int> { }
public class LightOnRedSignal : ASignal<int> { }


public class EggSpinUpSignal : ASignal<float> { }
public class EggSpinDownSignal : ASignal<float> { }
public class EggStopSignal : ASignal{ }
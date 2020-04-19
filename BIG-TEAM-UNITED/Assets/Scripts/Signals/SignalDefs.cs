
using deVoid.Utils;
using UnityEngine;

/// <summary>
/// Input Events
/// </summary>
//button press by ID
public class ButtonPressedSignal : ASignal<Component, int> { }
//button press by Id and held time
public class ButtonHeldSignal : ASignal<Component, int, float> { }
public class ButtonReleasedSignal : ASignal<Component, int, float> { }

/// <summary>
/// Game Events
/// </summary>
public class StartGameSignal : ASignal { }
public class EndGameSignal : ASignal { }
public class ScoreSignal : ASignal<string, int> { }


public class EnableButtonSignal : ASignal<Component, int> { }
public class DisableButtonSignal : ASignal<Component, int> { }

public class LightOffSignal : ASignal<int> { }
public class LightOnGreenSignal : ASignal<int> { }
public class LightOnYellowSignal : ASignal<int> { }
public class LightOnRedSignal : ASignal<int> { }


public class EggSpinUpSignal : ASignal<float> { }
public class EggSpinDownSignal : ASignal<float> { }
public class EggStopSignal : ASignal{ }



public class PerformVerbSignal : ASignal<Component, LifeformManager.EControlVerbs, int> { };

public class DismissTankSignal : ASignal { }

public class ReadyTankSignal : ASignal { }

public class NewStageStartingSignal : ASignal<State_LifeForm_Growing> { };
public class StageSucceeded : ASignal<State_LifeForm_Growing> { };

public class StageFailed : ASignal<State_LifeForm_Growing> { };
public class DisplayTerminalMessageSignal : ASignal<string> { }

public class PuzzleProgress : ASignal<Component> { };
public class PuzzleComplete : ASignal<Component> { };

public class PuzzleError : ASignal<Component> { };

public class TimeOut : ASignal { };
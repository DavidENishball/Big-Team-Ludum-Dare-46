using deVoid.Utils;

public class SimonButtonPressedSignal : ASignal<int> { }

public class LockLightSignal : ASignal<int> { }

public class CorrectLightSignal : ASignal<int> { }
public class IncorrectLightSignal : ASignal<int> { }

public class FlashLightSignal : ASignal<int, int> { }
public class ClearLightSignal : ASignal<int> { }

public class SubmitLightPress : ASignal<int> { }
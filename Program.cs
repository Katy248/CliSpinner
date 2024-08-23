// var spinner = new Spinner("⠋⠙⠹⠸⠼⠴⠦⠧⠇⠏");
// var spinner = new Spinner("🌑🌒🌓🌔🌕🌖🌗🌘");
// var spinner = new Spinner("🕐🕑🕒🕓🕔🕕🕖🕗🕘🕙🕚");
// var spinner = new Spinner("◲◲◲◲◳◰◱");
var spinner = new Spinner("⢤⣠⣄⡤⠖⠋⠖⡤");
var duration = 200;

var cursor = Console.GetCursorPosition();
Console.ForegroundColor = ConsoleColor.Red;
Console.CursorVisible = false;
var thread = new Thread(() =>
{
  while (true)
  {
    spinner.PrintNext();
    Thread.Sleep(duration);
  }
});
thread.Start();

Console.ReadKey();
Console.CursorVisible = true;


thread.Interrupt();

class CursorPosition : Tuple<int, int>
{
  public CursorPosition(int left, int top) : base(left, top)
  {
  }
  public int Left => this.Item1;
  public int Top => this.Item2;

  public void Set()
  {
    Console.SetCursorPosition(Left, Top);
  }

  public static CursorPosition FromCurrent()
  {
    var position = Console.GetCursorPosition();
    return new(position.Left, position.Top);
  }
}

class Frame { }
class Spinner
{
  public Spinner(string frames)
  {
    Frames = frames;
    _cursorPosition = CursorPosition.FromCurrent();
  }
  private CursorPosition _cursorPosition;
  public string Frames { get; }
  private int _currentFrameIndex = -1;

  public void PrintNext()
  {
    _cursorPosition.Set();
    Next();
    Console.Write(GetFrame());
  }
  public char GetFrame()
  {
    return Frames[_currentFrameIndex];
  }

  public void Next()
  {
    _currentFrameIndex++;
    if (_currentFrameIndex >= Frames.Length)
      _currentFrameIndex = 0;
  }
}



namespace ios_exception_test;

[Register ("AppDelegate")]
public class AppDelegate : UIApplicationDelegate
{
    public override UIWindow? Window { get; set; }

    private UILabel _label;

    public override bool FinishedLaunching (UIApplication application, NSDictionary? launchOptions)
    {
        // create a new window instance based on the screen size
        Window = new UIWindow (UIScreen.MainScreen.Bounds);

        // create a UIViewController with a single UILabel
        var vc = new UIViewController();

        _label = new UILabel(Window!.Frame)
        {
            BackgroundColor = UIColor.SystemBackground,
            TextAlignment = UITextAlignment.Center,
            Text = "Tap here to start testing",
            AutoresizingMask = UIViewAutoresizing.All,
            UserInteractionEnabled = true
        };

        vc.View!.AddSubview (_label);

        _label.AddGestureRecognizer (new UITapGestureRecognizer(OnClick));
        
        Window.RootViewController = vc;

        // make the window visible
        Window.MakeKeyAndVisible ();

        return true;
    }

    private void OnClick()
    {
        try
        {
            TestAsync();
        }
        catch
        {
        }
    }

    private async Task TestAsync()
    {
        try
        {
            var tasks = Enumerable.Range(0, 30).Select(_ => Task.Run(TestTaskAsync));
            await Task.WhenAll(tasks);
        }
        catch
        {
        }
        
        _label.Text = "Test finished: " + DateTime.Now.ToString();
    }

    private async Task TestTaskAsync()
    {
        await Task.Yield();

        throw new Exception();
    }
}

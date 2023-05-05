using BoDi;
using Microsoft.Playwright;
using Newtonsoft.Json;

namespace SpecFlowPlaywrightXUnitExample.Steps
{
    [Binding]
    internal class Hooks
    {
        public IBrowser browser;
        public IBrowserContext context;
        public IPage page;
        public IPlaywright playwright;
        private string cdpUrl = "";
        private readonly IObjectContainer _objectContainer;
        private readonly ScenarioContext _scenarioContext;

        public Hooks(IObjectContainer objectContainer, ScenarioContext scenarioContext)
        {
            _objectContainer = objectContainer;
            _scenarioContext = scenarioContext;
        }
        [BeforeScenario()]
        public async Task CreateCapabilities()
        {
            string user, accessKey;
            string browserName =  JsonConvert.SerializeObject(_scenarioContext.ScenarioInfo.Arguments["browser"]).Replace("\"", "");


            user = Environment.GetEnvironmentVariable("BROWSERSTACK_USERNAME");
            accessKey = Environment.GetEnvironmentVariable("BROWSERSTACK_ACCESS_KEY");
            Dictionary<string, string> browserstackOptions = new Dictionary<string, string>();

            browserstackOptions.Add("project", "Playwright Test");
            browserstackOptions.Add("name", "Playwright Specflow Test");
            browserstackOptions.Add("build", "Playwright C-Sharp Specflow XUnit tests");
            browserstackOptions.Add("os", "Windows");
            browserstackOptions.Add("os_version", "10");
            browserstackOptions.Add("browserstack.username", user);
            browserstackOptions.Add("browserstack.accessKey", accessKey);
            browserstackOptions.Add("browser", browserName);
            browserstackOptions.Add("browser_version", "latest");

            try {
                string capsJson = JsonConvert.SerializeObject(browserstackOptions);
                cdpUrl = "wss://cdp.browserstack.com/playwright?caps=" + Uri.EscapeDataString(capsJson);
                playwright = await Playwright.CreateAsync();
                browser = await playwright.Chromium.ConnectAsync(cdpUrl);
                context = await browser.NewContextAsync();
                page = await context.NewPageAsync();
                _objectContainer.RegisterInstanceAs(page);
            } catch (Exception e) {
                Console.WriteLine("{0} Exception caught.", e);
            }

        }

        [AfterScenario()]
        public async Task CloseDriver()
        {
            if (_scenarioContext.TestError != null)
            {
                await SetSessionStatus("failed", _scenarioContext.TestError.Message.Replace("\"", ""), page);
            } else {
                await SetSessionStatus("passed", "Correct Search Engine Entered", page);
            }
           await browser.DisposeAsync();
        }
        private static async Task SetSessionStatus(string status, string reason, IPage _page) {
            await _page.EvaluateAsync("_ => {}", "browserstack_executor: {\"action\": \"setSessionStatus\", \"arguments\": {\"status\":\"" + status + "\", \"reason\": \"" + reason + "\"}}");
        }
    }
}

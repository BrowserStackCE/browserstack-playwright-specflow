# Testing with playwright-browserstack Specflow in C# with XUnit

[Playwright](https://playwright.dev/dotnet/) Integration with BrowserStack.

![BrowserStack Logo](https://d98b8t1nnulk5.cloudfront.net/production/images/layout/logo-header.png?1469004780)

## Setup

* Clone the repo and run `cd bstack_playwright_specflow `
* Set env variable BROWSERSTACK_USERNAME="browserstack_username" and BROWSERSTACK_ACCESS_KEY="browserstack_accessKey" 
* Run `dotnet build`

## Running your tests
- Run `dotnet test`

## Testing frameworks and libraries
* SpecFlow is used as a BDD framework and XUnit is used for test runner.
* Dotnet 6.0 is configured but different versions of .Net can be used

## Notes
* There are 2 different feature files and each feature files includes 2 test cases, for chrome and microsoft edge.
* When the tests are run with command `dotnet test` XUnit runner default behaviour is to run the different features parallel but it runs the scenarios in the same feature sequentially.
* If you would like to disable parallel execution, you can set `[assembly: CollectionBehavior(DisableTestParallelization = true)]` or it can also be configurable in the `xunit.runner.json` file.
* Parallel execution maximum count can be configurable from `xunit.runner.json` with variable maxParallelThreads.
* XUnit does not support parallel execution in the same feature. Here is the documentation https://docs.specflow.org/projects/specflow/en/latest/Execution/Parallel-Execution.html
* For running scenarios parallel, it seems only supported runner for .net is SpecFlow+

* Understand how many parallel sessions you need by using our [Parallel Test Calculator](https://www.browserstack.com/automate/parallel-calculator?ref=github)

* You can view your test results on the [BrowserStack Automate dashboard](https://www.browserstack.com/automate)
* To view the capabilities for playwright visit (https://www.browserstack.com/docs/automate/playwright/playwright-capabilities)

## Additional Resources
* [Documentation for writing Automate test scripts with BrowserStack](https://www.browserstack.com/docs/automate/playwright)

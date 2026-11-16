using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace ConsoleSelenium.Tests;

[TestClass]
public class WebFormularioDesafioTest
{
    [TestMethod]
    public void DevePreencherCadastroCompleto()
    {
        using IWebDriver driver = new ChromeDriver();

        try
        {
            driver.Navigate().GoToUrl(
                "https://www.selenium.dev/selenium/web/web-form.html"
            );

            driver.FindElement(By.Name("my-text"))
                .SendKeys("Alice Barbosa");

            driver.FindElement(By.Name("my-password"))
                .SendKeys("SenhaSegura@2026");

            driver.FindElement(By.Name("my-textarea"))
                .SendKeys("Testes automatizados com C#, MSTest e Selenium.");

            string valorReadonly = driver
                .FindElement(By.Name("my-readonly"))
                .GetAttribute("value")!;

            Assert.AreEqual("Readonly input", valorReadonly);

            driver.FindElement(By.CssSelector("button")).Click();

            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            IWebElement mensagem = wait.Until(d =>
                d.FindElement(By.Id("message"))
            );

            Assert.AreEqual("Received!", mensagem.Text);
        }
        finally
        {
            driver.Quit();
        }
    }

    [TestMethod]
    public void DeveSelecionarDropdownEPreencherDatalist()
    {
        using IWebDriver driver = new ChromeDriver();

        try
        {
            driver.Navigate().GoToUrl(
                "https://www.selenium.dev/selenium/web/web-form.html"
            );

            var dropdown = new SelectElement(
                driver.FindElement(By.Name("my-select"))
            );

            dropdown.SelectByText("Two");

            Assert.AreEqual(
                "2",
                dropdown.SelectedOption.GetAttribute("value")
            );

            driver.FindElement(By.Name("my-datalist"))
                .SendKeys("Seattle");

            driver.FindElement(By.CssSelector("button")).Click();

            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            IWebElement mensagem = wait.Until(d =>
                d.FindElement(By.Id("message"))
            );

            Assert.AreEqual("Received!", mensagem.Text);
        }
        finally
        {
            driver.Quit();
        }
    }

    [TestMethod]
    public void DeveSelecionarCheckboxRadioEColorPicker()
    {
        using IWebDriver driver = new ChromeDriver();

        try
        {
            // Arrange
            driver.Navigate().GoToUrl(
                "https://www.selenium.dev/selenium/web/web-form.html"
            );

            // Act - Selecionar checkboxes
            IWebElement checkbox1 = driver.FindElement(
                By.Id("my-check-1")
            );

            IWebElement checkbox2 = driver.FindElement(
                By.Id("my-check-2")
            );

            if (!checkbox1.Selected)
                checkbox1.Click();

            if (!checkbox2.Selected)
                checkbox2.Click();

            // Assert - Validar checkboxes
            Assert.IsTrue(checkbox1.Selected);
            Assert.IsTrue(checkbox2.Selected);

            // Act - Selecionar radio button
            IWebElement radio = driver.FindElement(
                By.Id("my-radio-2")
            );

            radio.Click();

            // Assert - Validar radio
            Assert.IsTrue(radio.Selected);

            // Act - Alterar Color Picker
            IWebElement colorPicker = driver.FindElement(
                By.Name("my-colors")
            );

            ((IJavaScriptExecutor)driver).ExecuteScript(
                "arguments[0].value = '#ff0000';",
                colorPicker
            );

            // Assert - Validar cor
            Assert.AreEqual(
                "#ff0000",
                colorPicker.GetAttribute("value")
            );

            // Enviar formulário
            driver.FindElement(By.CssSelector("button")).Click();

            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            IWebElement mensagem = wait.Until(d =>
                d.FindElement(By.Id("message"))
            );

            Assert.AreEqual("Received!", mensagem.Text);
        }
        finally
        {
            driver.Quit();
        }
    }
}
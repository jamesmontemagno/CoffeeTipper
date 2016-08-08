using System.Linq;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace CoffeeTip.UITests
{
    [TestFixture(Platform.Android)]
    //[TestFixture(Platform.iOS)]
    public class Tests
    {
        IApp app;
        Platform platform;

        public Tests(Platform platform)
        {
            this.platform = platform;
        }

        [SetUp]
        public void BeforeEachTest()
        {
            app = AppInitializer.StartApp(platform);
        }

        [Test]
        public void MyFirstTest()
        {
            app.ClearText("SubTotal");
            app.EnterText("SubTotal", "3.50");
            app.Tap("DrinkType");
            app.Tap("Latte");
            app.Query("Total");
            app.Tap("Tamered");
            var total = app.Query("Total").First();

            Assert.AreEqual(total.Text, "Total: $4.50");
        }
        




        [Test]
        public void DripCoffeeTest()
        {
            app.ClearText("SubTotal");
            app.EnterText("SubTotal", "4.00");
            var total = app.Query("Total").First();
            var tip = app.Query("TipAmount").First();

            Assert.AreEqual(total.Text, "Total: $4.50");
            Assert.AreEqual(tip.Text, "Tip: $0.50");

        }


        [Test]
        public void AtStarbucks()
        {
            var screen = new TipScreen(app);
            app.Screenshot("When I run the app");

            screen.EnterSubTotal(5.00M);
            screen.ToggleStarbucks();

            Assert.AreEqual("Total: $5.00", screen.TotalText);
            Assert.AreEqual("Tip: $0.00", screen.TipText);
        }

        [Test]
        public void NewTest()
        {
            
            app.Tap(x => x.Marked("SubTotal"));
            app.ClearText(x => x.Marked("SubTotal"));
            app.EnterText(x => x.Marked("SubTotal"), "3.00");
            app.Tap(x => x.Marked("DrinkType"));
            app.Tap(x => x.Text("Espresso"));
            app.Tap(x => x.Marked("Starbucks"));
            app.Screenshot("Tapped on view with class: SwitchCompat marked: Starbucks");

        }

        [Test]
        public void NewTest1()
        {
            app.Tap(x => x.Marked("SubTotal"));
            app.ClearText(x => x.Marked("SubTotal"));
            app.EnterText(x => x.Marked("SubTotal"), "4.00");
            app.Tap(x => x.Marked("DrinkType"));
            app.Tap(x => x.Text("Espresso"));
            app.ClearText(x => x.Marked("DrinkType"));
            app.EnterText(x => x.Marked("DrinkType"), "Drip Coffee");
            app.Tap(x => x.Marked("Tamered"));
            app.Tap(x => x.Marked("Starbucks"));
            app.Tap(x => x.Marked("Reset"));
            app.Screenshot("Cleared Text");
            app.WaitForElement(x => x.Marked("Reset"));


            var total = app.Query("Total").First();
            var tip = app.Query("TipAmount").First();

            Assert.AreEqual(total.Text, "Total: $3.00");
            Assert.AreEqual(tip.Text, "Tip: $0.50");
        }

        [Test]
        public void NewTest2()
        {
            app.Tap(x => x.Marked("SubTotal"));
            app.ClearText(x => x.Marked("SubTotal"));
            app.EnterText(x => x.Marked("SubTotal"), "3.00");
            app.Tap(x => x.Marked("DrinkType"));
            app.Tap(x => x.Text("Pour Over Coffee"));
            app.EnterText(x => x.Marked("DrinkType"), "o");
            app.Tap(x => x.Marked("Tamered"));
        }

        [Test]
        public void NewTest3()
        {
            app.Screenshot("Screenshot");
            app.Tap(x => x.Marked("SubTotal"));
            app.ClearText(x => x.Marked("SubTotal"));
            app.EnterText(x => x.Marked("SubTotal"), "3.00");
            app.Tap(x => x.Marked("DrinkType"));
            app.Tap(x => x.Text("Espresso"));
            app.ClearText(x => x.Marked("DrinkType"));
            app.EnterText(x => x.Marked("DrinkType"), "Drip Coffee");
            app.Tap(x => x.Marked("Tamered"));
            app.Tap(x => x.Marked("Starbucks"));
            app.Tap(x => x.Marked("Reset"));
            app.ClearText(x => x.Marked("SubTotal"));
            app.EnterText(x => x.Marked("SubTotal"), "3.0");
            app.EnterText(x => x.Marked("DrinkType"), "f");
        }


    }

    public enum CoffeeDrink
    {
        Drip,
        PourOver,
        Espresso,
        Latte
    }

    class TipScreen
    {
        readonly IApp app;

        public TipScreen(IApp app)
        {
            this.app = app;
        }

        public void ToggleStarbucks()
        {
            app.Tap("Starbucks");
        }

        public void ToggleTampered()
        {
            app.Tap("Tamered");
        }

        public void Reset()
        {
            app.Tap("Reset");
        }

        public void SelectDrink(int drink)
        {
            app.Tap("DrinkType");
            switch (drink)
            {
                case 0:
                    app.Tap("Drip Coffee");
                    break;
                case 1:
                    app.Tap("Pour Over Coffee");
                    break;
                case 2:
                    app.Tap("Espresso");
                    break;
                case 3:
                    app.Tap("Latte");
                    break;
            }
            app.DismissKeyboard();
        }

        public void EnterSubTotal(decimal subTotal)
        {
            app.ClearText("SubTotal");
            app.EnterText("SubTotal", subTotal.ToString());
            app.DismissKeyboard();
        }

        public string TipText
        {
            get { return app.Query("TipAmount").Single().Text; }
        }
        public string TotalText
        {
            get { return app.Query("Total").Single().Text; }
        }
    }
}


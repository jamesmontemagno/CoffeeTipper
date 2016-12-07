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
		public void TamperedDisabled_PourOver()
		{
			app.Tap(x => x.Id("DrinkType"));
			app.Screenshot("Tapped Drink Type");

			app.Tap(x => x.Marked("Pour Over Coffee"));
			app.Screenshot("Tapped on Pour Over Coffee");

			app.DismissKeyboard();

			app.Screenshot("Dismissed Keyboard");
			var result = app.Query(x => x.Id("Tamered")).First();
			Assert.IsFalse(result.Enabled, "Tampered is enabled, when it shouldn't be");

		}



		[Test]
        public void DripCoffeeTest()
        {

            app.Screenshot("When I run the app");

            app.ClearText("SubTotal");
            app.Screenshot("When I clear text");

            app.EnterText("SubTotal", "4.00");
            app.Screenshot("And Enter $4.00");

            var total = app.Query("Total").First();
            var tip = app.Query("TipAmount").First();

            Assert.AreEqual(total.Text, "Total: $4.50");
            Assert.AreEqual(tip.Text, "Tip: $0.50");


            app.Screenshot("Total is $4.50 with $0.50 tip");

        }


        [Test]
        public void AtStarbucks()
        {
            var screen = new TipScreen(app);
            app.Screenshot("When I run the app");

            screen.EnterSubTotal(5.00M);
            app.Screenshot("And I enter $5.00");

            screen.ToggleStarbucks();
            app.Screenshot("Then Toggle Starbucks");

            Assert.AreEqual("Total: $5.00", screen.TotalText);
            Assert.AreEqual("Tip: $0.00", screen.TipText);

            app.Screenshot("Total is $5.00 with $0.00 tip");
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


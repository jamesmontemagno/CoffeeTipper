using System;
using System.IO;
using System.Linq;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace CoffeeTip.UITests
{
    public class AppInitializer
    {
        public static IApp StartApp(Platform platform)
        {
            if (platform == Platform.Android)
            {
                return ConfigureApp.Android
					               .ApkFile(@"..\..\..\Droid\bin\Debug\com.xamarin.coffee_tip.apk")
					               .StartApp();
            }

            return ConfigureApp.iOS.StartApp();
        }
    }
}


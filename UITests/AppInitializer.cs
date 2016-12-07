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

				var mac = @"../../../Droid/bin/Debug/com.xamarin.coffee_tip.apk";

				var pc = @"..\..\..\Droid\bin\Debug\com.xamarin.coffee_tip.apk";
                
				return ConfigureApp.Android
					               .ApkFile(mac)
					               .StartApp();
            }

            return ConfigureApp.iOS.StartApp();
        }
    }
}


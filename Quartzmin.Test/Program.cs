using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Quartz.Util;

namespace Quartzmin.Test
{
    class Program
    {
        public static void Main(string[] args)
        {
            FixQuartzLinuxTimezoneMapping();
 
            try
            {
                CreateWebHostBuilder(args).Build().Run();

                Environment.Exit(0);
            }
            catch (Exception ex)
            {
                Environment.Exit(1);
            }
        }
        
        private static void FixQuartzLinuxTimezoneMapping()
        {
            var isLinux = RuntimeInformation
                .IsOSPlatform(OSPlatform.Linux);
            if (isLinux)
            {
                var timeZoneIdAliases =
                    (Dictionary<string, string>) typeof(TimeZoneUtil).GetField("timeZoneIdAliases",
                        BindingFlags.NonPublic | BindingFlags.Static)?.GetValue(null);

                if (timeZoneIdAliases != null)
                    timeZoneIdAliases["China Standard Time"] = "Asia/Shanghai";
            }
        }
        
        private static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            return WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
        }
    }
}
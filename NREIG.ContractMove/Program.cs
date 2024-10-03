using Microsoft.Extensions.Configuration;

namespace NREIG.ContractMove
{
    internal static class Program
    {
        public static IConfiguration Configuration { get; private set; }
        public static string? Server { get; set; }
        public static string? ServerName { get; set; }
        public static string? ServerConfig { get; set; } = "";

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            Configuration = builder.Build();

            ApplicationConfiguration.Initialize();
            Application.Run(new ContractMove());
        }

        public static System.Drawing.Color GetServerColor()
        {
            var color = System.Drawing.Color.White;

            switch (Server)
            {
                case "DEV":
                    color = System.Drawing.Color.Indigo;
                    break;
                case "Local":
                    color = System.Drawing.Color.CornflowerBlue;
                    break;
                case "Pre-UAT":
                    color = System.Drawing.Color.Green;
                    break;
                case "UAT":
                    color = System.Drawing.Color.Aqua;
                    break;
                case "STG":
                    color = System.Drawing.Color.Orange;
                    break;
                case "STG2":
                    color = System.Drawing.Color.DeepPink;
                    break;
                case "PROD_Report":
                    color = System.Drawing.Color.Black;
                    break;
                case "PROD":
                    color = System.Drawing.Color.Red;
                    break;
            }

            return color;
        }

        public static void SetContrastingForeColor(Control control)
        {
            Color backColor = control.BackColor;

            // Calculate the brightness of the background color
            double luminance = (0.299 * backColor.R + 0.587 * backColor.G + 0.114 * backColor.B) / 255;

            // Set forecolor to white if luminance is low (dark background), otherwise set to black
            control.ForeColor = luminance > 0.5 ? Color.Black : Color.White;
        }
    }
}
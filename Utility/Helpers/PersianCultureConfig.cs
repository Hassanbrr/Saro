 
using System.Globalization;
 

namespace Utility.Helpers
{
    public static class PersianCultureConfig
    {
        public static void ConfigurePersianCulture()
        {
            CultureInfo persianCulture = new CultureInfo("fa-IR");
            persianCulture.DateTimeFormat.Calendar = new PersianCalendar();
            persianCulture.DateTimeFormat.ShortDatePattern = "yyyy/MM/dd";
            persianCulture.DateTimeFormat.LongDatePattern = "dddd, dd MMMM yyyy";
            persianCulture.DateTimeFormat.AMDesignator = "ق.ظ";
            persianCulture.DateTimeFormat.PMDesignator = "ب.ظ"; 
            CultureInfo.DefaultThreadCurrentCulture = persianCulture;
            CultureInfo.DefaultThreadCurrentUICulture = persianCulture;
        }
    }

}

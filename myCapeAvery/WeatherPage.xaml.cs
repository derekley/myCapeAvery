using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace myCapeAvery
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WeatherPage : ContentPage
    {
        public WeatherPage()
        {
            InitializeComponent();

            /* 
             * Will display next 7 days in the following format:
             * Hi: 72/Lo: 60 - Precipitation: 100%
             * IMAGES: Sun.png, Cloud.png, Rain.png, Snow.png
             * SEASONS: Dec-Feb = Winter, Mar-May = Spring, Jun-Aug = Summer, Sep-Nov = Fall
             * HI RANGE: 15-30, 35-50, 70-85, 35-50
             * LO RANGE: 0-10, 15-30, 50-65, 15-30
             * FORECAST: Snow (-5), Rain (-5), Cloud (-5), Sun (+5)
             */

            Random r = new Random();

            if (DateTime.Today.Month == 12 || DateTime.Today.Month == 1 || DateTime.Today.Month == 2)
            {
                // Winter
                for(int i = 1; i <= 7; i++)
                {
                    // Get Weather Forecast
                    int conditions = r.Next(1, 4); // 1 = Snow, 2 = Cloud, 3 = Sun
                    int hi = r.Next(15, 31);
                    int lo = r.Next(11);
                    int precip = r.Next(20, 81);
                    int precipSun = r.Next(21);

                    // Display Forecast
                    if (i == 1)
                    {
                        if (conditions == 1)
                        {
                            imgDay1.Source = "Snow.png";
                            lblDay1.Text = "Hi: " + (hi - 5) + "/Lo: " + (lo - 5) + " - Precipitation: " + (precip + 20) + "%";
                        }
                        else if (conditions == 2)
                        {
                            imgDay1.Source = "Cloud.png";
                            lblDay1.Text = "Hi: " + (hi - 5) + "/Lo: " + (lo - 5) + " - Precipitation: " + precip + "%";
                        }
                        else if (conditions == 3)
                        {
                            imgDay1.Source = "Sun.png";
                            lblDay1.Text = "Hi: " + (hi + 5) + "/Lo: " + (lo + 5) + " - Precipitation: " + precipSun + "%";
                        }
                    }
                    else if (i == 2)
                    {
                        if (conditions == 1)
                        {
                            imgDay2.Source = "Snow.png";
                            lblDay2.Text = "Hi: " + (hi - 5) + "/Lo: " + (lo - 5) + " - Precipitation: " + (precip + 20) + "%";
                        }
                        else if (conditions == 2)
                        {
                            imgDay2.Source = "Cloud.png";
                            lblDay2.Text = "Hi: " + (hi - 5) + "/Lo: " + (lo - 5) + " - Precipitation: " + precip + "%";
                        }
                        else if (conditions == 3)
                        {
                            imgDay2.Source = "Sun.png";
                            lblDay2.Text = "Hi: " + (hi + 5) + "/Lo: " + (lo + 5) + " - Precipitation: " + precipSun + "%";
                        }
                    }
                    else if (i == 3)
                    {
                        if (conditions == 1)
                        {
                            imgDay3.Source = "Snow.png";
                            lblDay3.Text = "Hi: " + (hi - 5) + "/Lo: " + (lo - 5) + " - Precipitation: " + (precip + 20) + "%";
                        }
                        else if (conditions == 2)
                        {
                            imgDay3.Source = "Cloud.png";
                            lblDay3.Text = "Hi: " + (hi - 5) + "/Lo: " + (lo - 5) + " - Precipitation: " + precip + "%";
                        }
                        else if (conditions == 3)
                        {
                            imgDay3.Source = "Sun.png";
                            lblDay3.Text = "Hi: " + (hi + 5) + "/Lo: " + (lo + 5) + " - Precipitation: " + precipSun + "%";
                        }
                    }
                    else if (i == 4)
                    {
                        if (conditions == 1)
                        {
                            imgDay4.Source = "Snow.png";
                            lblDay4.Text = "Hi: " + (hi - 5) + "/Lo: " + (lo - 5) + " - Precipitation: " + (precip + 20) + "%";
                        }
                        else if (conditions == 2)
                        {
                            imgDay4.Source = "Cloud.png";
                            lblDay4.Text = "Hi: " + (hi - 5) + "/Lo: " + (lo - 5) + " - Precipitation: " + precip + "%";
                        }
                        else if (conditions == 3)
                        {
                            imgDay4.Source = "Sun.png";
                            lblDay4.Text = "Hi: " + (hi + 5) + "/Lo: " + (lo + 5) + " - Precipitation: " + precipSun + "%";
                        }
                    }
                    else if (i == 5)
                    {
                        if (conditions == 1)
                        {
                            imgDay5.Source = "Snow.png";
                            lblDay5.Text = "Hi: " + (hi - 5) + "/Lo: " + (lo - 5) + " - Precipitation: " + (precip + 20) + "%";
                        }
                        else if (conditions == 2)
                        {
                            imgDay5.Source = "Cloud.png";
                            lblDay5.Text = "Hi: " + (hi - 5) + "/Lo: " + (lo - 5) + " - Precipitation: " + precip + "%";
                        }
                        else if (conditions == 3)
                        {
                            imgDay5.Source = "Sun.png";
                            lblDay5.Text = "Hi: " + (hi + 5) + "/Lo: " + (lo + 5) + " - Precipitation: " + precipSun + "%";
                        }
                    }
                    else if (i == 6)
                    {
                        if (conditions == 1)
                        {
                            imgDay6.Source = "Snow.png";
                            lblDay6.Text = "Hi: " + (hi - 5) + "/Lo: " + (lo - 5) + " - Precipitation: " + (precip + 20) + "%";
                        }
                        else if (conditions == 2)
                        {
                            imgDay6.Source = "Cloud.png";
                            lblDay6.Text = "Hi: " + (hi - 5) + "/Lo: " + (lo - 5) + " - Precipitation: " + precip + "%";
                        }
                        else if (conditions == 3)
                        {
                            imgDay6.Source = "Sun.png";
                            lblDay6.Text = "Hi: " + (hi + 5) + "/Lo: " + (lo + 5) + " - Precipitation: " + precipSun + "%";
                        }
                    }
                    else if (i == 7)
                    {
                        if (conditions == 1)
                        {
                            imgDay7.Source = "Snow.png";
                            lblDay7.Text = "Hi: " + (hi - 5) + "/Lo: " + (lo - 5) + " - Precipitation: " + (precip + 20) + "%";
                        }
                        else if (conditions == 2)
                        {
                            imgDay7.Source = "Cloud.png";
                            lblDay7.Text = "Hi: " + (hi - 5) + "/Lo: " + (lo - 5) + " - Precipitation: " + precip + "%";
                        }
                        else if (conditions == 3)
                        {
                            imgDay7.Source = "Sun.png";
                            lblDay7.Text = "Hi: " + (hi + 5) + "/Lo: " + (lo + 5) + " - Precipitation: " + precipSun + "%";
                        }
                    }
                }
            }
            else if(DateTime.Today.Month == 3 || DateTime.Today.Month == 4 || DateTime.Today.Month == 5)
            {
                // Spring
                for (int i = 1; i <= 7; i++)
                {
                    // Get Weather Forecast
                    int conditions = r.Next(1, 4); // 1 = Rain, 2 = Cloud, 3 = Sun
                    int hi = r.Next(35, 51);
                    int lo = r.Next(15, 31);
                    int precip = r.Next(20, 81);
                    int precipSun = r.Next(21);

                    // Display Forecast
                    if (i == 1)
                    {
                        if (conditions == 1)
                        {
                            imgDay1.Source = "Rain.png";
                            lblDay1.Text = "Hi: " + (hi - 5) + "/Lo: " + (lo - 5) + " - Precipitation: " + (precip + 20) + "%";
                        }
                        else if (conditions == 2)
                        {
                            imgDay1.Source = "Cloud.png";
                            lblDay1.Text = "Hi: " + (hi - 5) + "/Lo: " + (lo - 5) + " - Precipitation: " + precip + "%";
                        }
                        else if (conditions == 3)
                        {
                            imgDay1.Source = "Sun.png";
                            lblDay1.Text = "Hi: " + (hi + 5) + "/Lo: " + (lo + 5) + " - Precipitation: " + precipSun + "%";
                        }
                    }
                    else if (i == 2)
                    {
                        if (conditions == 1)
                        {
                            imgDay2.Source = "Rain.png";
                            lblDay2.Text = "Hi: " + (hi - 5) + "/Lo: " + (lo - 5) + " - Precipitation: " + (precip + 20) + "%";
                        }
                        else if (conditions == 2)
                        {
                            imgDay2.Source = "Cloud.png";
                            lblDay2.Text = "Hi: " + (hi - 5) + "/Lo: " + (lo - 5) + " - Precipitation: " + precip + "%";
                        }
                        else if (conditions == 3)
                        {
                            imgDay2.Source = "Sun.png";
                            lblDay2.Text = "Hi: " + (hi + 5) + "/Lo: " + (lo + 5) + " - Precipitation: " + precipSun + "%";
                        }
                    }
                    else if (i == 3)
                    {
                        if (conditions == 1)
                        {
                            imgDay3.Source = "Rain.png";
                            lblDay3.Text = "Hi: " + (hi - 5) + "/Lo: " + (lo - 5) + " - Precipitation: " + (precip + 20) + "%";
                        }
                        else if (conditions == 2)
                        {
                            imgDay3.Source = "Cloud.png";
                            lblDay3.Text = "Hi: " + (hi - 5) + "/Lo: " + (lo - 5) + " - Precipitation: " + precip + "%";
                        }
                        else if (conditions == 3)
                        {
                            imgDay3.Source = "Sun.png";
                            lblDay3.Text = "Hi: " + (hi + 5) + "/Lo: " + (lo + 5) + " - Precipitation: " + precipSun + "%";
                        }
                    }
                    else if (i == 4)
                    {
                        if (conditions == 1)
                        {
                            imgDay4.Source = "Rain.png";
                            lblDay4.Text = "Hi: " + (hi - 5) + "/Lo: " + (lo - 5) + " - Precipitation: " + (precip + 20) + "%";
                        }
                        else if (conditions == 2)
                        {
                            imgDay4.Source = "Cloud.png";
                            lblDay4.Text = "Hi: " + (hi - 5) + "/Lo: " + (lo - 5) + " - Precipitation: " + precip + "%";
                        }
                        else if (conditions == 3)
                        {
                            imgDay4.Source = "Sun.png";
                            lblDay4.Text = "Hi: " + (hi + 5) + "/Lo: " + (lo + 5) + " - Precipitation: " + precipSun + "%";
                        }
                    }
                    else if (i == 5)
                    {
                        if (conditions == 1)
                        {
                            imgDay5.Source = "Rain.png";
                            lblDay5.Text = "Hi: " + (hi - 5) + "/Lo: " + (lo - 5) + " - Precipitation: " + (precip + 20) + "%";
                        }
                        else if (conditions == 2)
                        {
                            imgDay5.Source = "Cloud.png";
                            lblDay5.Text = "Hi: " + (hi - 5) + "/Lo: " + (lo - 5) + " - Precipitation: " + precip + "%";
                        }
                        else if (conditions == 3)
                        {
                            imgDay5.Source = "Sun.png";
                            lblDay5.Text = "Hi: " + (hi + 5) + "/Lo: " + (lo + 5) + " - Precipitation: " + precipSun + "%";
                        }
                    }
                    else if (i == 6)
                    {
                        if (conditions == 1)
                        {
                            imgDay6.Source = "Rain.png";
                            lblDay6.Text = "Hi: " + (hi - 5) + "/Lo: " + (lo - 5) + " - Precipitation: " + (precip + 20) + "%";
                        }
                        else if (conditions == 2)
                        {
                            imgDay6.Source = "Cloud.png";
                            lblDay6.Text = "Hi: " + (hi - 5) + "/Lo: " + (lo - 5) + " - Precipitation: " + precip + "%";
                        }
                        else if (conditions == 3)
                        {
                            imgDay6.Source = "Sun.png";
                            lblDay6.Text = "Hi: " + (hi + 5) + "/Lo: " + (lo + 5) + " - Precipitation: " + precipSun + "%";
                        }
                    }
                    else if (i == 7)
                    {
                        if (conditions == 1)
                        {
                            imgDay7.Source = "Rain.png";
                            lblDay7.Text = "Hi: " + (hi - 5) + "/Lo: " + (lo - 5) + " - Precipitation: " + (precip + 20) + "%";
                        }
                        else if (conditions == 2)
                        {
                            imgDay7.Source = "Cloud.png";
                            lblDay7.Text = "Hi: " + (hi - 5) + "/Lo: " + (lo - 5) + " - Precipitation: " + precip + "%";
                        }
                        else if (conditions == 3)
                        {
                            imgDay7.Source = "Sun.png";
                            lblDay7.Text = "Hi: " + (hi + 5) + "/Lo: " + (lo + 5) + " - Precipitation: " + precipSun + "%";
                        }
                    }
                }
            }
            else if(DateTime.Today.Month == 6 || DateTime.Today.Month == 7 || DateTime.Today.Month == 8)
            {
                // Summer
                for (int i = 1; i <= 7; i++)
                {
                    // Get Weather Forecast
                    int conditions = r.Next(1, 4); // 1 = Rain, 2 = Cloud, 3 = Sun
                    int hi = r.Next(70, 86);
                    int lo = r.Next(50, 66);
                    int precip = r.Next(20, 81);
                    int precipSun = r.Next(21);

                    // Display Forecast
                    if (i == 1)
                    {
                        if (conditions == 1)
                        {
                            imgDay1.Source = "Rain.png";
                            lblDay1.Text = "Hi: " + (hi - 5) + "/Lo: " + (lo - 5) + " - Precipitation: " + (precip + 20) + "%";
                        }
                        else if (conditions == 2)
                        {
                            imgDay1.Source = "Cloud.png";
                            lblDay1.Text = "Hi: " + (hi - 5) + "/Lo: " + (lo - 5) + " - Precipitation: " + precip + "%";
                        }
                        else if (conditions == 3)
                        {
                            imgDay1.Source = "Sun.png";
                            lblDay1.Text = "Hi: " + (hi + 5) + "/Lo: " + (lo + 5) + " - Precipitation: " + precipSun + "%";
                        }
                    }
                    else if (i == 2)
                    {
                        if (conditions == 1)
                        {
                            imgDay2.Source = "Rain.png";
                            lblDay2.Text = "Hi: " + (hi - 5) + "/Lo: " + (lo - 5) + " - Precipitation: " + (precip + 20) + "%";
                        }
                        else if (conditions == 2)
                        {
                            imgDay2.Source = "Cloud.png";
                            lblDay2.Text = "Hi: " + (hi - 5) + "/Lo: " + (lo - 5) + " - Precipitation: " + precip + "%";
                        }
                        else if (conditions == 3)
                        {
                            imgDay2.Source = "Sun.png";
                            lblDay2.Text = "Hi: " + (hi + 5) + "/Lo: " + (lo + 5) + " - Precipitation: " + precipSun + "%";
                        }
                    }
                    else if (i == 3)
                    {
                        if (conditions == 1)
                        {
                            imgDay3.Source = "Rain.png";
                            lblDay3.Text = "Hi: " + (hi - 5) + "/Lo: " + (lo - 5) + " - Precipitation: " + (precip + 20) + "%";
                        }
                        else if (conditions == 2)
                        {
                            imgDay3.Source = "Cloud.png";
                            lblDay3.Text = "Hi: " + (hi - 5) + "/Lo: " + (lo - 5) + " - Precipitation: " + precip + "%";
                        }
                        else if (conditions == 3)
                        {
                            imgDay3.Source = "Sun.png";
                            lblDay3.Text = "Hi: " + (hi + 5) + "/Lo: " + (lo + 5) + " - Precipitation: " + precipSun + "%";
                        }
                    }
                    else if (i == 4)
                    {
                        if (conditions == 1)
                        {
                            imgDay4.Source = "Rain.png";
                            lblDay4.Text = "Hi: " + (hi - 5) + "/Lo: " + (lo - 5) + " - Precipitation: " + (precip + 20) + "%";
                        }
                        else if (conditions == 2)
                        {
                            imgDay4.Source = "Cloud.png";
                            lblDay4.Text = "Hi: " + (hi - 5) + "/Lo: " + (lo - 5) + " - Precipitation: " + precip + "%";
                        }
                        else if (conditions == 3)
                        {
                            imgDay4.Source = "Sun.png";
                            lblDay4.Text = "Hi: " + (hi + 5) + "/Lo: " + (lo + 5) + " - Precipitation: " + precipSun + "%";
                        }
                    }
                    else if (i == 5)
                    {
                        if (conditions == 1)
                        {
                            imgDay5.Source = "Rain.png";
                            lblDay5.Text = "Hi: " + (hi - 5) + "/Lo: " + (lo - 5) + " - Precipitation: " + (precip + 20) + "%";
                        }
                        else if (conditions == 2)
                        {
                            imgDay5.Source = "Cloud.png";
                            lblDay5.Text = "Hi: " + (hi - 5) + "/Lo: " + (lo - 5) + " - Precipitation: " + precip + "%";
                        }
                        else if (conditions == 3)
                        {
                            imgDay5.Source = "Sun.png";
                            lblDay5.Text = "Hi: " + (hi + 5) + "/Lo: " + (lo + 5) + " - Precipitation: " + precipSun + "%";
                        }
                    }
                    else if (i == 6)
                    {
                        if (conditions == 1)
                        {
                            imgDay6.Source = "Rain.png";
                            lblDay6.Text = "Hi: " + (hi - 5) + "/Lo: " + (lo - 5) + " - Precipitation: " + (precip + 20) + "%";
                        }
                        else if (conditions == 2)
                        {
                            imgDay6.Source = "Cloud.png";
                            lblDay6.Text = "Hi: " + (hi - 5) + "/Lo: " + (lo - 5) + " - Precipitation: " + precip + "%";
                        }
                        else if (conditions == 3)
                        {
                            imgDay6.Source = "Sun.png";
                            lblDay6.Text = "Hi: " + (hi + 5) + "/Lo: " + (lo + 5) + " - Precipitation: " + precipSun + "%";
                        }
                    }
                    else if (i == 7)
                    {
                        if (conditions == 1)
                        {
                            imgDay7.Source = "Rain.png";
                            lblDay7.Text = "Hi: " + (hi - 5) + "/Lo: " + (lo - 5) + " - Precipitation: " + (precip + 20) + "%";
                        }
                        else if (conditions == 2)
                        {
                            imgDay7.Source = "Cloud.png";
                            lblDay7.Text = "Hi: " + (hi - 5) + "/Lo: " + (lo - 5) + " - Precipitation: " + precip + "%";
                        }
                        else if (conditions == 3)
                        {
                            imgDay7.Source = "Sun.png";
                            lblDay7.Text = "Hi: " + (hi + 5) + "/Lo: " + (lo + 5) + " - Precipitation: " + precipSun + "%";
                        }
                    }
                }
            }
            else
            {
                // Fall
                for (int i = 1; i <= 7; i++)
                {
                    // Get Weather Forecast
                    int conditions = r.Next(1, 4); // 1 = Rain, 2 = Cloud, 3 = Sun
                    int hi = r.Next(35, 51);
                    int lo = r.Next(15, 31);
                    int precip = r.Next(20, 81);
                    int precipSun = r.Next(21);

                    // Display Forecast
                    if (i == 1)
                    {
                        if (conditions == 1)
                        {
                            imgDay1.Source = "Rain.png";
                            lblDay1.Text = "Hi: " + (hi - 5) + "/Lo: " + (lo - 5) + " - Precipitation: " + (precip + 20) + "%";
                        }
                        else if (conditions == 2)
                        {
                            imgDay1.Source = "Cloud.png";
                            lblDay1.Text = "Hi: " + (hi - 5) + "/Lo: " + (lo - 5) + " - Precipitation: " + precip + "%";
                        }
                        else if (conditions == 3)
                        {
                            imgDay1.Source = "Sun.png";
                            lblDay1.Text = "Hi: " + (hi + 5) + "/Lo: " + (lo + 5) + " - Precipitation: " + precipSun + "%";
                        }
                    }
                    else if (i == 2)
                    {
                        if (conditions == 1)
                        {
                            imgDay2.Source = "Rain.png";
                            lblDay2.Text = "Hi: " + (hi - 5) + "/Lo: " + (lo - 5) + " - Precipitation: " + (precip + 20) + "%";
                        }
                        else if (conditions == 2)
                        {
                            imgDay2.Source = "Cloud.png";
                            lblDay2.Text = "Hi: " + (hi - 5) + "/Lo: " + (lo - 5) + " - Precipitation: " + precip + "%";
                        }
                        else if (conditions == 3)
                        {
                            imgDay2.Source = "Sun.png";
                            lblDay2.Text = "Hi: " + (hi + 5) + "/Lo: " + (lo + 5) + " - Precipitation: " + precipSun + "%";
                        }
                    }
                    else if (i == 3)
                    {
                        if (conditions == 1)
                        {
                            imgDay3.Source = "Rain.png";
                            lblDay3.Text = "Hi: " + (hi - 5) + "/Lo: " + (lo - 5) + " - Precipitation: " + (precip + 20) + "%";
                        }
                        else if (conditions == 2)
                        {
                            imgDay3.Source = "Cloud.png";
                            lblDay3.Text = "Hi: " + (hi - 5) + "/Lo: " + (lo - 5) + " - Precipitation: " + precip + "%";
                        }
                        else if (conditions == 3)
                        {
                            imgDay3.Source = "Sun.png";
                            lblDay3.Text = "Hi: " + (hi + 5) + "/Lo: " + (lo + 5) + " - Precipitation: " + precipSun + "%";
                        }
                    }
                    else if (i == 4)
                    {
                        if (conditions == 1)
                        {
                            imgDay4.Source = "Rain.png";
                            lblDay4.Text = "Hi: " + (hi - 5) + "/Lo: " + (lo - 5) + " - Precipitation: " + (precip + 20) + "%";
                        }
                        else if (conditions == 2)
                        {
                            imgDay4.Source = "Cloud.png";
                            lblDay4.Text = "Hi: " + (hi - 5) + "/Lo: " + (lo - 5) + " - Precipitation: " + precip + "%";
                        }
                        else if (conditions == 3)
                        {
                            imgDay4.Source = "Sun.png";
                            lblDay4.Text = "Hi: " + (hi + 5) + "/Lo: " + (lo + 5) + " - Precipitation: " + precipSun + "%";
                        }
                    }
                    else if (i == 5)
                    {
                        if (conditions == 1)
                        {
                            imgDay5.Source = "Rain.png";
                            lblDay5.Text = "Hi: " + (hi - 5) + "/Lo: " + (lo - 5) + " - Precipitation: " + (precip + 20) + "%";
                        }
                        else if (conditions == 2)
                        {
                            imgDay5.Source = "Cloud.png";
                            lblDay5.Text = "Hi: " + (hi - 5) + "/Lo: " + (lo - 5) + " - Precipitation: " + precip + "%";
                        }
                        else if (conditions == 3)
                        {
                            imgDay5.Source = "Sun.png";
                            lblDay5.Text = "Hi: " + (hi + 5) + "/Lo: " + (lo + 5) + " - Precipitation: " + precipSun + "%";
                        }
                    }
                    else if (i == 6)
                    {
                        if (conditions == 1)
                        {
                            imgDay6.Source = "Rain.png";
                            lblDay6.Text = "Hi: " + (hi - 5) + "/Lo: " + (lo - 5) + " - Precipitation: " + (precip + 20) + "%";
                        }
                        else if (conditions == 2)
                        {
                            imgDay6.Source = "Cloud.png";
                            lblDay6.Text = "Hi: " + (hi - 5) + "/Lo: " + (lo - 5) + " - Precipitation: " + precip + "%";
                        }
                        else if (conditions == 3)
                        {
                            imgDay6.Source = "Sun.png";
                            lblDay6.Text = "Hi: " + (hi + 5) + "/Lo: " + (lo + 5) + " - Precipitation: " + precipSun + "%";
                        }
                    }
                    else if (i == 7)
                    {
                        if (conditions == 1)
                        {
                            imgDay7.Source = "Rain.png";
                            lblDay7.Text = "Hi: " + (hi - 5) + "/Lo: " + (lo - 5) + " - Precipitation: " + (precip + 20) + "%";
                        }
                        else if (conditions == 2)
                        {
                            imgDay7.Source = "Cloud.png";
                            lblDay7.Text = "Hi: " + (hi - 5) + "/Lo: " + (lo - 5) + " - Precipitation: " + precip + "%";
                        }
                        else if (conditions == 3)
                        {
                            imgDay7.Source = "Sun.png";
                            lblDay7.Text = "Hi: " + (hi + 5) + "/Lo: " + (lo + 5) + " - Precipitation: " + precipSun + "%";
                        }
                    }
                }
            }

        }
    }
}
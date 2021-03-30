using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using Plugin.XamarinFormsSaveOpenPDFPackage;
using Xamarin.Forms;

namespace XamarinTechInvestigation.Pdf
{
    public partial class PdfPage : ContentPage
    {
        public PdfPage()
        {
            InitializeComponent();
        }
        async void Button_Clicked(System.Object sender, System.EventArgs e)
        {
            var http = new HttpClient();
            var stream = await http.GetStreamAsync("https://gerald.verslu.is/subscribe.pdf");

            using (var memory = new MemoryStream()) {
                await stream.CopyToAsync(memory);

                await Plugin.XamarinFormsSaveOpenPDFPackage.CrossXamarinFormsSaveOpenPDFPackage.Current.SaveAndView(
                    "myfile.pdf",
                    "application/pdf",
                    memory,
                    PDFOpenContext.InApp
                );
            }
                    
        }
    }
}

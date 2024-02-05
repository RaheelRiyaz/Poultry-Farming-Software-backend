using System;
using DinkToPdf;
using DinkToPdf.Contracts;

public interface IPdfService
{
    byte[] GeneratePdf(string htmlContent);
}

public class PdfService : IPdfService
{
    private readonly IConverter _converter;

    public PdfService(IConverter converter)
    {
        _converter = converter;
    }

    public byte[] GeneratePdf(string htmlContent)
    {
        try
        {
            var globalSettings = new GlobalSettings
            {
                ColorMode = ColorMode.Color,
                Orientation = Orientation.Portrait,
                PaperSize = PaperKind.A4,
            };

            var objectSettings = new ObjectSettings
            {
                PagesCount = true,
                HtmlContent = htmlContent,
            };

            var document = new HtmlToPdfDocument()
            {
                GlobalSettings = globalSettings,
                Objects = { objectSettings },
            };

            return _converter.Convert(document);
        }
        catch (Exception ex)
        {
            // Log the exception or throw a custom exception if needed
            Console.WriteLine(ex.Message);
            return null;
        }
    }
}

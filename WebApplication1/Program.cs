using WebApplication1.model;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");
app.MapGet("/print", (ILoggerFactory loggerFactory) =>
{
    var logger = loggerFactory.CreateLogger("Start");
    logger.LogInformation("Begin print");
    string WT1 = "TSC Printers";
    string B1 = "20080101";
    byte[] result_unicode = System.Text.Encoding.GetEncoding("utf-16").GetBytes("unicode test");
    byte[] result_utf8 = System.Text.Encoding.UTF8.GetBytes("TEXT 40,620,\"ARIAL.TTF\",0,12,12,\"utf8 test Wörter auf Deutsch\"");

    TSC.openport("TSC TE210");
    TSC.sendcommand("SIZE 100 mm, 120 mm");
    TSC.sendcommand("SPEED 4");
    TSC.sendcommand("DENSITY 12");
    TSC.sendcommand("DIRECTION 1");
    TSC.sendcommand("SET TEAR ON");
    TSC.sendcommand("CODEPAGE UTF-8");
    TSC.clearbuffer();
    TSC.downloadpcx("UL.PCX", "UL.PCX");
    TSC.windowsfont(40, 490, 48, 0, 0, 0, "Arial", "Windows Font Test");
    TSC.windowsfontUnicode(40, 550, 48, 0, 0, 0, "Arial", result_unicode);
    TSC.sendcommand("PUTPCX 40,40,\"UL.PCX\"");
    TSC.sendBinaryData(result_utf8, result_utf8.Length);
    TSC.barcode("40", "300", "128", "80", "1", "0", "2", "2", B1);
    TSC.printerfont("40", "440", "0", "0", "15", "15", WT1);
    TSC.printlabel("1", "1");
    TSC.closeport();
    logger.LogInformation("End print");
}
);

app.Run();

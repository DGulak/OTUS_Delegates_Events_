using Barcoded;
using Zen.Barcode;

namespace ClassLibrary1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string VALUE = "1";

            LinearBarcode newBarcode = new LinearBarcode(VALUE, Symbology.Ean13)
            {
                Encoder = 
                        {
                            Dpi = 300,
                            BarcodeHeight = 100,
                            XDimension = 1,
                            HumanReadableSymbolAligned = true,
                            Quietzone = true,
                            
                           
                        }
            };

            newBarcode.Encoder.HumanReadableValue = VALUE;

            newBarcode.Encoder.SetHumanReadablePosition("Embedded");

            newBarcode.Encoder.SetHumanReadableFont("Arial", 20);

            CodeForm form = new CodeForm();

            form.codeBox.Image = newBarcode.Image;

            form.ShowDialog();
        }
    }
}

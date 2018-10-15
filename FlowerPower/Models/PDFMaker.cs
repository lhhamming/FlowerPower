using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using FlowerPower.Models;
using FlowerPower;
using System.Globalization;

namespace FlowerPower.Models
{
    public class PDFMaker
    {
        private DB_A3D6D6_FlowerPowerLuukEntities db = new DB_A3D6D6_FlowerPowerLuukEntities();
        Document _document;
        Font _fontstyle;
        PdfPTable _pdfTable = new PdfPTable(4);
        PdfPCell _pdfCell;
        MemoryStream _memoryStream = new MemoryStream();

        public byte[] PreparePDF(bestelling bestelling)
        {
            _document = new Document(PageSize.A4, 0f, 0f, 0f, 0f);
            _document.SetPageSize(PageSize.A4);
            _document.SetMargins(20f, 20f, 20f, 20f);
            _pdfTable.WidthPercentage = 100;
            _pdfTable.HorizontalAlignment = Element.ALIGN_CENTER;
            _fontstyle = FontFactory.GetFont("Tahoma", 8f, 1);
            PdfWriter.GetInstance(_document, _memoryStream);
            _document.Open();
            _pdfTable.SetWidths(new float[] { 30f, 90f, 50f, 100f });

            this.ReportHeader(bestelling);
            this.ReportBody(bestelling);
            _pdfTable.HeaderRows = 2;
            _document.Add(_pdfTable);
            _document.Close();
            return _memoryStream.ToArray();
        }

        private void ReportHeader(bestelling bestelling)
        {
            _fontstyle = FontFactory.GetFont("Tahoma", 11f, 1);
            _pdfCell = new PdfPCell(new Phrase("Aan:", _fontstyle));
            _pdfCell.Colspan = 3;
            _pdfCell.HorizontalAlignment = Element.ALIGN_LEFT;
            _pdfCell.Border = 0;
            _pdfCell.BackgroundColor = BaseColor.WHITE;
            _pdfCell.ExtraParagraphSpace = 0;
            _pdfTable.AddCell(_pdfCell);
            _pdfCell = new PdfPCell(new Phrase("Van:", _fontstyle));
            _pdfCell.Colspan = 1;
            _pdfCell.HorizontalAlignment = Element.ALIGN_RIGHT;
            _pdfCell.Border = 0;
            _pdfCell.BackgroundColor = BaseColor.WHITE;
            _pdfCell.ExtraParagraphSpace = 0;
            _pdfTable.AddCell(_pdfCell);
            _pdfTable.CompleteRow();
            _fontstyle = FontFactory.GetFont("Tahoma", 11f, 0);
            _pdfCell = new PdfPCell(new Phrase(bestelling.klant.voorletters + " " + bestelling.klant.tussenvoegsels + " " + bestelling.klant.achternaam, _fontstyle));
            _pdfCell.Colspan = 3;
            _pdfCell.HorizontalAlignment = Element.ALIGN_LEFT;
            _pdfCell.Border = 0;
            _pdfCell.BackgroundColor = BaseColor.WHITE;
            _pdfCell.ExtraParagraphSpace = 0;
            _pdfTable.AddCell(_pdfCell);
            _pdfCell = new PdfPCell(new Phrase("Flower Power", _fontstyle));
            _pdfCell.Colspan = 1;
            _pdfCell.HorizontalAlignment = Element.ALIGN_RIGHT;
            _pdfCell.Border = 0;
            _pdfCell.BackgroundColor = BaseColor.WHITE;
            _pdfCell.ExtraParagraphSpace = 0;
            _pdfTable.AddCell(_pdfCell);
            _pdfTable.CompleteRow();
            _pdfCell = new PdfPCell(new Phrase(bestelling.klant.adres, _fontstyle));
            _pdfCell.Colspan = 3;
            _pdfCell.HorizontalAlignment = Element.ALIGN_LEFT;
            _pdfCell.Border = 0;
            _pdfCell.BackgroundColor = BaseColor.WHITE;
            _pdfCell.ExtraParagraphSpace = 0;
            _pdfTable.AddCell(_pdfCell);
            _pdfCell = new PdfPCell(new Phrase("Beekzicht 21", _fontstyle));
            _pdfCell.Colspan = 1;
            _pdfCell.HorizontalAlignment = Element.ALIGN_RIGHT;
            _pdfCell.Border = 0;
            _pdfCell.BackgroundColor = BaseColor.WHITE;
            _pdfCell.ExtraParagraphSpace = 0;
            _pdfTable.AddCell(_pdfCell);
            _pdfTable.CompleteRow(); 
            _pdfCell = new PdfPCell(new Phrase(bestelling.klant.postcode + " " + bestelling.klant.woonplaats, _fontstyle));
            _pdfCell.Colspan = 3;
            _pdfCell.HorizontalAlignment = Element.ALIGN_LEFT;
            _pdfCell.Border = 0;
            _pdfCell.BackgroundColor = BaseColor.WHITE;
            _pdfCell.ExtraParagraphSpace = 0;
            _pdfTable.AddCell(_pdfCell);
            _pdfCell = new PdfPCell(new Phrase("1246 HD Amsterdam", _fontstyle));
            _pdfCell.Colspan = 1;
            _pdfCell.HorizontalAlignment = Element.ALIGN_RIGHT;
            _pdfCell.Border = 0;
            _pdfCell.BackgroundColor = BaseColor.WHITE;
            _pdfCell.ExtraParagraphSpace = 0;
            _pdfTable.AddCell(_pdfCell);
            _pdfTable.CompleteRow();
            _pdfCell = new PdfPCell(new Phrase("Tel. 021 1234567", _fontstyle));
            _pdfCell.Colspan = 4;
            _pdfCell.HorizontalAlignment = Element.ALIGN_RIGHT;
            _pdfCell.Border = 0;
            _pdfCell.BackgroundColor = BaseColor.WHITE;
            _pdfCell.ExtraParagraphSpace = 0;
            _pdfTable.AddCell(_pdfCell);
            _pdfTable.CompleteRow();
            _pdfCell = new PdfPCell(new Phrase("Web: www.powerflower.nl", _fontstyle));
            _pdfCell.Colspan = 4;
            _pdfCell.HorizontalAlignment = Element.ALIGN_RIGHT;
            _pdfCell.Border = 0;
            _pdfCell.BackgroundColor = BaseColor.WHITE;
            _pdfCell.ExtraParagraphSpace = 0;
            _pdfTable.AddCell(_pdfCell);
            _pdfTable.CompleteRow();
            _pdfCell = new PdfPCell(new Phrase("Mail: info@powerflower.nl", _fontstyle));
            _pdfCell.Colspan = 4;
            _pdfCell.HorizontalAlignment = Element.ALIGN_RIGHT;
            _pdfCell.Border = 0;
            _pdfCell.BackgroundColor = BaseColor.WHITE;
            _pdfCell.ExtraParagraphSpace = 0;
            _pdfTable.AddCell(_pdfCell);
            _pdfTable.CompleteRow();
            _pdfCell = new PdfPCell(new Phrase("\n", _fontstyle));
            _pdfCell.Colspan = 4;
            _pdfCell.HorizontalAlignment = Element.ALIGN_RIGHT;
            _pdfCell.Border = 0;
            _pdfCell.BackgroundColor = BaseColor.WHITE;
            _pdfCell.ExtraParagraphSpace = 0;
            _pdfTable.AddCell(_pdfCell);
            _pdfTable.CompleteRow();
            _pdfCell = new PdfPCell(new Phrase("\n", _fontstyle));
            _pdfCell.Colspan = 4;
            _pdfCell.HorizontalAlignment = Element.ALIGN_RIGHT;
            _pdfCell.Border = 0;
            _pdfCell.BackgroundColor = BaseColor.WHITE;
            _pdfCell.ExtraParagraphSpace = 0;
            _pdfTable.AddCell(_pdfCell);
            _pdfTable.CompleteRow();
            _pdfCell = new PdfPCell(new Phrase("\n", _fontstyle));
            _pdfCell.Colspan = 4;
            _pdfCell.HorizontalAlignment = Element.ALIGN_RIGHT;
            _pdfCell.Border = 0;
            _pdfCell.BackgroundColor = BaseColor.WHITE;
            _pdfCell.ExtraParagraphSpace = 0;
            _pdfTable.AddCell(_pdfCell);
            _pdfTable.CompleteRow();
            _fontstyle = FontFactory.GetFont("Tahoma", 11f, 1);
            _pdfCell = new PdfPCell(new Phrase("Factuurgegevens", _fontstyle));
            _pdfCell.Colspan = 4;
            _pdfCell.HorizontalAlignment = Element.ALIGN_LEFT;
            _pdfCell.Border = 0;
            _pdfCell.BackgroundColor = BaseColor.WHITE;
            _pdfCell.ExtraParagraphSpace = 0;
            _pdfTable.AddCell(_pdfCell);
            _pdfTable.CompleteRow();
            _fontstyle = FontFactory.GetFont("Tahoma", 11f, 0);
            _pdfCell = new PdfPCell(new Phrase("Factuurnummer: " + bestelling.bestellingid, _fontstyle));
            _pdfCell.Colspan = 4;
            _pdfCell.HorizontalAlignment = Element.ALIGN_LEFT;
            _pdfCell.Border = 0;
            _pdfCell.BackgroundColor = BaseColor.WHITE;
            _pdfCell.ExtraParagraphSpace = 0;
            _pdfTable.AddCell(_pdfCell);
            _pdfTable.CompleteRow();
            _pdfCell = new PdfPCell(new Phrase("Factuurdatum: " + bestelling.besteldatum.ToString().Remove(bestelling.besteldatum.ToString().Length - 9), _fontstyle));
            _pdfCell.Colspan = 4;
            _pdfCell.HorizontalAlignment = Element.ALIGN_LEFT;
            _pdfCell.Border = 0;
            _pdfCell.BackgroundColor = BaseColor.WHITE;
            _pdfCell.ExtraParagraphSpace = 0;
            _pdfTable.AddCell(_pdfCell);
            _pdfTable.CompleteRow();
            _pdfCell = new PdfPCell(new Phrase("Afhaaldatum: " + bestelling.ophaaldatum.ToString().Remove(bestelling.ophaaldatum.ToString().Length - 9), _fontstyle));
            _pdfCell.Colspan = 4;
            _pdfCell.HorizontalAlignment = Element.ALIGN_LEFT;
            _pdfCell.Border = 0;
            _pdfCell.BackgroundColor = BaseColor.WHITE;
            _pdfCell.ExtraParagraphSpace = 0;
            _pdfTable.AddCell(_pdfCell);
            _pdfTable.CompleteRow();
            string afhaaltijdstip = bestelling.ophaaldatum.ToString();
            string afhaaltijdstipformat = afhaaltijdstip.Remove(0, 11);
            _pdfCell = new PdfPCell(new Phrase("Afhaaltijdstip: " + afhaaltijdstipformat.Remove(afhaaltijdstipformat.Length - 3), _fontstyle));
            _pdfCell.Colspan = 4;
            _pdfCell.HorizontalAlignment = Element.ALIGN_LEFT;
            _pdfCell.Border = 0;
            _pdfCell.BackgroundColor = BaseColor.WHITE;
            _pdfCell.ExtraParagraphSpace = 0;
            _pdfTable.AddCell(_pdfCell);
            _pdfTable.CompleteRow();
            _pdfCell = new PdfPCell(new Phrase("Afhaallocatie: " + bestelling.vestiging.vestigingsadres + " - " + bestelling.vestiging.vestigingsplaats + " - " + bestelling.vestiging.vestigingspostcode, _fontstyle));
            _pdfCell.Colspan = 4;
            _pdfCell.HorizontalAlignment = Element.ALIGN_LEFT;
            _pdfCell.Border = 0;
            _pdfCell.BackgroundColor = BaseColor.WHITE;
            _pdfCell.ExtraParagraphSpace = 0;
            _pdfTable.AddCell(_pdfCell);
            _pdfTable.CompleteRow();
            _pdfCell = new PdfPCell(new Phrase("\n", _fontstyle));
            _pdfCell.Colspan = 4;
            _pdfCell.HorizontalAlignment = Element.ALIGN_RIGHT;
            _pdfCell.Border = 0;
            _pdfCell.BackgroundColor = BaseColor.WHITE;
            _pdfCell.ExtraParagraphSpace = 0;
            _pdfTable.AddCell(_pdfCell);
            _pdfTable.CompleteRow();
            _pdfCell = new PdfPCell(new Phrase("\n", _fontstyle));
            _pdfCell.Colspan = 4;
            _pdfCell.HorizontalAlignment = Element.ALIGN_RIGHT;
            _pdfCell.Border = 0;
            _pdfCell.BackgroundColor = BaseColor.WHITE;
            _pdfCell.ExtraParagraphSpace = 0;
            _pdfTable.AddCell(_pdfCell);
            _pdfTable.CompleteRow();
            _pdfCell = new PdfPCell(new Phrase("\n", _fontstyle));
            _pdfCell.Colspan = 4;
            _pdfCell.HorizontalAlignment = Element.ALIGN_RIGHT;
            _pdfCell.Border = 0;
            _pdfCell.BackgroundColor = BaseColor.WHITE;
            _pdfCell.ExtraParagraphSpace = 0;
            _pdfTable.AddCell(_pdfCell);
            _pdfTable.CompleteRow();
        }

        private void ReportBody(bestelling bestelling)
        {
            // Table header
            _fontstyle = FontFactory.GetFont("Tahoma", 8f, 1);
            _pdfCell = new PdfPCell(new Phrase("Aantal", _fontstyle));
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _pdfCell.BackgroundColor = BaseColor.LIGHT_GRAY;
            _pdfTable.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(new Phrase("Naam boeket", _fontstyle));
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _pdfCell.BackgroundColor = BaseColor.LIGHT_GRAY;
            _pdfTable.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(new Phrase("Prijs per stuk", _fontstyle));
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _pdfCell.BackgroundColor = BaseColor.LIGHT_GRAY;
            _pdfTable.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(new Phrase("Subtotaal", _fontstyle));
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _pdfCell.BackgroundColor = BaseColor.LIGHT_GRAY;
            _pdfTable.AddCell(_pdfCell);
            _pdfTable.CompleteRow();

            
            // Table body
            _fontstyle = FontFactory.GetFont("Tahoma", 8f, 0);
            var bestelregel = db.bestelregels.Where(b => b.bestelling_bestellingid == bestelling.bestellingid);
            

            decimal? totaalPrijsExclBtw = 0;
            foreach (var artikel in bestelregel)
            {

                _pdfCell = new PdfPCell(new Phrase(artikel.aantal.ToString(), _fontstyle));
                _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pdfCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _pdfCell.BackgroundColor = BaseColor.WHITE;
                _pdfTable.AddCell(_pdfCell);

                _pdfCell = new PdfPCell(new Phrase(artikel.artikel.artikelnaam, _fontstyle));
                _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pdfCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _pdfCell.BackgroundColor = BaseColor.WHITE;
                _pdfTable.AddCell(_pdfCell);

                _pdfCell = new PdfPCell(new Phrase("€" + artikel.artikel.prijs.ToString().Remove(artikel.artikel.prijs.ToString().Length - 2), _fontstyle));
                _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pdfCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _pdfCell.BackgroundColor = BaseColor.WHITE;
                _pdfTable.AddCell(_pdfCell);

                var totaalPrijsArtikel = artikel.aantal * artikel.artikel.prijs;

                _pdfCell = new PdfPCell(new Phrase("€" + totaalPrijsArtikel.ToString().Remove(totaalPrijsArtikel.ToString().Length - 2), _fontstyle));
                _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pdfCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _pdfCell.BackgroundColor = BaseColor.WHITE;
                _pdfTable.AddCell(_pdfCell);

                totaalPrijsExclBtw += totaalPrijsArtikel;

                _pdfTable.CompleteRow();
            }


            _fontstyle = FontFactory.GetFont("Tahoma", 8f, 1);
            _pdfCell = new PdfPCell(new Phrase("Totaal excl. BTW:", _fontstyle));
            _pdfCell.Colspan = 3;
            _pdfCell.HorizontalAlignment = Element.ALIGN_RIGHT;
            _pdfCell.Border = 0;
            _pdfCell.BackgroundColor = BaseColor.WHITE;
            _pdfCell.ExtraParagraphSpace = 0;
            _pdfTable.AddCell(_pdfCell);
            _fontstyle = FontFactory.GetFont("Tahoma", 8f, 0);
            _pdfCell = new PdfPCell(new Phrase("€" + totaalPrijsExclBtw.ToString().Remove(totaalPrijsExclBtw.ToString().Length - 2), _fontstyle));
            _pdfCell.Colspan = 1;
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.Border = 0;
            _pdfCell.BackgroundColor = BaseColor.WHITE;
            _pdfCell.ExtraParagraphSpace = 0;
            _pdfTable.AddCell(_pdfCell);
            _pdfTable.CompleteRow();
            decimal? totaalPrijsBTW = totaalPrijsExclBtw * 0.21m;
            _fontstyle = FontFactory.GetFont("Tahoma", 8f, 1);
            _pdfCell = new PdfPCell(new Phrase("21% BTW: ", _fontstyle));
            _pdfCell.Colspan = 3;
            _pdfCell.HorizontalAlignment = Element.ALIGN_RIGHT;
            _pdfCell.Border = 0;
            _pdfCell.BackgroundColor = BaseColor.WHITE;
            _pdfCell.ExtraParagraphSpace = 0;
            _pdfTable.AddCell(_pdfCell);
            _fontstyle = FontFactory.GetFont("Tahoma", 8f, 0);
            _pdfCell = new PdfPCell(new Phrase("€" + totaalPrijsBTW.ToString().Remove(totaalPrijsBTW.ToString().Length - 4), _fontstyle));
            _pdfCell.Colspan = 1;
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.Border = 0;
            _pdfCell.BackgroundColor = BaseColor.WHITE;
            _pdfCell.ExtraParagraphSpace = 0;
            _pdfTable.AddCell(_pdfCell);
            _pdfTable.CompleteRow();
            _fontstyle = FontFactory.GetFont("Tahoma", 8f, 1);
            _pdfCell = new PdfPCell(new Phrase("Totaal incl. BTW: ", _fontstyle));
            _pdfCell.Colspan = 3;
            _pdfCell.HorizontalAlignment = Element.ALIGN_RIGHT;
            _pdfCell.Border = 0;
            _pdfCell.BackgroundColor = BaseColor.WHITE;
            _pdfCell.ExtraParagraphSpace = 0;
            _pdfTable.AddCell(_pdfCell);
            decimal? TotaalInclBTW = totaalPrijsExclBtw + totaalPrijsBTW;
            _fontstyle = FontFactory.GetFont("Tahoma", 8f, 0);
            _pdfCell = new PdfPCell(new Phrase("€" + TotaalInclBTW.ToString().Remove(TotaalInclBTW.ToString().Length - 4), _fontstyle));
            _pdfCell.Colspan = 1;
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.Border = 0;
            _pdfCell.BackgroundColor = BaseColor.WHITE;
            _pdfCell.ExtraParagraphSpace = 0;
            _pdfTable.AddCell(_pdfCell);
            _pdfTable.CompleteRow();
            _pdfCell = new PdfPCell(new Phrase("\n", _fontstyle));
            _pdfCell.Colspan = 4;
            _pdfCell.HorizontalAlignment = Element.ALIGN_LEFT;
            _pdfCell.Border = 0;
            _pdfCell.BackgroundColor = BaseColor.WHITE;
            _pdfCell.ExtraParagraphSpace = 0;
            _pdfTable.AddCell(_pdfCell);
            _pdfTable.CompleteRow();
            _pdfCell = new PdfPCell(new Phrase("\n", _fontstyle));
            _pdfCell.Colspan = 4;
            _pdfCell.HorizontalAlignment = Element.ALIGN_LEFT;
            _pdfCell.Border = 0;
            _pdfCell.BackgroundColor = BaseColor.WHITE;
            _pdfCell.ExtraParagraphSpace = 0;
            _pdfTable.AddCell(_pdfCell);
            _pdfTable.CompleteRow();
            _pdfCell = new PdfPCell(new Phrase("Wij verzoeken u vriendelijk bij het afhalen van de bestelde artikelen het verschuldigde bedrag, giraal of chartaal, te betalen.", _fontstyle));
            _pdfCell.Colspan = 4;
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.Border = 0;
            _pdfCell.BackgroundColor = BaseColor.WHITE;
            _pdfCell.ExtraParagraphSpace = 0;
            _pdfTable.AddCell(_pdfCell);
            _pdfTable.CompleteRow();
        }
    }
}
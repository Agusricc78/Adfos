using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Adfos.Common;
using Adfos.Entities;
using Adfos.Logging;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;

namespace IntegracionApi.Formatters
{
    public class RegistroDiscapacidadExcelFormatter : MediaTypeFormatter
    {
        Log _log = new Log();
        private static readonly Type SupportedType = typeof(IEnumerable<RegistroDiscapacidad>);
        public RegistroDiscapacidadExcelFormatter()
        {
            SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/vnd.ms-excel"));
            MediaTypeMappings.Add(new UriPathExtensionMapping("xlsx", "application/vnd.ms-excel"));
        }

        public override bool CanReadType(Type type)
        {
            return SupportedType == type;
        }

        public override bool CanWriteType(Type type)
        {
            return SupportedType == type;
        }

        public override Task WriteToStreamAsync(Type type, object value, Stream writeStream, HttpContent content,
            TransportContext transportContext)
        {

            var taskSource = new TaskCompletionSource<object>();
            try
            {
                MemoryStream obj_stream = new MemoryStream();

                var tempFile = Path.Combine(Path.GetTempPath(), Guid.NewGuid() + ".xlsx");
                CreateExcelDoc(tempFile, value);
                obj_stream = new MemoryStream(File.ReadAllBytes(tempFile));
                obj_stream.CopyTo(writeStream);

                var st = new StreamWriter(writeStream);
                File.Delete(tempFile);
                taskSource.SetResult(st);
            }
            catch (Exception ex)
            {
                taskSource.SetException(ex);
                _log.Database(new LogEntry
                {
                    Source = ex.Source,
                    Type = EventLogEntryType.Error,
                    Number = -1,
                    Code = ex.HResult,
                    Message = ex.GetExceptionMessages(),
                    userId = "Integracion.ReintegrosDiscapacidad",
                    Ip = General.GetIp()
                });
            }
            return taskSource.Task;
        }

        public void CreateExcelDoc(string fileName, object value)
        {
            using (SpreadsheetDocument document = SpreadsheetDocument.Create(fileName, SpreadsheetDocumentType.Workbook))
            {
                WorkbookPart workbookPart = document.AddWorkbookPart();
                workbookPart.Workbook = new Workbook();

                WorksheetPart worksheetPart = workbookPart.AddNewPart<WorksheetPart>();
                worksheetPart.Worksheet = new Worksheet();

                // Adding style
                WorkbookStylesPart stylePart = workbookPart.AddNewPart<WorkbookStylesPart>();
                stylePart.Stylesheet = GenerateStylesheet();
                stylePart.Stylesheet.Save();

                // Setting up columns
                Columns columns = new Columns(
                        new Column // CUIL
                        {
                            Min = 1,
                            Max = 1,
                            Width = 12,
                            CustomWidth = true
                        },
                        new Column // CUIT
                        {
                            Min = 2,
                            Max = 2,
                            Width = 12,
                            CustomWidth = true
                        },
                        new Column // Tipo
                        {
                            Min = 3,
                            Max = 3,
                            Width = 10,
                            CustomWidth = true
                        },
                        new Column // Fecha
                        {
                            Min = 4,
                            Max = 4,
                            Width = 12,
                            CustomWidth = true
                        },
                        new Column // Numero
                        {
                            Min = 5,
                            Max = 5,
                            Width = 14,
                            CustomWidth = true
                        },
                        new Column // Periodo
                        {
                            Min = 6,
                            Max = 6,
                            Width = 11,
                            CustomWidth = true
                        },
                        new Column // Practica
                        {
                            Min = 7,
                            Max = 7,
                            Width = 64,
                            CustomWidth = true
                        },
                        new Column // Cantidad
                        {
                            Min = 8,
                            Max = 8,
                            Width = 8,
                            CustomWidth = true
                        },
                        new Column // Importe
                        {
                            Min = 9,
                            Max = 9,
                            Width = 12,
                            CustomWidth = true
                        },
                        new Column // Estado
                        {
                            Min = 10,
                            Max = 10,
                            Width = 16,
                            CustomWidth = true
                        },
                        new Column // Numero Envio Afip
                        {
                            Min = 11,
                            Max = 11,
                            Width = 9,
                            CustomWidth = true
                        },
                        new Column // Error
                        {
                            Min = 12,
                            Max = 12,
                            Width = 9,
                            CustomWidth = true
                        });

                worksheetPart.Worksheet.AppendChild(columns);

                Sheets sheets = workbookPart.Workbook.AppendChild(new Sheets());

                Sheet sheet = new Sheet() { Id = workbookPart.GetIdOfPart(worksheetPart), SheetId = 1, Name = "Reporte Registro Discapacidad" };

                sheets.Append(sheet);

                workbookPart.Workbook.Save();

                SheetData sheetData = worksheetPart.Worksheet.AppendChild(new SheetData());

                // Constructing header
                Row row = new Row();

                row.Append(
                    ConstructCell("CUIL", CellValues.String, 2),
                    ConstructCell("CUIT", CellValues.String, 2),
                    ConstructCell("Tipo", CellValues.String, 2),
                    ConstructCell("Fecha", CellValues.String, 2),
                    ConstructCell("Numero", CellValues.String, 2),
                    ConstructCell("Periodo", CellValues.String, 2),
                    ConstructCell("Practica", CellValues.String, 2),
                    ConstructCell("Cant", CellValues.String, 2),
                    ConstructCell("Importe", CellValues.String, 2),
                    ConstructCell("Estado", CellValues.String, 2),
                    ConstructCell("Envio Afip", CellValues.String, 2),
                    ConstructCell("Error", CellValues.String, 2)
                    );

                // Insert the header row to the Sheet Data
                sheetData.AppendChild(row);

                var datos = (IEnumerable<RegistroDiscapacidad>)value;
                // Inserting each RegistroDiscapacidad
                foreach (var item in datos)
                {
                    row = new Row();

                    //var numeroEnvioAfip = "-";
                    var numeroEnvioAfip = "";
                    if (item.Procesado == 8 || item.Procesado == 9 || item.Procesado == 10)
                    { // Liquidado
                        numeroEnvioAfip = item.NumeroEnvioAfip.ToString();
                    }

                    // Suma 4 porque los primeros tres CellFormat son: default, body, header e importes
                    var styleIndex = Convert.ToUInt32(item.Procesado + 4);
                    if (item.Procesado == 99)
                    {
                        styleIndex = Convert.ToUInt32(15);
                    }

                    var errorCodigo = "";
                    //item.ErrorCodigo != 0
                    if (item.Procesado == 4)
                    {
                        errorCodigo = item.ErrorCodigo.ToString();
                    }

                    row.Append(
                        ConstructCell(item.Cuil, CellValues.Number, 1),
                        ConstructCell(item.Cuit, CellValues.Number, 1),
                        ConstructCell(item.TipoComprobante, CellValues.String, 1),
                        ConstructCell(item.FechaEmision.ToString("dd/MM/yyyy"), CellValues.String, 1),
                        ConstructCell(item.PuntoVenta.ToString().PadLeft(4, '0') + "-" + item.NumeroComprobante.ToString().PadLeft(8, '0'), CellValues.String, 1),
                        ConstructCell(item.Periodo.ToString("MM/yyyy"), CellValues.String, 1),
                        ConstructCell(item.Practica, CellValues.String, 1),
                        ConstructCell(item.Cantidad.ToString(), CellValues.Number, 1),
                        ConstructCell(item.Importe.ToString(CultureInfo.InvariantCulture), CellValues.Number, 3),
                        ConstructCell(item.Estado, CellValues.String, styleIndex),
                        ConstructCell(numeroEnvioAfip, CellValues.Number, 1),
                        ConstructCell(errorCodigo, CellValues.Number, 1));

                    sheetData.AppendChild(row);
                }

                decimal totalImportes = datos.Sum(x => x.Importe);

                row = new Row();
                row.Append(
                 ConstructCell("Total:", CellValues.String, 1),
                 ConstructCell(string.Empty, CellValues.String, 1),
                 ConstructCell(string.Empty, CellValues.String, 1),
                 ConstructCell(string.Empty, CellValues.String, 1),
                 ConstructCell(string.Empty, CellValues.String, 1),
                 ConstructCell(string.Empty, CellValues.String, 1),
                 ConstructCell(string.Empty, CellValues.String, 1),
                 ConstructCell(string.Empty, CellValues.String, 1),
                 ConstructCell(totalImportes.ToString(CultureInfo.InvariantCulture), CellValues.Number, 3),
                 ConstructCell(string.Empty, CellValues.String, 1),
                 ConstructCell(string.Empty, CellValues.String, 1),
                 ConstructCell(string.Empty, CellValues.String, 1));
                sheetData.AppendChild(row);

                worksheetPart.Worksheet.Save();
                
            }
        }

        private Cell ConstructCell(string value, CellValues dataType, uint styleIndex = 0)
        {
            return new Cell()
            {
                CellValue = new CellValue(value),
                DataType = new EnumValue<CellValues>(dataType),
                StyleIndex = styleIndex
            };
        }

        private Stylesheet GenerateStylesheet()
        {
            Stylesheet styleSheet = null;

            Fonts fonts = new Fonts(
                new Font( // Index 0 - default
                    new FontSize() { Val = 10 }
                    ),
                new Font( // Index 1 - White Bold
                    new FontSize() { Val = 10 },
                    new Bold(),
                    new Color() { Rgb = "FFFFFF" }
                    ),
                new Font( // Index 2 - Red Bold
                    new FontSize() { Val = 10 },
                    new Bold(),
                    new Color() { Rgb = "dc3545" }
                    ),
                new Font( // Index 3 - Default Bold
                    new FontSize() { Val = 10 },
                    new Bold()
                    ),
                new Font( // Index 4 - Purple Bold
                    new FontSize() { Val = 10 },
                    new Bold(),
                    new Color() { Rgb = "512DA8" }
                    )
                )
            ;

            Fills fills = new Fills(
                    new Fill(new PatternFill() { PatternType = PatternValues.None }), // Index 0 - default
                    new Fill(new PatternFill() { PatternType = PatternValues.Gray125 }), // Index 1 - default
                    new Fill(new PatternFill(new ForegroundColor { Rgb = new HexBinaryValue() { Value = "66666666" } })
                    { PatternType = PatternValues.Solid }), // Index 2 - header
                    new Fill(new PatternFill(new ForegroundColor { Rgb = new HexBinaryValue() { Value = "f8f9fa" } })
                    { PatternType = PatternValues.Solid }), // Index 3 - 0 Sin Presentar
                    new Fill(new PatternFill(new ForegroundColor { Rgb = new HexBinaryValue() { Value = "007bff" } })
                    { PatternType = PatternValues.Solid }), // Index 4 - 1 Generado
                    new Fill(new PatternFill(new ForegroundColor { Rgb = new HexBinaryValue() { Value = "6c757d" } })
                    { PatternType = PatternValues.Solid }), // Index 5 - 2 Presentado
                    new Fill(new PatternFill(new ForegroundColor { Rgb = new HexBinaryValue() { Value = "28a745" } })
                    { PatternType = PatternValues.Solid }), // Index 6 - 3 Aceptado
                    new Fill(new PatternFill(new ForegroundColor { Rgb = new HexBinaryValue() { Value = "dc3545" } })
                    { PatternType = PatternValues.Solid }), // Index 7 - 4 Error
                    new Fill(new PatternFill(new ForegroundColor { Rgb = new HexBinaryValue() { Value = "ffc107" } })
                    { PatternType = PatternValues.Solid }), // Index 8 - 5 Corregido
                    new Fill(new PatternFill(new ForegroundColor { Rgb = new HexBinaryValue() { Value = "17a2b8" } })
                    { PatternType = PatternValues.Solid }), // Index 9 - 6 En Liquidación
                    new Fill(new PatternFill(new ForegroundColor { Rgb = new HexBinaryValue() { Value = "ffffff" } })
                    { PatternType = PatternValues.Solid }), // Index 10 - 7 Anulado
                    new Fill(new PatternFill(new ForegroundColor { Rgb = new HexBinaryValue() { Value = "512DA8" } })
                    { PatternType = PatternValues.Solid }),  // Index 11 - 8 Liquidado
                    new Fill(new PatternFill(new ForegroundColor { Rgb = new HexBinaryValue() { Value = "512DA8" } })
                    { PatternType = PatternValues.Solid }),  // Index 12 - 9 Liquidado Manual
                    new Fill(new PatternFill(new ForegroundColor { Rgb = new HexBinaryValue() { Value = "ffffff" } })
                    { PatternType = PatternValues.Solid }),  // Index 13 - 10 Anulado Liquidado
                    new Fill(new PatternFill(new ForegroundColor { Rgb = new HexBinaryValue() { Value = "#343a40" } })
                    { PatternType = PatternValues.Solid })  // Index 14 - 99 Histórico
                );

            Borders borders = new Borders(
                    new Border(), // index 0 default
                    new Border( // index 1 black border
                        new LeftBorder(new Color() { Auto = true }) { Style = BorderStyleValues.Thin },
                        new RightBorder(new Color() { Auto = true }) { Style = BorderStyleValues.Thin },
                        new TopBorder(new Color() { Auto = true }) { Style = BorderStyleValues.Thin },
                        new BottomBorder(new Color() { Auto = true }) { Style = BorderStyleValues.Thin },
                        new DiagonalBorder())
                );

            CellFormats cellFormats = new CellFormats(
                    new CellFormat(), // 0 default
                    new CellFormat { FontId = 0, FillId = 0, BorderId = 1, ApplyBorder = true }, // 1 body
                    new CellFormat { FontId = 1, FillId = 2, BorderId = 1, ApplyFill = true }, // 2 header
                    new CellFormat { FontId = 0, FillId = 0, BorderId = 1, ApplyBorder = true, NumberFormatId = 4, ApplyNumberFormat = true }, // 3 Importes
                    new CellFormat { FontId = 3, FillId = 3, BorderId = 1, ApplyBorder = true, ApplyFill = true }, // 4 => 0 Sin Presentar
                    new CellFormat { FontId = 1, FillId = 4, BorderId = 1, ApplyBorder = true, ApplyFill = true }, // 5 => 1 Generado
                    new CellFormat { FontId = 1, FillId = 5, BorderId = 1, ApplyBorder = true, ApplyFill = true }, // 6 => 2 Presentado
                    new CellFormat { FontId = 1, FillId = 6, BorderId = 1, ApplyBorder = true, ApplyFill = true }, // 7 => 3 Aceptado
                    new CellFormat { FontId = 1, FillId = 7, BorderId = 1, ApplyBorder = true, ApplyFill = true }, // 8 => 4 Error
                    new CellFormat { FontId = 3, FillId = 8, BorderId = 1, ApplyBorder = true, ApplyFill = true }, // 9 => 5 Corregido
                    new CellFormat { FontId = 1, FillId = 9, BorderId = 1, ApplyBorder = true, ApplyFill = true }, // 10 => 6 En Liquidación
                    new CellFormat { FontId = 2, FillId = 10, BorderId = 1, ApplyBorder = true, ApplyFill = true }, // 11 => 7 Anulado
                    new CellFormat { FontId = 1, FillId = 11, BorderId = 1, ApplyBorder = true, ApplyFill = true },  // 12 => 8 Liquidado
                    new CellFormat { FontId = 1, FillId = 12, BorderId = 1, ApplyBorder = true, ApplyFill = true },  // 13 => 9 Liquidado Manual
                    new CellFormat { FontId = 4, FillId = 13, BorderId = 1, ApplyBorder = true, ApplyFill = true },  // 14 => 10 Anulado Liquidado
                    new CellFormat { FontId = 1, FillId = 14, BorderId = 1, ApplyBorder = true, ApplyFill = true }  // 15 => 99 Histórico
                );

            styleSheet = new Stylesheet(fonts, fills, borders, cellFormats);

            return styleSheet;
        }
    }
}
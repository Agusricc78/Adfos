using System;
using System.Collections.Generic;
using System.Configuration;
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
    public class RegistroDevolucionExcelFormatter : MediaTypeFormatter
    {
        Log _log = new Log();
        private static readonly Type SupportedType = typeof(IEnumerable<RegistroDevolucion>);
        public RegistroDevolucionExcelFormatter()
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
            bool showCuentaPropia = Boolean.Parse(ConfigurationManager.AppSettings["showCuentaPropia"]);
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
                        new Column // Clave
                        {
                            Min = 1,
                            Max = 1,
                            Width = 12,
                            CustomWidth = true
                        },
                        new Column // Presentacion
                        {
                            Min = 2,
                            Max = 2,
                            Width = 9,
                            CustomWidth = true
                        },
                        new Column // Prestacion
                        {
                            Min = 3,
                            Max = 3,
                            Width = 9,
                            CustomWidth = true
                        },
                        new Column // CUIL
                        {
                            Min = 4,
                            Max = 4,
                            Width = 12,
                            CustomWidth = true
                        },
                        new Column // Afiliado
                        {
                            Min = 5,
                            Max = 5,
                            Width = 36,
                            CustomWidth = true
                        },
                        new Column // Codigo Practica
                        {
                            Min = 6,
                            Max = 6,
                            Width = 13,
                            CustomWidth = true
                        },
                        new Column // Practica
                        {
                            Min = 7,
                            Max = 7,
                            Width = 32,
                            CustomWidth = true
                        },
                        new Column // Subsidiado
                        {
                            Min = 8,
                            Max = 8,
                            Width = 12,
                            CustomWidth = true
                        },
                        new Column // Solicitado
                        {
                            Min = 9,
                            Max = 9,
                            Width = 12,
                            CustomWidth = true
                        },
                        new Column // Nro Afip
                        {
                            Min = 10,
                            Max = 10,
                            Width = 8,
                            CustomWidth = true
                        },
                        new Column // CUIT
                        {
                            Min = 11,
                            Max = 11,
                            Width = 12,
                            CustomWidth = true
                        },
                        new Column // Prestador
                        {
                            Min = 12,
                            Max = 12,
                            Width = 44,
                            CustomWidth = true
                        },
                        new Column // Trans. I
                        {
                            Min = 13,
                            Max = 13,
                            Width = 11,
                            CustomWidth = true
                        },
                        new Column // Importe Aplicado
                        {
                            Min = 14,
                            Max = 14,
                            Width = 12,
                            CustomWidth = true
                        });

                if (showCuentaPropia)
                {
                    columns.Append(
                        new Column // Cuenta Propia
                        {
                            Min = 15,
                            Max = 15,
                            Width = 12,
                            CustomWidth = true
                        });
                }

                worksheetPart.Worksheet.AppendChild(columns);

                Sheets sheets = workbookPart.Workbook.AppendChild(new Sheets());

                Sheet sheet = new Sheet() { Id = workbookPart.GetIdOfPart(worksheetPart), SheetId = 1, Name = "Reporte Devolucion" };

                sheets.Append(sheet);

                workbookPart.Workbook.Save();

                SheetData sheetData = worksheetPart.Worksheet.AppendChild(new SheetData());

                // Constructing header
                Row row = new Row();

                row.Append(
                    ConstructCell("Clave", CellValues.String, 2),
                    ConstructCell("Presentacion", CellValues.String, 2),
                    ConstructCell("Prestacion", CellValues.String, 2),
                    ConstructCell("CUIL", CellValues.String, 2),
                    ConstructCell("Afiliado", CellValues.String, 2),
                    ConstructCell("Codigo Practica", CellValues.String, 2),
                    ConstructCell("Practica", CellValues.String, 2),
                    ConstructCell("Liquidado", CellValues.String, 2),
                    ConstructCell("Solicitado", CellValues.String, 2),
                    ConstructCell("Nro Afip", CellValues.String, 2),
                    ConstructCell("CUIT", CellValues.String, 2),
                    ConstructCell("Prestador", CellValues.String, 2),
                    ConstructCell("Trans. I", CellValues.String, 2),                  
                    ConstructCell("Importe Aplicado", CellValues.String, 2));

                if (showCuentaPropia)
                {
                    row.Append(
                    ConstructCell("Cuenta Propia", CellValues.String, 2));
                }

                // Insert the header row to the Sheet Data
                sheetData.AppendChild(row);

                var datos = (IEnumerable<RegistroDevolucion>)value;
                // Inserting each RegistroDevolucion
                foreach (var item in datos)
                {
                    row = new Row();

                    String fechaTransferencia = "";
                    if (item.FechaTransferenciaI != null && item.FechaTransferenciaI > DateTime.MinValue)
                    {
                        fechaTransferencia = item.FechaTransferenciaI.ToString("dd/MM/yyyy");
                    }

                    row.Append(
                        ConstructCell(item.ClaveRendicion, CellValues.Number, 1),
                        ConstructCell(item.PeriodoPresentacion.ToString("MM/yyyy"), CellValues.String, 1),
                        ConstructCell(item.PeriodoPrestacion.ToString("MM/yyyy"), CellValues.String, 1),
                        ConstructCell(item.Cuil, CellValues.Number, 1),
                        ConstructCell(item.NombreApellido, CellValues.String, 1),
                        ConstructCell(item.CodigoPractica.ToString(), CellValues.Number, 1),
                        ConstructCell(item.Practica, CellValues.String, 1),
                        ConstructCell(item.ImporteSubsidiado.ToString(CultureInfo.InvariantCulture), CellValues.Number, 3),
                        ConstructCell(item.ImporteSolicitado.ToString(CultureInfo.InvariantCulture), CellValues.Number, 3),
                        ConstructCell(item.NroEnvioAfip.ToString(), CellValues.Number, 1),
                        ConstructCell(item.CuitDelCbu, CellValues.Number, 1),
                        ConstructCell(item.Prestador, CellValues.String, 1),
                        ConstructCell(fechaTransferencia, CellValues.String, 1),
                        ConstructCell(item.ImporteAplicadoSSS.ToString(CultureInfo.InvariantCulture), CellValues.Number, 3)
                        );

                    if (showCuentaPropia) {
                        row.Append(ConstructCell(item.FondosPropiosOtraCuenta.ToString(CultureInfo.InvariantCulture), CellValues.Number, 3));
                    }

                    sheetData.AppendChild(row);
                }

                decimal totalSubsidiado = datos.Sum(x => x.ImporteSubsidiado);
                decimal totalSolicitado = datos.Sum(x => x.ImporteSolicitado);
                decimal totalImporteAplicadoSSS = datos.Sum(x => x.ImporteAplicadoSSS);
                decimal totalFondosPropiosOtraCuenta = datos.Sum(x => x.FondosPropiosOtraCuenta);

                row = new Row();
                row.Append(
                 ConstructCell("Total:", CellValues.String, 1),
                 ConstructCell(string.Empty, CellValues.String, 1),
                 ConstructCell(string.Empty, CellValues.String, 1),
                 ConstructCell(string.Empty, CellValues.String, 1),
                 ConstructCell(string.Empty, CellValues.String, 1),
                 ConstructCell(string.Empty, CellValues.String, 1),
                 ConstructCell(string.Empty, CellValues.String, 1),
                 ConstructCell(totalSubsidiado.ToString(CultureInfo.InvariantCulture), CellValues.Number, 3),
                 ConstructCell(totalSolicitado.ToString(CultureInfo.InvariantCulture), CellValues.Number, 3),
                 ConstructCell(string.Empty, CellValues.String, 1),
                 ConstructCell(string.Empty, CellValues.String, 1),
                 ConstructCell(string.Empty, CellValues.String, 1),
                 ConstructCell(string.Empty, CellValues.String, 1),
                 ConstructCell(totalImporteAplicadoSSS.ToString(CultureInfo.InvariantCulture), CellValues.Number, 3)
                 );

                if (showCuentaPropia)
                {
                    row.Append(ConstructCell(totalFondosPropiosOtraCuenta.ToString(CultureInfo.InvariantCulture), CellValues.Number, 3));
                }

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
                new Font( // Index 1 - header
                    new FontSize() { Val = 10 },
                    new Bold(),
                    new Color() { Rgb = "FFFFFF" }

                ));

            Fills fills = new Fills(
                    new Fill(new PatternFill() { PatternType = PatternValues.None }), // Index 0 - default
                    new Fill(new PatternFill() { PatternType = PatternValues.Gray125 }), // Index 1 - default
                    new Fill(new PatternFill(new ForegroundColor { Rgb = new HexBinaryValue() { Value = "66666666" } })
                    { PatternType = PatternValues.Solid }) // Index 2 - header
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
                    new CellFormat(), // default
                    new CellFormat { FontId = 0, FillId = 0, BorderId = 1, ApplyBorder = true }, // body
                    new CellFormat { FontId = 1, FillId = 2, BorderId = 1, ApplyFill = true }// header
                    , new CellFormat { FontId = 0, FillId = 0, BorderId = 1, ApplyBorder = true, NumberFormatId = 4, ApplyNumberFormat = true }
                );

            styleSheet = new Stylesheet(fonts, fills, borders, cellFormats);

            return styleSheet;
        }
    }
}
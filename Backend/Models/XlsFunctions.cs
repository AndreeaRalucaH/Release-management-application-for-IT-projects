using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Relmonitor.Models
{
    public static class XlsFunctions
    {
        public static void insertCells(string docName, List<Tuple<string, string, string, string, string, string>> content, uint rowIndex, string columnName, string defaultRequestor)
        {
            using (SpreadsheetDocument spreadSheet =
            SpreadsheetDocument.Open(docName, true))
            {
                WorksheetPart worksheetPart =
                GetWorksheetPartByName(spreadSheet, "Sheet1");
                Worksheet worksheet = worksheetPart.Worksheet;
                SheetData sheetData = worksheet.GetFirstChild<SheetData>();
                var rowNumber = content.Count;
                var i = 0;
                var test = worksheet.GetFirstChild<SheetData>().Elements<Row>().ElementAt((int)rowIndex);
                for (i = 0; i < content.Count; i++)
                {
                    var jira = content[i];

                    var newRow = (Row)test.CloneNode(true);
                    SetCellValue(worksheet, "D", newRow, jira.Item2);
                    SetCellValue(worksheet, "C", newRow, jira.Item1);
                    SetCellValue(worksheet, "E", newRow, jira.Item3);
                    SetCellValue(worksheet, "G", newRow, String.IsNullOrEmpty(jira.Item4) ? defaultRequestor : jira.Item4);
                    SetCellValue(worksheet, "H", newRow, jira.Item5);

                    InsertRow(rowIndex, worksheetPart, newRow, false);
                }

                for (i = 0; i < content.Count; i++)
                {
                    MergeTwoCells(worksheet, "E" + (i + rowIndex), "F" + (i + rowIndex));
                    spreadSheet.Save();
                }
            }
        }

        private static void SetCellValue(Worksheet worksheet, string column, Row newRow, string value)
        {
            var ticketCell = GetCellFromRow(worksheet, column, newRow);
            ticketCell.CellValue = new CellValue(value);
            ticketCell.DataType =
            new EnumValue<CellValues>(CellValues.String);
        }

        private static void SetMergedCel1Value(Worksheet worksheet, String column, String endcolumn, Row newRow, string value)
        {
            var ticketCell = GetCellFromRow(worksheet, column, newRow);
            ticketCell.CellValue = new CellValue(value);
            ticketCell.DataType =
            new EnumValue<CellValues>(CellValues.String);
        }

        public static void UpdateCell(string docName, string text, uint rowIndex, string columnName)
        {
            using (SpreadsheetDocument spreadSheet =
                SpreadsheetDocument.Open(docName, true))
            {
                WorksheetPart worksheetPart = GetWorksheetPartByName(spreadSheet, "Sheet1");

                if (worksheetPart != null)
                {
                    Cell cell = GetCell(worksheetPart.Worksheet, columnName, rowIndex);
                    cell.CellValue = new CellValue(text);
                    cell.DataType = new EnumValue<CellValues>(CellValues.String);

                    worksheetPart.Worksheet.Save();
                }
                spreadSheet.Save();
            }
        }

        private static WorksheetPart
            GetWorksheetPartByName(SpreadsheetDocument document, string sheetName)
        {
            IEnumerable<Sheet> sheets = document.WorkbookPart.Workbook.GetFirstChild<Sheets>().Elements<Sheet>().Where(s => s.Name == sheetName);

            if (sheets.Count() == 0)
            {
                return null;
            }

            string relationshipId = sheets.First().Id.Value;
            WorksheetPart worksheetPart = (WorksheetPart)document.WorkbookPart.GetPartById(relationshipId);
            return worksheetPart;
        }

        private static Cell GetCell(Worksheet worksheet, string columnName, uint rowIndex)
        {
            Row row = GetRow(worksheet, rowIndex);

            if (row == null)
            {
                return null;
            }

            return row.Elements<Cell>().Where(c => string.Compare(c.CellReference.Value, columnName + rowIndex, true) == 0).First();
        }

        private static Cell GetCellFromRow(Worksheet worksheet, string columnName, Row row)
        {
            if (row == null)
                return null;

            return row.Elements<Cell>().Where(c => c.CellReference.Value.Contains(columnName)).First();
        }

        private static MergeCells GetMergedCellFromRow(Worksheet worksheet, string columnName, Row row)
        {
            if (row == null)
                return null;

            return row.Elements<MergeCells>().Where(c => c.LocalName.Contains(columnName)).First();
        }

        private static Row GetRow(Worksheet worksheet, uint rowIndex)
        {
            return worksheet.GetFirstChild<SheetData>().Elements<Row>().Where(r => r.RowIndex == rowIndex).First();
        }

        public static Row InsertRow(uint rowIndex, WorksheetPart worksheetPart, Row insertRow, bool isNewLastRow = false)
        {
            Worksheet worksheet = worksheetPart.Worksheet;
            SheetData sheetData = worksheet.GetFirstChild<SheetData>();
            Row retRow = !isNewLastRow ? sheetData.Elements<Row>().FirstOrDefault(r => r.RowIndex == rowIndex) : null;

            if (retRow != null)
            {
                if (insertRow != null)
                {
                    UpdateRowIndexes(worksheetPart, rowIndex, false);
                    UpdateMergedCellReferences(worksheetPart, rowIndex, false);
                    UpdateHyperlinkReferences(worksheetPart, rowIndex, false);

                    retRow = sheetData.InsertBefore(insertRow, retRow);
                    string curIndex = retRow.RowIndex.ToString();
                    string newIndex = rowIndex.ToString();

                    foreach (Cell cell in retRow.Elements<Cell>())
                    {
                        cell.CellReference = new StringValue(cell.CellReference.Value.Replace(curIndex, newIndex));
                    }

                    retRow.RowIndex = rowIndex;
                }
            }
            else
            {
                Row refRow = !isNewLastRow ? sheetData.Elements<Row>().FirstOrDefault(row => row.RowIndex > rowIndex) : null;

                retRow = insertRow ?? new Row() { RowIndex = rowIndex };

                IEnumerable<Cell> cellsInRow = retRow.Elements<Cell>();
                if (cellsInRow.Any())
                {
                    string curIndex = retRow.RowIndex.ToString();
                    string newIndex = rowIndex.ToString();
                    foreach (Cell cell in cellsInRow)
                    {
                        cell.CellReference = new StringValue(cell.CellReference.Value.Replace(curIndex, newIndex));
                    }
                    retRow.RowIndex = rowIndex;
                }
                sheetData.InsertBefore(retRow, refRow);
            }
            return retRow;
        }

        private static void UpdateRowIndexes(WorksheetPart worksheetPart, uint rowIndex, bool isDeletedRow)
        {
            IEnumerable<Row> rows = worksheetPart.Worksheet.Descendants<Row>().Where(r => r.RowIndex.Value >= rowIndex);
            foreach (Row row in rows)
            {
                uint newIndex = (isDeletedRow ? row.RowIndex - 1 : row.RowIndex + 1);
                string curRowIndex = row.RowIndex.ToString();
                string newRowIndex = newIndex.ToString();
                foreach (Cell cell in row.Elements<Cell>())
                {
                    cell.CellReference = new StringValue(cell.CellReference.Value.Replace(curRowIndex, newRowIndex));
                }
                row.RowIndex = newIndex;
            }
        }

        private static void UpdateMergedCellReferences(WorksheetPart worksheetPart, uint rowIndex, bool isDeletedRow)
        {
            if (worksheetPart.Worksheet.Elements<MergeCells>().Count() > 0)
            {

                MergeCells mergeCells = worksheetPart.Worksheet.Elements<MergeCells>().FirstOrDefault();

                if (mergeCells != null)
                {
                    List<MergeCell> mergeCellsList = mergeCells.Elements<MergeCell>().Where(r => r.Reference.HasValue).
                        Where(r => GetRowIndex(r.Reference.Value.Split(':').ElementAt(0)) >= rowIndex ||
                                   GetRowIndex(r.Reference.Value.Split(':').ElementAt(1)) >= rowIndex).ToList();

                    if (isDeletedRow)
                    {
                        List<MergeCell> mergeCellsToDelete = mergeCellsList.Where(r => GetRowIndex(r.Reference.Value.Split(':').ElementAt(0)) == rowIndex ||
                                                                                       GetRowIndex(r.Reference.Value.Split(':').ElementAt(1)) == rowIndex).ToList();

                        foreach (MergeCell cellToDelete in mergeCellsToDelete)
                        {
                            cellToDelete.Remove();
                        }

                        mergeCellsList = mergeCells.Elements<MergeCell>().Where(r => r.Reference.HasValue)
                                                                           .Where(r => GetRowIndex(r.Reference.Value.Split(':').ElementAt(0)) > rowIndex ||
                                                                                       GetRowIndex(r.Reference.Value.Split(':').ElementAt(1)) > rowIndex).ToList();


                    }

                    foreach (MergeCell mergeCell in mergeCellsList)
                    {
                        string[] cellReference = mergeCell.Reference.Value.Split(':');

                        if (GetRowIndex(cellReference.ElementAt(0)) >= rowIndex)
                        {
                            string columnName = GetColumnName(cellReference.ElementAt(0));
                            cellReference[0] = isDeletedRow ? columnName + (GetRowIndex(cellReference.ElementAt(0)) - 1).ToString() : IncrementCellReference(cellReference.ElementAt(0), CellReferencePartEnum.Row);
                        }

                        if (GetRowIndex(cellReference.ElementAt(1)) >= rowIndex)
                        {
                            string columnName = GetColumnName(cellReference.ElementAt(1));
                            cellReference[1] = isDeletedRow ? columnName + (GetRowIndex(cellReference.ElementAt(1)) - 1).ToString() : IncrementCellReference(cellReference.ElementAt(1), CellReferencePartEnum.Row);
                        }

                        mergeCell.Reference = new StringValue(cellReference[0] + ":" + cellReference[1]);
                    }
                }
            }
        }

        private static void UpdateHyperlinkReferences(WorksheetPart worksheetPart, uint rowIndex, bool isDeletedRow)
        {
            Hyperlinks hyperlinks = worksheetPart.Worksheet.Elements<Hyperlinks>().FirstOrDefault();

            if (hyperlinks != null)
            {
                Match hyperlinkRowIndexMatch;
                uint hyperlinkRowIndex;

                foreach (Hyperlink hyperlink in hyperlinks.Elements<Hyperlink>())
                {
                    hyperlinkRowIndexMatch = Regex.Match(hyperlink.Reference.Value, "[0-9]+");

                    if (hyperlinkRowIndexMatch.Success && uint.TryParse(hyperlinkRowIndexMatch.Value, out hyperlinkRowIndex) && hyperlinkRowIndex >= rowIndex)
                    {
                        if (isDeletedRow)
                        {
                            if (hyperlinkRowIndex == rowIndex)
                            {
                                hyperlink.Remove();
                            }
                            else
                            {
                                hyperlink.Reference.Value = hyperlink.Reference.Value.Replace(hyperlinkRowIndexMatch.Value, (hyperlinkRowIndex - 1).ToString());
                            }
                        }
                        else
                        {
                            hyperlink.Reference.Value = hyperlink.Reference.Value.Replace(hyperlinkRowIndexMatch.Value, (hyperlinkRowIndex + 1).ToString());
                        }
                    }
                }
                if (hyperlinks.Elements<Hyperlinks>().Count() == 0)
                {
                    hyperlinks.Remove();
                }
            }
        }

        private static uint GetRowIndex(string cellReference)
        {
            Regex regex = new Regex(@"\d+");
            Match match = regex.Match(cellReference);

            return uint.Parse(match.Value);
        }

        private static string IncrementCellReference(string reference, CellReferencePartEnum cellRefPart)
        {
            string newReference = reference;

            if (cellRefPart != CellReferencePartEnum.None && !String.IsNullOrEmpty(reference))
            {
                string[] parts = Regex.Split(reference, "([A-Z]+)");

                if (cellRefPart == CellReferencePartEnum.Column || cellRefPart == CellReferencePartEnum.Both)
                {
                    List<char> col = parts[1].ToCharArray().ToList();
                    bool needsIncrement = true;
                    int index = col.Count - 1;

                    do
                    {
                        col[index] = Letters[Letters.IndexOf(col[index]) + 1];

                        if (col[index] == Letters[Letters.Count - 1])
                        {
                            col[index] = Letters[0];
                        }
                        else
                        {
                            needsIncrement = false;
                        }

                    } while (needsIncrement && --index >= 0);

                    if (needsIncrement)
                    {
                        col.Add(Letters[0]);
                    }

                    parts[1] = new String(col.ToArray());
                }

                if (cellRefPart == CellReferencePartEnum.Row || cellRefPart == CellReferencePartEnum.Both)
                {
                    parts[2] = (int.Parse(parts[2]) + 1).ToString();
                }

                newReference = parts[1] + parts[2];
            }

            return newReference;
        }

        private static string GetColumnName(string cellName)
        {
            Regex regex = new Regex("[A-Za-z]+");
            Match match = regex.Match(cellName);

            return match.Value;
        }

        private enum CellReferencePartEnum
        {
            None,
            Column,
            Row,
            Both
        }

        private static List<char> Letters = new List<char>() { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', ' ' };

        private static void MergeTwoCells(Worksheet worksheet, string cell1Name, string cell2Name)
        {
            if (worksheet == null || string.IsNullOrEmpty(cell1Name))
            {
                return;
            }

            MergeCells mergeCells;
            if (worksheet.Elements<MergeCells>().Count() > 0)
            {
                mergeCells = worksheet.Elements<MergeCells>().First();
            }
            else
            {
                mergeCells = new MergeCells();

                if (worksheet.Elements().Count() > 0)
                {
                    worksheet.InsertAfter(mergeCells, worksheet.Elements().First());
                }
                else if(worksheet.Elements().Count() > 0)
                {
                    worksheet.InsertAfter(mergeCells, worksheet.Elements().First());
                }
                else if (worksheet.Elements().Count() > 0)
                {
                    worksheet.InsertAfter(mergeCells, worksheet.Elements().First());
                }
                else if (worksheet.Elements().Count() > 0)
                {
                    worksheet.InsertAfter(mergeCells, worksheet.Elements().First());
                }
                else if (worksheet.Elements().Count() > 0)
                {
                    worksheet.InsertAfter(mergeCells, worksheet.Elements().First());
                }
                else if (worksheet.Elements().Count() > 0)
                {
                    worksheet.InsertAfter(mergeCells, worksheet.Elements().First());
                }
                else if (worksheet.Elements().Count() > 0)
                {
                    worksheet.InsertAfter(mergeCells, worksheet.Elements().First());
                }
                else if (worksheet.Elements().Count() > 0)
                {
                    worksheet.InsertAfter(mergeCells, worksheet.Elements().First());
                }
                else
                {
                    worksheet.InsertAfter(mergeCells, worksheet.Elements().First());
                }
            }

            MergeCell mergeCell = new MergeCell() { Reference = new StringValue(cell1Name + ":" + cell2Name) };
            mergeCells.Append(mergeCell);

            worksheet.Save();
        }

    }


}

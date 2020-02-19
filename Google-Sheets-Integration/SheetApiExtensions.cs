using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Google_Sheets_Integration
{
    public static class SheetApiExtensions
    {
        public static async Task<Spreadsheet> GetSpreadsheet(this SheetsService api, string id) => await api.Spreadsheets.Get(id).ExecuteAsync();
        public static async Task<Spreadsheet> GetSpreadsheet(this SheetsService api, SpreadsheetTools tools) => await api.GetSpreadsheet(tools.Id);
        public static async Task<Spreadsheet> GetSpreadsheet(this SpreadsheetTools tools) => await tools.Api.GetSpreadsheet(tools.Id);
        public static async Task<SpreadsheetTools> GetSpreadsheetTools(this SheetsService api, string id) => 
            new SpreadsheetTools(await api.GetSpreadsheet(id), api);
        public static async Task<SpreadsheetTools> Refresh(this SpreadsheetTools tools) => 
            new SpreadsheetTools(await tools.Api.GetSpreadsheet(tools.Id), tools.Api);
        public static async Task<SpreadsheetTools> Refresh(this SpreadsheetTools tools, SheetsService api) => await api.GetSpreadsheetTools(tools.Id);


        public static Sheet WithTitle(this IList<Sheet> sheets, string title) => sheets.FirstOrDefault(x => x.Properties.Title == title);


        public static async Task<BatchUpdateSpreadsheetResponse> DuplicateSheet(this SpreadsheetTools tools, string oldSheetName, string newSheetName) => await DuplicateSheet(tools.Api,tools, oldSheetName, newSheetName);
        public static async Task<BatchUpdateSpreadsheetResponse> DuplicateSheet(this SpreadsheetTools tools, Sheet oldSheet, string newSheetName) => await DuplicateSheet(tools.Api, tools, oldSheet, newSheetName);
        public static async Task<BatchUpdateSpreadsheetResponse> DuplicateSheet(this SheetsService api, SpreadsheetTools tools, string oldSheetName, string newSheetName) =>
            await api.Spreadsheets.BatchUpdate(new BatchUpdateSpreadsheetRequest
            {
                Requests = new List<Request> { new Request { DuplicateSheet = new DuplicateSheetRequest { SourceSheetId = tools.Sheets.WithTitle(oldSheetName).Properties.SheetId, NewSheetName = newSheetName } } }
            }, tools.Id).ExecuteAsync();

        public static async Task<BatchUpdateSpreadsheetResponse> DuplicateSheet(this SheetsService api, SpreadsheetTools tools, Sheet oldSheet, string newSheetName) =>
            await api.Spreadsheets.BatchUpdate(new BatchUpdateSpreadsheetRequest
            {
                Requests = new List<Request> { new Request { DuplicateSheet = new DuplicateSheetRequest { SourceSheetId = oldSheet.Properties.SheetId, NewSheetName = newSheetName } } }
            }, tools.Id).ExecuteAsync();

        public static async Task<BatchUpdateSpreadsheetResponse> DeleteSheet(this SpreadsheetTools tools, Sheet sheet) =>
            await DeleteSheet(tools.Api, tools, sheet);
        public static async Task<BatchUpdateSpreadsheetResponse> DeleteSheet(this SheetsService api, SpreadsheetTools tools, Sheet sheet) =>
            await api.Spreadsheets.BatchUpdate(new BatchUpdateSpreadsheetRequest
            {
                Requests = new List<Request> { new Request { DeleteSheet = new DeleteSheetRequest { SheetId = sheet.Properties.SheetId} } }
            }, tools.Id).ExecuteAsync();

        public static async Task<string> ReadCell(this SpreadsheetTools tools, string range) =>
            await ReadCell(tools.Api, tools, range);
        public static async Task<string> ReadCell(this SheetsService api, SpreadsheetTools tools, string range) =>
            (await api.Spreadsheets.Values.Get(tools.Id, range).ExecuteAsync()).Values[0][0].ToString();

        public static async Task<List<IList<object>>> ReadRange(this SpreadsheetTools tools, string range) =>
            await ReadRange(tools.Api, tools, range);
        public static async Task<List<IList<object>>> ReadRange(this SheetsService api, SpreadsheetTools tools, string range) =>
            (await api.Spreadsheets.Values.Get(tools.Id, range).ExecuteAsync()).Values.ToList();

        public static async Task<UpdateValuesResponse> EditCell(this SpreadsheetTools tools, string range, string data) =>
            await EditCell(tools.Api, tools, range, data);
        public static async Task<UpdateValuesResponse> EditCell(this SheetsService api, SpreadsheetTools tools, string range, string data)
        {
            var toSend = api.Spreadsheets.Values.Update((range, data).BuildSingleValueRange(), tools.Id, range);
            toSend.ValueInputOption = SpreadsheetsResource.ValuesResource.UpdateRequest.ValueInputOptionEnum.RAW;
            return await toSend.ExecuteAsync();
        }

        public static ValueRange BuildSingleValueRange(this (string range, string data) pair) => new ValueRange { Values = new List<IList<object>> { new List<object> { pair.data } }, Range = pair.range };
    }
}

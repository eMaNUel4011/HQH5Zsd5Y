// 代码生成时间: 2025-09-23 23:25:22
using System;
using System.IO;
using System.Windows;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;

namespace ExcelGeneratorApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 生成Excel表格的按钮点击事件处理函数。
        /// </summary>
        private void GenerateExcelButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // 获取用户输入的数据
                string data = TextBoxData.Text;
                if (string.IsNullOrEmpty(data))
                {
                    MessageBox.Show("请输入数据。");
                    return;
                }

                // 创建Excel文件
                string filePath = "GeneratedExcel.xlsx";
                using (SpreadsheetDocument document = SpreadsheetDocument.Create(filePath, SpreadsheetDocumentType.Workbook))
                {
                    // 添加工作簿和工作表
                    WorkbookPart workbookPart = document.AddWorkbookPart();
                    workbookPart Workbook = new Workbook();
                    workbookPart.PutWorkbook(Workbook);

                    SheetData sheetData = new SheetData();
                    Sheet sheet = new Sheet() { Id = document.WorkbookPart.GetIdOfPart(workbookPart.WorksheetParts.AddNewPart()) };
                    sheet.SheetId = 1;
                    sheet.Name = "Sheet1";

                    // 将数据添加到工作表
                    sheetData.Append(new Row() { Children = new[] { new Cell() { CellValue = new CellValue(data), DataType = new EnumValue<CellValues>(CellValues.String) } } });

                    // 保存更改
                    document.WorkbookPart.Workbook.AppendChild(sheet);
                    document.WorkbookPart.Workbook.Save();
                }

                MessageBox.Show("Excel文件已生成。");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"发生错误：{ex.Message}");
            }
        }
    }
}
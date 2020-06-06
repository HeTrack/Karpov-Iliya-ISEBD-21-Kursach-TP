using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
using MigraDoc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;
using UniversityBusinessLogic.Enums;

namespace UniversityBusinessLogic.BisinessLogic
{
    public class SaveToPdf
    {
        public static void CreateDoc(PdfInfo info)
        {
            Document document = new Document();
            DefineStyles(document);
            Section section = document.AddSection();
            Paragraph paragraph = section.AddParagraph(info.Title);
            paragraph.Format.Alignment = ParagraphAlignment.Center;
            paragraph.Style = "NormalTitle";
            foreach (var education in info.Educations)
            {
                var edLabel = section.AddParagraph("Курс №" + education.Id + " от " + education.EdCreate.ToShortDateString());
                edLabel.Style = "NormalTitle";
                edLabel.Format.SpaceBefore = "1cm";
                edLabel.Format.SpaceAfter = "0,25cm";
                var coursesLabel = section.AddParagraph("Курсы:");
                coursesLabel.Style = "NormalTitle";
                var courseTable = document.LastSection.AddTable();
                List<string> headerWidths = new List<string> { "1cm", "3cm", "2cm", "3cm", "3cm", "3cm", "2,5cm" };
                foreach (var elem in headerWidths)
                {
                    courseTable.AddColumn(elem);
                }
                CreateRow(new PdfRowParameters
                {
                    Table = courseTable,
                    Texts = new List<string> { "Курс №", "Название курса", "ФИО Преподавателя", "Дата начала курса", "Цена"},
                    Style = "NormalTitle",
                    ParagraphAlignment = ParagraphAlignment.Center
                });
                int i = 1;
                foreach (var course in education.EducationCourses)
                {
                    CreateRow(new PdfRowParameters
                    {
                        Table = courseTable,
                        Texts = new List<string> { i.ToString(), course.CourseName, course.LecturerFIO, course.StartCourse, course.Cost.ToString() },
                        Style = "Normal",
                        ParagraphAlignment = ParagraphAlignment.Left
                    });
                    i++;
                }

                CreateRow(new PdfRowParameters
                {
                    Table = courseTable,
                    Texts = new List<string> { "", "", "", "", "", "Итого:", education.FinalCost.ToString() },
                    Style = "Normal",
                    ParagraphAlignment = ParagraphAlignment.Left
                });
                if (education.Status == EducationStatus.Принят)
                {
                    CreateRow(new PdfRowParameters
                    {
                        Table = courseTable,
                        Texts = new List<string> { "", "", "", "", "", "К оплате:", education.FinalCost.ToString() },
                        Style = "Normal",
                        ParagraphAlignment = ParagraphAlignment.Left
                    });
                }
                else
                {
                    CreateRow(new PdfRowParameters
                    {
                        Table = courseTable,
                        Texts = new List<string> { "", "", "", "", "", "К оплате:", education.Remain.ToString() },
                        Style = "Normal",
                        ParagraphAlignment = ParagraphAlignment.Left
                    });
                }
                if (info.Pays[education.Id].Count == 0)
                {
                    continue;
                }
                var paysLabel = section.AddParagraph("Платежи:");
                paysLabel.Style = "NormalTitle";
                var payTable = document.LastSection.AddTable();
                headerWidths = new List<string> { "1cm", "3cm", "3cm", "3cm" };
                foreach (var elem in headerWidths)
                {
                    payTable.AddColumn(elem);
                }
                CreateRow(new PdfRowParameters
                {
                    Table = payTable,
                    Texts = new List<string> { "№", "Дата", "Сумма" },
                    Style = "NormalTitle",
                    ParagraphAlignment = ParagraphAlignment.Center
                });
                i = 1;
                foreach (var pay in info.Pays[education.Id])
                {
                    CreateRow(new PdfRowParameters
                    {
                        Table = payTable,
                        Texts = new List<string> { i.ToString(), pay.PayDate.ToString(), pay.Sum.ToString() },
                        Style = "Normal",
                        ParagraphAlignment = ParagraphAlignment.Left
                    });
                    i++;
                }
            }
            PdfDocumentRenderer renderer = new PdfDocumentRenderer(true)
            {
                Document = document
            };
            renderer.RenderDocument();
            renderer.PdfDocument.Save(info.FileName);
        }
        private static void DefineStyles(Document document)
        {
            Style style = document.Styles["Normal"];
            style.Font.Name = "Times New Roman";
            style.Font.Size = 14;
            style = document.Styles.AddStyle("NormalTitle", "Normal");
            style.Font.Bold = true;
        }
        private static void CreateRow(PdfRowParameters rowParameters)
        {
            Row row = rowParameters.Table.AddRow();
            for (int i = 0; i < rowParameters.Texts.Count; ++i)
            {
                FillCell(new PdfCellParameters
                {
                    Cell = row.Cells[i],
                    Text = rowParameters.Texts[i],
                    Style = rowParameters.Style,
                    BorderWidth = 0.5,
                    ParagraphAlignment = rowParameters.ParagraphAlignment
                });
            }
        }
        private static void FillCell(PdfCellParameters cellParameters)
        {
            cellParameters.Cell.AddParagraph(cellParameters.Text);
            if (!string.IsNullOrEmpty(cellParameters.Style))
            {
                cellParameters.Cell.Style = cellParameters.Style;
            }
            cellParameters.Cell.Borders.Left.Width = cellParameters.BorderWidth;
            cellParameters.Cell.Borders.Right.Width = cellParameters.BorderWidth;
            cellParameters.Cell.Borders.Top.Width = cellParameters.BorderWidth;
            cellParameters.Cell.Borders.Bottom.Width = cellParameters.BorderWidth;
            cellParameters.Cell.Format.Alignment = cellParameters.ParagraphAlignment;
            cellParameters.Cell.VerticalAlignment = VerticalAlignment.Center;
        }
    }
}

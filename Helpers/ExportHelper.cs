using System.Drawing.Printing;
using System.Text;
using LibraryManagementSystem.Helpers;

namespace LibraryManagementSystem.Helpers
{
    /// <summary>
    /// Provides standardized Excel (CSV), PDF/HTML, and Print capabilities
    /// for DataGridView reports, respecting all active filters.
    /// </summary>
    public static class ExportHelper
    {
        /// <summary>
        /// Exports the DataGridView contents to a UTF-8 CSV spreadsheet compatible with Microsoft Excel.
        /// </summary>
        /// <summary>
        /// Saves the DataGridView contents to a UTF-8 CSV spreadsheet at the specified file path.
        /// </summary>
        public static void SaveExcelFile(DataGridView dgv, string filePath, string reportTitle, string filterSummary)
        {
            var sb = new StringBuilder();

            // Report Title and Metadata Header
            sb.AppendLine($"\"{reportTitle}\"");
            sb.AppendLine($"\"Generated on: {DateTime.Now:dd/MM/yyyy HH:mm:ss}\"");
            sb.AppendLine($"\"Filters: {filterSummary}\"");
            sb.AppendLine($"\"Total Records: {dgv.Rows.Count}\"");
            sb.AppendLine();

            // Column Headers
            var visibleCols = dgv.Columns.Cast<DataGridViewColumn>()
                .Where(c => c.Visible)
                .OrderBy(c => c.DisplayIndex)
                .ToList();

            sb.AppendLine(string.Join(",", visibleCols.Select(c => EscapeCsv(c.HeaderText))));

            // Data Rows
            foreach (DataGridViewRow row in dgv.Rows)
            {
                if (row.IsNewRow) continue;
                var fields = visibleCols.Select(c =>
                {
                    var val = row.Cells[c.Index].Value;
                    string text = val switch
                    {
                        null => "",
                        DateTime dt => dt.ToString("dd/MM/yyyy"),
                        decimal dec => dec.ToString("N0"),
                        _ => val.ToString() ?? ""
                    };
                    return EscapeCsv(text);
                });
                sb.AppendLine(string.Join(",", fields));
            }

            // Write with UTF-8 BOM so Excel opens Khmer & Unicode characters without corruption
            File.WriteAllText(filePath, sb.ToString(), new UTF8Encoding(true));
        }

        /// <summary>
        /// Saves the DataGridView contents to a genuine PDF document with professional headers, table grid, and pagination.
        /// </summary>
        public static void SavePdfFile(DataGridView dgv, string filePath, string reportTitle, string filterSummary)
        {
            var visibleCols = dgv.Columns.Cast<DataGridViewColumn>()
                .Where(c => c.Visible)
                .OrderBy(c => c.DisplayIndex)
                .ToList();

            if (visibleCols.Count == 0) return;

            using var doc = new PdfSharp.Pdf.PdfDocument();
            doc.Info.Title = reportTitle;
            doc.Info.Author = "Library Management System";

            // Typography & Styling
            var titleFont = new PdfSharp.Drawing.XFont("Arial", 15, PdfSharp.Drawing.XFontStyleEx.Bold);
            var subFont = new PdfSharp.Drawing.XFont("Arial", 8.5, PdfSharp.Drawing.XFontStyleEx.Regular);
            var headerFont = new PdfSharp.Drawing.XFont("Arial", 8.5, PdfSharp.Drawing.XFontStyleEx.Bold);
            var cellFont = new PdfSharp.Drawing.XFont("Arial", 8, PdfSharp.Drawing.XFontStyleEx.Regular);
            var footerFont = new PdfSharp.Drawing.XFont("Arial", 7.5, PdfSharp.Drawing.XFontStyleEx.Italic);

            var headerBgBrush = new PdfSharp.Drawing.XSolidBrush(PdfSharp.Drawing.XColor.FromArgb(70, 130, 180)); // Steel Blue
            var altRowBgBrush = new PdfSharp.Drawing.XSolidBrush(PdfSharp.Drawing.XColor.FromArgb(248, 250, 252));
            var borderPen = new PdfSharp.Drawing.XPen(PdfSharp.Drawing.XColor.FromArgb(226, 232, 240), 0.5);
            var textBrush = PdfSharp.Drawing.XBrushes.Black;
            var headerTextBrush = PdfSharp.Drawing.XBrushes.White;
            var mutedBrush = new PdfSharp.Drawing.XSolidBrush(PdfSharp.Drawing.XColor.FromArgb(100, 116, 139));

            // Orientation: landscape if > 5 columns for optimal legibility
            var orientation = visibleCols.Count > 5
                ? PdfSharp.PageOrientation.Landscape
                : PdfSharp.PageOrientation.Portrait;

            double leftMargin = 30;
            double topMargin = 30;
            double bottomMargin = 30;
            double rowHeight = 18;
            double headerHeight = 22;

            double totalGridWidth = visibleCols.Sum(c => Math.Max(c.Width, 40));

            PdfSharp.Pdf.PdfPage CreatePage(out PdfSharp.Drawing.XGraphics g, out double width, out double startY)
            {
                var page = doc.AddPage();
                page.Orientation = orientation;
                page.Size = PdfSharp.PageSize.A4;
                g = PdfSharp.Drawing.XGraphics.FromPdfPage(page);
                width = page.Width.Point - (leftMargin * 2);

                // Header
                g.DrawString(reportTitle, titleFont, new PdfSharp.Drawing.XSolidBrush(PdfSharp.Drawing.XColor.FromArgb(13, 59, 102)), new PdfSharp.Drawing.XPoint(leftMargin, topMargin + 14));
                g.DrawString($"Generated on: {DateTime.Now:dd/MM/yyyy HH:mm:ss}   |   Filters: {filterSummary}   |   Total Records: {dgv.Rows.Count}",
                    subFont, mutedBrush, new PdfSharp.Drawing.XPoint(leftMargin, topMargin + 28));

                startY = topMargin + 42;
                return page;
            }

            var currentPage = CreatePage(out var gfx, out var usableWidth, out var currentY);
            var colWidths = visibleCols.Select(c => (Math.Max(c.Width, 40) / totalGridWidth) * usableWidth).ToArray();

            void DrawTableHeader(PdfSharp.Drawing.XGraphics g, double y)
            {
                double x = leftMargin;
                for (int i = 0; i < visibleCols.Count; i++)
                {
                    var rect = new PdfSharp.Drawing.XRect(x, y, colWidths[i], headerHeight);
                    g.DrawRectangle(headerBgBrush, rect);
                    g.DrawRectangle(borderPen, rect);

                    var textRect = new PdfSharp.Drawing.XRect(x + 4, y + 4, colWidths[i] - 8, headerHeight - 6);
                    g.DrawString(visibleCols[i].HeaderText, headerFont, headerTextBrush, textRect, PdfSharp.Drawing.XStringFormats.TopLeft);
                    x += colWidths[i];
                }
            }

            DrawTableHeader(gfx, currentY);
            currentY += headerHeight;

            int rowIndex = 0;
            foreach (DataGridViewRow row in dgv.Rows)
            {
                if (row.IsNewRow) continue;

                if (currentY + rowHeight > currentPage.Height.Point - bottomMargin - 15)
                {
                    gfx.DrawString($"Page {doc.PageCount}   |   Library Management System", footerFont, mutedBrush,
                        new PdfSharp.Drawing.XPoint(leftMargin, currentPage.Height.Point - 15));
                    gfx.Dispose();

                    currentPage = CreatePage(out gfx, out usableWidth, out currentY);
                    DrawTableHeader(gfx, currentY);
                    currentY += headerHeight;
                }

                double x = leftMargin;
                bool isAlt = rowIndex % 2 == 1;

                for (int i = 0; i < visibleCols.Count; i++)
                {
                    var rect = new PdfSharp.Drawing.XRect(x, currentY, colWidths[i], rowHeight);
                    if (isAlt) gfx.DrawRectangle(altRowBgBrush, rect);
                    gfx.DrawRectangle(borderPen, rect);

                    var val = row.Cells[visibleCols[i].Index].Value;
                    string text = val switch
                    {
                        null => "",
                        DateTime dt => dt.ToString("dd/MM/yyyy"),
                        decimal dec => dec.ToString("N0"),
                        _ => val.ToString() ?? ""
                    };

                    var textRect = new PdfSharp.Drawing.XRect(x + 4, currentY + 3, colWidths[i] - 8, rowHeight - 6);
                    gfx.DrawString(text, cellFont, textBrush, textRect, PdfSharp.Drawing.XStringFormats.TopLeft);
                    x += colWidths[i];
                }

                currentY += rowHeight;
                rowIndex++;
            }

            gfx.DrawString($"Page {doc.PageCount}   |   Library Management System", footerFont, mutedBrush,
                new PdfSharp.Drawing.XPoint(leftMargin, currentPage.Height.Point - 15));
            gfx.Dispose();

            doc.Save(filePath);
        }

        /// <summary>
        /// Saves the DataGridView contents to a styled HTML report at the specified file path.
        /// </summary>
        public static void SaveHtmlReportFile(DataGridView dgv, string filePath, string reportTitle, string filterSummary)
        {
            var visibleCols = dgv.Columns.Cast<DataGridViewColumn>()
                .Where(c => c.Visible)
                .OrderBy(c => c.DisplayIndex)
                .ToList();

            var sb = new StringBuilder();
            sb.AppendLine("<!DOCTYPE html>");
            sb.AppendLine("<html><head><meta charset='utf-8'>");
            sb.AppendLine($"<title>{reportTitle}</title>");
            sb.AppendLine("<style>");
            sb.AppendLine("body { font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif; margin: 30px; color: #1e293b; }");
            sb.AppendLine("h1 { color: #0d3b66; margin-bottom: 4px; font-size: 24px; }");
            sb.AppendLine(".meta { color: #64748b; font-size: 13px; margin-bottom: 20px; line-height: 1.6; }");
            sb.AppendLine("table { width: 100%; border-collapse: collapse; margin-top: 15px; font-size: 13px; }");
            sb.AppendLine("th { background-color: #4682b4; color: white; text-align: left; padding: 10px 12px; font-weight: 600; }");
            sb.AppendLine("td { padding: 9px 12px; border-bottom: 1px solid #e2e8f0; }");
            sb.AppendLine("tr:nth-child(even) { background-color: #f8fafc; }");
            sb.AppendLine(".footer { margin-top: 25px; font-size: 12px; color: #94a3b8; text-align: right; }");
            sb.AppendLine("@media print { body { margin: 10mm; } th { -webkit-print-color-adjust: exact; print-color-adjust: exact; } }");
            sb.AppendLine("</style></head><body>");

            sb.AppendLine($"<h1>{System.Net.WebUtility.HtmlEncode(reportTitle)}</h1>");
            sb.AppendLine("<div class='meta'>");
            sb.AppendLine($"<div><strong>Generated on:</strong> {DateTime.Now:dddd, MMMM dd, yyyy HH:mm:ss}</div>");
            sb.AppendLine($"<div><strong>Active Filters:</strong> {System.Net.WebUtility.HtmlEncode(filterSummary)}</div>");
            sb.AppendLine($"<div><strong>Total Records:</strong> {dgv.Rows.Count}</div>");
            sb.AppendLine("</div>");

            sb.AppendLine("<table><thead><tr>");
            foreach (var col in visibleCols)
            {
                sb.AppendLine($"<th>{System.Net.WebUtility.HtmlEncode(col.HeaderText)}</th>");
            }
            sb.AppendLine("</tr></thead><tbody>");

            foreach (DataGridViewRow row in dgv.Rows)
            {
                if (row.IsNewRow) continue;
                sb.AppendLine("<tr>");
                foreach (var col in visibleCols)
                {
                    var val = row.Cells[col.Index].Value;
                    string text = val switch
                    {
                        null => "",
                        DateTime dt => dt.ToString("dd/MM/yyyy"),
                        decimal dec => dec.ToString("N0"),
                        _ => val.ToString() ?? ""
                    };
                    sb.AppendLine($"<td>{System.Net.WebUtility.HtmlEncode(text)}</td>");
                }
                sb.AppendLine("</tr>");
            }

            sb.AppendLine("</tbody></table>");
            sb.AppendLine($"<div class='footer'>Report generated by Library Management System — {DateTime.Now:yyyy}</div>");
            sb.AppendLine("</body></html>");

            File.WriteAllText(filePath, sb.ToString(), Encoding.UTF8);
        }

        /// <summary>
        /// Exports the DataGridView contents to a UTF-8 CSV spreadsheet compatible with Microsoft Excel.
        /// </summary>
        public static void ExportToExcel(DataGridView dgv, string reportTitle, string filterSummary)
        {
            if (dgv.Rows.Count == 0)
            {
                MessageBox.Show("There are no records to export.", "Empty Report", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using var sfd = new SaveFileDialog
            {
                Filter = "Excel CSV Spreadsheet (*.csv)|*.csv",
                DefaultExt = "csv",
                FileName = $"{SanitizeFileName(reportTitle)}_{DateTime.Now:yyyyMMdd_HHmmss}.csv",
                Title = "Export Report to Excel"
            };

            if (sfd.ShowDialog() != DialogResult.OK) return;

            try
            {
                SaveExcelFile(dgv, sfd.FileName, reportTitle, filterSummary);

                var res = MessageBox.Show(
                    $"Report successfully exported to Excel!\n\nFile saved at:\n{sfd.FileName}\n\nWould you like to open it now?",
                    "Export Successful",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Information);

                if (res == DialogResult.Yes)
                {
                    System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                    {
                        FileName = sfd.FileName,
                        UseShellExecute = true
                    });
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[ExportHelper.ExportToExcel] {ex}");
                MessageBox.Show("Failed to export Excel report. Please verify file permissions and try again.", "Export Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Exports the DataGridView contents to a genuine PDF document (*.pdf).
        /// </summary>
        public static void ExportToPdf(DataGridView dgv, string reportTitle, string filterSummary)
        {
            if (dgv.Rows.Count == 0)
            {
                MessageBox.Show("There are no records to export.", "Empty Report", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using var sfd = new SaveFileDialog
            {
                Filter = "PDF Document (*.pdf)|*.pdf|Printable Web Report (*.html)|*.html",
                DefaultExt = "pdf",
                FileName = $"{SanitizeFileName(reportTitle)}_{DateTime.Now:yyyyMMdd_HHmmss}.pdf",
                Title = "Export Report to PDF"
            };

            if (sfd.ShowDialog() != DialogResult.OK) return;

            try
            {
                if (sfd.FileName.EndsWith(".html", StringComparison.OrdinalIgnoreCase))
                {
                    SaveHtmlReportFile(dgv, sfd.FileName, reportTitle, filterSummary);
                }
                else
                {
                    SavePdfFile(dgv, sfd.FileName, reportTitle, filterSummary);
                }

                var res = MessageBox.Show(
                    $"Report successfully exported to PDF!\n\nFile saved at:\n{sfd.FileName}\n\nWould you like to open it now?",
                    "Export Successful",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Information);

                if (res == DialogResult.Yes)
                {
                    System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                    {
                        FileName = sfd.FileName,
                        UseShellExecute = true
                    });
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[ExportHelper.ExportToPdf] {ex}");
                MessageBox.Show("Failed to export PDF report. Please verify file permissions and try again.", "Export Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Prints the DataGridView report directly using native Windows Forms PrintDocument and PrintPreviewDialog.
        /// </summary>
        public static void PrintReport(DataGridView dgv, string reportTitle, string filterSummary)
        {
            if (dgv.Rows.Count == 0)
            {
                MessageBox.Show("There are no records to print.", "Empty Report", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                var printDoc = new PrintDocument();
                printDoc.DefaultPageSettings.Landscape = true;
                printDoc.DefaultPageSettings.Margins = new Margins(40, 40, 40, 40);

                int currentRowIndex = 0;
                int pageNumber = 1;

                var visibleCols = dgv.Columns.Cast<DataGridViewColumn>()
                    .Where(c => c.Visible)
                    .OrderBy(c => c.DisplayIndex)
                    .ToList();

                printDoc.PrintPage += (s, e) =>
                {
                    var g = e.Graphics!;
                    var bounds = e.MarginBounds;
                    int x = bounds.Left;
                    int y = bounds.Top;

                    using var titleFont = new Font("Segoe UI", 14, FontStyle.Bold);
                    using var metaFont = new Font("Segoe UI", 8.5f, FontStyle.Regular);
                    using var headerFont = new Font("Segoe UI", 8.5f, FontStyle.Bold);
                    using var cellFont = new Font("Segoe UI", 8f, FontStyle.Regular);

                    using var navyBrush = new SolidBrush(Color.FromArgb(13, 59, 102));
                    using var darkBrush = new SolidBrush(Color.FromArgb(30, 41, 59));
                    using var grayBrush = new SolidBrush(Color.FromArgb(100, 116, 139));
                    using var headerBgBrush = new SolidBrush(Color.FromArgb(70, 130, 180));
                    using var altRowBrush = new SolidBrush(Color.FromArgb(248, 250, 252));
                    using var borderPen = new Pen(Color.FromArgb(226, 232, 240));

                    // Header on page 1
                    if (pageNumber == 1)
                    {
                        g.DrawString(reportTitle, titleFont, navyBrush, x, y);
                        y += 26;
                        g.DrawString($"Filters: {filterSummary}  |  Generated: {DateTime.Now:dd/MM/yyyy HH:mm}  |  Records: {dgv.Rows.Count}", metaFont, grayBrush, x, y);
                        y += 20;
                    }
                    else
                    {
                        g.DrawString($"{reportTitle} (Continued) — Page {pageNumber}", metaFont, grayBrush, x, y);
                        y += 18;
                    }

                    // Column Width Calculation
                    int availableWidth = bounds.Width;
                    int totalWeight = visibleCols.Sum(c => Math.Max(60, c.Width));
                    var colWidths = visibleCols.Select(c => (int)Math.Round((double)Math.Max(60, c.Width) / totalWeight * availableWidth)).ToArray();

                    // Table Header Row
                    int headerHeight = 26;
                    g.FillRectangle(headerBgBrush, x, y, bounds.Width, headerHeight);
                    int curX = x;
                    for (int i = 0; i < visibleCols.Count; i++)
                    {
                        var rect = new RectangleF(curX + 4, y + 5, colWidths[i] - 8, headerHeight - 6);
                        g.DrawString(visibleCols[i].HeaderText, headerFont, Brushes.White, rect);
                        curX += colWidths[i];
                    }
                    y += headerHeight;

                    // Table Data Rows
                    int rowHeight = 22;
                    while (currentRowIndex < dgv.Rows.Count)
                    {
                        var row = dgv.Rows[currentRowIndex];
                        if (y + rowHeight > bounds.Bottom - 25)
                        {
                            // Page break
                            e.HasMorePages = true;
                            pageNumber++;
                            return;
                        }

                        if (currentRowIndex % 2 == 1)
                        {
                            g.FillRectangle(altRowBrush, x, y, bounds.Width, rowHeight);
                        }
                        g.DrawLine(borderPen, x, y + rowHeight, bounds.Right, y + rowHeight);

                        curX = x;
                        for (int i = 0; i < visibleCols.Count; i++)
                        {
                            var cellVal = row.Cells[visibleCols[i].Index].Value;
                            string text = cellVal switch
                            {
                                null => "",
                                DateTime dt => dt.ToString("dd/MM/yyyy"),
                                decimal dec => dec.ToString("N0"),
                                _ => cellVal.ToString() ?? ""
                            };
                            var rect = new RectangleF(curX + 4, y + 4, colWidths[i] - 8, rowHeight - 6);
                            g.DrawString(text, cellFont, darkBrush, rect);
                            curX += colWidths[i];
                        }

                        y += rowHeight;
                        currentRowIndex++;
                    }

                    // Footer
                    g.DrawString($"Page {pageNumber} — Library Management System", metaFont, grayBrush, x, bounds.Bottom - 15);
                    e.HasMorePages = false;
                };

                using var preview = new PrintPreviewDialog
                {
                    Document = printDoc,
                    Width = 1000,
                    Height = 700,
                    StartPosition = FormStartPosition.CenterParent
                };
                preview.ShowDialog();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[ExportHelper.PrintReport] {ex}");
                MessageBox.Show("Unable to display print preview. Please verify printer configuration and try again.", "Print Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static string EscapeCsv(string text)
        {
            if (string.IsNullOrEmpty(text)) return "\"\"";
            return $"\"{text.Replace("\"", "\"\"")}\"";
        }

        private static string SanitizeFileName(string name)
        {
            var invalid = Path.GetInvalidFileNameChars();
            var clean = new string(name.Where(c => !invalid.Contains(c) && c != ' ').ToArray());
            return string.IsNullOrWhiteSpace(clean) ? "Report" : clean;
        }
    }
}

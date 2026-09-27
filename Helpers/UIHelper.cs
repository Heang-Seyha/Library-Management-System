using System.Drawing.Drawing2D;

namespace LibraryManagementSystem.Helpers
{
    /// <summary>
    /// UI helper methods used across all forms and panels.
    /// Provides consistent styling for DataGridViews, buttons, toolbars, empty states, and typography
    /// based on the Professional Blue color palette.
    /// </summary>
    public static class UIHelper
    {
        // ── Color Palette — Soft Light Blue (Matches Exit Button #DCEBFC) ─────
        public static readonly Color PrimaryAccent     = Color.FromArgb(220, 235, 252); // #DCEBFC Soft light blue matching exit button
        public static readonly Color PrimaryAccentDark = Color.FromArgb(195, 220, 250); // #C3DCFA Hover / active highlight tint
        public static readonly Color AccentHover       = Color.FromArgb(195, 220, 250); // Alias
        public static readonly Color MainBackground    = Color.FromArgb(235, 243, 250); // #EBF3FA Light sky blue background
        public static readonly Color CardBackground    = Color.FromArgb(255, 255, 255); // #FFFFFF Card / table surface
        public static readonly Color SecondaryAccent   = Color.FromArgb(220, 235, 252); // #DCEBFC Light blue tint for secondary buttons
        public static readonly Color BorderColor       = Color.FromArgb(208, 225, 253); // #D0E1FD Borders & dividers
        public static readonly Color TextDark          = Color.FromArgb(13, 59, 102);   // #0D3B66 Primary text
        public static readonly Color TextMedium        = Color.FromArgb(100, 116, 139); // #64748B Muted text / labels
        public static readonly Color TextLight         = Color.FromArgb(148, 163, 184); // #94A3B8 Secondary muted text
        public static readonly Color SidebarText       = Color.FromArgb(13, 59, 102);   // #0D3B66 Deep navy for light blue sidebar
        public static readonly Color GridHeaderBg      = Color.FromArgb(220, 235, 252); // Soft light blue table header
        public static readonly Color GridAltRow        = Color.FromArgb(247, 250, 253); // #F7FAFD Soft alternating row tint
        public static readonly Color DangerRed         = Color.FromArgb(220, 38, 38);   // Semantic red
        public static readonly Color SuccessGreen      = Color.FromArgb(22, 101, 52);   // Semantic forest green
        public static readonly Color WarningAmber      = Color.FromArgb(217, 119, 6);   // Semantic dark amber
        public static readonly Color BrandNavy         = Color.FromArgb(13, 71, 161);   // High-contrast navy for cards/accents

        // Backward compatibility aliases
        public static Color PrimaryBlue  => BrandNavy;
        public static Color PrimaryGreen => SuccessGreen;
        public static Color LightGray    => MainBackground;
        public static Color BorderGray   => BorderColor;

        // ── Preferred Icon Font Determination ────────────────────────────────
        public static readonly string IconFontFamily = GetPreferredIconFont();

        private static string GetPreferredIconFont()
        {
            try
            {
                using var testFont = new Font("Segoe Fluent Icons", 12f);
                if (testFont.Name.Equals("Segoe Fluent Icons", StringComparison.OrdinalIgnoreCase))
                    return "Segoe Fluent Icons";
            }
            catch { }
            try
            {
                using var testFont2 = new Font("Segoe MDL2 Assets", 12f);
                if (testFont2.Name.Equals("Segoe MDL2 Assets", StringComparison.OrdinalIgnoreCase))
                    return "Segoe MDL2 Assets";
            }
            catch { }
            return "Segoe MDL2 Assets";
        }

        /// <summary>
        /// Renders an icon glyph from Segoe Fluent Icons / Segoe MDL2 Assets onto a crisp Bitmap.
        /// </summary>
        public static Bitmap CreateIconBitmap(string glyph, int size, Color color)
        {
            var bmp = new Bitmap(size, size, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            using (var g = Graphics.FromImage(bmp))
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
                g.Clear(Color.Transparent);

                try
                {
                    float fontSize = size * 0.72f;
                    using var font = new Font(IconFontFamily, fontSize, FontStyle.Regular, GraphicsUnit.Pixel);
                    using var brush = new SolidBrush(color);
                    var sf = new StringFormat
                    {
                        Alignment = StringAlignment.Center,
                        LineAlignment = StringAlignment.Center
                    };
                    g.DrawString(glyph, font, brush, new RectangleF(0, 0, size, size), sf);
                }
                catch
                {
                    using var pen = new Pen(color, 2);
                    g.DrawEllipse(pen, 2, 2, size - 4, size - 4);
                }
            }
            return bmp;
        }

        // ── Typography ────────────────────────────────────────────────────────
        public static readonly Font FontTitle       = new Font("Segoe UI Semibold", 16f);
        public static readonly Font FontSubtitle    = new Font("Segoe UI", 9.5f);
        public static readonly Font FontSection     = new Font("Segoe UI Semibold", 12f);
        public static readonly Font FontLabelBold   = new Font("Segoe UI", 9.5f, FontStyle.Bold);
        public static readonly Font FontBody        = new Font("Segoe UI", 9.5f, FontStyle.Regular);
        public static readonly Font FontIconLarge   = new Font(IconFontFamily, 18f, FontStyle.Regular);
        public static readonly Font FontIconMedium  = new Font(IconFontFamily, 13f, FontStyle.Regular);
        public static readonly Font FontIconSmall   = new Font(IconFontFamily, 10.5f, FontStyle.Regular);

        // ── Standardized Outline Icons (Fluent / MDL2 Standard Glyphs) ────────
        public static class Icons
        {
            public const string Dashboard  = "\uE80F"; // Home outline
            public const string Books      = "\uE736"; // Library outline
            public const string Members    = "\uE716"; // People outline
            public const string Librarian  = "\uE77B"; // Contact outline
            public const string Borrow     = "\uE898"; // Send outline
            public const string ReturnBook = "\uE7A7"; // Return outline
            public const string Reports    = "\uE9D9"; // Document/report outline
            public const string Categories = "\uE8EC"; // Tag outline
            public const string Authors    = "\uE70F"; // Pen/edit outline
            public const string Publishers = "\uE8D4"; // Building outline
            public const string Logout     = "\uE7E8"; // Sign out outline
            public const string Add        = "\uE710"; // Plus outline
            public const string Edit       = "\uE70F"; // Edit pencil outline
            public const string Delete     = "\uE74D"; // Trash outline
            public const string Search     = "\uE721"; // Search outline
            public const string Refresh    = "\uE72C"; // Refresh outline
            public const string Save       = "\uE74E"; // Save outline
            public const string Cancel     = "\uE711"; // Cancel X outline
            public const string Warning    = "\uE7BA"; // Warning outline
            public const string Success    = "\uE73E"; // Checkmark outline
        }

        // ── Standard 8px Spacing & Sizing Scale ─────────────────────────────
        public const int SpacingXs            = 4;   // 4px micro gap
        public const int SpacingSm            = 8;   // 8px base unit / tight gap
        public const int SpacingMd            = 16;  // 16px standard padding
        public const int SpacingLg            = 24;  // 24px section spacing
        public const int SpacingXl            = 32;  // 32px large container spacing
        public const int DefaultButtonHeight  = 32;  // Standard button height (32px)
        public const int DefaultInputHeight   = 30;  // Standard single-line input height
        public const int DefaultHeaderHeight  = 52;  // Form header height
        public const int DefaultToolbarHeight = 48;  // Panel toolbar height
        public const int DefaultGridHdrHeight = 36;  // DataGridView header height
        public const int DefaultGridRowHeight = 32;  // DataGridView row height

        /// <summary>
        /// Applies a clean, professional modern style to a DataGridView with compact 8px padding.
        /// Modern web-like appearance: borderless container, subtle divider lines, soft blue header.
        /// </summary>
        public static void StyleDataGridView(DataGridView dgv)
        {
            dgv.BackgroundColor = Color.White;
            dgv.BorderStyle = BorderStyle.None;
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgv.GridColor = Color.FromArgb(226, 232, 240); // #E2E8F0 subtle modern divider
            dgv.RowHeadersVisible = false;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.ReadOnly = true;
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;
            dgv.MultiSelect = false;

            // Compact header style (36px height, 8px/4px padding)
            dgv.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = GridHeaderBg,
                ForeColor = TextDark,
                Font = new Font("Segoe UI", 9.5f, FontStyle.Bold),
                Padding = new Padding(8, 4, 8, 4),
                SelectionBackColor = GridHeaderBg,
                SelectionForeColor = TextDark
            };
            dgv.ColumnHeadersHeight = DefaultGridHdrHeight;
            dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

            // Compact row style (32px height, 8px/4px padding)
            dgv.DefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.White,
                ForeColor = TextDark,
                Font = new Font("Segoe UI", 9.5f),
                Padding = new Padding(8, 4, 8, 4),
                SelectionBackColor = Color.FromArgb(219, 234, 254), // #DBEAFE
                SelectionForeColor = TextDark,
                WrapMode = DataGridViewTriState.False
            };

            // Alternating row color
            dgv.AlternatingRowsDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = GridAltRow,
                ForeColor = TextDark,
                SelectionBackColor = Color.FromArgb(205, 225, 250),
                SelectionForeColor = TextDark,
                WrapMode = DataGridViewTriState.False
            };

            dgv.RowTemplate.Height = DefaultGridRowHeight;
            dgv.EnableHeadersVisualStyles = false;
        }

        /// <summary>Applies primary button styling (Brand Navy, high contrast white text).</summary>
        public static void ApplyPrimaryButtonStyle(Button btn, string? iconGlyph = null, bool includeIcon = false)
        {
            btn.BackColor = BrandNavy;
            btn.ForeColor = Color.White;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(21, 101, 192); // #1565C0
            btn.FlatAppearance.MouseDownBackColor = Color.FromArgb(13, 71, 161);
            btn.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
            btn.Cursor = Cursors.Hand;
            btn.Height = Math.Max(btn.Height, DefaultButtonHeight);

            if (includeIcon && !string.IsNullOrEmpty(iconGlyph))
            {
                btn.Image = CreateIconBitmap(iconGlyph, 16, Color.White);
                btn.ImageAlign = ContentAlignment.MiddleLeft;
                btn.TextAlign = ContentAlignment.MiddleLeft;
                btn.TextImageRelation = TextImageRelation.ImageBeforeText;
                btn.Padding = new Padding(8, 0, 0, 0);
            }
            else
            {
                btn.Image = null;
                btn.Padding = Padding.Empty;
                btn.TextAlign = ContentAlignment.MiddleCenter;
                btn.TextImageRelation = TextImageRelation.Overlay;
            }

            EnsureSafeButtonWidth(btn, includeIcon && !string.IsNullOrEmpty(iconGlyph));
        }

        /// <summary>Applies secondary button styling (Soft Blue background, dark navy text, subtle border).</summary>
        public static void ApplySecondaryButtonStyle(Button btn, string? iconGlyph = null, bool includeIcon = false)
        {
            btn.BackColor = PrimaryAccent;
            btn.ForeColor = TextDark;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 1;
            btn.FlatAppearance.BorderColor = BorderColor;
            btn.FlatAppearance.MouseOverBackColor = PrimaryAccentDark;
            btn.FlatAppearance.MouseDownBackColor = Color.FromArgb(185, 212, 246);
            btn.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
            btn.Cursor = Cursors.Hand;
            btn.Height = Math.Max(btn.Height, DefaultButtonHeight);

            if (includeIcon && !string.IsNullOrEmpty(iconGlyph))
            {
                btn.Image = CreateIconBitmap(iconGlyph, 16, TextDark);
                btn.ImageAlign = ContentAlignment.MiddleLeft;
                btn.TextAlign = ContentAlignment.MiddleLeft;
                btn.TextImageRelation = TextImageRelation.ImageBeforeText;
                btn.Padding = new Padding(8, 0, 0, 0);
            }
            else
            {
                btn.Image = null;
                btn.Padding = Padding.Empty;
                btn.TextAlign = ContentAlignment.MiddleCenter;
                btn.TextImageRelation = TextImageRelation.Overlay;
            }

            EnsureSafeButtonWidth(btn, includeIcon && !string.IsNullOrEmpty(iconGlyph));
        }

        /// <summary>Applies danger button styling (Semantic red, white text).</summary>
        public static void ApplyDangerButtonStyle(Button btn, string? iconGlyph = null, bool includeIcon = false)
        {
            btn.BackColor = DangerRed;
            btn.ForeColor = Color.White;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(185, 28, 28);
            btn.FlatAppearance.MouseDownBackColor = Color.FromArgb(153, 27, 27);
            btn.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
            btn.Cursor = Cursors.Hand;
            btn.Height = Math.Max(btn.Height, DefaultButtonHeight);

            if (includeIcon && !string.IsNullOrEmpty(iconGlyph))
            {
                btn.Image = CreateIconBitmap(iconGlyph, 16, Color.White);
                btn.ImageAlign = ContentAlignment.MiddleLeft;
                btn.TextAlign = ContentAlignment.MiddleLeft;
                btn.TextImageRelation = TextImageRelation.ImageBeforeText;
                btn.Padding = new Padding(8, 0, 0, 0);
            }
            else
            {
                btn.Image = null;
                btn.Padding = Padding.Empty;
                btn.TextAlign = ContentAlignment.MiddleCenter;
                btn.TextImageRelation = TextImageRelation.Overlay;
            }

            EnsureSafeButtonWidth(btn, includeIcon && !string.IsNullOrEmpty(iconGlyph));
        }

        /// <summary>Applies success button styling (Forest green, white text).</summary>
        public static void ApplySuccessButtonStyle(Button btn, string? iconGlyph = null, bool includeIcon = false)
        {
            btn.BackColor = SuccessGreen;
            btn.ForeColor = Color.White;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(21, 128, 61);
            btn.FlatAppearance.MouseDownBackColor = Color.FromArgb(20, 83, 45);
            btn.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
            btn.Cursor = Cursors.Hand;
            btn.Height = Math.Max(btn.Height, DefaultButtonHeight);

            if (includeIcon && !string.IsNullOrEmpty(iconGlyph))
            {
                btn.Image = CreateIconBitmap(iconGlyph, 16, Color.White);
                btn.ImageAlign = ContentAlignment.MiddleLeft;
                btn.TextAlign = ContentAlignment.MiddleLeft;
                btn.TextImageRelation = TextImageRelation.ImageBeforeText;
                btn.Padding = new Padding(8, 0, 0, 0);
            }
            else
            {
                btn.Image = null;
                btn.Padding = Padding.Empty;
                btn.TextAlign = ContentAlignment.MiddleCenter;
                btn.TextImageRelation = TextImageRelation.Overlay;
            }

            EnsureSafeButtonWidth(btn, includeIcon && !string.IsNullOrEmpty(iconGlyph));
        }

        /// <summary>
        /// Ensures button has adequate width so text is never truncated.
        /// Minimum width is 95px, or measured text width + generous horizontal padding.
        /// </summary>
        public static void EnsureSafeButtonWidth(Button btn, bool hasIcon = false)
        {
            if (string.IsNullOrEmpty(btn.Text)) return;

            int padding = hasIcon ? 36 : 24;
            int measured = TextRenderer.MeasureText(btn.Text, btn.Font).Width + padding;
            int minW = Math.Max(95, measured);
            if (btn.Width < minW)
            {
                btn.Width = minW;
            }
        }

        /// <summary>Creates a styled action button with proper contrast colors.</summary>
        public static Button CreateActionButton(string text, Color color, int width = 110, int height = DefaultButtonHeight)
        {
            Color foreColor = (color == CardBackground || color == SecondaryAccent || color == MainBackground || color == GridAltRow || color == PrimaryAccent)
                ? TextDark
                : Color.White;

            var btn = new Button
            {
                Size = new Size(width, height),
                Font = new Font("Segoe UI", 9.5f, FontStyle.Bold),
                BackColor = color,
                ForeColor = foreColor,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand,
                TextAlign = ContentAlignment.MiddleCenter,
                Text = text
            };
            btn.FlatAppearance.BorderSize = (color == PrimaryAccent || color == SecondaryAccent) ? 1 : 0;
            if (btn.FlatAppearance.BorderSize > 0)
                btn.FlatAppearance.BorderColor = BorderColor;

            if (color == PrimaryAccent || color == SecondaryAccent)
                btn.FlatAppearance.MouseOverBackColor = PrimaryAccentDark;
            else if (color == DangerRed)
                btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(185, 28, 28);

            EnsureSafeButtonWidth(btn, false);

            return btn;
        }

        /// <summary>Creates a styled action button (position-overload for backward compatibility).</summary>
        public static Button CreateActionButton(string text, Color color, int x, int y, int width = 110, int height = DefaultButtonHeight)
        {
            var btn = CreateActionButton(text, color, width, height);
            btn.Location = new Point(x, y);
            return btn;
        }

        /// <summary>Creates an action button with Segoe icon glyph prefix.</summary>
        public static Button CreateIconButton(string iconGlyph, string text, Color color, int width = 120, int height = DefaultButtonHeight)
        {
            var btn = CreateActionButton(text, color, width, height);
            Color foreColor = btn.ForeColor;
            btn.Image = CreateIconBitmap(iconGlyph, 16, foreColor);
            btn.ImageAlign = ContentAlignment.MiddleLeft;
            btn.TextAlign = ContentAlignment.MiddleLeft;
            btn.TextImageRelation = TextImageRelation.ImageBeforeText;
            btn.Padding = new Padding(8, 0, 0, 0);
            btn.Text = " " + text;
            EnsureSafeButtonWidth(btn, true);
            return btn;
        }

        /// <summary>Creates a styled text search box.</summary>
        public static TextBox CreateSearchBox(int width = 320)
        {
            return new TextBox
            {
                Size = new Size(width, DefaultButtonHeight),
                Font = new Font("Segoe UI", 9.5f),
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.White,
                ForeColor = TextDark,
                PlaceholderText = "Search..."
            };
        }

        /// <summary>Creates a styled text search box (position-overload for backward compatibility).</summary>
        public static TextBox CreateSearchBox(int x, int y, int width = 320)
        {
            var tb = CreateSearchBox(width);
            tb.Location = new Point(x, y);
            return tb;
        }

        /// <summary>
        /// Creates a responsive modern toolbar with search on left and action buttons on right.
        /// Prevents button clipping and overlapping across all window widths down to 1024x600.
        /// </summary>
        public static Panel CreateToolbar(Control? searchControl, params Control[] actionButtons)
        {
            var toolbar = new Panel
            {
                Dock = DockStyle.Top,
                Height = DefaultToolbarHeight,
                BackColor = Color.White,
                Padding = new Padding(SpacingMd, SpacingSm, SpacingMd, SpacingSm)
            };

            toolbar.Paint += (s, e) =>
            {
                using var p = new Pen(BorderColor, 1);
                e.Graphics.DrawLine(p, 0, toolbar.Height - 1, toolbar.Width, toolbar.Height - 1);
            };

            if (searchControl != null)
            {
                searchControl.Dock = DockStyle.Left;
                toolbar.Controls.Add(searchControl);
            }

            if (actionButtons != null && actionButtons.Length > 0)
            {
                var flpActions = new FlowLayoutPanel
                {
                    Dock = searchControl != null ? DockStyle.Right : DockStyle.Left,
                    FlowDirection = FlowDirection.LeftToRight,
                    AutoSize = true,
                    WrapContents = false,
                    BackColor = Color.Transparent,
                    Height = DefaultButtonHeight,
                    Margin = Padding.Empty,
                    Padding = Padding.Empty
                };

                for (int i = 0; i < actionButtons.Length; i++)
                {
                    var btn = actionButtons[i];
                    btn.Margin = searchControl != null
                        ? new Padding(SpacingSm, 0, 0, 0)
                        : new Padding(0, 0, SpacingSm, 0);
                    flpActions.Controls.Add(btn);
                }

                toolbar.Controls.Add(flpActions);
            }

            return toolbar;
        }

        /// <summary>Creates a standard form/panel header with compact 8px padding.</summary>
        public static Panel CreateFormHeader(string title, string subtitle = "")
        {
            var panel = new Panel
            {
                Dock = DockStyle.Top,
                Height = DefaultHeaderHeight,
                BackColor = Color.White,
                Padding = new Padding(SpacingMd, 0, SpacingMd, 0)
            };

            var lblTitle = new Label
            {
                Text = title,
                Font = new Font("Segoe UI Semibold", 15f),
                ForeColor = TextDark,
                Location = new Point(SpacingMd, subtitle.Length > 0 ? 6 : 14),
                Size = new Size(700, 24),
                AutoSize = true
            };
            panel.Controls.Add(lblTitle);

            if (!string.IsNullOrEmpty(subtitle))
            {
                var lblSub = new Label
                {
                    Text = subtitle,
                    Font = new Font("Segoe UI", 9f),
                    ForeColor = TextMedium,
                    Location = new Point(SpacingMd, 29),
                    Size = new Size(700, 18),
                    AutoSize = true
                };
                panel.Controls.Add(lblSub);
            }

            panel.Paint += (s, e) =>
            {
                using var pen = new Pen(BorderColor, 1);
                e.Graphics.DrawLine(pen, 0, panel.Height - 1, panel.Width, panel.Height - 1);
            };

            return panel;
        }

        /// <summary>
        /// Updates the empty-state overlay for a DataGridView.
        /// Displays a clean message + icon when row count is zero.
        /// Distinguishes between zero data and zero search results.
        /// </summary>
        public static void UpdateGridState(
            DataGridView dgv,
            int rowCount,
            bool isSearchActive,
            string entityName,
            string? searchQuery = null,
            Action? onClearSearch = null)
        {
            var parent = dgv.Parent;
            if (parent == null) return;

            const string EmptyOverlayName = "_emptyStateOverlay";
            var existing = parent.Controls.Find(EmptyOverlayName, false).FirstOrDefault();

            if (rowCount > 0)
            {
                if (existing != null) existing.Visible = false;
                return;
            }

            Panel overlay;
            if (existing is Panel p)
            {
                overlay = p;
                overlay.Controls.Clear();
            }
            else
            {
                overlay = new Panel
                {
                    Name = EmptyOverlayName,
                    BackColor = Color.White,
                    Size = new Size(380, 136),
                    Anchor = AnchorStyles.None
                };
                overlay.Paint += (s, e) =>
                {
                    using var pen = new Pen(BorderColor, 1);
                    e.Graphics.DrawRectangle(pen, 0, 0, overlay.Width - 1, overlay.Height - 1);
                };
                parent.Controls.Add(overlay);
            }

            // Center overlay inside parent container
            overlay.Location = new Point(
                Math.Max(10, (parent.ClientSize.Width - overlay.Width) / 2),
                Math.Max(30, (parent.ClientSize.Height - overlay.Height) / 2));

            var lblIcon = new Label
            {
                Text = isSearchActive ? Icons.Search : Icons.Books,
                Font = FontIconLarge,
                ForeColor = TextMedium,
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Top,
                Height = 36
            };

            var lblMsg = new Label
            {
                Text = isSearchActive
                    ? $"No results found for \"{searchQuery}\".\nTry checking the spelling or clear the filter."
                    : $"No {entityName} found in the database yet.\nClick \"Add {entityName}\" above to create one.",
                Font = new Font("Segoe UI", 9f, FontStyle.Regular),
                ForeColor = TextMedium,
                TextAlign = ContentAlignment.TopCenter,
                Dock = DockStyle.Fill
            };

            overlay.Controls.Add(lblMsg);
            overlay.Controls.Add(lblIcon);

            if (isSearchActive && onClearSearch != null)
            {
                var btnClear = new Button
                {
                    Text = $"{Icons.Refresh} Clear Search",
                    Size = new Size(120, 30),
                    Font = new Font("Segoe UI", 9f, FontStyle.Bold),
                    BackColor = SecondaryAccent,
                    ForeColor = TextDark,
                    FlatStyle = FlatStyle.Flat,
                    Cursor = Cursors.Hand,
                    Dock = DockStyle.Bottom
                };
                btnClear.FlatAppearance.BorderSize = 0;
                btnClear.Click += (s, e) => onClearSearch();
                overlay.Controls.Add(btnClear);
            }

            overlay.Visible = true;
            overlay.BringToFront();
        }
    }
}

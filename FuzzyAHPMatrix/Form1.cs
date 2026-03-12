using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace FuzzyAHPMatrix
{
    public partial class Form1 : Form
    {
        // DataGridView controls
        private DataGridView dgvDM1, dgvDM2, dgvDM3;
        private DataGridView dgvAggregated, dgvRanking;
        private const int CriteriaCount = 8;

        public Form1()
        {
            // Initialize components first
            InitializeComponent();

            // Then create and setup all controls
            InitializeControls();
            InitializeGrids();
        }

        private void InitializeControls()
        {
            // Create and configure all DataGridViews
            dgvDM1 = CreateDataGridView("DM1", new Point(20, 20));
            dgvDM2 = CreateDataGridView("DM2", new Point(400, 20));
            dgvDM3 = CreateDataGridView("DM3", new Point(780, 20));

            dgvAggregated = CreateResultsGridView("Aggregated Results", new Point(20, 300));
            dgvRanking = CreateResultsGridView("Rankings", new Point(550, 300));

            // Create and configure calculate button
            var btnCalculate = new Button
            {
                Text = "Calculate",
                Size = new Size(150, 40),
                Location = new Point(450, 550),
                Font = new Font("Arial", 10)
            };
            btnCalculate.Click += btnCalculate_Click;

            // Add all controls to the form
            this.Controls.AddRange(new Control[] {
                dgvDM1, dgvDM2, dgvDM3,
                dgvAggregated, dgvRanking,
                btnCalculate
            });

            // Configure form properties
            this.Text = "Fuzzy AHP Matrix Calculator";
            this.Size = new Size(1200, 650);
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private DataGridView CreateDataGridView(string name, Point location)
        {
            var dgv = new DataGridView
            {
                Name = name,
                Location = location,
                Size = new Size(350, 250),
                ColumnHeadersHeight = 30,
                RowHeadersWidth = 80,
                AllowUserToAddRows = false,
                ScrollBars = ScrollBars.Both
            };
            return dgv;
        }

        private DataGridView CreateResultsGridView(string name, Point location)
        {
            var dgv = new DataGridView
            {
                Name = name,
                Location = location,
                Size = new Size(500, 200),
                ColumnHeadersHeight = 30,
                AllowUserToAddRows = false
            };
            return dgv;
        }

        private void InitializeGrids()
        {
            var decisionMakerGrids = new[] { dgvDM1, dgvDM2, dgvDM3 };

            foreach (var dgv in decisionMakerGrids)
            {
                dgv.ColumnCount = CriteriaCount;
                dgv.RowCount = CriteriaCount;

                for (int i = 0; i < CriteriaCount; i++)
                {
                    dgv.Columns[i].Width = 50;
                    dgv.Columns[i].HeaderText = "C" + (i + 1);
                    dgv.Rows[i].HeaderCell.Value = "C" + (i + 1);

                    for (int j = 0; j < CriteriaCount; j++)
                    {
                        dgv.Rows[i].Cells[j].Value = (i == j) ? "1" : "1";
                    }
                }
            }
        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            try
            {
                var allFuzzyWeights = new List<(double, double, double)[]>();
                var decisionMakerGrids = new[] { dgvDM1, dgvDM2, dgvDM3 };

                foreach (var dgv in decisionMakerGrids)
                {
                    var matrix = ReadMatrix(dgv);
                    var fuzzyWeights = CalculateFuzzyWeights(matrix);
                    allFuzzyWeights.Add(fuzzyWeights);
                }

                var aggregatedWeights = AggregateFuzzyWeights(allFuzzyWeights);
                var lmu = aggregatedWeights.Select(w => (w.Item1 + w.Item2 + w.Item3) / 3).ToArray();

                // Create a concrete type for rankings to avoid dynamic issues
                var rankings = lmu
                    .Select((val, idx) => new CriteriaRanking { Index = idx + 1, Weight = val })
                    .OrderByDescending(x => x.Weight)
                    .ToList();

                DisplayAggregatedResults(aggregatedWeights, lmu);
                DisplayRankingResults(rankings);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error in calculation: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DisplayAggregatedResults((double, double, double)[] aggregatedWeights, double[] lmu)
        {
            dgvAggregated.Columns.Clear();
            dgvAggregated.Columns.Add("Criteria", "Criteria");
            dgvAggregated.Columns.Add("L", "L");
            dgvAggregated.Columns.Add("M", "M");
            dgvAggregated.Columns.Add("U", "U");
            dgvAggregated.Columns.Add("Avg", "Average");

            dgvAggregated.Rows.Clear();
            for (int i = 0; i < CriteriaCount; i++)
            {
                dgvAggregated.Rows.Add(
                    $"C{i + 1}",
                    aggregatedWeights[i].Item1.ToString("F3"),
                    aggregatedWeights[i].Item2.ToString("F3"),
                    aggregatedWeights[i].Item3.ToString("F3"),
                    lmu[i].ToString("F3"));
            }
        }

        private void DisplayRankingResults(List<CriteriaRanking> rankings)
        {
            dgvRanking.Columns.Clear();
            dgvRanking.Columns.Add("Rank", "Rank");
            dgvRanking.Columns.Add("Criteria", "Criteria");
            dgvRanking.Columns.Add("Weight", "Weight");

            dgvRanking.Rows.Clear();
            for (int i = 0; i < rankings.Count; i++)
            {
                dgvRanking.Rows.Add(
                    i + 1,
                    "C" + rankings[i].Index,
                    rankings[i].Weight.ToString("F3"));
            }
        }

        private double[,] ReadMatrix(DataGridView dgv)
        {
            double[,] matrix = new double[CriteriaCount, CriteriaCount];

            for (int i = 0; i < CriteriaCount; i++)
            {
                for (int j = 0; j < CriteriaCount; j++)
                {
                    if (dgv.Rows[i].Cells[j].Value == null ||
                        string.IsNullOrEmpty(dgv.Rows[i].Cells[j].Value.ToString()))
                    {
                        throw new Exception($"Missing value at row {i + 1}, column {j + 1}");
                    }

                    if (!double.TryParse(dgv.Rows[i].Cells[j].Value.ToString(), out double value))
                    {
                        throw new Exception($"Invalid number at row {i + 1}, column {j + 1}");
                    }

                    matrix[i, j] = value;
                }
            }
            return matrix;
        }

        private (double, double, double)[] CalculateFuzzyWeights(double[,] matrix)
        {
            double[,] lMatrix = new double[CriteriaCount, CriteriaCount];
            double[,] mMatrix = new double[CriteriaCount, CriteriaCount];
            double[,] uMatrix = new double[CriteriaCount, CriteriaCount];

            for (int i = 0; i < CriteriaCount; i++)
            {
                for (int j = 0; j < CriteriaCount; j++)
                {
                    double val = matrix[i, j];
                    if (val == 1)
                    {
                        lMatrix[i, j] = 1;
                        mMatrix[i, j] = 1;
                        uMatrix[i, j] = 1;
                    }
                    else if (val > 1)
                    {
                        lMatrix[i, j] = val - 0.1;
                        mMatrix[i, j] = val;
                        uMatrix[i, j] = val + 0.1;
                    }
                    else
                    {
                        lMatrix[i, j] = 1 / (val + 0.1);
                        mMatrix[i, j] = 1 / val;
                        uMatrix[i, j] = 1 / (val - 0.1);
                    }
                }
            }

            var sumL = new double[CriteriaCount];
            var sumM = new double[CriteriaCount];
            var sumU = new double[CriteriaCount];

            for (int j = 0; j < CriteriaCount; j++)
            {
                for (int i = 0; i < CriteriaCount; i++)
                {
                    sumL[j] += lMatrix[i, j];
                    sumM[j] += mMatrix[i, j];
                    sumU[j] += uMatrix[i, j];
                }
            }

            var SA = new (double, double, double)[CriteriaCount];
            for (int i = 0; i < CriteriaCount; i++)
            {
                SA[i] = (0, 0, 0);
                for (int j = 0; j < CriteriaCount; j++)
                {
                    SA[i].Item1 += lMatrix[i, j] / sumU[j];
                    SA[i].Item2 += mMatrix[i, j] / sumM[j];
                    SA[i].Item3 += uMatrix[i, j] / sumL[j];
                }
                SA[i].Item1 /= CriteriaCount;
                SA[i].Item2 /= CriteriaCount;
                SA[i].Item3 /= CriteriaCount;
            }

            return SA;
        }

        private (double, double, double)[] AggregateFuzzyWeights(List<(double, double, double)[]> matrices)
        {
            int n = matrices[0].Length;
            var aggregated = new (double, double, double)[n];

            for (int i = 0; i < n; i++)
            {
                var l = matrices.Select(mat => mat[i].Item1).ToList();
                var mVals = matrices.Select(mat => mat[i].Item2).ToList();
                var u = matrices.Select(mat => mat[i].Item3).ToList();

                aggregated[i] = (l.Min(), mVals.Average(), u.Max());
            }

            return aggregated;
        }
    }

    // Helper class to avoid dynamic type issues
    public class CriteriaRanking
    {
        public int Index { get; set; }
        public double Weight { get; set; }
    }
}
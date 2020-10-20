using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Map_Editor
{
    public partial class MapConverter : Form
    {
        private MapReader map;
        private Dictionary<int, int> ConvertBackIdxDict = new Dictionary<int, int>();
        private Dictionary<int, int> ConvertMiddleIdxDict = new Dictionary<int, int>();
        private Dictionary<int, int> ConvertFrontIdxDict = new Dictionary<int, int>();
        private Dictionary<int, int> ConvertDoorIdxDict = new Dictionary<int, int>();
        public MapConverter()
        {
            InitializeComponent();
        }

        public void SetData(MapReader map)
        {
            if(map==null)
            {
                return;
            }
            this.map = map;

            RefreshDGV();
        }

        private void RefreshDGV()
        {
            this.BackLibConvert_DGV.Rows.Clear();
            this.MiddleLibConvert_DGV.Rows.Clear();
            this.FrontLibConvert_DGV.Rows.Clear();
            this.DoorLibConvert_DGV.Rows.Clear();

            foreach(var i in map.BackLibDict)
            {
                DataGridViewRow r1 = new DataGridViewRow();
                r1.CreateCells(this.BackLibConvert_DGV);
                r1.Cells[0].Value = i.Key;
                r1.Cells[1].Value = i.Value!=null?i.Value.FileName:"";
                this.BackLibConvert_DGV.Rows.Add(r1);
            }

            foreach (var i in map.MiddleLibDict)
            {
                DataGridViewRow r1 = new DataGridViewRow();
                r1.CreateCells(this.MiddleLibConvert_DGV);
                r1.Cells[0].Value = i.Key;
                r1.Cells[1].Value = i.Value != null ? i.Value.FileName : "";
                this.MiddleLibConvert_DGV.Rows.Add(r1);
            }

            foreach (var i in map.FrontLibDict)
            {
                DataGridViewRow r1 = new DataGridViewRow();
                r1.CreateCells(this.FrontLibConvert_DGV);
                r1.Cells[0].Value = i.Key;
                r1.Cells[1].Value = i.Value != null ? i.Value.FileName : "";
                this.FrontLibConvert_DGV.Rows.Add(r1);
            }

            foreach (var i in map.DoorLibDict)
            {
                DataGridViewRow r1 = new DataGridViewRow();
                r1.CreateCells(this.DoorLibConvert_DGV);
                r1.Cells[0].Value = i.Key;
                r1.Cells[1].Value = i.Value != null ? i.Value.FileName : "";
                this.DoorLibConvert_DGV.Rows.Add(r1);
            }
        }

        private void ApplyTo_BTN_Click(object sender, EventArgs e)
        {
            foreach(DataGridViewRow row in this.BackLibConvert_DGV.Rows)
            {
                if(row.Cells[1].Value != null)
                {
                    int idx = (int)row.Cells[0].Value;
                    string libFileName = (string)row.Cells[1].Value;
                    map.BackLibDict[idx] = new MLibrary(libFileName);
                }
            }

            foreach (DataGridViewRow row in this.MiddleLibConvert_DGV.Rows)
            {
                if (row.Cells[1].Value != null)
                {
                    int idx = (int)row.Cells[0].Value;
                    string libFileName = (string)row.Cells[1].Value;
                    map.MiddleLibDict[idx] = new MLibrary(libFileName);
                }
            }

            foreach (DataGridViewRow row in this.FrontLibConvert_DGV.Rows)
            {
                if (row.Cells[1].Value != null)
                {
                    int idx = (int)row.Cells[0].Value;
                    string libFileName = (string)row.Cells[1].Value;
                    map.FrontLibDict[idx] = new MLibrary(libFileName);
                }
            }

            foreach (DataGridViewRow row in this.DoorLibConvert_DGV.Rows)
            {
                if (row.Cells[1].Value != null)
                {
                    int idx = (int)row.Cells[0].Value;
                    string libFileName = (string)row.Cells[1].Value;
                    map.DoorLibDict[idx] = new MLibrary(libFileName);
                }
            }

            foreach(var obj in map.MapCellObjs)
            {
                obj.BackLib = map.BackLibDict[obj.Info.OriginalBackIndex];
                obj.MiddleLib = map.MiddleLibDict[obj.Info.OriginalMiddleIndex];
                obj.FrontLib = map.FrontLibDict[obj.Info.OriginalFrontIndex];
                obj.DoorLib = map.DoorLibDict[obj.Info.OriginalDoorIndex];
            }
        }

        private void ConvertLib(string path)
        {
            foreach (DataGridViewRow row in this.BackLibConvert_DGV.Rows)
            {
                if (row.Cells[1].Value != null && row.Cells[2].Value!=null)
                {
                    int tgtIdx;
                    if(!int.TryParse(row.Cells[2].Value.ToString(), out tgtIdx))
                    {
                        continue;
                    }
                    File.Copy((string)row.Cells[1].Value, path+"/Tiles" + tgtIdx+".lib");
                }
                if (row.Cells[0].Value != null && row.Cells[2].Value != null)
                {
                    int tgtIdx;
                    int srcIdx;

                    if (int.TryParse(row.Cells[2].Value.ToString(), out tgtIdx)
                        && int.TryParse(row.Cells[0].Value.ToString(), out srcIdx))
                    {
                        ConvertBackIdxDict[srcIdx] = tgtIdx;
                    }
                }
            }

            foreach (DataGridViewRow row in this.MiddleLibConvert_DGV.Rows)
            {
                if (row.Cells[1].Value != null && row.Cells[2].Value != null)
                {
                    int tgtIdx;
                    if (!int.TryParse(row.Cells[2].Value.ToString(), out tgtIdx))
                    {
                        continue;
                    }
                    File.Copy((string)row.Cells[1].Value, path + "/SmTiles" + tgtIdx + ".lib");
                }

                if (row.Cells[0].Value != null && row.Cells[2].Value != null)
                {
                    int tgtIdx;
                    int srcIdx;

                    if (int.TryParse(row.Cells[2].Value.ToString(), out tgtIdx)
                        && int.TryParse(row.Cells[0].Value.ToString(), out srcIdx))
                    {
                        ConvertMiddleIdxDict[srcIdx] = tgtIdx;
                    }
                }
            }

            foreach (DataGridViewRow row in this.FrontLibConvert_DGV.Rows)
            {
                if (row.Cells[1].Value != null && row.Cells[2].Value != null)
                {
                    int tgtIdx;
                    if (!int.TryParse(row.Cells[2].Value.ToString(), out tgtIdx))
                    {
                        continue;
                    }
                    File.Copy((string)row.Cells[1].Value, path + "/Objects" + tgtIdx + ".lib");
                }

                if (row.Cells[0].Value != null && row.Cells[2].Value != null)
                {
                    int tgtIdx;
                    int srcIdx;

                    if (int.TryParse(row.Cells[2].Value.ToString(), out tgtIdx)
                        && int.TryParse(row.Cells[0].Value.ToString(), out srcIdx))
                    {
                        ConvertFrontIdxDict[srcIdx] = tgtIdx;
                    }
                }
            }

            foreach (DataGridViewRow row in this.DoorLibConvert_DGV.Rows)
            {
                if (row.Cells[1].Value != null && row.Cells[2].Value != null)
                {
                    int tgtIdx;
                    if (!int.TryParse(row.Cells[2].Value.ToString(), out tgtIdx))
                    {
                        continue;
                    }
                    //File.Copy((string)row.Cells[1].Value, path+"/Objects" + tgtIdx+".lib");
                }

                if (row.Cells[0].Value != null && row.Cells[2].Value != null)
                {
                    int tgtIdx;
                    int srcIdx;

                    if (int.TryParse(row.Cells[2].Value.ToString(), out tgtIdx)
                        && int.TryParse(row.Cells[0].Value.ToString(), out srcIdx))
                    {
                        ConvertDoorIdxDict[srcIdx] = tgtIdx;
                    }
                }
            }
        }
        private void ConvertMap(string path,string fileName)
        {
            ConvertLib(path);
            CellInfo[,] newData = new CellInfo[map.Width,map.Height];

            for(int i=0;i< map.Width;i++)
            {
                for (int j = 0; j < map.Height; j++)
                {
                    newData[i,j] = map.MapCells[i,j].Clone();
                    int tgtIdx;
                    if(ConvertBackIdxDict.TryGetValue(newData[i, j].OriginalBackIndex,out tgtIdx))
                    {
                        newData[i, j].BackIndex = (short)tgtIdx;
                    }
                    else
                    {
                        newData[i, j].BackIndex = newData[i, j].OriginalBackIndex;
                    }
                    if (ConvertMiddleIdxDict.TryGetValue(newData[i, j].OriginalMiddleIndex, out tgtIdx))
                    {
                        newData[i, j].MiddleIndex = (short)tgtIdx;
                    }
                    else
                    {
                        newData[i, j].MiddleIndex = newData[i, j].OriginalMiddleIndex;
                    }
                    if (ConvertFrontIdxDict.TryGetValue(newData[i, j].OriginalFrontIndex, out tgtIdx))
                    {
                        newData[i, j].FrontIndex = (short)tgtIdx;
                    }
                    else
                    {
                        newData[i, j].FrontIndex = newData[i, j].OriginalFrontIndex;
                    }
                    if (ConvertDoorIdxDict.TryGetValue(newData[i, j].OriginalDoorIndex, out tgtIdx))
                    {
                        newData[i, j].DoorIndex = (byte)tgtIdx;
                    }
                    else
                    {
                        newData[i, j].DoorIndex = newData[i, j].OriginalDoorIndex;
                    }
                }
            }

            map.SaveAsNormal(path+"/"+ fileName, newData, map.Width, map.Height);
        }

        private void SaveTo_BTN_Click(object sender, EventArgs e)
        {
            var saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "地图文件(*.map)|*.map";
            saveFileDialog.FileName = "NormalMap";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string path = Path.GetDirectoryName(saveFileDialog.FileName);
                string fileName=Path.GetFileName(saveFileDialog.FileName);
                ConvertMap(path, fileName);
                MessageBox.Show("保存成功!");
            }
        }

        private void DGV_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if(e.ColumnIndex==1)
            {
                var openFileDialog = new OpenFileDialog();
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    DataGridView dgv = (DataGridView)sender;
                    dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = openFileDialog.FileName;
                }
            }
            
        }
    }
}

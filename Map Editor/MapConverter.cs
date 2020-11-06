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
    public class ConvertData
    {
        public int TargetLibIndex;
        public string TargetLibFileName;
        public string OldLibFileName;
        public MLibrary OldLib;
        public int[] ImageIndexConvert;
    }
    public partial class MapConverter : Form
    {
        private MapReader map;
        private Dictionary<int, ConvertData> ConvertBackIdxDict = new Dictionary<int, ConvertData>();
        private Dictionary<int, ConvertData> ConvertMiddleIdxDict = new Dictionary<int, ConvertData>();
        private Dictionary<int, ConvertData> ConvertFrontIdxDict = new Dictionary<int, ConvertData>();
        private Dictionary<int, ConvertData> ConvertDoorIdxDict = new Dictionary<int, ConvertData>();

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

        private void PrepareConvertInfo(string path)
        {
            foreach (DataGridViewRow row in this.BackLibConvert_DGV.Rows)
            {
                //变更原有map的lib index
                if (row.Cells[0].Value != null && row.Cells[1].Value != null && row.Cells[2].Value != null)
                {
                    int tgtIdx;
                    int srcIdx;

                    if (int.TryParse(row.Cells[2].Value.ToString(), out tgtIdx)
                        && int.TryParse(row.Cells[0].Value.ToString(), out srcIdx))
                    {
                        ConvertData convertData=new ConvertData();
                        convertData.TargetLibIndex = tgtIdx;
                        convertData.OldLibFileName = (string)row.Cells[1].Value;
                        convertData.OldLib = new MLibrary(convertData.OldLibFileName);
                        convertData.TargetLibFileName = path + "/Tiles" + tgtIdx + ".lib";
                        //提取图片模式
                        if (row.Cells[3].EditedFormattedValue.ToString() == "True")
                        {
                            convertData.ImageIndexConvert = new int[convertData.OldLib.Images.Count];
                        }
                        ConvertBackIdxDict[srcIdx] = convertData;
                    }
                }
            }

            foreach (DataGridViewRow row in this.MiddleLibConvert_DGV.Rows)
            {

                if (row.Cells[0].Value != null && row.Cells[1].Value != null && row.Cells[2].Value != null)
                {
                    int tgtIdx;
                    int srcIdx;

                    if (int.TryParse(row.Cells[2].Value.ToString(), out tgtIdx)
                        && int.TryParse(row.Cells[0].Value.ToString(), out srcIdx))
                    {
                        ConvertData convertData = new ConvertData();
                        convertData.TargetLibIndex = tgtIdx;
                        convertData.OldLibFileName = (string)row.Cells[1].Value;
                        convertData.OldLib = new MLibrary(convertData.OldLibFileName);
                        convertData.TargetLibFileName = path + "/SmTiles" + tgtIdx + ".lib";
                        //提取图片模式
                        if (row.Cells[3].EditedFormattedValue.ToString() == "True")
                        {
                            convertData.ImageIndexConvert = new int[convertData.OldLib.Images.Count];
                        }

                        ConvertMiddleIdxDict[srcIdx] = convertData;
                    }
                }
            }

            foreach (DataGridViewRow row in this.FrontLibConvert_DGV.Rows)
            {

                if (row.Cells[0].Value != null && row.Cells[1].Value != null && row.Cells[2].Value != null)
                {
                    int tgtIdx;
                    int srcIdx;

                    if (int.TryParse(row.Cells[2].Value.ToString(), out tgtIdx)
                        && int.TryParse(row.Cells[0].Value.ToString(), out srcIdx))
                    {
                        ConvertData convertData = new ConvertData();
                        convertData.TargetLibIndex = tgtIdx;
                        convertData.OldLibFileName = (string)row.Cells[1].Value;
                        convertData.OldLib = new MLibrary(convertData.OldLibFileName);
                        convertData.TargetLibFileName = path + "/Objects" + tgtIdx + ".lib";
                        //提取图片模式
                        if (row.Cells[3].EditedFormattedValue.ToString() == "True")
                        {
                            convertData.ImageIndexConvert = new int[convertData.OldLib.Images.Count];
                        }

                        ConvertFrontIdxDict[srcIdx] = convertData;
                    }
                }
            }

            foreach (DataGridViewRow row in this.DoorLibConvert_DGV.Rows)
            {

                if (row.Cells[0].Value != null && row.Cells[1].Value != null && row.Cells[2].Value != null)
                {
                    int tgtIdx;
                    int srcIdx;

                    if (int.TryParse(row.Cells[2].Value.ToString(), out tgtIdx)
                        && int.TryParse(row.Cells[0].Value.ToString(), out srcIdx))
                    {
                        ConvertData convertData = new ConvertData();
                        convertData.TargetLibIndex = tgtIdx;
                        convertData.OldLibFileName = (string)row.Cells[1].Value;
                        convertData.OldLib = new MLibrary(convertData.OldLibFileName);
                        convertData.TargetLibFileName = path + "/Objects" + tgtIdx + ".lib";
                        //提取图片模式
                        if (row.Cells[3].EditedFormattedValue.ToString() == "True")
                        {
                            convertData.ImageIndexConvert = new int[convertData.OldLib.Images.Count];
                        }

                        ConvertDoorIdxDict[srcIdx] = convertData;
                    }
                }
            }
        }

        private void DumpNewLib()
        {
            
            //转储新的lib文件
            foreach (var backConvertData in ConvertBackIdxDict.Values)
            {
                if (backConvertData != null)
                {
                    if(backConvertData.ImageIndexConvert != null)
                    {
                        MLibrary newBackLib = new MLibrary(backConvertData.TargetLibFileName);
                        for (int i = 0; i < backConvertData.ImageIndexConvert.Length; i++)
                        {
                            if (backConvertData.ImageIndexConvert[i] == -1)
                            {
                                backConvertData.OldLib.CheckImage(i,true,false);
                                newBackLib.Images.Add(backConvertData.OldLib.Images[i]);
                                backConvertData.ImageIndexConvert[i] = newBackLib.Images.Count - 1;
                            }
                        }
                        newBackLib.Save();
                        newBackLib.Destroy();
                    }
                    else
                    {
                        File.Copy(backConvertData.OldLibFileName, backConvertData.TargetLibFileName);
                    }
                    MessageBox.Show("保存完毕: " + backConvertData.TargetLibFileName);
                }
            }

            foreach (var middleConvertData in ConvertMiddleIdxDict.Values)
            {
                if (middleConvertData != null)
                {
                    if (middleConvertData.ImageIndexConvert != null)
                    {
                        MLibrary newMiddleLib = new MLibrary(middleConvertData.TargetLibFileName);
                        for (int i = 0; i < middleConvertData.ImageIndexConvert.Length; i++)
                        {
                            if (middleConvertData.ImageIndexConvert[i] == -1)
                            {
                                middleConvertData.OldLib.CheckImage(i, true, false);
                                newMiddleLib.Images.Add(middleConvertData.OldLib.Images[i]);
                                middleConvertData.ImageIndexConvert[i] = newMiddleLib.Images.Count - 1;
                            }
                        }
                        newMiddleLib.Save();
                        newMiddleLib.Destroy();
                    }
                    else
                    {
                        File.Copy(middleConvertData.OldLibFileName, middleConvertData.TargetLibFileName);
                    }

                    MessageBox.Show("保存完毕: " + middleConvertData.TargetLibFileName);
                }
            }

            foreach (var frontConvertData in ConvertFrontIdxDict.Values)
            {
                if (frontConvertData != null)
                {
                    if (frontConvertData.ImageIndexConvert != null)
                    {
                        MLibrary newFrontLib = new MLibrary(frontConvertData.TargetLibFileName);
                        for (int i = 0; i < frontConvertData.ImageIndexConvert.Length; i++)
                        {
                            if (frontConvertData.ImageIndexConvert[i] == -1)
                            {
                                frontConvertData.OldLib.CheckImage(i, true, false);
                                newFrontLib.Images.Add(frontConvertData.OldLib.Images[i]);
                                frontConvertData.ImageIndexConvert[i] = newFrontLib.Images.Count - 1;
                            }
                        }
                        newFrontLib.Save();
                        newFrontLib.Destroy();
                    }
                    else
                    {
                        File.Copy(frontConvertData.OldLibFileName, frontConvertData.TargetLibFileName);
                    }

                    MessageBox.Show("保存完毕: " + frontConvertData.TargetLibFileName);
                }
            }
        }

        private void ConvertMap(string path,string fileName)
        {

            PrepareConvertInfo(path);
            
            CellInfo[,] newData = new CellInfo[map.Width,map.Height];

            //先转换mapdata里的libIndex数据,顺便提取出image index
            for (int i=0;i< map.Width;i++)
            {
                for (int j = 0; j < map.Height; j++)
                {
                    newData[i,j] = map.MapCells[i,j].Clone();
                    ConvertData convertData;
                    if(ConvertBackIdxDict.TryGetValue(newData[i, j].OriginalBackIndex,out convertData))
                    {
                        newData[i, j].BackIndex = (short)convertData.TargetLibIndex;
                        int index = (map.MapCells[i, j].BackImage & 0x1FFFFFFF) - 1;
                        //提取图片模式
                        if (convertData.ImageIndexConvert!=null && index >= 0 && index < convertData.OldLib.Images.Count)
                        {
                            //先临时设置一下值
                            convertData.ImageIndexConvert[index] = -1;
                        }
                    }
                    else
                    {
                        newData[i, j].BackIndex = newData[i, j].OriginalBackIndex;
                    }
                    if (ConvertMiddleIdxDict.TryGetValue(newData[i, j].OriginalMiddleIndex, out convertData))
                    {
                        newData[i, j].MiddleIndex = (short)convertData.TargetLibIndex;
                        int index = map.MapCells[i, j].MiddleImage - 1;

                        int animation = map.MapCells[i, j].MiddleAnimationFrame;
                        if ((animation > 0) && (animation < 255))
                        {
                            if ((animation & 0x0f) > 0)
                            {
                                animation &= 0x0f;
                            }
                            if (animation > 0)
                            {
                                //var animationTick = M2CellInfo[x, y].MiddleAnimationTick;
                                //index += AnimationCount % (animation + animation * animationTick) / (1 + animationTick);
                            }
                        }

                        animation = animation < 0 || animation >= 255 ? 0 : animation;

                        for (int a = 0; a <= animation; a++)
                        {
                            //提取图片模式
                            if (convertData.ImageIndexConvert != null && (index + a) >= 0 && (index + a) < convertData.OldLib.Images.Count)
                            {
                                //先临时设置一下值
                                convertData.ImageIndexConvert[index+a] = -1;
                            }
                        }
                        
                    }
                    else
                    {
                        newData[i, j].MiddleIndex = newData[i, j].OriginalMiddleIndex;
                    }
                    if (ConvertFrontIdxDict.TryGetValue(newData[i, j].OriginalFrontIndex, out convertData))
                    {
                        newData[i, j].FrontIndex = (short)convertData.TargetLibIndex;
                        //object的image index不太一样
                        int index = (map.MapCells[i, j].FrontImage & 0x7FFF) - 1;

                        int animation = map.MapCells[i, j].FrontAnimationFrame;
                        if ((animation & 0x80) > 0)
                        {
                            animation &= 0x7F;
                        }

                        animation = animation <=0 ? 0 : animation;

                        for (int a = 0; a <= animation; a++)
                        {
                            //提取图片模式
                            if (convertData.ImageIndexConvert != null && (index + a) >= 0 && (index + a) < convertData.OldLib.Images.Count)
                            {
                                //先临时设置一下值
                                convertData.ImageIndexConvert[index + a] = -1;
                            }
                        }
                    }
                    else
                    {
                        newData[i, j].FrontIndex = newData[i, j].OriginalFrontIndex;
                    }

                    //if (ConvertDoorIdxDict.TryGetValue(newData[i, j].OriginalDoorIndex, out convertData))
                    //{
                    //    newData[i, j].DoorIndex = (byte)convertData.TargetLibIndex;
                    //    //提取图片模式
                    //    if (convertData.ImageIndexConvert != null  && map.MapCells[i, j].FrontImage >= 0 && map.MapCells[i, j].FrontImage < convertData.OldLib.Images.Count)
                    //    {
                    //        //先临时设置一下值
                    //        convertData.ImageIndexConvert[map.MapCells[i, j].FrontImage] = -1;
                    //    }
                    //}
                    //else
                    //{
                    //    newData[i, j].DoorIndex = newData[i, j].OriginalDoorIndex;
                    //}
                }
            }
            
            //保存新的lib
            DumpNewLib();

            //提取图片模式的话image index需要更新
            //再根据新的lib转换一下mapdata中的image index
            for (int i = 0; i < map.Width; i++)
            {
                for (int j = 0; j < map.Height; j++)
                {
                    ConvertData convertData;
                    if (ConvertBackIdxDict.TryGetValue(newData[i, j].OriginalBackIndex, out convertData))
                    {
                        int index = (newData[i, j].BackImage & 0x1FFFFFFF) - 1;
                        //提取图片模式
                        if (convertData!=null && convertData.ImageIndexConvert != null && index >= 0 && index < convertData.ImageIndexConvert.Length)
                        {
                            //convertData.ImageIndexConvert[index]存的是新的lib文件中的index，还要转成对应的imageIndex
                            newData[i, j].BackImage = convertData.ImageIndexConvert[index]+1;
                        }
                    }
                    if (ConvertMiddleIdxDict.TryGetValue(newData[i, j].OriginalMiddleIndex, out convertData))
                    {
                        int index = newData[i, j].MiddleImage - 1;
                        //提取图片模式
                        if (convertData != null && convertData.ImageIndexConvert != null && index >= 0 && index < convertData.ImageIndexConvert.Length)
                        {
                            newData[i, j].MiddleImage = (short)(convertData.ImageIndexConvert[index]+1);
                        }
                    }

                    if (ConvertFrontIdxDict.TryGetValue(newData[i, j].OriginalFrontIndex, out convertData))
                    {
                        //object的image index不太一样
                        int index = (newData[i, j].FrontImage & 0x7FFF) - 1;
                        //提取图片模式
                        if (convertData != null && convertData.ImageIndexConvert != null && index >= 0 && index < convertData.ImageIndexConvert.Length)
                        {
                            newData[i, j].FrontImage = (short)(convertData.ImageIndexConvert[index]+1);
                        }
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
                if(map.LibFilePath!=null)
                {
                    openFileDialog.InitialDirectory = map.LibFilePath;
                }
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    DataGridView dgv = (DataGridView)sender;
                    dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = openFileDialog.FileName;
                    map.LibFilePath = Path.GetDirectoryName(openFileDialog.FileName);
                }
            }
            
        }
    }
}

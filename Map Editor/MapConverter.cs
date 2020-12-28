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

        #region prepare convert info
        private void PrepareConvertInfoBack(string path,List<ConvertInfo> convertInfos)
        {
            foreach (ConvertInfo row in convertInfos)
            {
                //变更原有map的lib index
                if (row.OriginalLibIdx != null && row.LibFileName != null && row.TargetLibIdx != null)
                {
                    int tgtIdx;
                    int srcIdx;

                    if (int.TryParse(row.TargetLibIdx, out tgtIdx)
                        && int.TryParse(row.OriginalLibIdx, out srcIdx))
                    {
                        ConvertData convertData = new ConvertData();
                        convertData.TargetLibIndex = tgtIdx;
                        convertData.OldLibFileName = row.LibFileName;
                        convertData.OldLib = new MLibrary(convertData.OldLibFileName);
                        convertData.TargetLibFileName = path + "/Tiles" + tgtIdx + ".lib";
                        //提取图片模式
                        if (row.IfExportImage)
                        {
                            convertData.ImageIndexConvert = new int[convertData.OldLib.Images.Count];
                        }
                        ConvertBackIdxDict[srcIdx] = convertData;
                    }
                }
            }
        }

        private void PrepareConvertInfoMiddle(string path, List<ConvertInfo> convertInfos)
        {
            foreach (ConvertInfo row in convertInfos)
            {
                //变更原有map的lib index
                if (row.OriginalLibIdx != null && row.LibFileName != null && row.TargetLibIdx != null)
                {
                    int tgtIdx;
                    int srcIdx;

                    if (int.TryParse(row.TargetLibIdx, out tgtIdx)
                        && int.TryParse(row.OriginalLibIdx, out srcIdx))
                    {
                        ConvertData convertData = new ConvertData();
                        convertData.TargetLibIndex = tgtIdx;
                        convertData.OldLibFileName = row.LibFileName;
                        convertData.OldLib = new MLibrary(convertData.OldLibFileName);
                        convertData.TargetLibFileName = path + "/SmTiles" + tgtIdx + ".lib";
                        //提取图片模式
                        if (row.IfExportImage)
                        {
                            convertData.ImageIndexConvert = new int[convertData.OldLib.Images.Count];
                        }
                        ConvertMiddleIdxDict[srcIdx] = convertData;
                    }
                }
            }
        }

        private void PrepareConvertInfoFront(string path, List<ConvertInfo> convertInfos)
        {
            foreach (ConvertInfo row in convertInfos)
            {
                //变更原有map的lib index
                if (row.OriginalLibIdx != null && row.LibFileName != null && row.TargetLibIdx != null)
                {
                    int tgtIdx;
                    int srcIdx;

                    if (int.TryParse(row.TargetLibIdx, out tgtIdx)
                        && int.TryParse(row.OriginalLibIdx, out srcIdx))
                    {
                        ConvertData convertData = new ConvertData();
                        convertData.TargetLibIndex = tgtIdx;
                        convertData.OldLibFileName = row.LibFileName;
                        convertData.OldLib = new MLibrary(convertData.OldLibFileName);
                        convertData.TargetLibFileName = path + "/Objects" + tgtIdx + ".lib";
                        //提取图片模式
                        if (row.IfExportImage)
                        {
                            convertData.ImageIndexConvert = new int[convertData.OldLib.Images.Count];
                        }
                        ConvertFrontIdxDict[srcIdx] = convertData;
                    }
                }
            }
        }

        private void PrepareConvertInfoDoor(string path, List<ConvertInfo> convertInfos)
        {
            foreach (ConvertInfo row in convertInfos)
            {
                //变更原有map的lib index
                if (row.OriginalLibIdx != null && row.LibFileName != null && row.TargetLibIdx != null)
                {
                    int tgtIdx;
                    int srcIdx;

                    if (int.TryParse(row.TargetLibIdx, out tgtIdx)
                        && int.TryParse(row.OriginalLibIdx, out srcIdx))
                    {
                        ConvertData convertData = new ConvertData();
                        convertData.TargetLibIndex = tgtIdx;
                        convertData.OldLibFileName = row.LibFileName;
                        convertData.OldLib = new MLibrary(convertData.OldLibFileName);
                        convertData.TargetLibFileName = path + "/Objects" + tgtIdx + ".lib";
                        //提取图片模式
                        if (row.IfExportImage)
                        {
                            convertData.ImageIndexConvert = new int[convertData.OldLib.Images.Count];
                        }
                        ConvertDoorIdxDict[srcIdx] = convertData;
                    }
                }
            }
        }
        
        private void PrepareConvertInfo(string path, List<ConvertInfo> backConvertInfos, List<ConvertInfo> middleConvertInfos, List<ConvertInfo> frontConvertInfos, List<ConvertInfo> doorConvertInfos)
        {
            PrepareConvertInfoBack(path, backConvertInfos);
            PrepareConvertInfoMiddle(path, middleConvertInfos);
            PrepareConvertInfoFront(path, frontConvertInfos);
            PrepareConvertInfoDoor(path, doorConvertInfos);
        }
        #endregion

        #region dump new lib
        private void DumpNewLibBack()
        {
            foreach (var backConvertData in ConvertBackIdxDict.Values)
            {
                if (backConvertData != null)
                {
                    if (backConvertData.ImageIndexConvert != null)
                    {
                        MLibrary newBackLib = new MLibrary(backConvertData.TargetLibFileName);
                        for (int i = 0; i < backConvertData.ImageIndexConvert.Length; i++)
                        {
                            if (backConvertData.ImageIndexConvert[i] == -1)
                            {
                                backConvertData.OldLib.CheckImage(i, true, false);
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
                    //MessageBox.Show("保存完毕: " + backConvertData.TargetLibFileName);
                }
            }
        }

        private void DumpNewLibMiddle()
        {
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

                    //MessageBox.Show("保存完毕: " + middleConvertData.TargetLibFileName);
                }
            }
        }

        private void DumpNewLibFront()
        {
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

                    //MessageBox.Show("保存完毕: " + frontConvertData.TargetLibFileName);
                }
            }
        }

        private void DumpNewLib()
        {
            //转储新的lib文件
            DumpNewLibBack();
            DumpNewLibMiddle();
            DumpNewLibFront();
        }
        #endregion

        private void ConvertMap(ConvertType convertType,
            string targetPath,
            string fileName, 
            List<ConvertInfo> backConvertInfos, 
            List<ConvertInfo> middleConvertInfos, 
            List<ConvertInfo> frontConvertInfos, 
            List<ConvertInfo> doorConvertInfos)
        {
            if(convertType == ConvertType.Back)
            {
                PrepareConvertInfoBack(targetPath, backConvertInfos);
            }
            else
            {
                PrepareConvertInfo(targetPath, backConvertInfos, middleConvertInfos, frontConvertInfos, doorConvertInfos);
            }

            CellInfo[,] newData = new CellInfo[map.Width,map.Height];

            //先转换mapdata里的libIndex数据,顺便提取出image index
            for (int i=0;i< map.Width;i++)
            {
                for (int j = 0; j < map.Height; j++)
                {
                    newData[i,j] = map.MapCells[i,j].Clone();
                    ConvertData convertData;
                    if(ConvertBackIdxDict.TryGetValue(newData[i, j].OriginalBackIndex,out convertData) && (convertType == ConvertType.Back || convertType == ConvertType.All))
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
                    if (ConvertMiddleIdxDict.TryGetValue(newData[i, j].OriginalMiddleIndex, out convertData) && (convertType == ConvertType.Middle || convertType == ConvertType.All))
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
                    if (ConvertFrontIdxDict.TryGetValue(newData[i, j].OriginalFrontIndex, out convertData) && (convertType == ConvertType.Front || convertType == ConvertType.All))
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
            if (convertType == ConvertType.Back)
            {
                DumpNewLibBack();
            }
            else
            {
                DumpNewLib();
            }

            //提取图片模式的话image index需要更新
            //再根据新的lib转换一下mapdata中的image index
            for (int i = 0; i < map.Width; i++)
            {
                for (int j = 0; j < map.Height; j++)
                {
                    ConvertData convertData;
                    if (ConvertBackIdxDict.TryGetValue(newData[i, j].OriginalBackIndex, out convertData) && (convertType == ConvertType.Back || convertType == ConvertType.All))
                    {
                        int index = (newData[i, j].BackImage & 0x1FFFFFFF) - 1;
                        //提取图片模式
                        if (convertData!=null && convertData.ImageIndexConvert != null && index >= 0 && index < convertData.ImageIndexConvert.Length)
                        {
                            //convertData.ImageIndexConvert[index]存的是新的lib文件中的index，还要转成对应的imageIndex
                            newData[i, j].BackImage = convertData.ImageIndexConvert[index]+1;
                        }
                    }
                    if (ConvertMiddleIdxDict.TryGetValue(newData[i, j].OriginalMiddleIndex, out convertData) && (convertType == ConvertType.Middle || convertType == ConvertType.All))
                    {
                        int index = newData[i, j].MiddleImage - 1;
                        //提取图片模式
                        if (convertData != null && convertData.ImageIndexConvert != null && index >= 0 && index < convertData.ImageIndexConvert.Length)
                        {
                            newData[i, j].MiddleImage = (short)(convertData.ImageIndexConvert[index]+1);
                        }
                    }

                    if (ConvertFrontIdxDict.TryGetValue(newData[i, j].OriginalFrontIndex, out convertData) && (convertType == ConvertType.Front || convertType == ConvertType.All))
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

            map.SaveAsNormal(targetPath + "/"+ fileName, newData, map.Width, map.Height);
        }

        private List<ConvertInfo> GetConvertInfos(DataGridView dgv)
        {
            List<ConvertInfo> convertInfos = new List<ConvertInfo>();
            foreach (DataGridViewRow row in dgv.Rows)
            {
                ConvertInfo convertInfo = new ConvertInfo();
                if (row.Cells[0].Value != null && !row.Cells[0].Value.ToString().Trim().Equals(""))
                {
                    convertInfo.OriginalLibIdx = row.Cells[0].Value.ToString().Trim();
                }
                if (row.Cells[1].Value != null && !row.Cells[1].Value.ToString().Trim().Equals(""))
                {
                    convertInfo.LibFileName = row.Cells[1].Value.ToString().Trim();
                }
                if (row.Cells[2].Value != null && !row.Cells[2].Value.ToString().Trim().Equals(""))
                {
                    convertInfo.TargetLibIdx = row.Cells[2].Value.ToString().Trim();
                }
                if (row.Cells[3].EditedFormattedValue != null && !row.Cells[3].EditedFormattedValue.ToString().Equals("True"))
                {
                    convertInfo.IfExportImage = true;
                }
                convertInfos.Add(convertInfo);
            }

            return convertInfos;
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
                ConvertMap(ConvertType.All,path, fileName,
                    GetConvertInfos(this.BackLibConvert_DGV),
                    GetConvertInfos(this.MiddleLibConvert_DGV),
                    GetConvertInfos(this.FrontLibConvert_DGV),
                    GetConvertInfos(this.DoorLibConvert_DGV));
                MessageBox.Show("保存成功!");
            }
        }

        private Dictionary<string, Tuple<string,int>> ParseBatchConfig(string batchConfigTxt)
        {
            Dictionary<string, Tuple<string, int>> result = new Dictionary<string, Tuple<string, int>>();

            if(File.Exists(batchConfigTxt))
            {
                using (var fs= new FileStream(batchConfigTxt, FileMode.Open, FileAccess.Read))
                {
                    using (StreamReader sr = new StreamReader(fs))
                    {
                        string strLine= sr.ReadLine();
                        while (strLine != null)
                        {
                            string[] strs=strLine.Split(' ');
                            int idx;
                            if(strs.Length>=3 && !strs[0].Trim().Equals("") && !strs[1].Trim().Equals("") && !strs[2].Trim().Equals("") && int.TryParse(strs[2].Trim(),out idx))
                            {
                                result[strs[0].Trim()] = new Tuple<string,int>(strs[1].Trim(),idx);
                            }
                            strLine = sr.ReadLine();
                        }
                    }
                }
            }
            else
            {
                throw new Exception("配置文件不存在: "+ batchConfigTxt);
            }

            return result;
        }

        private void BatchTilesProc_BTN_Click(object sender, EventArgs e)
        {
            string batchConfigTxt = "batchtiles.txt";
            MessageBox.Show("本功能支持且仅支持针对多个地图做从Tiles.lib文件中提取生成新的lib文件。\n请先在指定的目录配置好"+ batchConfigTxt + "文件，以空格分隔，第一列是地图文件名前缀(不包含.map扩展名)，第二列是原来的back tiles绝对路径(一般是Tiles.lib绝对路径)，第三列是提取后back tiles文件名前缀。");
            FolderBrowserDialog pathDialog = new FolderBrowserDialog();
            pathDialog.SelectedPath = Environment.CurrentDirectory;
            if (pathDialog.ShowDialog() == DialogResult.OK)
            {
                string txtPath = pathDialog.SelectedPath;

                Dictionary<string, Tuple<string, int>>  convertMapInfos=ParseBatchConfig(batchConfigTxt);

                List<ConvertInfo> convertBackInfos = new List<ConvertInfo>();
                foreach (var pair in convertMapInfos)
                {
                    convertBackInfos.Clear();

                    ConvertInfo convertInfo = new ConvertInfo();
                    convertInfo.OriginalLibIdx = "0";//为抽取Tiles.lib里的图片生成新的lib文件
                    convertInfo.LibFileName = pair.Value.Item1;
                    convertInfo.TargetLibIdx = pair.Value.Item2+"";
                    convertInfo.IfExportImage = true;

                    convertBackInfos.Add(convertInfo);
                    ConvertMap(ConvertType.Back, txtPath, pair.Key, convertBackInfos, null, null, null);
                }
                MessageBox.Show("完成");
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

    class ConvertData
    {
        public int TargetLibIndex;
        public string TargetLibFileName;
        public string OldLibFileName;
        public MLibrary OldLib;
        public int[] ImageIndexConvert;
    }

    class ConvertInfo
    {
        public string OriginalLibIdx=null;
        public string LibFileName = null;
        public string TargetLibIdx = null;
        public bool IfExportImage=false;

    }

    enum ConvertType
    {
        All,
        Back,
        Middle,
        Front,
        Door
    }
}
